using System;
using Microsoft.Extensions.Configuration;
using BankingApp.TransactionAPI.Controllers;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BankingApp.TransactionAPI.Domain.Commands;

namespace BankingApp.TransactionAPI.UnitTest.Controllers
{
    [TestClass]
    public class TransactionControllerTests
    {
        private IMediator _mediator;
        private TransactionController _controller;
        private IConfiguration _configuration;

        [TestInitialize]
        public void Setup(IMediator mediator)
        {
            _mediator = mediator;
            _controller = new TransactionController(_mediator, _configuration);
        }

        [TestMethod]
        public async Task CreateTransactionAsyncTest(IMediator mediator)
        {
            CreateTransactionCommand createTransactionCommand = new CreateTransactionCommand(Guid.Empty, "Account is created");
            ActionResult result = await _controller.CreateTransactionAsync(createTransactionCommand);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetTransactionAsync(Guid guid)
        {
            Guid transactionGuid = Guid.Empty;
            var transaction = await _controller.GetTransactionAsync(transactionGuid);

            Assert.IsNotNull(transaction);
        }
    }
}
