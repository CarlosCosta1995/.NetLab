using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ficha_4
{
    public class Triangle : Shape
    {
        //Como os Point sao definidos e atribuidos na Class Point
        //Podemos Simplemente Invocar os Points fazendo set e get das coordenadas
         //public Point pointA { get; set; } //Ponto A passa a ser o Ponto herdado da Shape
         public Point pointB { get; set; }
         public Point pointC { get; set; }

        //Construtuor por omissão
        //usamos o construtor Point proveniente da Class Point
        //Atrivação de uma variavel por omissão.[Sempre que nao se sabe o valor de entrada.]
        public Triangle ()
        {
            //pointA = new Point(); 
            pointB = new Point();
            pointC = new Point();
        }
        public Triangle(Point A, Point B, Point C) // Constructor por parametros
        {
            this.position = A;
            this.pointB = B;
            this.pointC = C;
        }

        public Point A 
        { 
            get { return position; }
            set { position = value; }
        }

        public Point B
        {
            get { return pointB; }
            set { pointB = value; }
        }

        public Point C
        {
            get { return pointC; }
            set { pointC = value; }
        }

        public double CalculateWidth()
        {
            return position.DistanceTo(pointB);
        }
        public double CalculateHeight()
        {
            return position.DistanceTo(pointC);
        }

        /*Como herda poderiafazer de 2 opçoes:
         * Colocar Overide no CalculateArea;
         * Criar de novo o metodo de calcular area herdado (lampada)

        public double CalculateArea()
        {
            double width = CalculateWidth();
            double height = CalculateHeight();

            double area = (width * height)/2;
            return area;
        }*/

        public override double getArea()
        {
            double width = CalculateWidth();
            double height = CalculateHeight();

            double area = (width * height) / 2;
            return area;
        }

        public override double getPerimeter()
        {
            return position.DistanceTo(pointC) + position.DistanceTo(pointB) + pointB.DistanceTo(pointC);
        }
    }
}
