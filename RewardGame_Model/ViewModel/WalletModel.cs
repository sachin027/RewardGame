using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardGame_Model.ViewModel
{
    public class WalletModel
    {
        public int WalletID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<byte> RemainingChance { get; set; }
        public Nullable<int> creditedAmount { get; set; }
        public Nullable<int> debitedAmount { get; set; }
        public Nullable<int> TotalBalance { get; set; }
        public Nullable<System.DateTime> transactiondate { get; set; }
    }
}
