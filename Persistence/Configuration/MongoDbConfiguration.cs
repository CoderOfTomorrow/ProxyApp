using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;

namespace Persistence.Configuration
{
    public class MongoDbConfiguration
    {
        private readonly MongoDbOptions dbOptions;
        public string TableName { get; private set; }
        public MongoDbConfiguration(IOptions<MongoDbOptions> mongoDbOptions)
        {
            dbOptions = mongoDbOptions.Value ?? throw new ArgumentNullException(nameof(mongoDbOptions));
            TableName = dbOptions.DatabaseName;
        }

        public IMongoDatabase GetDatabase()
        {
            return new MongoClient(dbOptions.ConnectionString).GetDatabase(dbOptions.DatabaseName);
        }
    }
}
