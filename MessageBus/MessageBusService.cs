using StackExchange.Redis;
using System.Collections.Concurrent;
using System.Text.Json;
using System.Threading.Tasks;

namespace MessageBus
{
    public class MessageBusService
    {
        private ConnectionMultiplexer redisClient;
        private ISubscriber messageBus;
        public ConcurrentQueue<SyncMessage> MessageQueue = new(); 
        public MessageBusService()
        {

        }

        public bool Connect(string ip)
        {
            redisClient = ConnectionMultiplexer.Connect(ip);
            messageBus = redisClient.GetSubscriber();

            if (redisClient.IsConnected)
                return true;
            else
                return false;
        }
        public async Task Subscribe()
        {
            await messageBus.SubscribeAsync("sync", (channel, message) =>
            {
                var syncMessage = JsonSerializer.Deserialize<SyncMessage>(message.ToString());
                MessageQueue.Enqueue(syncMessage);
            });
        }

        public async Task Publish(SyncMessage syncMessage)
        {
            var message = JsonSerializer.Serialize(syncMessage);
            await messageBus.PublishAsync("sync", message);
        }
    }
}
