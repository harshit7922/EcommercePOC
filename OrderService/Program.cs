using MediatR;
using Microsoft.EntityFrameworkCore;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(typeof(Program));

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<CartCheckedOutEventConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost"); // Use docker host or config for your RabbitMQ instance
         cfg.ReceiveEndpoint("order-service-cart-checkedout", e =>
        {
            e.ConfigureConsumer<CartCheckedOutEventConsumer>(context);
        });
    });
});

var app = builder.Build();

app.UseSwagger(); 
app.UseSwaggerUI();

app.Run();

