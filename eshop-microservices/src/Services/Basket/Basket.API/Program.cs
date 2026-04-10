var builder = WebApplication.CreateBuilder(args);
//ADD Services to container


var app = builder.Build();

//Configure the HTTP Request pipeline

app.MapGet("/", () => "Hello World!");

app.Run();