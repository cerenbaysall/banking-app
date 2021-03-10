using System;
using Microsoft.Extensions.Configuration;
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
    public class CustomerControllerTests
    {
        private IMediator _mediator;
        private CustomerController _controller;
        private IConfiguration _configuration;
        
        [TestInitialize]
        public void Setup(IMediator mediator)
        {
            _mediator = mediator;
            _controller = new CustomerController(_mediator, _configuration);
        }

        [TestMethod]
        public async Task CreateCustomerAsyncTest()
        {
            CreateCustomerCommand createCustomerCommand = new CreateCustomerCommand("Ceren", "Test", "test-customer-no");
            ActionResult result = await _controller.CreateCustomerAsync(createCustomerCommand);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetCustomerAsync(Guid guid)
        {
            Guid customerGuid = Guid.Empty;
            var customer = await _controller.GetCustomerAsync(customerGuid);

            Assert.IsNotNull(customer);
        }
    }
}
