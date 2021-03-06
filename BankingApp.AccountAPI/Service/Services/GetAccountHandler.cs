using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BankingApp.AccountAPI.Data.IRepositories;
using BankingApp.AccountAPI.Service.Mappers;
using BankingApp.AccountAPI.Domain.Models;
using BankingApp.AccountAPI.Domain.Queries;
using BankingApp.AccountAPI.Domain.Dto;
using MediatR;

namespace BankingApp.AccountAPI.Service.Services
{
    public class GetAccountHandler : IRequestHandler<GetAccountQuery, AccountDto>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountMapper _accountMapper;

        public GetAccountHandler(IAccountRepository accountRepository, IAccountMapper accountMapper)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _accountMapper = accountMapper ?? throw new ArgumentNullException(nameof(accountMapper));
        }

        public async Task<AccountDto> Handle(GetAccountQuery request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetAsync(e =>
                e.Id == request.AccountId);

            if (account != null)
            {
                var accountDto = _accountMapper.MapAccountDto(account);
                return accountDto;
            }

            return null;
        }
    }
}