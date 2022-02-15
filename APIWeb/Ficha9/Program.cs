using System.Diagnostics;
using Ficha9;

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//================= EndPoints ================
app.MapGet("/" , () =>
{
    return Results.Ok("DEFAULT");
});

// ============= MiddleWares ==========
//A ORDEM DOS MIDDLEWARES INFLUENCIA
app.Use(async (context, next) =>
{
    Debug.WriteLine("BEFORE FIRST MIDDLEWARE");
    await next.Invoke();
    Debug.WriteLine("AFTER FIRST MIDDLEWARE");
});

app.Use(async (context, next) =>
{
    Debug.WriteLine("BEFORE SECOND MIDDLEWARE");
    await next.Invoke();
    Debug.WriteLine("AFTER SECOND MIDDLEWARE");
});

//============= Custom MiddleWare ==============
app.UseCustomMiddleware();
app.UseLoggerMiddleware();

app.Run();
