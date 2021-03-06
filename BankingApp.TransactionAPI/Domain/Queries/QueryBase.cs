using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace BankingApp.TransactionAPI.Domain.Queries
{
    public abstract class QueryBase<TResult> : IRequest<TResult> where TResult : class
    {
    }
}