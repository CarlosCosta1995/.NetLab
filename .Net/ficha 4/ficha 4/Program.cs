// See https://aka.ms/new-console-template for more information

//Reference to fiind the reference of File Point whit the namespace ficha_4
using ficha_4;

//Class Name, Instance name = new Class Name ( x ,  y);
Point p1 = new Point(); //For point 1
Point p2 = new Point(10, 10); //For point 2

Console.WriteLine(p1.X);
Console.WriteLine(p2.Y);

//Changing the p1 coordenates (x,y)
p1.X = 1;
p1.Y = 1;


//p2.X = 2;
//p2.Y = 5;
p2.SetXandY(2, 5);
Console.WriteLine(p2.X);
Console.WriteLine(p2.Y);

//calculation DistanceTo
double distance = p1.DistanceTo(p2);
Console.WriteLine("\nDistancia de p1 a p2: {0}",distance);    


//Triangulo
Point pointA = new Point(1, 1);
Point pointB = new Point(1, 4);
Point pointC = new Point(3, 1);

Triangle triangle = new Triangle();
triangle.getArea();
triangle.getPerimeter();    
Console.WriteLine("Triangle Area{0}, Triangle Perimeter{1}", triangle.getArea() ,triangle.getPerimeter());  


//Rectangulo
Point PointD = new Point(0, 5);

Rectangle r1 = new Rectangle(PointD, 5 , 5);

double areaRect = r1.getArea();
double perimRect = r1.getPerimeter(); //Perimeter

Console.WriteLine("\nPerimeter for r1: {0}, Area for r1: {1}", perimRect, areaRect);

Point pointE = new Point(1, 4);
Point pointF = new Point(6, 6);
Console.WriteLine("\nr1 Contains Point E: {0}, r1 Contains Point F: {1}", r1.Contains(pointE) , r1.Contains(pointF));


//Circle
Circle c1 = new Circle();   

//Point pointG = new Point(5, 5); 
//Circle c2= new Circle(pointG, 5);
Circle c2 = new Circle(new Point( 5, 5), 5);

double perimeterCircle = c1.getPerimeter();
double areaCircle = c2.getArea();  

Console.WriteLine("\nPerimeter for c1: {0}, Area for c1: {1}", perimeterCircle, areaCircle);
Console.WriteLine("The Center of the circle are in: {0}", c2.ToString());
//cw+ tab = Console.WriteLine();