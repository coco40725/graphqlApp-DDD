using GraphQL;
using GraphQL.Types;
using graphqlapidemo;
using GraphQLApp.API.GraphQL;
using GraphQLApp.API.GraphQL.Type;
using GraphQLApp.Domain.AggregatesModel.OrderAggregate;
using GraphQLApp.Infrastructure.Repositories;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

//https://binodmahto.medium.com/graphql-with-asp-net-core-api-making-your-api-smart-50b27e9577e5
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// 註冊所有 controller
builder.Services.AddControllers(); 

// Add scope
builder.Services.AddSingleton<IOrderRepo, OrderRepo>();
builder.Services.AddSingleton<IOrderItemRepo, OrderItemRepo>();
// health check
builder.Services.AddHealthChecks();

// GraphQL
builder.Services.AddSingleton<OrderType>();
builder.Services.AddSingleton<OrderItemType>();
builder.Services.AddSingleton<Query>();
builder.Services.AddSingleton<Mutation>();
builder.Services.AddSingleton<ISchema, MySchema>();
builder.Services.AddGraphQL(b => b
    .AddSystemTextJson()  // serializer: 把 object 轉成 json 傳給前端
    .AddAutoSchema<Query>()
);


// builder.Services.AddGraphQL(builder => builder
//     .AddSystemTextJson(jsonSerializerOptions => jsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase)
//     .AddDataLoader()
//     .AddSchema<BwdspSchema>()
//     .AddGraphTypes(typeof(BwdspSchema).Assembly)
//     .AddAuthorizationRule()
//     .AddUserContextBuilder<UserContextBuilder>()
//     .UseMiddleware<PrometheusMetricsFieldMiddleware>());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseSwagger();
    // app.UseSwaggerUI();
    app.UseGraphQLGraphiQL(path: "/explorer");
}


// health
app.MapHealthChecks("/webapp-bg/healthz/ready", new HealthCheckOptions
{
    Predicate = healthCheck => healthCheck.Tags.Contains("ready")
});

app.MapHealthChecks("/webapp-bg/healthz/live", new HealthCheckOptions
{
    Predicate = _ => false
});

// 將 request map 到對應的 controller
app.MapControllers();


// app.UseCors(allowCorsRule);
app.UseHttpsRedirection();
app.UseGraphQL<ISchema>("/graphql"); // url to host GraphQL endpoint; 告訴 GraphQL 要用哪個 schema

app.Run();
