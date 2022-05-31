using Microsoft.AspNetCore.Mvc;
using MinimalApi.Models;
using MinimalApi.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<CustomerRepository>();

var app = builder.Build();

app.MapGet("/customers", ([FromServices] CustomerRepository repo) =>
 {
     return repo.GetAll();
 });

app.MapGet("/customers/{id}", ([FromServices] CustomerRepository repo, Guid id) =>
{
    var customer = repo.GetById(id);
    return customer is not null ? Results.Ok(customer) : Results.NotFound();
});

app.MapPost("/customers", ([FromServices] CustomerRepository repo, Customer customer) =>
{
    repo.Create(customer);
    return Results.Created($"/customers/{customer.Id}", customer);
});

app.MapPut("/customers/{id}", ([FromServices] CustomerRepository repo, Guid id, Customer customer) =>
{
    repo.Update(customer);
    return Results.Created($"/customers/{customer.Id}", customer);
});

app.MapDelete("/customers", ([FromServices] CustomerRepository repo, Guid id) =>
{
    repo.Delete(id);
    return Results.NoContent();
});

app.Run();
