
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using MyAPI.Services;
using Serilog;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

var folder = Environment.SpecialFolder.LocalApplicationData;
var path = Environment.GetFolderPath(folder);
var DbPath = System.IO.Path.Join(path, "mydb.db");
// Add services to the container.
builder.Services.AddTransient<ITransactionRepository, TransactionRepository>();
builder.Services.AddTransient<IArticleRepository, ArticleRepository>();
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<IPaymentRepository, PaymentRepository>();
builder.Services.AddTransient<ITransactionService, TransactionService>();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var logger = LoggerFactory.Create(config =>
{
    config.AddConsole();
}).CreateLogger("Program");
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MyDbContext>(
        options => options.UseSqlite($"Data Source={DbPath}"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler(exceptionHandlerApp =>
      {

          exceptionHandlerApp.Run(async context =>
          {

              context.Response.StatusCode = StatusCodes.Status500InternalServerError;

              // using static System.Net.Mime.MediaTypeNames;
              context.Response.ContentType = Text.Plain;

              await context.Response.WriteAsync("An exception was thrown.");

              var exceptionHandlerPathFeature =
                  context.Features.Get<IExceptionHandlerPathFeature>();
              logger.LogError("Error ocured" + exceptionHandlerPathFeature?.Error?.Message);
              if (exceptionHandlerPathFeature?.Path == "/")
              {
                  await context.Response.WriteAsync(" Page: Home.");
              }
          });
      });
}


app.MapControllers();

app.Run();
