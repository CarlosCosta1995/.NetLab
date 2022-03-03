using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ficha11
{
    public class VehicleTest
    {
        private int _seat;

        public int Seat
        {
            get { return _seat; }
            set { _seat = value; }
        }

        public VehicleTest(Car car)
        {
            this._seat = car._numberOfSeat;
        }
    }
}
