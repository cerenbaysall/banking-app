using System;
using System.Collections.Generic;
using System.Text;
using BankingApp.AccountAPI.Data.IRepositories;

namespace BankingApp.AccountAPI.Data.Repositories
{
    public class CustomerRepository : Repository<Domain.Models.Customer>, ICustomerRepository
    {
        AccountDbContext _dbContext;
        public CustomerRepository(AccountDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
