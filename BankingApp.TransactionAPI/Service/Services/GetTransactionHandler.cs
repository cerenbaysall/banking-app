using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BankingApp.TransactionAPI.Data.IRepositories;
using BankingApp.TransactionAPI.Service.Mappers;
using BankingApp.TransactionAPI.Domain.Models;
using BankingApp.TransactionAPI.Domain.Queries;
using BankingApp.TransactionAPI.Domain.Dto;
using MediatR;

namespace BankingApp.TransactionAPI.Service.Services
{
    public class GetTransactionHandler : IRequestHandler<GetTransactionQuery, TransactionDto>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ITransactionMapper _transactionMapper;

        public GetTransactionHandler(ITransactionRepository transactionRepository, ITransactionMapper transactionMapper)
        {
            _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
            _transactionMapper = transactionMapper ?? throw new ArgumentNullException(nameof(transactionMapper));
        }

        public async Task<TransactionDto> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
        {
            var transaction = await _transactionRepository.GetAsync(e =>
                e.Id == request.TransactionId);

            if (transaction != null)
            {
                var transactionDto = _transactionMapper.MapTransactionDto(transaction);
                return transactionDto;
            }

            return null;
        }
    }
}