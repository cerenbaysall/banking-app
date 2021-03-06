using MediatR;

namespace BankingApp.AccountAPI.Domain.Commands
{
    public class CommandBase<T> : IRequest<T> where T : class
    {
    }
}