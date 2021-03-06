using MediatR;

namespace BankingApp.TransactionAPI.Domain.Commands
{
    public class CommandBase<T> : IRequest<T> where T : class
    {
    }
}