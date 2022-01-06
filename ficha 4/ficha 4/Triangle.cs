using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ficha_4
{
    public class Triangle
    {
        //Como os Point sao definidos e atribuidos na Class Point
        //Podemos Simplemente Invocar os Points fazendo set e get das coordenadas
         public Point pointA { get; set; } //Seleção get + atribuação set
         public Point pointB { get; set; }
         public Point pointC { get; set; }

        public Triangle ()//Construtuor por omissão
        {
            //usamos o construtor Point dentro da Class Point
            pointA = new Point(); //Atrivação deum varo por omissão.Sempre que nao saber o valor de entrada.
            pointB = new Point();
            pointC = new Point();
        }
        public void pointTriangle(Point A, Point B, Point C) // Constructor por parametros
        {
            this.pointA = A;
            this.pointB = B;
            this.pointC = C;
        }
        public double CalculateWidth()
        {
            return pointA.DistanceTo(pointB);
        }
        public double CalculateHeight()
        {
            return pointA.DistanceTo(pointC);
        }

        public double CalculateArea()
        {
            double width = CalculateWidth();
            double height = CalculateHeight();

            double area = (width * height)/2;
            return area;
        }
    }
}
