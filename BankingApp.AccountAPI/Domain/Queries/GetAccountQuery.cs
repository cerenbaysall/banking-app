using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BankingApp.AccountAPI.Domain.Dto;
using Newtonsoft.Json;

namespace BankingApp.AccountAPI.Domain.Queries
{
    public class GetAccountQuery : QueryBase<AccountDto>
    {
        public GetAccountQuery()
        {
        }

        [JsonProperty("id")]
        [Required]
        public Guid AccountId { get; set; }

        [JsonConstructor]
        public GetAccountQuery(Guid accountId)
        {
            AccountId = accountId;
        }
    }
}
