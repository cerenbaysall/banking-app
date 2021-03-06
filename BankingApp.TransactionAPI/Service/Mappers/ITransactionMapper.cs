using System;
using System.Collections.Generic;
using System.Text;
using BankingApp.TransactionAPI.Domain.Dto;

namespace BankingApp.TransactionAPI.Service.Mappers
{
    public interface ITransactionMapper
    {
        TransactionDto MapTransactionDto(Domain.Models.Transaction transaction);
    }
}
