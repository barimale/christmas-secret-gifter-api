using AutoMapper;
using Christmas.Secret.Gifter.Domain;
using Christmas.Secret.Gifter.Infrastructure.Entities;

namespace Christmas.Secret.Gifter.Infrastructure.Extensions
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<GiftEvent, EventEntry>()
                .ReverseMap();

            CreateMap<Participant, ParticipantEntry>()
                .ReverseMap();

            CreateMap<EventEntry, EventEntry>();
            CreateMap<ParticipantEntry, ParticipantEntry>();
        }
    }
}
