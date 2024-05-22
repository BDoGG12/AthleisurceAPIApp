using AthleisurceAPI;
using AthleisurceAPI.Service;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container via Startup
var startup = new Startup();
startup.ConfigureServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline via Startup
var env = app.Environment;
startup.Configure(app, env);


app.Run();
