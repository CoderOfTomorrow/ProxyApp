using Domain.Common;
using MessageBus;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Persistence.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using System.Reflection;

namespace Sync
{
    public class Worker : BackgroundService
    {
        private readonly MessageBusService messageBus;
        private readonly IRepository<Entity> repository;

        public Worker(IRepository<Entity> repository, MessageBusService messageBus)
        {
            this.messageBus = messageBus;
            this.repository = repository;
        }

        public override async Task<Task> StartAsync(CancellationToken cancellationToken)
        {
            messageBus.Connect("localhost:6379");
            await messageBus.Subscribe();
            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (true)
            {
                messageBus.MessageQueue.TryDequeue(out SyncMessage message);
                if (message != null)
                {
                    Assembly asm = typeof(Entity).Assembly;
                    Type type = asm.GetType(message.MessageType);

                    var entity = JsonSerializer.Deserialize(message.JsonData, type);

                    var et = (Entity)entity;
                    if (message.Method == Methods.Upsert)
                        await repository.UpsertEntity(et);
                    else
                        await repository.DeleteEntity(et.Id);
                }
            }
        }
    }
}
