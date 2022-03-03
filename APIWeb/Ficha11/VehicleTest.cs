using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ficha11
{
    public class VehicleTest
    {
        private Car _car;
        private Motorcycle _motorcycle;

        public Car Car { get { return _car; } }

        public VehicleTest(Car car)
        {
            this._car = car;
        }

        public VehicleTest(Motorcycle moto)
        {
            this._motorcycle = moto;
        }
    }
}
