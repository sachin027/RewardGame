using RewardGame_Model.DBContext;
using RewardGame_Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardGame_Repository.Interface
{
    public interface IUserInterface
    {
        List<TransactionsHistory> GetWalletHistory(int userId);

        void UpdateWalletAmount(int randomNumber, int UserId);

        int GetBalanceLeft(int id);
        int GetLeftChances(int id);
        int PerDayProfit(int id);

        int BuyNewChances(int id);
        int FreeChance(int id);
        int checkchance(int id);
        int getAmountInOneDay(int id, int amount);
    }
}
