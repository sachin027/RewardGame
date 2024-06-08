using Newtonsoft.Json;
using RewardGame.CustomFilter;
using RewardGame.WebHelper;
using RewardGame_Model.DBContext;
using RewardGame_Model.ViewModel;
using RewardGame_Repository.Interface;
using RewardGame_Repository.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RewardGame.Controllers
{
    [CustomAuthorize]
    public class GameController : Controller
    {
        // GET: Game
        SachinKanzariya_452Entities _DBContext = new SachinKanzariya_452Entities();
        IUserInterface _userInterface = new UserService();
        public async Task<ActionResult> GameDashboard()
        {
            try
            {
                ViewBag.Username = SessionHelper.SessionHelper.Username;

                string url1 = "api/WalletAPI/TotalWins?userId=" + SessionHelper.SessionHelper.UserID;
                string url2 = "api/WalletAPI/GetBalanceLeft?userId=" + SessionHelper.SessionHelper.UserID;
                string url3 = "api/WalletAPI/GetLeftChances?userId=" + SessionHelper.SessionHelper.UserID;
                string url4 = "api/WalletAPI/PerDayProfit?userId=" + SessionHelper.SessionHelper.UserID;


                string response1 = await WebHelper.WebHelpers.HttpRequestResponce(url1);
                string response2 = await WebHelper.WebHelpers.HttpRequestResponce(url2);
                string response3 = await WebHelper.WebHelpers.HttpRequestResponce(url3);
                string response4 = await WebHelper.WebHelpers.HttpRequestResponce(url4);


                ViewBag.totalWin = JsonConvert.DeserializeObject<int>(response1);
                ViewBag.balance = JsonConvert.DeserializeObject<int>(response2);
                ViewBag.chance = JsonConvert.DeserializeObject<int>(response3);
                ViewBag.profit = JsonConvert.DeserializeObject<int>(response4);


                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult GamePage()
        {
            int id = SessionHelper.SessionHelper.UserID;
            ViewBag.Username = SessionHelper.SessionHelper.Username;
                    SqlParameter[] sqlParameters = new SqlParameter[]
        {
                    new SqlParameter("@UserId",id)
        };

            int chance = _DBContext.Database.SqlQuery<int>("exec SP_CheckChance @UserId", sqlParameters).FirstOrDefault();

            Session["chance"] = chance;
            return View();
        }

        public async Task<ActionResult> Wallet(int? pageNumber)
        {
            try
            {
                int id = SessionHelper.SessionHelper.UserID;
                ViewBag.Username = SessionHelper.SessionHelper.Username;
                string url = "api/UserAPI/GetWalletHistory?userId=" + SessionHelper.SessionHelper.UserID;
                string url5 = "api/WalletAPI/PerDayDebitAmount?userId=" + SessionHelper.SessionHelper.UserID;

                string response = await WebHelper.WebHelpers.HttpRequestResponce(url);
                string response5 = await WebHelper.WebHelpers.HttpRequestResponce(url5);

                List<TransactionsHistory> TransactionList = JsonConvert.DeserializeObject<List<TransactionsHistory>>(response) ;
                ViewBag.deductAmount = JsonConvert.DeserializeObject<int>(response5);

                int page = pageNumber ?? 1;
                var PaginationList = Pager<TransactionsHistory>.Pagination(TransactionList, page);

                ViewBag.totalCount = Pager<TransactionsHistory>.totalCount;
                ViewBag.page = Pager<TransactionsHistory>.page;
                ViewBag.pageSize = Pager<TransactionsHistory>.pageSize;
                ViewBag.totalPage = Pager<TransactionsHistory>.totalPage;

                ViewBag.balance = _userInterface.GetBalanceLeft(id);
                ViewBag.chance = _userInterface.GetLeftChances(id);

                return View(PaginationList);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int updateWalletAmount( int amount, int id)
        {
            //Session["creditamount"] = amount;
            int Todayamount = _userInterface.getAmountInOneDay(id, amount);
            if (Todayamount == 1)
            {
            _userInterface.UpdateWalletAmount(amount, id);
                return 1;
            }
            else
            {
                return 0;
            }

        }

    }
}