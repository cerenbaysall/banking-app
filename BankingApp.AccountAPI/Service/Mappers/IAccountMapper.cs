using System;
using System.Collections.Generic;
using System.Text;
using BankingApp.AccountAPI.Domain.Dto;

namespace BankingApp.AccountAPI.Service.Mappers
{
    public interface IAccountMapper
    {
        AccountDto MapAccountDto(Domain.Models.Account account);
    }
}
