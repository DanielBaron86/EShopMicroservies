using Catalog.API.Products.CreateProduct;
using JasperFx;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
//Add services to the container
builder.Services.AddCarter(new DependencyContextAssemblyCatalog(AppDomain.CurrentDomain.GetAssemblies()));
builder.Services.AddMediatR(config =>
{
config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});
builder.Services.AddMarten(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("Database");
    options.Connection(connectionString);
}).UseLightweightSessions();

var app = builder.Build();

//Configure the HTTP Request pipeline
app.MapCarter();
app.Run();