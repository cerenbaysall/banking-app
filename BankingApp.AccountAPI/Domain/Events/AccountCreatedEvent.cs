using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace BankingApp.AccountAPI.Domain.Events
{
    public class AccountCreatedEvent : INotification
    {
        public Guid AccountId { get; }

        public string Status { get; }

        public AccountCreatedEvent(Guid accountId, string status)
        {
            AccountId = accountId;
            Status = status;
        }
    }
}
