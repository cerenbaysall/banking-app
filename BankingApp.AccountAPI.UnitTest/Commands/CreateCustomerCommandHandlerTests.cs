using System;
using System.Threading;
using System.Threading.Tasks;
using BankingApp.AccountAPI.Data;
using BankingApp.AccountAPI.Data.IRepositories;
using BankingApp.AccountAPI.Data.Repositories;
using BankingApp.AccountAPI.Domain.Commands;
using BankingApp.AccountAPI.Service.Mappers;
using BankingApp.AccountAPI.Service.Services;
using BankingApp.AccountAPI.UnitTests.Data;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BankingApp.AccountAPI.UnitTests.Commands
{
    [TestClass]
    public class CreateCustomerCommandHandlerTests
    {
        ServiceCollection services;
        AccountDbContext context;

        [TestInitialize]
        public void Setup()
        {
            context = DbContextFixture.GetDbContext();
            services = new ServiceCollection();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddMediatR(typeof(CreateCustomerHandler));
        }

        [TestMethod]
        public async Task CreateCustomerHandlerTest()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            CustomerRepository customerRepository = new CustomerRepository(context);
            CustomerMapper customerMapper = new CustomerMapper();

            // Act
            CreateCustomerHandler handler = new CreateCustomerHandler(customerRepository, customerMapper);
            var command = new CreateCustomerCommand("Ceren","Keles","test-customer-no");
            var actual = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNotNull(actual);
        }
    }
}
