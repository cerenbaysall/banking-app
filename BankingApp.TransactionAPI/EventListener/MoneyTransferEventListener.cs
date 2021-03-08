using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using BankingApp.TransactionAPI.Data.Repositories;
using BankingApp.TransactionAPI.Domain.Models;

namespace BankingApp.TransactionAPI.EventListener
{
    public class MoneyTransferEventListener : BackgroundService
    {  
        private MoneyTransferEventConfig _contextConfig;

        public MoneyTransferEventListener(MoneyTransferEventConfig contextConfig)
        {
            _contextConfig = contextConfig;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();
            var consumerConfig = new ConsumerConfig();

            consumerConfig.BootstrapServers = "localhost:9092";
            consumerConfig.GroupId = "test-consumer-group";
            consumerConfig.AutoOffsetReset = AutoOffsetReset.Latest;

            using (var c = new ConsumerBuilder<Ignore, string>(consumerConfig).Build())
            {
                c.Subscribe("money-transfer");
                CancellationTokenSource cts = new CancellationTokenSource();

                try
                {
                    while (true)
                    {
                        try
                        {
                            var cr = c.Consume(cts.Token);
                            string[] splitArr = cr.Value.ToString().Split(",");

                            if (splitArr.Length > 1)
                            {
                                string accountId = splitArr[0];
                                string transactionType = splitArr[1];

                                TransactionRepository transactionRepository = new TransactionRepository(_contextConfig.dContext);

                                transactionRepository.Add(new Transaction()
                                {
                                    AccountId = new Guid(accountId),
                                    TransactionType = transactionType,
                                    CreatedDate = DateTime.Now
                                });

                                await transactionRepository.SaveChangesAsync();
                            }
                        }
                        catch (ConsumeException e)
                        {
                            _contextConfig.logger.LogError("An error occured while listening the topic from Kafka. Detail : " + e.Message);
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    c.Close();
                }
            }
        }
    }
}