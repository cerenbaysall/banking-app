using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BankingApp.TransactionAPI.Domain.Dto;
using Newtonsoft.Json;

namespace BankingApp.TransactionAPI.Domain.Queries
{
    public class GetAccountTransactionsQuery : QueryBase<IEnumerable<TransactionDto>>
    {
        public GetAccountTransactionsQuery()
        {
        }

        [JsonProperty("accountId")]
        [Required]
        public Guid AccountId { get; set; }

        [JsonConstructor]
        public GetAccountTransactionsQuery(Guid accountId)
        {
            AccountId = accountId;
        }
    }
}
