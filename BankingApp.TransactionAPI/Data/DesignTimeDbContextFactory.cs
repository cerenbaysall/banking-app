using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BankingApp.TransactionAPI.Data
{
    public abstract class DesignTimeDbContextFactory<TContext>
    : IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    {
        public TContext CreateDbContext(string[] args)
        {
            throw new NotImplementedException();
        }

        protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);
    }
}
