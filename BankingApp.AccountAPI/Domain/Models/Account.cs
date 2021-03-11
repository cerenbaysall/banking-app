using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApp.AccountAPI.Domain.Models
{
    public class Account : ModelBase
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public int Balance { get; set; }

        public string Iban { get; set; }

        public Account(Guid customerId, int balance, string iban)
        {
            Id = new Guid();
            CustomerId = customerId;
            Balance = balance;
            Iban = iban;
        }

        public Account(){
            
        }
    }
}
