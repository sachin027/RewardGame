using RewardGame_Model.DBContext;
using RewardGame_Model.ViewModel;
using RewardGame_Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardGame_Repository.Service
{
    public class UserService : IUserInterface
    {
        SachinKanzariya_452Entities _DBContext = new SachinKanzariya_452Entities();

        public void UpdateWalletAmount(int randomNumber, int UserId)
        {
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("@userId" , UserId),
                    new SqlParameter("@CreditAmount" , randomNumber)

                };
                 _DBContext.Database.ExecuteSqlCommand("exec updateWalletAmount  @userId ,@CreditAmount", sqlParameters);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<TransactionsHistory> GetWalletHistory(int userId)
        {
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("@userId" ,userId)
                };
                List<TransactionsHistory> TransactionList = _DBContext.Database.SqlQuery<TransactionsHistory>("exec SP_TransactionHistory @userId", sqlParameters).ToList();
                //List<TransactionsHistory> TransactionList = _DBContext.Database.ExecuteSqlCommand("exec SP_TransactionHistory @userId", sqlParameters);
                return TransactionList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int GetBalanceLeft(int id)
        {
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("@userId" , id),

                };
                int balance = _DBContext.Database.SqlQuery<int>("exec GetBalanceLeft  @userId", sqlParameters).FirstOrDefault();
                return balance;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int GetLeftChances(int id)
        {
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("@userId" , id),

                };
                int balance = _DBContext.Database.SqlQuery<int>("exec GetRemainingChance  @userId", sqlParameters).FirstOrDefault();
                return balance;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int BuyNewChances(int id)
        {
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("@userId" , id)
                };
                int newChances = _DBContext.Database.SqlQuery<int>("exec SP_BuyNewChances @userId ", sqlParameters).FirstOrDefault();
                return newChances;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int FreeChance(int id)
        {
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("@UserId" , id)
                };
                int chance = _DBContext.Database.SqlQuery<int>("exec SP_FreeChance @UserId ", sqlParameters).FirstOrDefault();
                return chance;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int checkchance(int id)
        {
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("@UserId" , id)
                };
                int chance = _DBContext.Database.SqlQuery<int>("exec SP_CheckChance @UserId ", sqlParameters).FirstOrDefault();
                return chance;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public int PerDayProfit(int id)
        {
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("@userId" , id),

                };
                int balance = _DBContext.Database.SqlQuery<int>("exec SP_PerDayProfit  @userId", sqlParameters).FirstOrDefault();
                return balance;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }        
        public int PerDayDebitAmount(int id)
        {
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("@userId" , id),

                };
                int balance = _DBContext.Database.SqlQuery<int>("exec SP_GetDebitAmountPerDay  @userId", sqlParameters).FirstOrDefault();
                return balance;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public int TotalWins(int id)
        {
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("@userId" , id),

                };
                int balance = _DBContext.Database.SqlQuery<int>("exec SP_GetTotalEarn  @userId", sqlParameters).FirstOrDefault();
                return balance;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int getAmountInOneDay(int id, int amount)
        {
            SqlParameter[] sqlParameter = new SqlParameter[]
               {
                  new SqlParameter("@userId",id),
                  new SqlParameter("@creditAmount",amount)
               };

            int Amount = _DBContext.Database.SqlQuery<int>("exec getAmountInOneDay @userId,@creditAmount", sqlParameter).FirstOrDefault();
            return Amount;
        }
    }
}
