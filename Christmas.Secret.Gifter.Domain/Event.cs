namespace Christmas.Secret.Gifter.Domain
{
    public class Event
    {
        public string Id { get; set; }
        public int OrginizerId { get; set; }
        public EventState State { get; set; }
    }
}