// See https://aka.ms/new-console-template for more information
using Ficha11;

//Engine(int torque, int displacement, int horsepower)
Engine carEngine = new Engine(120, 1000, 80);
Engine motoEngine = new Engine(150, 1500, 100);

//Car(int numberOfDoor, int numberOfSeat, Travel travel, string color, double weight, string brand, string model, Engine engine)
Car car = new Car(5, 5, Vehicle.Travel.LAND, "red", 12, "BMW", "cs12", carEngine);

Console.WriteLine(carEngine.ToString());
Console.WriteLine(car.ToString());

//Motorcycle(Type type, int velocity, Travel travel, string color, double weight, string brand, string model, Engine engine)
Motorcycle moto = new Motorcycle(Motorcycle.Type.SPORT, 150, Vehicle.Travel.LAND, "lime", 23, "Kawasaki", "RTX", motoEngine);

Console.WriteLine(motoEngine.ToString());
Console.WriteLine(moto.ToString());

//VehicleTest(Car car)
VehicleTest test = new VehicleTest(car);
Console.WriteLine(test.Car);

test.Car.TurnOnOff = true;
test.Car.Start();

Motorcycle b = test.SwapVehicle<Motorcycle>();
VehicleTest testB = new VehicleTest(b);
Console.WriteLine(testB.ToString());
