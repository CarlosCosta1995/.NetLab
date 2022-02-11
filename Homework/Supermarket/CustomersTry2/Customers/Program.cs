//Colocar os using
using CustomersNameSpace;
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

//instanciar LoadJsonText() para as duas classes
Customer customerJson = LoadCostumerJsonText();
Customers customersJson = LoadCustomersJsonText();
Customers Customers = new Customers();

//Ciar os metodos LoadJesonText, deserialize e returnar os repectivos objs
Customer LoadCostumerJsonText() 
{
    string _customer = File.ReadAllText("Customer.json");
    Customer newCustomer = JsonSerializer.Deserialize<Customer>(_customer);
    return newCustomer;
}

Customers LoadCustomersJsonText() 
{
    string _customers = File.ReadAllText("customers.json");
    Customers _newCustomers = JsonSerializer.Deserialize<Customers>(_customers);
    return _newCustomers;
}

//Criar um Http endpoint para mostrar todos os clients da lista
app.MapGet("/customers", () =>
{
    //Se o customersJson nao é lido com sucesso ou o ficheiro encontra-se vazio
    if (customersJson == null)
    {
        return Results.NotFound("Content was empty or error reading the json file.");
    }
    //Caso contrário, returna a leitura do ficheiro customersJson
    else
    {
      return Results.Ok(customersJson);
    }
});

app.MapGet("/customer/{id:int}",(int id) => 
{
    Customer customer = customersJson.CustomersList.Find(user => user.CustomerNumber == id);
    if (customer == null)
    {
        return Results.NotFound($"The {id} was not found.");
    }
    else
    {
       return Results.Ok(customer);
    }
});

app.MapGet("/customer/region/{region}", (string region) => 
{
    //procura uma lista porque pode haver mais do 1 elemento da mesma regiao
    List<Customer> customerRegion = customersJson.CustomersList.FindAll(user => user.Region == region);
    if (customerRegion == null) 
    { 
        return Results.NotFound($"{region} wasn't found."); 
    }
    else 
    { 
        return Results.Ok(customerRegion); 
    }
});


//Post nao precisa de um id apenas de um customer, apenas incrementa na lista
app.MapPost("/customers", (Customer _newCustomerAdd) => 
{
    //Ver se a lista tem elementos para incrementar ou nao o [index]
    if (customersJson.CustomersList.Count == 0)
    {
        _newCustomerAdd.CustomerNumber = 1;
        customersJson.CustomersList.Add(_newCustomerAdd);
    }
    else
    {
        Customer lastCustomerInTheList = customersJson.CustomersList.LastOrDefault();
        _newCustomerAdd.CustomerNumber = lastCustomerInTheList.CustomerNumber + 1;
        customersJson.CustomersList.Add(_newCustomerAdd);
    }
    return Results.Created("/customers",_newCustomerAdd);
});

//precisa do id e do customer a ser editado
app.MapPut("/customers/customersEdit/{id:int}", (int id, Customer _newCT) => 
{
    Customer ct = customersJson.CustomersList.Find(user => user.CustomerNumber == id);
    if (ct == null)
    {
        return Results.NotFound($"{id} was not found.");
    }
    else
    {
        //Atribuir os atributos/propriedades para serem alterados
        customerJson.Address = _newCT.Address;
        customerJson.City = _newCT.City;
        customerJson.Region = _newCT.Region;
        customerJson.LastName = _newCT.LastName;
        customerJson.FirstName = _newCT.FirstName;
        customerJson.PhoneNumber = _newCT.PhoneNumber;
        return Results.Ok(_newCT);
    }
});

app.MapDelete("/customers", (int id) =>
{
    int removed = customersJson.CustomersList.RemoveAll(e => e.CustomerNumber == id);
    if (removed == 0)
    {
        return Results.NotFound($"{id} not found.");
    }
    else
    {
        return Results.Ok($"{id} was removed.");
    }
});


//===== Erro o Download nao gera os customers da lista =====
app.MapGet("/customers/download", () => 
{
    string str = JsonSerializer.Serialize<Customers>(Customers);
    File.WriteAllText("customersList.json", str);

    byte[] bytes = File.ReadAllBytes("customersList.json");
    return Results.File(bytes, null, "customersList.json");
});

app.Run();
