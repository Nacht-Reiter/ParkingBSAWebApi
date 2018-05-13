using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingBSA
{
    public class Transaction
    {
        public DateTime TransactionDateTime { get; set; }
        public String CarID { get; set; }
        public decimal PayedMoney { get; set; }

        public Transaction(DateTime transactionDateTime, string carID, decimal payedMoney)
        {
            TransactionDateTime = transactionDateTime;
            CarID = carID;
            PayedMoney = payedMoney;
        }

        public override String ToString()
        {
            return $"{CarID,-12} {PayedMoney,-5} {TransactionDateTime}";
        }
    }
}
