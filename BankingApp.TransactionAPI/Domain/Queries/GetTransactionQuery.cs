using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BankingApp.TransactionAPI.Domain.Dto;
using Newtonsoft.Json;

namespace BankingApp.TransactionAPI.Domain.Queries
{
    public class GetTransactionQuery : QueryBase<TransactionDto>
    {
        public GetTransactionQuery()
        {
        }

        [JsonProperty("id")]
        [Required]
        public Guid TransactionId { get; set; }

        [JsonConstructor]
        public GetTransactionQuery(Guid transactionId)
        {
            TransactionId = transactionId;
        }
    }
}
