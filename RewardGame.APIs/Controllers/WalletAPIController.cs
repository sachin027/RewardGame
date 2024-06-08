using RewardGame_Repository.Interface;
using RewardGame_Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;

namespace RewardGame.APIs.Controllers
{
    public class WalletAPIController : ApiController
    {
        IUserInterface userInterface = new UserService();

        // GET: WalletAPI
        [System.Web.Http.Route("api/WalletAPI/TotalWins")]
        [HttpGet]
        public int TotalWins(int userId)
        {
            try
            {
                int TotalEarning  = userInterface.TotalWins(userId);
                return TotalEarning;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }        


        [System.Web.Http.Route("api/WalletAPI/GetBalanceLeft")]
        [HttpGet]
        public int GetBalanceLeft(int userId)
        {
            try
            {
                int leftbalance  = userInterface.GetBalanceLeft(userId);
                return leftbalance;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }        


        [System.Web.Http.Route("api/WalletAPI/GetLeftChances")]
        [HttpGet]
        public int GetLeftChances(int userId)
        {
            try
            {
                int chance  = userInterface.GetLeftChances(userId);
                return chance;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }        


        [System.Web.Http.Route("api/WalletAPI/PerDayProfit")]
        [HttpGet]
        public int PerDayProfit(int userId)
        {
            try
            {
                int profit  = userInterface.PerDayProfit(userId);
                return profit;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }        
        
        [System.Web.Http.Route("api/WalletAPI/PerDayDebitAmount")]
        [HttpGet]
        public int PerDayDebitAmount(int userId)
        {
            try
            {
                int debitAmount  = userInterface.PerDayDebitAmount(userId);
                return debitAmount;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}