using Ficha6;
using System.Text.Json;

People people = loadJsonData(); //invocar o methodo

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

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

app.Run();

People loadJsonData() //Instanciar, serializar,deserializar e returnar a instancia do metodo
{
    var jsonData = File.ReadAllText("data.json");
    People p = JsonSerializer.Deserialize<People>(jsonData);
    return p;
};


