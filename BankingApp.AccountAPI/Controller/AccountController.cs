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
using BankingApp.AccountAPI.Service.Services;
using BankingApp.AccountAPI.Domain.Commands;
using BankingApp.AccountAPI.Domain.Queries;
using BankingApp.AccountAPI.Domain.Dto;

namespace BankingApp.AccountAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ApiControllerBase
    {
        private readonly IConfiguration _configuration;

        public AccountController(IMediator mediator, IConfiguration configuration) : base(mediator)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("createaccount")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> CreateAccountAsync([FromBody] CreateAccountCommand command)
        {
            return Ok(await CommandAsync(command));
        }
        
        [HttpGet]
        [Route("getaccount/{guid:Guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<AccountDto> GetAccountAsync(Guid guid)
        {
            return await CommandAsync(new GetAccountQuery(guid));
        }

        [HttpPost]
        [Route("getcustomeraccounts")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IEnumerable<AccountDto>> GetCustomerAccountsAsync([FromBody] GetCustomerAccountsQuery query)
        {
            return await CommandAsync(query);
        }
    }
}
