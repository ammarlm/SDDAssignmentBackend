using SDDAssignmentBackend.Configurations;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
//Serilog.Debugging.SelfLog.Enable(Console.Out);
var logger = Serilog.Log.Logger = new LoggerConfiguration()
      .Enrich.FromLogContext()
      .WriteTo.Console()
      .ReadFrom.Configuration(builder.Configuration)
      .CreateLogger();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDependencies(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
