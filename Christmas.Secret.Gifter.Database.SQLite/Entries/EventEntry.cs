using Christmas.Secret.Gifter.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Christmas.Secret.Gifter.Database.SQLite.Entries
{
    public class EventEntry
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string EventId { get; set; }
        public int OrganizerId { get; set; }
        public EventState State { get; set; }
        public List<ParticipantEntry> Participants { get; set; } = new List<ParticipantEntry>();
    }
}
