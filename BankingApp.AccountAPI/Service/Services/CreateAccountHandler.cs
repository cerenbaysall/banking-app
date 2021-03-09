using System;
using System.Threading;
using System.Threading.Tasks;
using BankingApp.AccountAPI.Data.IRepositories;
using BankingApp.AccountAPI.Service.Mappers;
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
        private readonly IMediator _mediator;
        
        public CreateAccountHandler(IAccountRepository accountRepository, ICustomerRepository customerRepository, IAccountMapper accountMapper, IMediator mediator)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _accountMapper = accountMapper ?? throw new ArgumentNullException(nameof(accountMapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<AccountDto> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetAsync(e =>
                e.CustomerNo == request.CustomerNo);

            if(customer == null)
                throw new ApplicationException("There is no customer with the Customer No :" + request.CustomerNo);

            var account = new Domain.Models.Account(customer.Id, request.InitialCredit);
            _accountRepository.Add(account);

            if (await _accountRepository.SaveChangesAsync() == 0)
            {
                throw new ApplicationException();
            }

            if(request.InitialCredit != 0)
                await _mediator.Publish(new Domain.Events.AccountCreatedEvent(account.Id, "Account is created"), cancellationToken);

            var accountDto = _accountMapper.MapAccountDto(account);
            return accountDto;
        }
    }
}