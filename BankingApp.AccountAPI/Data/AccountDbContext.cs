using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingApp.AccountAPI.Domain.Models;
using BankingApp.AccountAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.AccountAPI.Data
{
    public class AccountDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options)
        {
        }
    }
}
