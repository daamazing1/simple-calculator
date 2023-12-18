using Serilog;
using SimpleCalculator.Parser;
using SimpleCalculator.Tokenizer;

var builder = WebApplication.CreateBuilder(args);

// Add Serilog
builder.Logging.ClearProviders();
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
builder.Logging.AddSerilog(logger);

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

// Minimal API end-point for calcualtion, takes the POST body in the form of application/json.
// example: {"value":"1+2"}
app.MapPost("/calculate", (Equation equation, ILoggerFactory loggerFactory) =>
{
    var logger = loggerFactory.CreateLogger("calculate");
    logger.LogInformation($"method: calculate, data: {equation.value}");

    var p = new Parser(new Tokenizer(), equation.value );
    p.Parse();

    return $"{equation.value} OK";
});

app.Logger.LogInformation("Simple Calculator started");

app.Run();

public partial class Program {};