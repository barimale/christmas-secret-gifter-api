using Christmas.Secret.Gifter.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Christmas.Secret.Gifter.Infrastructure.Entities
{
    public class EventEntry
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = null!;
        public int OrganizerId { get; set; }
        public EventState State { get; set; }
        public List<ParticipantEntry> Participants { get; set; } = new List<ParticipantEntry>();
    }
}
