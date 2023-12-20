using Microsoft.AspNetCore.Http.Json;
using Newtonsoft.Json;
using Serilog;
using SimpleCalculator.Parser;
using SimpleCalculator.Tokenizer;
using SimpleCalculator.ValueBuilder;

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
    logger.LogInformation($"calculate, data: {equation.value}");

    var parser = new Parser(new Tokenizer(logger), logger, equation.value );
    try
    {
        var ast = parser.Parse();

        // calculate the result of the expression by traversing the AST with value builder class.
        var result = ast.Accept(new ValueBuilder());

        // using Newtonsoft to serialize the return type for the Polymorphic Type serialization which
        // System.Text.Json does not currently support by design.
        return JsonConvert.SerializeObject(new {
            result = result,
            ast = ast,
            message = "OK"
        }, new JsonSerializerSettings(){
            TypeNameHandling = TypeNameHandling.Auto
        });
    }
    catch(InvalidTokenException ex)
    {
        logger.LogInformation($"calculate, {ex.Message}");
        return JsonConvert.SerializeObject(new {
            message = $"Error: {ex.Message}"
        });
    }
});

app.Logger.LogInformation("Simple Calculator started");

app.Run();

public partial class Program {};