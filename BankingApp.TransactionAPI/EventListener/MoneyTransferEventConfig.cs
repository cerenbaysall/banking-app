using BankingApp.TransactionAPI.Data;
using Microsoft.Extensions.Logging;

namespace BankingApp.TransactionAPI.EventListener
{
    public class MoneyTransferEventConfig
    {
        public TransactionDbContext dContext { get; set; }

        public ILogger<MoneyTransferEventListener> logger { get; set; }
    }
}