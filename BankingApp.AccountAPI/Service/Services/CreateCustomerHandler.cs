using System;
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
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, CustomerDto>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerMapper _customerMapper;

        public CreateCustomerHandler(ICustomerRepository customerRepository, ICustomerMapper customerMapper)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _customerMapper = customerMapper ?? throw new ArgumentNullException(nameof(customerMapper));
        }

        public async Task<CustomerDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer(request.Name, request.Surname);
            _customerRepository.Add(customer);

            if (await _customerRepository.SaveChangesAsync() == 0)
            {
                throw new ApplicationException();
            }

            var customerDto = _customerMapper.MapCustomerDto(customer);
            return customerDto;
        }
    }
}