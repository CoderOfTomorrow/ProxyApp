namespace MessageBus
{
    public class SyncMessage
    {
        public string JsonData { get; set; }
        public string MessageType { get; set; }
        public Methods Method { get; set; }
    }
}
