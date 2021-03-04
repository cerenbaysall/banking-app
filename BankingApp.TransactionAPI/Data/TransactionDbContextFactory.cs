using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.TransactionAPI.Data
{
    public class TransactionDbContextFactory : DesignTimeDbContextFactory<TransactionDbContext>
    {
        protected override TransactionDbContext CreateNewInstance(DbContextOptions<TransactionDbContext> options)
        {
            return new TransactionDbContext(options);
        }
    }
}
