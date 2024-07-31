using TypeGen.Core.TypeAnnotations;

namespace Christmas.Secret.Gifter.Domain
{
    [ExportTsInterface]
    public class GiftEvent
    {
        public string Id { get; set; } = null!;
        public int OrganizerId { get; set; }
        public EventState State { get; set; }
        public IEnumerable<Participant> Participants { get; set; } = new List<Participant>();
    }
}