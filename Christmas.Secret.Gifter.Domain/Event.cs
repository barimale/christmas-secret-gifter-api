using TypeGen.Core.TypeAnnotations;

namespace Christmas.Secret.Gifter.Domain
{
    [ExportTsInterface]
    public class Event
    {
        public string EventId { get; set; } = null!;
        public int OrginizerId { get; set; }
        public EventState State { get; set; }
        public IEnumerable<Participant> Participants { get; set; } = new List<Participant>();
    }
}