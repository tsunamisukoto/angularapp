
using Castle.Core.Resource;
using Entities;
using Microsoft.EntityFrameworkCore;
using Models;
using Moq;
using Services;

public class CustomerServiceFacts
{
    public class CustomerServiceFixture
    {
        public CustomerServiceFixture(IQueryable<Customer>? customers = null)
        {

            var dbContextMock = new Mock<EntityContext>();
            var customersDbSetMock = new Mock<DbSet<Customer>>();
            if (customers != null)
            {
                customersDbSetMock.As<IQueryable<Customer>>().Setup(x => x.Provider).Returns(customers.Provider);
                customersDbSetMock.As<IQueryable<Customer>>().Setup(x => x.Expression).Returns(customers.Expression);
                customersDbSetMock.As<IQueryable<Customer>>().Setup(x => x.ElementType).Returns(customers.ElementType);
                customersDbSetMock.As<IQueryable<Customer>>().Setup(x => x.GetEnumerator()).Returns(() => customers.GetEnumerator());
            }
            dbContextMock.SetupGet(x => x.Customers).Returns(customersDbSetMock.Object);
            Context = dbContextMock;
            Subject = new CustomerService(dbContextMock.Object);
        }

        public Mock<EntityContext> Context { get; }
        public CustomerService Subject { get; }
    }
    public class AddCustomer
    {
        [Fact]
        public async Task ShouldAddCustomer()
        {
            var input = new CustomerInput.Create
            {
                Address = "123 Street",
                Email = "test@example.com",
                FirstName = "John",
                LastName = "Doe",
                Mobile = "1234567890"
            };

            var fixture = new CustomerServiceFixture();

            var result = await fixture.Subject.AddCustomer(input);

            fixture.Context.Verify(x => x.Customers.Add(It.IsAny<Customer>()), Times.Once);
            Assert.Equal(input.Address, result.Address);
            Assert.Equal(input.Email, result.Email);
            Assert.Equal(input.FirstName, result.FirstName);
            Assert.Equal(input.LastName, result.LastName);
            Assert.Equal(input.Mobile, result.Mobile);
        }
    }
    public class GetById
    {
        [Fact]
        public async Task ExistingIdShouldReturn()
        {
            var customerId = 1;
            var customer = new Customer { Id = customerId };

            var customers = new List<Customer>
            {
                customer
            }.AsQueryable();
            var fixture = new CustomerServiceFixture(customers);

            var result = await fixture.Subject.GetById(customerId);

            Assert.NotNull(result);
            Assert.Equal(customerId, result.Id);
        }

        [Fact]
        public async Task NonexistentIdShouldReturnNull()
        {
            var customerId = 1;

            var customers = new List<Customer>
            {
            }.AsQueryable();
            var fixture = new CustomerServiceFixture(customers);

            var result = await fixture.Subject.GetById(customerId);

            Assert.Null(result);
        }
    }

    public class GetList
    {

        [Fact]
        public async Task ShouldReturnListOfCustomers()
        {
            var customers = new List<Customer>
        {
            new Customer { Id = 1 },
            new Customer { Id = 2 },
            new Customer { Id = 3 }
        }.AsQueryable();

            var fixture = new CustomerServiceFixture(customers);

            var result = await fixture.Subject.GetList();

            Assert.Equal(customers.Count(), result.Count);
            foreach (var customer in customers)
            {
                Assert.Contains(result, c => c.Id == customer.Id);
            }
        }
    }

}