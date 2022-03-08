using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ficha11
{
    public class Motorcycle : Vehicle
    {
        public enum Type
        {
            SPORT,
            CRUISER,
            ADVENTURE
        }
        private Type _type;
        private int _velocity;
        private bool _turnOnOff;
        public bool TurnOnOff
        {
            get { return _turnOnOff; }
            set { _turnOnOff = value; }
        }
        public Motorcycle(Type type, int velocity, Travel travel, string color, double weight, string brand, string model, Engine engine) : base(travel, color, weight, brand, model, engine)
        {
           this._type = type;
            this._velocity = velocity;
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
            return String.Format($"Type: {_type}, Velocity: {_velocity}, {base.ToString()}");
        }
        public override void Drive()
        {
            Console.WriteLine("you are going to fast!");
        }
    }
}
