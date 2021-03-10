using System;
using System.Collections.Generic;
using System.Text;
using BankingApp.AccountAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.AccountAPI.UnitTests.Data
{
    public static class DbContextFixture
    {
        public static AccountDbContext GetDbContext()
        {
            DbContextOptions<AccountDbContext> options = new DbContextOptions<AccountDbContext>();
            DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder((DbContextOptions)options);
            dbContextOptionsBuilder.UseInMemoryDatabase("accountdb");

            string[] args = { };
            return new AccountDbContext((DbContextOptions<AccountDbContext>)(dbContextOptionsBuilder.Options)); ;
        }
    }
}
