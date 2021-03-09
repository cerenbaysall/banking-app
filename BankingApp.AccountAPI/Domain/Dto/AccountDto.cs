using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BankingApp.AccountAPI.Domain.Dto
{
    public class AccountDto
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        
        [JsonProperty("customerId")]
        public Guid CustomerId { get; set; }

        [JsonProperty("balance")]
        public int Balance { get; set; }
    }
}
