using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using BankingApp.AccountAPI.Domain.Dto;

namespace BankingApp.AccountAPI.Domain.Commands
{
    public class CreateAccountCommand : CommandBase<AccountDto>
    {
        public CreateAccountCommand()
        {
        }

        [JsonProperty("customerId")]
        [Required]
        public Guid CustomerId { get; }

        [JsonProperty("initialCredit")]
        [Required]
        public int InitialCredit { get; }
        
        [JsonConstructor]
        public CreateAccountCommand(Guid customerId, int initialCredit)
        {
            CustomerId = customerId;
            InitialCredit = initialCredit;
        }
    }
}
