using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingBSA
{


    static class Settings
    {
        public static int Timeout { get; set; } = 3;
        public static int ParkingSpace { get; set; } = 100;
        public static decimal Fine { get; set; } = 1.5M;

        public static readonly Dictionary<CarTypes, decimal> Prices = new Dictionary<CarTypes, decimal>
        {
            [CarTypes.Truck] = 5,
            [CarTypes.Passenger] = 3,
            [CarTypes.Bus] = 2,
            [CarTypes.Motorcycle] = 1

        };
    }
}
