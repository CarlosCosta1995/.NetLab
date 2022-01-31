using Ficha7;
using System.Text.Json;


//Funcão que serializa uma instância do tipo Employee
Employees LoadEmployeesJSON()
{
    string employeesJsonFile = File.ReadAllText("Employees.json");
    Employees employees = JsonSerializer.Deserialize<Employees>(employeesJsonFile);
    return employees;
};

Employee LoadEmployeeJSON()
{
    string employeeJsonFile = File.ReadAllText("Employee.json");
    Employee employee = JsonSerializer.Deserialize<Employee>(employeeJsonFile);
    return employee;
};

Employee emp1 = new Employee() { };
Employee emp2 = new Employee() { };
Employees emp3 = new Employees() { };

Employees emps = new Employees();

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/employees/{id}", (int id) =>
{
    Employee emp = employees.EmployeesList.Find( e => e.UserID == id);
    if (emp == null)
    {
        return Results.NotFound("ID not found");
    }
    else
    {
        return Results.Ok(emp);
    }
});

app.UseHttpsRedirection();

app.Run();
