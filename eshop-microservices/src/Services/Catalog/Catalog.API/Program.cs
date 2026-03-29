using Catalog.API.Products.CreateProduct;

var builder = WebApplication.CreateBuilder(args);
//Add services to the container
builder.Services.AddCarter(new DependencyContextAssemblyCatalog(AppDomain.CurrentDomain.GetAssemblies()));
builder.Services.AddMediatR(config =>
{
config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});

var app = builder.Build();

//Configure the HTTP Request pipeline
app.MapCarter();
app.Run();