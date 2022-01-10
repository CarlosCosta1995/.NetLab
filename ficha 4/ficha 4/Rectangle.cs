using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ficha_4
{
    public class Rectangle
    {
        /*Class tem sempre:
         * Atributos
         * Selectores e Propriedades
         * Construtores
         * Metodos
         * */

        //Atributos (por norma sao privados, caso queira aceder temos de criar selectores e atribuidores
        private Point topLeftPoint;
        private double width;
        private double height;

        //Propriedades
        public Point TopLeftPoint 
        { 
            get { return topLeftPoint; } 
            set { topLeftPoint = value; } 
        }
        public double Width 
        {
            get { return width; }
            set { width = value; }
        }
        public double Height 
        { 
            get { return height; }
            set { height = value; }
        }

        //===== Construtores ====
        //Construtor poro missão(sem argumentos)
        public Rectangle ()
        { 
            this.topLeftPoint = new Point ();
            this.height = new double ();
            this.width = new double ();
        }

        //Construtor poro missão(por parametros(recebe argumentos[de forma generica sao os da propria class]))
        public Rectangle(Point topLeftPoint, double width, double height)
        {
            this.topLeftPoint = topLeftPoint;
            this.width = width;
            this.height = height;
        }

        //===== Metodos ====

        public double CalculateArea() 
        { 
            //Base * Altura
            return width * height;

        }

        public double CalculatePerimetro() 
        { 
            return (2*width) + (2*height);
        }

        public bool Contains(Point point)
        {
            Point topRigthPoint = new Point(topLeftPoint.X + width, topLeftPoint.Y + Height);
            Point bottomRigthPoint = new Point(topLeftPoint.X + Width, topLeftPoint.Y - Height);
            Point bottomLeftPoint = new Point(topLeftPoint.X , topLeftPoint.Y - Height);
            
            if(point.X > bottomLeftPoint.X && point.X < bottomRigthPoint.X && point.Y > bottomLeftPoint.Y && point.Y < topLeftPoint.Y) 
            {
               return true;
            }
            else { return false; }
        }
    }
}
