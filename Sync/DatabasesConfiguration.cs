using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Sync
{
    public class DatabasesConfiguration
    {
        private readonly DatabaseOptions database;
        public DatabasesConfiguration(IOptions<DatabaseOptions> config)
        {
            database = config.Value;
        }

        public IMongoDatabase GetMasterInstances()
        {
            return new MongoClient(database.ConnectionString).GetDatabase(database.MasterDatabase);
        }

        public IMongoDatabase GetSlaveInstances()
        {
            return new MongoClient(database.ConnectionString).GetDatabase(database.SlaveDatabase);
        }
    }
}
