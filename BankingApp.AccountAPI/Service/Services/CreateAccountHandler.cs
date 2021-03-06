using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BankingApp.AccountAPI.Data.IRepositories;
using BankingApp.AccountAPI.Service.Mappers;
using BankingApp.AccountAPI.Domain.Models;
using BankingApp.AccountAPI.Domain.Commands;
using BankingApp.AccountAPI.Domain.Dto;
using MediatR;

namespace BankingApp.AccountAPI.Service.Services
{
    public class CreateAccountHandler : IRequestHandler<CreateAccountCommand, AccountDto>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IAccountMapper _accountMapper;

        public CreateAccountHandler(IAccountRepository accountRepository, ICustomerRepository customerRepository, IAccountMapper accountMapper)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _accountMapper = accountMapper ?? throw new ArgumentNullException(nameof(accountMapper));
        }

        public async Task<AccountDto> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetAsync(e =>
                e.Id == request.CustomerId);

            if(customer == null)
                throw new ApplicationException("There is no customer with the customerId :" + request.CustomerId);

            var account = new Domain.Models.Account(request.CustomerId, request.InitialCredit);
            _accountRepository.Add(account);

            if (await _accountRepository.SaveChangesAsync() == 0)
            {
                throw new ApplicationException();
            }

            var accountDto = _accountMapper.MapAccountDto(account);
            return accountDto;
        }
    }
}