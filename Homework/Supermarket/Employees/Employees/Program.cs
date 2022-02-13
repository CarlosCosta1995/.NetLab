using EmployeesNameSpace;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//criar uma instancia das classes e fazer load do ficheiro Json
Employee emp = LoadEmployeeJsonText();
Employees emps = LoadEmployeesJsonText();


//Fazer leitura do ficheiro JSon
Employee LoadEmployeeJsonText() 
{
    string employee = File.ReadAllText("Employee.json");
    Employee employeeJson = JsonSerializer.Deserialize<Employee>(employee);
    return employeeJson;
}

Employees LoadEmployeesJsonText()
{
    string employees = File.ReadAllText("Employees.json");
    Employees employeesJson = JsonSerializer.Deserialize<Employees>(employees);
    return employeesJson;
}

//Listar todos os funcionarios e devolver como resposta uma lista de funcionarios
app.MapGet("/employees", () => 
{
    if (emps == null)
    {
        return Results.NotFound("Json file was not found.");
    }
    else
    {
        return Results.Ok(emps);
    }
});

/*Adicionar um novo funcionário, 
 * o ID deve ser gerado automaticamente tendo em conta o número de funcionários existentes. 
 * O ID do novo funcionário deve ser devolvido na resposta */
app.MapPost("/employees", (Employee newEmployee) =>
{
    if (emps.EmployeesList.Count == 0)
    {
        newEmployee.UserId = 1;
        emps.EmployeesList.Add(newEmployee);
    }
    else
    {
        Employee lastEmployee = emps.EmployeesList.LastOrDefault();
        newEmployee.UserId = lastEmployee.UserId +1;
        emps.EmployeesList.Add(newEmployee);
    }
    return Results.Created("/employees", newEmployee);
});

/*Apagar um funcionário pelo seu ID. O ID do funcionário removido deve ser devolvido na resposta*/
app.MapDelete("/employees/{id:int}", (int id) => 
{ 
    int removed = emps.EmployeesList.RemoveAll(users => users.UserId == id);
    if (removed == 0) 
    {
        return Results.NotFound($"No users was found with this id number {id}.");
    }
    else
    {
        return Results.Ok($"All users with this id number {id} have been removed.");
    }
});

//Selecionar apenas um funcionário pelo seu ID e devolver esse mesmo funcionário na resposta
app.MapGet("/employees/employee/{id:int}", (int id) =>
{
    Employee employee = emps.EmployeesList.Find(e => e.UserId == id);
    if (employee == null)
    {
        return Results.NotFound($"A employee with this id number {id} was not found.");
    }
    else
    {
        return Results.Ok(employee);
    }
});

//Alterar os detalhes de um determinado funcionário
//pelo seu ID e devolver essa mesma funcionário atualizada na resposta
app.MapPut("/employees/{id:int}", (int id, Employee changeEmployee) => 
{ 
   Employee p = emps.EmployeesList.Find(user => user.UserId == id);
    if (p == null)
    {
        return Results.NotFound($"A user with this id number {id} wasn't found.");
    }
    else 
    {
        emp.JobTitle = changeEmployee.JobTitle;
        emp.PhoneNumber = changeEmployee.PhoneNumber;
        emp.Region = changeEmployee.Region;
        emp.LastName = changeEmployee.LastName;
        emp.FirstName = changeEmployee.FirstName;
        emp.EmailAddress = changeEmployee.EmailAddress;
        emp.EmployeeCode = changeEmployee.EmployeeCode;
        return Results.Ok(changeEmployee);
    }
});

//Listar todos os funcionários por região
app.MapGet("/employees/region/{region}", (string region) => 
{ 
    List<Employee> ps = emps.EmployeesList.FindAll(user => user.Region == region);
    if (ps == null) 
    {
        return Results.NotFound($"No user was founded with this region: {region}.");
    }
    else
    {
        return Results.Ok(ps);
    }

});

//Efetuar download da lista atual de funcionários como um ficheiro .json
app.MapGet("/employees/download", () =>
{
    string listEmps = JsonSerializer.Serialize<Employees>(emps);
    File.WriteAllText("EmployeesList.json", listEmps);

    byte[] bytes = File.ReadAllBytes("EmployeesList.json");
    return Results.File(bytes, null, "EmployeesList.json");
});

app.Run();
