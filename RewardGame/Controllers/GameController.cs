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
        public ActionResult GameDashboard()
        {
            int id = SessionHelper.SessionHelper.UserID;
            ViewBag.balance = _userInterface.GetBalanceLeft(id);
            ViewBag.chance = _userInterface.GetLeftChances(id);
            ViewBag.profit = _userInterface.PerDayProfit(id);
            return View();
        }

        public ActionResult GamePage()
        {
            int id = SessionHelper.SessionHelper.UserID;
            SqlParameter[] sqlParameters = new SqlParameter[]
{
            new SqlParameter("@UserId",id)
};

            int chance = _DBContext.Database.SqlQuery<int>("exec SP_CheckChance @UserId", sqlParameters).FirstOrDefault();

            Session["chance"] = chance;
            return View();
        }

        public async Task<ActionResult> Wallet()
        {
            try
            {
                int id = SessionHelper.SessionHelper.UserID;
                string url = "api/UserAPI/GetWalletHistory?userId=" + SessionHelper.SessionHelper.UserID;
                string response = await WebHelper.WebHelpers.HttpRequestResponce(url);

                List<TransactionsHistory> TransactionList = JsonConvert.DeserializeObject<List<TransactionsHistory>>(response) ;

                ViewBag.balance = _userInterface.GetBalanceLeft(id);
                ViewBag.chance = _userInterface.GetLeftChances(id);

                return View(TransactionList);
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