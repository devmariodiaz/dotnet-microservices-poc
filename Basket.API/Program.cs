using Basket.API.Application.Interfaces;
using Basket.API.Infrastructure;
using Discount;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis")));

builder.Services.AddScoped<IBasketRepository, RedisBasketRepository>();

builder.Services.AddGrpcClient<DiscountService.DiscountServiceClient>(o =>
{
    o.Address = new Uri("http://discount.grpc:80");
});

var app = builder.Build();

app.Urls.Add("http://*:80");

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

await app.RunAsync();