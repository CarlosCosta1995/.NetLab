var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapGet("/", () => 
{
    return "HELLO WORLD!";
});

app.MapGet("/name", () =>
{
    return "Carlos";
});

//MapDelete, MapPost .... 
/*{
 * app.MapGet("/test", () => "Hello World!");
() => DeleGate??
});*/

app.Run();
