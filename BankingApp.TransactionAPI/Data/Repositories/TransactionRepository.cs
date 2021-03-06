using System;
using System.Collections.Generic;
using System.Text;
using BankingApp.TransactionAPI.Data.IRepositories;

namespace BankingApp.TransactionAPI.Data.Repositories
{
    public class TransactionRepository : Repository<Domain.Models.Transaction>, ITransactionRepository
    {
        TransactionDbContext _dbContext;
        public TransactionRepository(TransactionDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
