using AutoMapper;
using Christmas.Secret.Gifter.Database.SQLite.Entities;
using Christmas.Secret.Gifter.Domain;

namespace Christmas.Secret.Gifter.Database.SQLite.Configuration
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<Event, EventEntry>().ReverseMap();
        }
    }
}
