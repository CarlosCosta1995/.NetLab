// See https://aka.ms/new-console-template for more information
using ficha_4;
using Ficha5;

//triangle
Triangle t1 = new Triangle();
t1 = new Triangle(new Point(0, 2), new Point(1, 4), new Point(5, 5));

//rectangle
Rectangle r1 = new Rectangle();
 r1 = new Rectangle(new Point(0, 5), 5, 5);

//ciculo
Circle c1 = new Circle();
c1 = new Circle(new Point(5, 5), 5);

//Addicionar ao figure
//Class(Figure) Instancia(figure) =... 
Figure figure = new Figure();

figure.AddFigure(t1);
figure.AddFigure(r1);
figure.AddFigure(c1);

foreach (var shape in figure.Shapes) 
{
    //Console.WriteLine(shape.GetType());
    Console.WriteLine(shape.getArea());
}