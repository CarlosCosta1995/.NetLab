using ficha_4; //Addicionar referencia há ficha 4
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ficha5
{
    public class Figure
    {
        private List<Shape> shapes;  

        //Construtor por omissão
        public Figure() 
        { 
            this.shapes = new List<Shape>(); 
        }

        //Constructor por parametros
        //Shape pode ser trinagulo, rectangulo ou ciculo
        public void AddFigure(Shape shape) 
        { 
            //Addiciono 1 forma na lista, o atributo da class
            this.shapes.Add(shape); 
        }

        //Selector
        public List<Shape> Shapes 
        { 
            //Apenas para leitura/ selecção (get). Nao fazemos atribuição (set)
            get { return this.shapes; } 
        }

    }
}
