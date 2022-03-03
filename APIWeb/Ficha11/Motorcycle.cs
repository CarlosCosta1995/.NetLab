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
        public Type _type;
        public int _velocity;

        public Motorcycle(Type type, int velocity, Travel travel, string color, double weight, string brand, string model, Engine engine) : base(travel, color, weight, brand, model, engine)
        {
           this._type = type;
            this._velocity = velocity;
        }

        public override void Start()
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            return String.Format($"Type: {_type}, Velocity: {_velocity}, {base.ToString()}");
        }
    }
}
