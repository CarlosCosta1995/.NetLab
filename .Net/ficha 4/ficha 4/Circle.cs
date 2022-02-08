using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ficha_4
{
    //Esta class herda os atributos e os methos abstractos da class Shape
    public class Circle : Shape
    {
        private double radius;

        //======= Construtures ====
        //Construtor por omissão
        public Circle() 
        {
            this.position = new Point();    
            this.radius = new double();
        }

        //Construtor por parametros
        public Circle (Point point, double radius) 
        {
            this.position = point;
            this.radius = radius;
        }

        //======= Methods ====
        //usar a Lampada para implementar os methods abstractos da Class Shape
        public override double getArea()
        {
            //A = π*r²
            return Math.PI * (Math.Pow(radius,2));
        }

        public override double getPerimeter()
        {
            //throw new NotImplementedException(); Da um erro caso o metodo seja invocado e nao implementado.
            // P = 2π*r
            return (2*Math.PI) * radius;    
        }

        //ToString Serve sempre para fazer uma representação textual de um objecto
        public override string ToString()
        {
            //return "X: " + this.position.X + " Y: " + this.position.Y + " Radius: " + this.radius;
            return Position.ToString() + " Radius: " + this.radius;
        }
        //Nota: override (sobrepor / subrecarregar) == ignora o ToString do systema e sobrepoe por este metodo
    }
}
