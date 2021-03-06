using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApp.AccountAPI.Domain.Models
{
    public class Customer : ModelBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public Customer(string name, string surname)
        {
            Id = new Guid();
            Name = name;
            Surname = surname;
        }
        public Customer(){

        }
    }
}
