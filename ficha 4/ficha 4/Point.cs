﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ficha_4 //Project Context
{
    public class Point
    {
        private double x;
        private double y;

        public Point() //Omissão == significa que nao tem argumentos 
        {
            //starting attributes
            this.x = 0;
            this.y = 0;
        }

        //Constructers => SETTER for setting X and Y values
        //Define and atttribute a value to the coordenates
        public Point (double x, double y) 
        { 
            //Setting attributes
            this.x = x;
            this.y = y;
        }

        public double X 
        { 
            get { return this.x; } 
            set { this.x = value; }        
        }

        public double Y
        {
            get { return this.y; }
            set { this.y = value; }
        }

        public void SetXandY(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        
        /*public void setPointX(double x)
        {
            this.x = x;
        }

        public void setPointY(double y)
        {
            this.y = y;
        }*/


        //Methods GETTER for getting X and Y value (selector)
        //Return the defined value of the coordenates
        public double getPointX() 
        {
            return this.x;
        }
        public double getPointY()
        {
            return this.y;
        }

        // Metod/Função calcular a distancia entre dois pontos
        public double DistanceTo(Point other)
        {
            //this refere-se a quem chama no main e usa as funcoes dentro da class.
            //Enquanto other/that é referente ao p2(main) que quer realizar a operação.
            return Math.Sqrt(Math.Pow(other.x - this.x, 2) + Math.Pow(other.y - this.y, 2));
        }

        public override string ToString()
        {
            return "X: " + this.X + " Y: " + this.Y;
        }
    }
}
