using Microsoft.AspNetCore.Mvc;
using Newsletter.Api.Emails;
using Newsletter.Api.Messages;
using Rebus.Bus;
using Rebus.Config;
using Rebus.Routing.TypeBased;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IEmailService, EmailService>();

builder.Services.AddRebus(
    rebus => rebus
        .Routing(r =>
            r.TypeBased().MapAssemblyOf<Program>("newsletter-queue"))
        .Transport(t =>
            t.UseRabbitMq(
                builder.Configuration.GetConnectionString("RabbitMq"),
                inputQueueName: "newsletter-queue"))
        .Sagas(s =>
            s.StoreInSqlServer(
                builder.Configuration.GetConnectionString("SqlServer"),
                dataTableName: "Sagas",
                indexTableName: "SagaIndexes"))
        .Timeouts(t =>
            t.StoreInSqlServer(
                builder.Configuration.GetConnectionString("SqlServer"),
                tableName: "Timeouts"))
);

builder.Services.AutoRegisterHandlersFromAssemblyOf<Program>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/newsletters", async ([FromBody] string email, IBus bus) =>
{
    await bus.Send(new SubscribeToNewsletter(email));

    return Results.Accepted();
});

app.UseHttpsRedirection();

app.Run();
