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
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BankingApp.AccountAPI.UnitTests.Queries
{
    [TestClass]
    public class GetAccountQueryHandlerTests
    {
        ServiceCollection services;
        AccountDbContext context;

        [TestInitialize]
        public void Setup()
        {
            context = DbContextFixture.GetDbContext();
            services = new ServiceCollection();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddMediatR(typeof(GetAccountHandler));
        }

        [TestMethod]
        public async Task GetAccountHandlerTest()
        {
            // Arrange
            AccountRepository accountRepository = new AccountRepository(context);

            accountRepository.Add(new Account()
            {
                CustomerId = Guid.Empty,
                Balance = 6000,
                CreatedDate = DateTime.Now
            });

            await accountRepository.SaveChangesAsync();
            var account =  await accountRepository.GetAsync(x => x.Balance == 6000);

            AccountMapper accountMapper = new AccountMapper();

            // Act
            GetAccountHandler accountHandler = new GetAccountHandler(accountRepository, accountMapper);
            var query = new GetAccountQuery(account.Id);
            var actual = await accountHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(actual);
        }
    }
}
