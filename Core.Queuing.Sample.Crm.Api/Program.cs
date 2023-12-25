using DotNetCore.CAP;
using Microsoft.EntityFrameworkCore;
using Core.Queuing;
using Core.Queuing.Sample.Crm.Api.Service;
using Core.Queuing.Sample.Crm.Api.Subscriber;
using Core.Queuing.Sample.Crm.Api.DAL;
using Core.Queuing.Model;
using Microsoft.Extensions.Options;
using Core.Queuing.OptionsBuilder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*builder.Services
    .AddDbContext<ServiceDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
*/

/*builder.Services.AddQueue(ctg =>
{
    ctg.DefaultOptions = new DefaultOptions
    {
        DefaultGroupName = "Crm-Queue",
        FailedRetryCount = 3
    };

    ctg.AddSqlServer(cx =>
    {
        cx.ConnectionString = builder.Configuration.GetConnectionString("CAPConnection");
    });
    

    ctg.AddRabbitMQ(queue =>
    {
        queue.ConnectionFactoryOptions = options =>
        {
            options.Ssl.Enabled = false;
            options.HostName = "localhost";
            options.UserName = "guest";
            options.Password = "guest";
            options.Port = 5672;
        };
    });

    ctg.AddOpenSearch(x =>
    {
        x.UserName = "admin";
        x.Password = "admin";
        x.OpenSearchHostUrl = "http://localhost:9200";
    });

    ctg.AddTransactionFile();
});*/
/*

builder.Services.AddScoped<ICapSubscribe, CustomersSubscriberService>();*/
//builder.Services.AddScoped<ICustomerService, CustomerService>();

var app = builder.Build();
app.UseHealthChecks("/health");
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
