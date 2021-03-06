using System;
using System.Collections.Generic;
using System.Text;
using BankingApp.AccountAPI.Data.IRepositories;

namespace BankingApp.AccountAPI.Data.Repositories
{
    public class AccountRepository : Repository<Domain.Models.Account>, IAccountRepository
    {
        AccountDbContext _dbContext;
        public AccountRepository(AccountDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
