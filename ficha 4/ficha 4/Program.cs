// See https://aka.ms/new-console-template for more information

//Reference to fiind the reference of File Point whit the namespace ficha_4
using ficha_4;

//Class Name, Instance name = new Class Name ( x ,  y);
Point p1 = new Point(); //For point 1
Point p2 = new Point(10, 10); //For point 2

Console.WriteLine(p1.getPointX());
Console.WriteLine(p2.getPointX());

//Changing the p1 coordenates (x,y)
p1.setPointX(1);
p1.setPointY(1);


p2.setPointXandY(2, 5);
Console.WriteLine(p2.getPointX());
Console.WriteLine(p2.getPointY());

//calculation DistanceTo
double distance = p1.DistanceTo(p2);
Console.WriteLine("Distancia de p1 a p2: {0}",distance);    


/*//Triangulo
Point pointA = new Point(1, 1);
Point pointB = new Point(1, 4);
Point pointC = new Point(3, 1);

Triangle triangle = new Triangle();
triangle.CalculateArea();
Console.WriteLine(triangle.CalculateArea());  */ 


//Rectangulo
Point PointD = new Point(0, 5);

Rectangle r1 = new Rectangle(PointD, 5 , 5);
double areaRect = r1.CalculateArea();
double perimRect = r1.CalculatePerimetro();

Console.WriteLine("Perimetro:{0}, Area:{1}", perimRect, areaRect);

Point pointE = new Point(10, 10);
Console.WriteLine(r1.Contains(pointE));