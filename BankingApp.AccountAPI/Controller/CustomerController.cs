using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors;
using BankingApp.AccountAPI;
using BankingApp.AccountAPI.Domain.Queries;
using BankingApp.AccountAPI.Domain.Commands;
using BankingApp.AccountAPI.Domain.Dto;

namespace BankingApp.AccountAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ApiControllerBase
    {
        private readonly IConfiguration _configuration;

        public CustomerController(IMediator mediator, IConfiguration configuration) : base(mediator)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("createcustomer")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> CreateCustomerAsync([FromBody] CreateCustomerCommand command)
        {
            return Ok(await CommandAsync(command));
        }
        
        [HttpGet]
        [Route("getcustomer/{guid:Guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<CustomerDto> GetCustomerAsync(Guid guid)
        {
            return await CommandAsync(new GetCustomerQuery(guid));
        }
    }
}
