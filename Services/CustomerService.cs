using Entities;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Services;

public interface ICustomerService
{
    Task<Customer> AddCustomer(CustomerInput.Create input);
    Task<Customer?> GetById(int id);
    Task<List<Customer>> GetList();
}

public class CustomerService : ICustomerService
{
    private readonly EntityContext _context;

    public CustomerService(EntityContext context)
    {
        _context = context;
    }

    public async Task<Customer> AddCustomer(CustomerInput.Create input)
    {
        var customer = new Customer()
        {
            Address = input.Address,
            Email = input.Email,
            FirstName = input.FirstName,
            LastName = input.LastName,
            Mobile = input.Mobile,
        };
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task<Customer?> GetById(int id)
    {
        // Note I would .FirstOrDefaultAsync() this in an environement where I'd set up the boilerplate to make it work nicely in tests
        return _context.Customers.Where(x => x.Id == id).FirstOrDefault();
    }
    public async Task<List<Customer>> GetList()
    {
        // Note I would .ToListAsync() this in an environement where I'd set up the boilerplate to make it work nicely in tests
        return _context.Customers
            //.Where() //TODO: Filters etc
            .ToList();
    }
}
