using BankingApp.AccountAPI.Data;
using BankingApp.AccountAPI.Data.Repositories;
using BankingApp.AccountAPI.Domain.Models;
using MediatR;

namespace BankingApp.AccountAPI
{
    public static class DataCreate
    {
        public static async void CreateData(AccountDbContext context, IMediator mediator)
        {
            CustomerRepository customerRepository = new CustomerRepository(context);
            AccountRepository accountRepository = new AccountRepository(context);

            customerRepository.Add(new Customer()
            {
                Name = "John",
                Surname = "Smith"
            });

            customerRepository.Add(new Customer()
            {
                Name = "Ada",
                Surname = "Smith"
            });

            await customerRepository.SaveChangesAsync();

            var customer1 = await customerRepository.GetAsync(x => x.Name == "John");
            var customer2 = await customerRepository.GetAsync(x => x.Surname == "Ada");

            var account1 = new Domain.Models.Account()
            {
                CustomerId = customer1.Id
            };

            accountRepository.Add(account1);
            await accountRepository.SaveChangesAsync();
            
            var account2 = new Domain.Models.Account()
            {
                CustomerId = customer2.Id
            };

            accountRepository.Add(account2);
            await accountRepository.SaveChangesAsync();
        }
    }
}
