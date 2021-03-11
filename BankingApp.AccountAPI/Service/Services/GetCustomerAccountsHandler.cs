using System;
using System.Threading;
using System.Threading.Tasks;
using BankingApp.AccountAPI.Data.IRepositories;
using BankingApp.AccountAPI.Service.Mappers;
using BankingApp.AccountAPI.Domain.Queries;
using BankingApp.AccountAPI.Domain.Dto;
using MediatR;
using System.Collections.Generic;

namespace BankingApp.AccountAPI.Service.Services
{
    public class GetCustomerAccountsHandler : IRequestHandler<GetCustomerAccountsQuery, IEnumerable<AccountDto>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountMapper _accountMapper;
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerAccountsHandler(IAccountRepository accountRepository, ICustomerRepository customerRepository ,IAccountMapper accountMapper)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _accountMapper = accountMapper ?? throw new ArgumentNullException(nameof(accountMapper));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        public async Task<IEnumerable<AccountDto>> Handle(GetCustomerAccountsQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetAsync(e =>
                e.CustomerNo == request.CustomerNo);

            if(customer == null)
                throw new ApplicationException("There is no customer with the Customer No :" + request.CustomerNo);

            var accounts = await _accountRepository.GetListAsync(e =>
                e.CustomerId == customer.Id);

            var customerAccountsDto = new List<AccountDto>();
            foreach (var account in accounts)
            {
                var accountDto = _accountMapper.MapAccountDto(account);
                customerAccountsDto.Add(accountDto);
            }

            return customerAccountsDto;
        }
    }
}