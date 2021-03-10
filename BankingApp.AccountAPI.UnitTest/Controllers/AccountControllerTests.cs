using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using BankingApp.AccountAPI.Domain.Queries;
using BankingApp.AccountAPI.Controllers;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using BankingApp.AccountAPI.Domain.Commands;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.AccountAPI.UnitTests.Controllers
{
    [TestClass]
    public class AccountControllerTests
    {
        private IMediator _mediator;
        private AccountController _controller;
        private IConfiguration _configuration;

        [TestInitialize]
        public void Setup(IMediator mediator)
        {
            _mediator = mediator;
            _controller = new AccountController(_mediator, _configuration);
        }

        [TestMethod]
        public async Task CreateAccountAsyncTest()
        {
            CreateAccountCommand createAccountCommand = new CreateAccountCommand("test-customer-no", 6000);
            ActionResult result = await _controller.CreateAccountAsync(createAccountCommand);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetAccountAsync(Guid guid)
        {
            Guid accountGuid = Guid.Empty;
            var account = await _controller.GetAccountAsync(accountGuid);

            Assert.IsNotNull(account);
        }
    }
}
