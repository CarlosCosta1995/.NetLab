using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ficha11
{
    public class Car : Vehicle
    {
        public int _numberOfDoor;
        public int _numberOfSeat;

        //:base manda os atributos para a class do Vehicle
        public Car(int numberOfDoor, int numberOfSeat, Travel travel, string color, double weight, string brand, string model, Engine engine) :base(travel,  color,  weight,  brand,  model,  engine)
        {
            this._numberOfDoor = numberOfDoor;
            this._numberOfSeat = numberOfSeat;
        }

        public override void Start()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return String.Format($"Number of Seats: {_numberOfSeat}, Number of Doors: {_numberOfDoor} and {base.ToString()}");
        }
    }
}
