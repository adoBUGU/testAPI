using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using trackingAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<OrderDBContext>(option => option.UseInMemoryDatabase("MyDB"));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    var scope = scopedFactory.CreateScope();
    var context = scope.ServiceProvider.GetService<OrderDBContext>();
    SeedData.SeedOrder(context);

    //SEED SOME DATA
    //var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    //using (var scope = scopedFactory.CreateScope())
    //{
    //    var service = scope.ServiceProvider.GetService<SeedData>();
    //    service.Seed();
    //}



    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();



