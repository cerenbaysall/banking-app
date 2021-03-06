using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BankingApp.TransactionAPI.Data.IRepositories;
using BankingApp.TransactionAPI.Service.Mappers;
using BankingApp.TransactionAPI.Domain.Models;
using BankingApp.TransactionAPI.Domain.Commands;
using BankingApp.TransactionAPI.Domain.Dto;
using MediatR;

namespace BankingApp.TransactionAPI.Service.Services
{
    public class CreateTransactionHandler : IRequestHandler<CreateTransactionCommand, TransactionDto>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ITransactionMapper _transactionMapper;

        public CreateTransactionHandler(ITransactionRepository transactionRepository, ITransactionMapper transactionMapper)
        {
            _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
            _transactionMapper = transactionMapper ?? throw new ArgumentNullException(nameof(transactionMapper));
        }

        public async Task<TransactionDto> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = new Domain.Models.Transaction(request.AccountId);
            _transactionRepository.Add(transaction);

            if (await _transactionRepository.SaveChangesAsync() == 0)
            {
                throw new ApplicationException();
            }

            var transactionDto = _transactionMapper.MapTransactionDto(transaction);
            return transactionDto;
        }
    }
}