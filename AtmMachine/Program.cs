using AtmMachine.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.HttpLogging;
using System;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddDbContext<ATMContext>(Options => Options.UseSqlServer(
    builder.Configuration.GetConnectionString("defaultConnectionString"),
    Count =>
    {
        Count.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(5),
            errorNumbersToAdd: null
        );
    }
));


builder.Services.AddHttpLogging(logging =>
{
    // Tell .NET exactly what you want to print to the console
    logging.LoggingFields = HttpLoggingFields.RequestPath
                          | HttpLoggingFields.ResponseStatusCode
                          | HttpLoggingFields.ResponseBody; // <-- This prints the data!

    // Optional: Maximize payload characters printed (default is 32KB)
    logging.ResponseBodyLogLimit = 4096;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var app = builder.Build();


app.Use(async (context, next) =>
{
    Console.WriteLine("====================== Request Details ====================");
    System.Console.WriteLine("Request Path : " + context.Request.Path);
    //System.Console.WriteLine("" + context.Request.Protocol);
    //System.Console.WriteLine("" + context.Request.GetDisplayUrl);
    System.Console.WriteLine("=============== Request Time Details ================");
    DateTime date = DateTime.Now;
    System.Console.WriteLine("Resuest Time : " + date);
    System.Console.WriteLine("============= Response Details ==================");
    System.Console.WriteLine("StatusCode  : " + context.Response.StatusCode);
    await next.Invoke();
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
