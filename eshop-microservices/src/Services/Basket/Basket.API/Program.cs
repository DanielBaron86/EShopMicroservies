var builder = WebApplication.CreateBuilder(args);
//ADD Services to container
var assemblies = typeof(Program).Assembly;
builder.Services.AddCarter(new DependencyContextAssemblyCatalog(
    new[] { assemblies }
));
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(assemblies);
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
});
builder.Services.AddMarten(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("Database");
    options.Connection(connectionString);
    options.Schema.For<ShoppingCart>().Identity( x => x.UserName);
}).UseLightweightSessions();
builder.Services.AddValidatorsFromAssembly(assemblies);
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddHealthChecks().AddNpgSql(builder.Configuration.GetConnectionString("Database"));
builder.Services.AddScoped<IBasketRepository,BasketRepository>();
var app = builder.Build();

//Configure the HTTP Request pipeline
app.MapCarter();
app.UseExceptionHandler(appError =>
{

});
app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    }
);
app.Run();