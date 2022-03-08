using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ficha11
{
    public class VehicleTest
    {
       /* private Car _car;
        private Motorcycle _motorcycle;*/
       private Vehicle _Vehicle;

        //prop tab = create properties
        public Vehicle Vehicle { get { return _Vehicle; } }

        public VehicleTest(Vehicle vehicle)
        {
            this._Vehicle = vehicle;
        }
        /*
        public VehicleTest(Motorcycle moto)
        {
            this._motorcycle = moto;
        }*/
    }
}
