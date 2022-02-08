using Ficha6;
using System.Text.Json;

People people = loadJsonData(); //invocar o methodo


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//add o swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();


// Configure the HTTP request pipeline.

//add ifs do swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/People", () =>
{
    if (people == null)
    {
        return Results.NotFound(people);
    }
    else
    {
        //returna um codigo se tiver people.
        return Results.Ok(people);
    }
    
});

app.MapGet("/People/{Id}", (int id) =>
{
    for (int i = 0; i < people.PersonList.Count; i++)
    {
        Person person = people.PersonList[i];
        if (person.Id == id)
        {
            return Results.Ok(person);
        }
    }
    
    //return Results.NotFound($"/people/{ id}");
    return Results.NotFound($"ID: {id} not found!");
});

app.MapPost("/People", (Person person) =>
{
    people.PersonList.Add(person);
    return Results.Ok(person);
});


app.MapDelete("/People", (int id) =>
{
    //Person person = people.PersonList.RemoveAll(p => p.Id == id);

    for (int i = 0; i < people.PersonList.Count; i++)
    {
        //Procura o id person dentro da lista people
        Person person = people.PersonList[i];
        if (person.Id == id)
        {
            //Quando encontrado - remove o id == i Quando encontrado
            people.PersonList.RemoveAt(i);
            //Returna o id apagado
            return Results.Ok(id);
        }
    }
    return Results.NotFound($"ID: {id} not found!");
});

//=> delegate [sempre que este for executado faz o que esta dentro.]
//? == significa que devolve Null
app.MapPut("/People", (int id, Person person) =>
{
    //Funciona igual a um for em que p é todas as iteraçoes e verifica se é igual ao id inserido
    Person p = people.PersonList.Find(p => p.Id == id);

    if (p != null) 
    {
        return Results.NotFound($"ID: {id} not found!");
    }
    else
    {
        p.FirstName = person.FirstName;
        p.LastName = person.LastName;
        p.Profession = person.Profession;
        p.Gender = person.Gender;
        p.Age = person.Age;
        //p.Id = person.Id; ID mantem-se porque so queremos alterar os dados da pessoa
        return Results.Ok(person);
    }

    /*for (int i = 0; i < people.PersonList.Count; i++)
    {
        //Procura o id person dentro da lista people
        Person person = people.PersonList[i];
        if (person.Id == id)
        {
            //Quando encontrado - editar o id == i
            people.PersonList.Add(person);
            //Returna o faz update
            return Results.Ok(person);
        }
    }
    return Results.NotFound($"ID: {id} not found!");*/
});


app.Run();

People loadJsonData() //Instanciar, serializar,deserializar e returnar a instancia do metodo
{
    var jsonData = File.ReadAllText("data.json");
    People p = JsonSerializer.Deserialize<People>(jsonData);
    return p;
};


