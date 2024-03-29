using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using BankingApp.AccountAPI.Domain.Dto;

namespace BankingApp.AccountAPI.Domain.Commands
{
    public class CreateAccountCommand : CommandBase<AccountDto>
    {
        public CreateAccountCommand()
        {
        }

        [JsonProperty("customerNo")]
        [Required]
        public string CustomerNo { get; }

        [JsonProperty("initialCredit")]
        [Required]
        public int InitialCredit { get; }

        [JsonProperty("iban")]
        [Required]
        public string Iban { get; }
        
        [JsonConstructor]
        public CreateAccountCommand(string customerNo, int initialCredit, string iban)
        {
            CustomerNo = customerNo;
            InitialCredit = initialCredit;
            Iban = iban;
        }
    }
}
