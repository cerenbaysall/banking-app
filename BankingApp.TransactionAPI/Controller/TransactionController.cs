using System;
using System.Collections.Generic;
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
using Microsoft.AspNetCore.Cors;
using BankingApp.TransactionAPI;
using BankingApp.TransactionAPI.Service.Services;
using BankingApp.TransactionAPI.Domain.Commands;
using BankingApp.TransactionAPI.Domain.Queries;
using BankingApp.TransactionAPI.Domain.Dto;

namespace BankingApp.TransactionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ApiControllerBase
    {
        private readonly IConfiguration _configuration;
        public TransactionController(IMediator mediator, IConfiguration configuration) : base(mediator)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("createtransaction")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> CreateTransactionAsync([FromBody] CreateTransactionCommand command)
        {
            return Ok(await CommandAsync(command));
        }
        
        [HttpGet]
        [Route("gettransaction/{guid:Guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<TransactionDto> GetTransactionAsync(Guid guid)
        {
            return await CommandAsync(new GetTransactionQuery(guid));
        }
    }
}
