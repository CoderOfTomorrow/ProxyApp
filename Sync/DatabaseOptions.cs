using System.Collections.Generic;

namespace Sync
{
    public class DatabaseOptions
    {
        public string ConnectionString { get; set; }
        public string MasterDatabase { get; set; }
        public string SlaveDatabase { get; set; }
    }
}
