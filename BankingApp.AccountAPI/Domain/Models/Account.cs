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
        public int InitialCredit { get; set; }

        public Account(Guid customerId, int initialCredit)
        {
            Id = new Guid();
            CustomerId = customerId;
            InitialCredit = initialCredit;
        }

        public Account(){
            
        }
    }
}
