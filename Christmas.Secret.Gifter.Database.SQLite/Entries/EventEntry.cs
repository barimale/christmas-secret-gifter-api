using Christmas.Secret.Gifter.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Christmas.Secret.Gifter.Database.SQLite.Entities
{
    public class EventEntry
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public int OrganizerId { get; set; }
        public EventState Status { get; set; }
        public List<Participant> Participants { get; set; }
    }
}
