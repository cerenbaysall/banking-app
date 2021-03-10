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
    public class CreateAccountCommandHandlerTests
    {
        ServiceCollection services;
        AccountDbContext context;

        [TestInitialize]
        public void Setup()
        {
            context = DbContextFixture.GetDbContext();
            services = new ServiceCollection();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddMediatR(typeof(CreateAccountHandler));
        }

        [TestMethod]
        public async Task CreateAccountHandlerTest()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            AccountRepository accountRepository = new AccountRepository(context);
            CustomerRepository customerRepository = new CustomerRepository(context);
            AccountMapper accountMapper = new AccountMapper();

            // Act
            CreateAccountHandler handler = new CreateAccountHandler(accountRepository, customerRepository, accountMapper, mockMediator.Object);
            var command = new CreateAccountCommand("test-customerno", 6000);
            var actual = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNotNull(actual);
        }
    }
}
