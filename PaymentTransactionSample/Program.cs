using PaymentTransactionSample.Services;
using Serilog;
using Serilog.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IOrderService, OrderService>();

// Unfortunately, log4net.config not working for some reason..
//builder.Logging.ClearProviders();
//builder.Logging.AddLog4Net("log4net.config");

// Use Serilog instead.
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("Logs/Application.log", rollingInterval: RollingInterval.Day)

    .WriteTo.Logger(x => x
        .Filter.ByIncludingOnly(Matching.WithProperty<string>("Tag", t => t == "Http"))
        .WriteTo.File("Logs/Http.log", rollingInterval: RollingInterval.Day)
    )

    .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.MapGet("/", context =>
    {
        context.Response.Redirect("/swagger/index.html");
        return Task.CompletedTask;
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
