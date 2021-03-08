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
    public class GetAccountTransactionsHandler : IRequestHandler<GetAccountTransactionsQuery, IEnumerable<TransactionDto>>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ITransactionMapper _transactionMapper;

        public GetAccountTransactionsHandler(ITransactionRepository transactionRepository, ITransactionMapper transactionMapper)
        {
            _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
            _transactionMapper = transactionMapper ?? throw new ArgumentNullException(nameof(transactionMapper));
        }

        public async Task<IEnumerable<TransactionDto>> Handle(GetAccountTransactionsQuery request, CancellationToken cancellationToken)
        {
            var accountTransactions = await _transactionRepository.GetListAsync(e =>
            e.AccountId == request.AccountId);

            var accountTransactionsDto = new List<TransactionDto>();
            foreach (var accountTransaction in accountTransactions)
            {
                var transactionDto = _transactionMapper.MapTransactionDto(accountTransaction);
                accountTransactionsDto.Add(transactionDto);
            }

            return accountTransactionsDto;
        }
    }
}