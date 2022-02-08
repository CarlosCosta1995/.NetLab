using CustomersTry;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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

//Load Json files
//Cuidado esta ordem é importante.
Customer customerJson = LoadCustomerJson();
Customers customersListJson = LoadCustomersListJson();


Customers LoadCustomersListJson() 
{
    string _customersReadJson = File.ReadAllText("Customers.json");
    Customers customersJson = JsonSerializer.Deserialize<Customers>(_customersReadJson);
    return customersJson;
}

Customer LoadCustomerJson() 
{
    string _customerReadJson = File.ReadAllText("Customer.json");
    Customer customerJson = JsonSerializer.Deserialize<Customer>(_customerReadJson);
    return customerJson;
}

app.MapGet("/customers" , () =>
{
    if (customersListJson == null)
    {
        return Results.NotFound("The file wasn't found!");
    }
    else
    {
        return Results.Ok(customersListJson);
    }
});

//Colocar {id:int} para o programa nao confundir com os outros MapGet
app.MapGet("/customers/{id:int}", (int id) =>
{
    Customer c = customersListJson.CustomersList.Find(customer => customer.CustomerNumber == id);
    if (c == null)
    {

        return Results.NotFound($"{id} Not Found!");
    }
    else
    {
        return Results.Ok(c);
    }
});

app.Run();
