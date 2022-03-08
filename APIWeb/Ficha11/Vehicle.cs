using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ficha11
{
    public abstract class Vehicle : IVehicle
    {
        public enum Travel //enumerables, pode usar um switch, sao usados para menos
        {
            LAND,
            WATER,
            AIR
        }

        protected Travel _travel;
        protected string _color;
        protected double _weight;
        protected string _brand;
        protected string _model;
        protected Engine _engine;

        public Vehicle(Travel travel, string color, double weight, string brand, string model, Engine engine)
        {
            this._travel = travel;
            this._color = color;
            this._weight = weight;
            this._brand = brand;
            this._model = model;
            this._engine = engine;
        }

        public abstract void Start();

        public override string ToString()
        {
            return String.Format($"Travel: {_travel.ToString()}, Color: {_color}, Weight: {_weight}, Brand: {_brand}, Model: {_model} and Engine: {_engine.ToString()}");
        }

        //virutal = caso enhas que fazer overide depois em clases herdadas do Vehicle
        public virtual void Drive()
        {
            Console.WriteLine("You are driving to fast!");
        }
    }
}
