﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ficha_4
{
    public class Rectangle
    {
        /*Class tem sempre:
         * Atributos ==  Descrevem a class { Atribuições das variaveis da class [Tipos premitivos ou de referencias] }
         * [Atributos sao sempre public, private]
         * 
         * Construtores ==  Criam novas instâncias {Por omissao (sem parametros), por parametros (recebe argumentos/parametros) [podemos ter n construtores] // nome do construtor é sempre o nome da class (Constructor vs metodos)}
         * [Constructores sao sempre public]
         * 
         * Propriedades == Expõe os atributos { Selectores(get) e Modificadores(set) Modificar atribuição de variaveis/Valores }
         * [Propriedades sao sempre public]
         * 
         * Metodos == Premitim reatizar operaçoes nas instâncias
         * [Metodos sao sempre public, private]
         * 
         * Nota: Ao longo da contrução deverá ser criado Console.WriteLines(); de forma a verificar se operação está funcionando de forma correcta.
         * */

        //========================= Atributos ==========================//
        //Atributos (por norma sao privados, caso queira aceder temos de criar selectores e atribuidores
        private Point topLeftPoint;
        private double width;
        private double height;


        //========================= Construtores ==========================//
        //Construtor por omissão(sem argumentos)
        public Rectangle ()
        { 
            this.topLeftPoint = new Point ();
            this.height = new double ();
            this.width = new double ();
        }

        //Construtor por "normal"(por parametros(recebe argumentos[de forma generica sao os da propria class]))
        public Rectangle(Point topLeftPoint, double width, double height)
        {
            this.topLeftPoint = topLeftPoint;
            this.width = width;
            this.height = height;
        }

        //========================= Propriedades ==========================//
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


        //========================= Metodos ==========================//
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
