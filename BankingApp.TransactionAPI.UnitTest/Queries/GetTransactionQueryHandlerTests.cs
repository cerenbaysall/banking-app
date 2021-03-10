using System;
using System.Threading;
using System.Threading.Tasks;
using BankingApp.TransactionAPI.Data;
using BankingApp.TransactionAPI.Data.IRepositories;
using BankingApp.TransactionAPI.Data.Repositories;
using BankingApp.TransactionAPI.Domain.Queries;
using BankingApp.TransactionAPI.Service.Mappers;
using BankingApp.TransactionAPI.Service.Services;
using BankingApp.TransactionAPI.UnitTest.Data;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BankingApp.TransactionAPI.UnitTest.Queries
{
    [TestClass]
    public class GetTransactionQueryHandlerTests
    {
        ServiceCollection services;
        TransactionDbContext context;

        [TestInitialize]
        public void Setup()
        {
            context = DbContextFixture.GetDbContext();
            services = new ServiceCollection();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddMediatR(typeof(GetTransactionHandler));
        }

        [TestMethod]
        public async Task GetTransactionHandlerTest()
        {
            // Arrange
            TransactionRepository transactionRepository = new TransactionRepository(context);
            TransactionMapper transactionMapper = new TransactionMapper();

            // Act
            GetTransactionHandler handler = new GetTransactionHandler(transactionRepository, transactionMapper);
            var query = new GetTransactionQuery(Guid.Empty);
            var actual = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(actual);
        }
    }
}
