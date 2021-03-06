using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BankingApp.TransactionAPI.Domain.Dto
{
    public class TransactionDto
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        
        [JsonProperty("accountId")]
        public Guid AccountId { get; set; }
    }
}
