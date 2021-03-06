using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BankingApp.AccountAPI.Domain.Dto;
using Newtonsoft.Json;

namespace BankingApp.AccountAPI.Domain.Queries
{
    public class GetCustomerQuery : QueryBase<CustomerDto>
    {
        public GetCustomerQuery()
        {
        }

        [JsonProperty("id")]
        [Required]
        public Guid CustomerId { get; set; }

        [JsonConstructor]
        public GetCustomerQuery(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}
