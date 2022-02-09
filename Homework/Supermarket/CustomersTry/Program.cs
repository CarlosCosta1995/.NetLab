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

app.MapPost("/customers", (Customer newCostumer) =>
{
    //Addiciona um custumer a lista de custumer, mas precisa de receber o customer como parametro
    //De seguida, verifica quantos elementos já existem na lista.
    //Se a lista encontra-se vazia == newCostumer.id é o primeiro
    //Contrario, newCostumer.id == (ultimo elemento da lista [tem de ser o total de elementos da lista -1 {index[1] = 0} ]) + incrementa (+1) 
    if (customersListJson.CustomersList.Count == 0)
    {
        newCostumer.CustomerNumber = 1;
        customersListJson.CustomersList.Add(newCostumer);
    }
    else
    {
        newCostumer.CustomerNumber = (customersListJson.CustomersList.Count - 1) +1 ;
        customersListJson.CustomersList.Add(newCostumer);
        
    }
    return Results.Created("/custumers", newCostumer.CustomerNumber);
});

/* ===================  Não Funciona ============== */
app.MapPut("/customers/{id:int}", (int id, Customer nc) =>
{
    //Para alterar precisa de receber um id e um cliente nc(Para que possa inserir as propriedades a serem alteradas)
    //Cria-se uma instancia nova do customer para verificar se existe o mesmo na lista de Customers
    //Se a instancia é null, significa que o elemento nao foi encontrado [Returna o id como nao encontrado]
    //Caso encontre, igualar todas as propriedades do Customer do JSON às inseridas Customer do parametro [returna a pessoa]
    Customer newCustomer = customersListJson.CustomersList.Find(e => e.CustomerNumber == id);
    if (newCustomer == null)
    {
        return Results.NotFound($"A user with the id of {id} wasn't found!");
    }
    else
    {
        //int _costumerNumber, string _firstName, string _lastName, string _city, string _region, string Email, string _address
        //customerJson.CustomerNumber = nc.CustomerNumber; Não podemos igualar o id porque ja é dado no parametro
        customerJson.FirstName = nc.FirstName;
        customerJson.LastName = nc.LastName;
        customerJson.City = nc.City;
        customerJson.Region = nc.Region;
        customerJson.Email = nc.Email;
        customerJson.Address = nc.Address;
        return Results.Ok(nc);
    }
});


// ================= Deu erro mas fez o Delete do Customer =================
app.MapDelete("/custumers/{id:int}", (int id) =>
{
    //Criar uma variavel remover o id inserido existe na BD
    int nc = customersListJson.CustomersList.RemoveAll(ID => ID.CustomerNumber == id);

    //Se nao for encontrado, returna que o id nao foi encontrado
    if (nc == null)
    {
        return Results.NotFound($"{id} not found!");
    }
    else
    {
        //Why using the String.Format("Customer Number: {id} was deleted!", id)??? significa???
        return Results.Ok(String.Format("Customer Number: {id} was deleted!", id));
    }
});

app.MapGet("/customer/{region}", (string region) =>
{
    //Preciso de uma lista em que contenha todos os Customers referentes a esta zona (Parametro)
    List<Customer> regionCustomers = customersListJson.CustomersList.FindAll(e => e.Region == region);
    if (regionCustomers == null)
    {
        return Results.NotFound($"{region} not found!");
    }
    else
    {
        return Results.Ok(regionCustomers);
    }
});

app.MapGet("/custumers/download", () =>
{
    //Crear um array que contenha todos os bytes lidos do ficheiro JSON
    byte[] serialize = File.ReadAllBytes("Customers.json");
    return Results.File(serialize, null, "Customers.json");
});


app.Run();
