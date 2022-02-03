using Ficha7;
using System.Text.Json;

Employee emp = LoadEmployeeJSON();
Employees emps = LoadEmployeesJSON();

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

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/Employees", () =>
{
    if (emps == null)
    { 
        return Results.NotFound("ID not found");
    }
    else
    {
        return Results.Ok(emps);  
    }
});

app.MapPost("/Employees", (Employee p) =>
{
    /*Retorna o priemiro elemento da lista
    var firstEmp = emps.EmployeesList[0];
    var firstEmp = emps.EmployeesList.FirstOrDefault();

    Retorna o ultimo elemento da lista ( reminder: Os index comecam por 0[emps.EmployeesList.Count -1])
    var lastEmp = emps.EmployeesList[emps.EmployeesList.Count -1];
    var lastEmp = emps.EmployeesList.LastOrDefault();*/

    if (emps.EmployeesList.Count == 0)
    {
        //Caso nao exista um userid no indice 0, adiciona uma pessoa.
        p.UserId = 1;
        emps.EmployeesList.Add(p); 
    }
    else
    {
        //Percorre a lista e guarda na variavel o ultimo userid, adiciona uma pessoa seguindo o ultimo userid e incrementa
        var lastEmp = emps.EmployeesList[emps.EmployeesList.Count - 1];
        p.UserId = lastEmp.UserId + 1;
        emps.EmployeesList.Add(p);
    }
    return Results.Ok(p.UserId);
});

app.MapGet("/Employees/{id}", (int id) =>
{
    Employee emp = emps.EmployeesList.Find(e => e.UserId == id);
    if (emp == null)
    {
        return Results.NotFound("ID not found");
    }
    else
    {
        return Results.Ok(emp);
    }
});

app.MapDelete("/Employees/{id}", (int id) =>
{
    //Person person = people.PersonList.RemoveAll(p => p.Id == id);
    int removed = emps.EmployeesList.RemoveAll(e => e.UserId == id);

    if (removed == null)
    {
        //String format é um metodo de class != Metodo de instancia [str.indexof("o")]
        return Results.NotFound("Id not found!");
    }
    else
    {
        return Results.Ok(String.Format("ID: {0} deleted", id));
    }
});


app.MapPut("/Employees", (int id, Employees empPut) =>
{
    Employees employee = empPut.EmployeesList.Find(p => p.UserId == id);
    
    
    if (removed == null)
    {
        //String format é um metodo de class != Metodo de instancia [str.indexof("o")]
        return Results.NotFound("Id not found!");
    }
    else
    {
        return Results.Ok(String.Format("ID: {0} deleted", id));
    }
});

app.UseHttpsRedirection();

app.Run();
