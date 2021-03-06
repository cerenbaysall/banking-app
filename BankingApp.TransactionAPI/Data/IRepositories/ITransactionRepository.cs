using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApp.TransactionAPI.Data.IRepositories
{
    public interface ITransactionRepository : IRepository<Domain.Models.Transaction>
    {
    }
}
