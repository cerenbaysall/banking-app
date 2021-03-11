using BankingApp.AccountAPI.Data;
using BankingApp.AccountAPI.Data.Repositories;
using BankingApp.AccountAPI.Domain.Models;

namespace BankingApp.AccountAPI
{
    public static class DataCreate
    {
        public static async void CreateData(AccountDbContext context)
        {
            CustomerRepository customerRepository = new CustomerRepository(context);

            customerRepository.Add(new Customer()
            {
                Name = "John",
                Surname = "Smith",
                CustomerNo = "Customer-7f7156354028"
            });

            customerRepository.Add(new Customer()
            {
                Name = "Ada",
                Surname = "Smith",
                CustomerNo = "Customer-41ee5d1ac2a8"
            });

            await customerRepository.SaveChangesAsync();
        }
    }
}
