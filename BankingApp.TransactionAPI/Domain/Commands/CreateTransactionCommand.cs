using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using BankingApp.TransactionAPI.Domain.Dto;

namespace BankingApp.TransactionAPI.Domain.Commands
{
    public class CreateTransactionCommand : CommandBase<TransactionDto>
    {
        public CreateTransactionCommand()
        {
        }

        [JsonProperty("accountId")]
        [Required]
        public Guid AccountId { get; }

        [JsonProperty("transactionType")]
        [Required]
        public string TransactionType { get; }
        
        [JsonConstructor]
        public CreateTransactionCommand(Guid accountId, string transactionType)
        {
            AccountId = accountId;
            TransactionType = transactionType;
        }
    }
}
