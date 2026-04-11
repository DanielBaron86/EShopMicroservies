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
}).UseLightweightSessions();
/*if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogInitialData>();
}*/
builder.Services.AddValidatorsFromAssembly(assemblies);
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddHealthChecks();
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