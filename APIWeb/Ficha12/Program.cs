using Ficha12;
using Ficha12.Data;
using Ficha12.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Applying The Dependency Injection
builder.Services.AddDbContext<LibraryContext>(); //referencia do context com a base dados
builder.Services.AddScoped<IBookService, BookService>();//cada ver que o context é invocado verifica se existe ou acrescenta na DB

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

//Invoking  CreateDbIfNotExists criar base dados e inserir dados
app.CreateDbIfNotExists();

app.Run();
