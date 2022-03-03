// See https://aka.ms/new-console-template for more information
using Ficha11;

//Car(int numberOfDoor, int numberOfSeat, Travel travel, string color, double weight, string brand, string model, Engine engine)
Engine engine = new Engine(120, 1000, 80);
Car car = new Car(5, 5, Vehicle.Travel.LAND, "red", 12, "BMW", "cs12", engine);

engine.ToString();
car.ToString();