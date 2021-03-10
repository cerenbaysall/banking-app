using System;
using System.Threading;
using System.Threading.Tasks;
using BankingApp.AccountAPI.Data;
using BankingApp.AccountAPI.Data.IRepositories;
using BankingApp.AccountAPI.Data.Repositories;
using BankingApp.AccountAPI.Domain.Models;
using BankingApp.AccountAPI.Domain.Queries;
using BankingApp.AccountAPI.Service.Mappers;
using BankingApp.AccountAPI.Service.Services;
using BankingApp.AccountAPI.UnitTests.Data;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BankingApp.AccountAPI.UnitTests.Queries
{
    [TestClass]
    public class GetCustomerQueryHandlerTests
    {
        ServiceCollection services;
        AccountDbContext context;

        [TestInitialize]
        public void Setup()
        {
            context = DbContextFixture.GetDbContext();
            services = new ServiceCollection();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddMediatR(typeof(GetCustomerHandler));
        }

        [TestMethod]
        public async Task GetCustomerHandlerTest()
        {
            // Arrange
            CustomerRepository customerRepository = new CustomerRepository(context);

            customerRepository.Add(new Customer()
            {
                Name = "Ceren",
                Surname = "Test",
                CustomerNo = "test-customer-no",
                CreatedDate = DateTime.Now
            });

            await customerRepository.SaveChangesAsync();
            var customer =  await customerRepository.GetAsync(x => x.CustomerNo == "test-customer-no");

            CustomerMapper customerMapper = new CustomerMapper();

            // Act
            GetCustomerHandler customerHandler = new GetCustomerHandler(customerRepository, customerMapper);
            var query = new GetCustomerQuery(customer.Id);
            var actual = await customerHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(actual);
        }
    }
}
