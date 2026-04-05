using BuildingBlocks.Behaviours;

var builder = WebApplication.CreateBuilder(args);
//Add services to the container

var assemblies = typeof(Program).Assembly;
builder.Services.AddCarter(new DependencyContextAssemblyCatalog(
    new[] { assemblies }
));
builder.Services.AddMediatR(config =>
{
config.RegisterServicesFromAssemblies(assemblies);
config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
});
builder.Services.AddMarten(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("Database");
    options.Connection(connectionString);
}).UseLightweightSessions();
builder.Services.AddValidatorsFromAssembly(assemblies);
var app = builder.Build();

//Configure the HTTP Request pipeline
app.MapCarter();
app.Run();