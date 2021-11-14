using MessageBus;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sync
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMongoDatabase db;
        private readonly MessageBusService messageBus;

        public Worker(ILogger<Worker> logger, DatabasesConfiguration db, MessageBusService messageBus)
        {
            this.db = db.GetSlaveInstances();
            _logger = logger;
            this.messageBus = messageBus;
            this.messageBus.Connect("localhost:6379");
        }

        public override async Task<Task> StartAsync(CancellationToken cancellationToken)
        {
            await messageBus.Subscribe();
            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (true)
            {
                messageBus.MessageQueue.TryDequeue(out var message);
                if(message!= null)
                Console.WriteLine(message.JsonData);
            }
        }
    }
}
