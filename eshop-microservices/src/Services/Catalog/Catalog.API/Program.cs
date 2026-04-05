var builder = WebApplication.CreateBuilder(args);
//Add services to the container

builder.Services.AddCarter(new DependencyContextAssemblyCatalog(
    new[] { typeof(Program).Assembly }
));
builder.Services.AddMediatR(config =>
{
config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});
builder.Services.AddMarten(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("Database");
    options.Connection(connectionString);
}).UseLightweightSessions();
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
var app = builder.Build();

//Configure the HTTP Request pipeline
app.MapCarter();
app.Run();