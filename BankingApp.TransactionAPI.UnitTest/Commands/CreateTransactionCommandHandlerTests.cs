using System;
using System.Threading;
using System.Threading.Tasks;
using BankingApp.TransactionAPI.Data;
using BankingApp.TransactionAPI.Data.IRepositories;
using BankingApp.TransactionAPI.Data.Repositories;
using BankingApp.TransactionAPI.Domain.Commands;
using BankingApp.TransactionAPI.Service.Mappers;
using BankingApp.TransactionAPI.Service.Services;
using BankingApp.TransactionAPI.UnitTest.Data;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BankingApp.TransactionAPI.UnitTest.Commands
{
    [TestClass]
    public class CreateTransactionCommandHandlerTests
    {
        ServiceCollection services;
        TransactionDbContext context;

        [TestInitialize]
        public void Setup()
        {
            context = DbContextFixture.GetDbContext();
            services = new ServiceCollection();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddMediatR(typeof(CreateTransactionHandler));
        }

        [TestMethod]
        public async Task CreateTransactionHandlerTest()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            TransactionRepository transactionRepository = new TransactionRepository(context);
            TransactionMapper transactionMapper = new TransactionMapper();

            // Act
            CreateTransactionHandler handler = new CreateTransactionHandler(transactionRepository, transactionMapper);
            var command = new CreateTransactionCommand(Guid.Empty, "Account is created");
            var actual = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNotNull(actual);
        }
    }
}
