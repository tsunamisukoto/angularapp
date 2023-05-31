using Entities;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace webapi.Controllers;
[Route("api/customer")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;
    private readonly IValidator<CustomerInput.Create> _createValidator;
    public CustomerController(ICustomerService customerService, IValidator<CustomerInput.Create> createValidator)
    {
        _customerService = customerService;
        _createValidator = createValidator;
    }
    [HttpPost]
    [Route("customer.add")]
    public async Task<CustomerModel.DetailedView> AddCustomer([FromBody] CustomerInput.Create input)
    {
        var validationResult = _createValidator.Validate(input);
        if (!validationResult.IsValid)
        {
            throw new Exception($"Invalid: {string.Join(", ", validationResult.Errors)}");
            // NOTE: Would probably throw a specific type of exception or return an http action result here, but for all intents and purposes, this blocks the exceution to db so should be fine for demo purposes.
        }
        var newCustomer = await _customerService.AddCustomer(input);
        return MapCustomer(newCustomer);
    }

    [HttpPost]
    [Route("customer.getById")]
    public async Task<CustomerModel.DetailedView> GetById([FromBody] CustomerInput.IdOnly input)
    {
        var customer = await _customerService.GetById(input.Id);
        if (customer == null)
        {
            throw new Exception($"Not found for id: {input.Id}");
            //Note: same as above, probs would return a 404 here.
        }
        return MapCustomer(customer);
    }

    [HttpPost]
    [Route("customers.getList")]
    public async Task<List<CustomerModel.Listing>> GetList()
    {
        var customers = await _customerService.GetList();
        return customers.Select(x => new CustomerModel.Listing()
        {
            Id = x.Id,
            FullName = x.FirstName + " " + x.LastName,
        }).ToList();
    }

    private CustomerModel.DetailedView MapCustomer(Customer customer)
    {
        return new CustomerModel.DetailedView()
        {
            Address = customer.Address,
            Email = customer.Email,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Mobile = customer.Mobile,
        };
    }

}
