using MinimalApi.Models;
using MinimalApi.Repositories.Interfaces;
using MinimalApi.SecretSauce.Interfaces;

namespace MinimalApi.EndpointDefinitions;

public class CustomerEndpointDefinition : IEndpointDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapGet("/customers", GetAll);
        app.MapGet("/customers/{id}", GetById);
        app.MapPost("/customers", Create);
        app.MapPut("/customers/{id}", Update);
        app.MapDelete("/customers", Delete);
    }

    internal List<Customer> GetAll(ICustomerRepository repo)
    {
        return repo.GetAll();
    }

    internal IResult GetById(ICustomerRepository repo, Guid id)
    {
        var customer = repo.GetById(id);
        return customer is not null ? Results.Ok(customer) : Results.NotFound();
    }

    internal IResult Create(ICustomerRepository repo, Customer customer)
    {
        repo.Create(customer);
        return Results.Created($"/customers/{customer.Id}", customer);
    }

    internal IResult Update(ICustomerRepository repo, Customer customer)
    {
        repo.Update(customer);
        return Results.Created($"/customers/{customer.Id}", customer);
    }

    internal IResult Delete(ICustomerRepository repo, Guid id)
    {
        repo.Delete(id);
        return Results.NoContent();
    }

    public void DefineServices(IServiceCollection services)
    {
        services.AddSingleton<ICustomerRepository, CustomerRepository>();
    }
}
