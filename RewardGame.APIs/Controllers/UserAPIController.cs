using RewardGame.APIs.JWTAuthentication;
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
        [JwtAuthentication]
        [Route("api/UserAPI/GetWalletHistory")]
        public List<TransactionsHistory> GetWalletHistory(int userId )
        {
            try
            {
                JwtAuthenticationAttribute.OnAuthorization(HttpContext.Current);
                List<TransactionsHistory> wallets = userInterface.GetWalletHistory(userId);
                return wallets;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        [JwtAuthentication]
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