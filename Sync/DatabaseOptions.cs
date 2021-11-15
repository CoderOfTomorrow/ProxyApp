using System.Collections.Generic;

namespace Sync
{
    public class DatabaseOptions
    {
        public string ConnectionString { get; set; }
        public List<string> SlaveDatabases { get; set; }
    }
}
