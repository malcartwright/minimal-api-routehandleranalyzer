
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/weatherforecast", StaticWeatherHandler.GetWeatherForecast)
.WithName("GetWeatherForecast");

app.MapGet("/weatherforecast5", new InstanceWeatherHandler().GetWeatherForecast)
.WithName("GetWeatherForecast5");

app.MapGet("/weatherforecast2", StaticMethodNS.WeatherHandler2.GetWeatherForecast)
.WithName("GetWeatherForecast2");

app.MapGet("/weatherforecast3", WeatherHandler3.GetWeatherForecast)
.WithName("GetWeatherForecast3");

var weatherHandler = new WeatherHandler4();
app.MapGet("/weatherforecast4", weatherHandler.GetWeatherForecast)
.WithName("GetWeatherForecast4");

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

internal class StaticWeatherHandler
{
    private readonly static string[] summaries = new[]
    {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

    internal static WeatherForecast[] GetWeatherForecast()
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateTime.Now.AddDays(index),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
        return forecast;
    }

}

internal class InstanceWeatherHandler
{
    private readonly static string[] summaries = new[]
    {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

    internal WeatherForecast[] GetWeatherForecast()
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateTime.Now.AddDays(index),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
        return forecast;
    }

}
