using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.AccountAPI.Data
{
    public class AccountDbContextFactory : DesignTimeDbContextFactory<AccountDbContext>
    {
        protected override AccountDbContext CreateNewInstance(DbContextOptions<AccountDbContext> options)
        {
            return new AccountDbContext(options);
        }
    }
}
