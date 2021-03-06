using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApp.TransactionAPI.Domain.Models
{
    public class Transaction : ModelBase
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public string TransactionType { get; set; } 

        public Transaction(Guid accountId, string transactionType)
        {
            Id = new Guid();
            AccountId = accountId;
            TransactionType = transactionType;
        }

        public Transaction(){
            
        }
    }
}
