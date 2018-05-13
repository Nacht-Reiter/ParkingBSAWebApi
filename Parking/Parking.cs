using System;
using System.Collections.Generic;
using System.Timers;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ParkingBSA
{
    public class Parking
    {
        private static readonly Lazy<Parking> lazy = new Lazy<Parking>(() => new Parking());
        public static Parking Instanse { get { return lazy.Value; } }
        private Timer PayTimer = new Timer(1000*Settings.Timeout);
        private Timer LogTimer = new Timer(60000);

        private Parking()
        {
            PayTimer.AutoReset = true;
            PayTimer.Start();
            LogTimer.AutoReset = true;
            LogTimer.Start();
            LogTimer.Elapsed += Log;

        }

        public decimal Balance { get; private set; } = 0;

        public List<Car> CarsList { get; } = new List<Car>();
        public List<Transaction> TransactionsList { get; } = new List<Transaction>();


        public void AddCar(Car car) //Add car to parking
        {
            if (car != null && this.FreeSpace() > 0)
            {
                car.Payed += AddIncome;
                car.TransactionMade += AddTransaction;
                PayTimer.Elapsed += car.Pay;
                CarsList.Add(car);
            }
        }

        public void RemoveCar(Car car) //Remove car from parking
        {
            if (car != null)
            {
                if (!car.IsDebtor())
                {
                    PayTimer.Elapsed -= car.Pay;
                    car.Payed -= AddIncome;
                    car.TransactionMade -= AddTransaction;
                    CarsList.Remove(car);
                }
                else
                {
                    throw new ArithmeticException("This car is debtor, refill the balance to get car");
                }
            }
        }

        public void AddTransaction(Transaction transaction) //Create transaction
        {
            if (transaction != null)
            {
                TransactionsList.Add(transaction);
            }
            if ((DateTime.Now - TransactionsList[0].TransactionDateTime).Minutes > 0)//Delete transaction                                                                                    
            {                                                                        //which older that 1 minute
                TransactionsList.Remove(TransactionsList[0]);
            }
        }

        public void Log(object sender, ElapsedEventArgs e) // Write transactions into transaction.log
        {                                                  // Works every minute
            using (StreamWriter sw = new StreamWriter("Transactions.log", true))
            {
                foreach(Transaction i in TransactionsList)
                {
                    sw.WriteLine(i.ToString());
                }
            }

        }

        public int FreeSpace() //returns free space of parking
        {
            return Settings.ParkingSpace - CarsList.Count;
        }

        public void AddIncome(decimal income) //Adding car`s payments to balance
        {
            Balance += income;
        }

    }
}
