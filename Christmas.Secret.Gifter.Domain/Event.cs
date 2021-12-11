using TypeGen.Core.TypeAnnotations;

namespace Christmas.Secret.Gifter.Domain
{
    [ExportTsInterface]
    public class Event
    {
        public string Id { get; set; }
        public int OrginizerId { get; set; }
        public EventState State { get; set; }
    }
}