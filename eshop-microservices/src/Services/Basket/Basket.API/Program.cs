var builder = WebApplication.CreateBuilder(args);
//ADD Services to container
var assemblies = typeof(Program).Assembly;
builder.Services.AddCarter(new DependencyContextAssemblyCatalog(
    new[] { assemblies }
));

var app = builder.Build();

//Configure the HTTP Request pipeline
app.MapCarter();
app.MapGet("/", () => "Hello World!");

app.Run();