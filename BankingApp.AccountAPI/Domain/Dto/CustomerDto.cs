using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BankingApp.AccountAPI.Domain.Dto
{
    public class CustomerDto
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("customerNo")]
        public string CustomerNo { get; set; }
    }
}
