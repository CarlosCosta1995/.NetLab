using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ficha11
{
    public class Engine
    {
        public int _torque;
        public int _displacement;
        public int _horsepower;

        public Engine(int torque, int displacement, int horsepower)
        {
            this._torque = torque;
            this._displacement = displacement;
            this._horsepower = horsepower;
        }

        public override string ToString()
        {
            return String.Format($"Torque: {_torque}, Displacement: {_displacement} and Horsepower: {_horsepower}");
        }
    }
}
