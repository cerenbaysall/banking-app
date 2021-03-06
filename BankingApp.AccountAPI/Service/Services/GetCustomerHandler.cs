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
    public class GetCustomerHandler : IRequestHandler<GetCustomerQuery, CustomerDto>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerMapper _customerMapper;

        public GetCustomerHandler(ICustomerRepository customerRepository, ICustomerMapper customerMapper)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _customerMapper = customerMapper ?? throw new ArgumentNullException(nameof(customerMapper));
        }

        public async Task<CustomerDto> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetAsync(e =>
                e.Id == request.CustomerId);

            if (customer != null)
            {
                var customerDto = _customerMapper.MapCustomerDto(customer);
                return customerDto;
            }

            return null;
        }
    }
}