using Domain.Common;
using MessageBus;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServerAPI
{
    public class SyncService<T> where T : Entity
    {
        private readonly MessageBusService messageBus;
        public SyncService(MessageBusService messageBus)
        {
            this.messageBus = messageBus;
            this.messageBus.Connect("localhost:6379");
        }

        public async Task Upsert(T data)
        {
            SyncMessage message = new()
            {
                JsonData = JsonSerializer.Serialize(data),
                MessageType = typeof(T).ToString(),
                Method = Methods.Upsert
            };

            await messageBus.Publish(message);
        }

        public async Task Delete(T data)
        {
            SyncMessage message = new()
            {
                JsonData = JsonSerializer.Serialize(data),
                MessageType = data.GetType().ToString(),
                Method = Methods.Delete
            };
            await messageBus.Publish(message);
        }
    }
}
