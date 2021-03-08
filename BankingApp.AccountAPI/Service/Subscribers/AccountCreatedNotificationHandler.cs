using System;
using System.Threading;
using System.Threading.Tasks;
using BankingApp.AccountAPI.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using Confluent.Kafka;

namespace BankingApp.AccountAPI.Service.Subscribers
{
    public class AccountCreatedNotificationHandler : INotificationHandler<AccountCreatedEvent>
    {
        private readonly ILogger _logger;

        public AccountCreatedNotificationHandler(ILogger<AccountCreatedNotificationHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(AccountCreatedEvent notification, CancellationToken cancellationToken)
        {
            try{
                var kafkaUrl = Environment.GetEnvironmentVariable ("KAFKA_URL");
                var config = new ProducerConfig
                    {
                        BootstrapServers = "localhost:9092",
                        ClientId = Environment.GetEnvironmentVariable ("KAFKA_URL"),
                    };

                using (var producer = new ProducerBuilder<Null, string>(config).Build())
                {
                    var text = notification.AccountId + "," + notification.Status;
                    await producer.ProduceAsync("money-transfer", new Message<Null, string> { Value=text });
                }
            } catch(Exception ex){
                throw new ApplicationException(ex.Message);
            }
        }
    }
}
