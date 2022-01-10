using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ficha_4
{
    public abstract class Shape
    {
        //Add an atribute, type point, named position [always privated, unless it is need somewhere else]
        //Se for internal, todas as classes filhas herdam o point.
        //Caso seja Private, a implementação seria feita com o get e set.
        internal Point position;

        //abstract need at leat 1 abstract Method
        public Shape() 
        {
            position = new Point();
        }
        
        //Create a normal construct
        public Shape(Point position) 
        { 
            this.position = position;
        }

        //Implement a Selector[get] and its Propreties[set]
        public Point Position 
        { 
            get { return position; }
            set { position = value; }
        }

        //Methods abstractos so providenciam a assinaturado método
        //Sendo uma clase abstracta e quem quiser herdar os contributos da class shape, terão de implementar os methods da area e perimetro.

        //Add a method to get the Area
        public abstract double getArea();

        //Add a method to get the Peremeter
        public abstract double getPerimeter(); 

    }
}
