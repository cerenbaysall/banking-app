﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingApp.TransactionAPI.Domain.Models;
using BankingApp.TransactionAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.TransactionAPI.Data
{
    public class TransactionDbContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }

        public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options)
        {

        }
    }
}
