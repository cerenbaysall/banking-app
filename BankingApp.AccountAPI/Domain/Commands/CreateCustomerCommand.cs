using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using BankingApp.AccountAPI.Domain.Dto;

namespace BankingApp.AccountAPI.Domain.Commands
{
    public class CreateCustomerCommand : CommandBase<CustomerDto>
    {
        public CreateCustomerCommand()
        {
        }

        [JsonProperty("name")]
        [Required]
        public string Name { get; }

        [JsonProperty("surname")]
        [Required]
        public string Surname { get; }

        [JsonProperty("customerNo")]
        [Required]
        public string CustomerNo { get; }
        
        [JsonConstructor]
        public CreateCustomerCommand(string name, string surname, string customerNo)
        {
            Name = name;
            Surname = surname;
            CustomerNo = customerNo;
        }
    }
}
