using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BankingApp.AccountAPI.Domain.Dto;
using Newtonsoft.Json;

namespace BankingApp.AccountAPI.Domain.Queries
{
    public class GetCustomerAccountsQuery : QueryBase<IEnumerable<AccountDto>>
    {
        public GetCustomerAccountsQuery()
        {
        }

        [JsonProperty("customerNo")]
        [Required]
        public string CustomerNo { get; set; }

        [JsonConstructor]
        public GetCustomerAccountsQuery(string customerNo)
        {
            CustomerNo = customerNo;
        }
    }
}
