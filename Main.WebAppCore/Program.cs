using Main.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices ( builder.Configuration );

builder.Services.AddControllersWithViews ( );

var app = builder.Build();

app.UseExceptionHandler ( );

app.UseStatusCodePages ( );

app.MapControllers ( );

app.Run();
