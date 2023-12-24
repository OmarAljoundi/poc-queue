using DotNetCore.CAP;
using Microsoft.EntityFrameworkCore;
using Core.Queuing;
using Core.Queuing.Sample.Store.Api.Subscriber;
using Core.Queuing.Sample.Store.Api.Service;
using Core.Queuing.Sample.Store.Api.DAL;
using Core.Queuing.Model;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddDbContext<ServiceDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddQueue(ctg =>
{
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
});


///adding services
builder.Services.AddScoped<IOrderService,OrderService>();
builder.Services.AddScoped<ICapSubscribe, OrderSubscriberService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
