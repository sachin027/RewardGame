using RewardGame_Model.DBContext;
using RewardGame_Model.ViewModel;
using RewardGame_Repository.Interface;
using RewardGame_Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using RouteAttribute = System.Web.Mvc.RouteAttribute;

namespace RewardGame.APIs.Controllers
{
    public class UserAPIController : ApiController
    {
        IUserInterface userInterface = new UserService();
        // GET: UserAPI
        [Route("api/UserAPI/GetWalletHistory")]
        public List<TransactionsHistory> GetWalletHistory(int userId )
        {
            try
            {
                List<TransactionsHistory> wallets = userInterface.GetWalletHistory(userId);
                return wallets;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        [Route("api/UserAPI/AddNumberIntoWallet")]
        [HttpPost]
        public void AddNumberIntoWallet(int randomNumber , int id)
        {
            try
            {
                userInterface.UpdateWalletAmount(randomNumber , id);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}