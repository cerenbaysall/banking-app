using System;
using System.Collections.Generic;
using System.Text;
using BankingApp.TransactionAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.TransactionAPI.UnitTest.Data
{
    public static class DbContextFixture
    {
        public static TransactionDbContext GetDbContext()
        {
            DbContextOptions<TransactionDbContext> options = new DbContextOptions<TransactionDbContext>();
            DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder((DbContextOptions)options);
            dbContextOptionsBuilder.UseInMemoryDatabase("transactiondb");

            string[] args = { };
            return new TransactionDbContext((DbContextOptions<TransactionDbContext>)(dbContextOptionsBuilder.Options)); ;
        }
    }
}
