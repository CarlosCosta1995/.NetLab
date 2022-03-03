using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ficha11
{
    public class Car : Vehicle, IVehicle
    {
        public int _numberOfDoor;
        public int _numberOfSeat;
        private bool _turnOnOff;
        public bool TurnOnOff
        {
            get { return _turnOnOff; }
            set { _turnOnOff = value; }
        }

        //:base manda os atributos para a class do Vehicle
        public Car(int numberOfDoor, int numberOfSeat, Travel travel, string color, double weight, string brand, string model, Engine engine) :base(travel,  color,  weight,  brand,  model,  engine)
        {
            this._numberOfDoor = numberOfDoor;
            this._numberOfSeat = numberOfSeat;
        }

        public override void Start()
        {
            if (_turnOnOff == false)
            {
                Console.WriteLine("The car has turned off.");
            }
            else
            {
                Console.WriteLine("The car has turned on.");
            }
        }

        public override string ToString()
        {
            return String.Format($"Number of Seats: {_numberOfSeat}, Number of Doors: {_numberOfDoor} and {base.ToString()}");
        }

        public void Drive()
        {
            throw new NotImplementedException();
        }
    }
}
