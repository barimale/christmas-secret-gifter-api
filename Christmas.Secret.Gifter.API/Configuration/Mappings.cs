using Albergue.Administrator.Entities;
using Albergue.Administrator.Model;
using AutoMapper;
using System.Linq;

namespace Albergue.Administrator.Mappings
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<Category, CategoryEntry>().ReverseMap();
            CreateMap<ShopItem, ShopItemEntry>().ReverseMap();
            CreateMap<Language, LanguageBaseEntry>().ReverseMap();
            CreateMap<Image, ImageEntry>().ReverseMap();
            CreateMap<CategoryTranslatableDetails, CategoryTranslatableDetailsEntry>().ReverseMap();
            CreateMap<ShopItemTranslatableDetails, ShopItemTranslatableDetailsEntry>().ReverseMap();

            CreateMap<LanguageMapEntry, LanguageMapEntry>();
            CreateMap<ShopItemTranslatableDetails, ShopItemTranslatableDetails>();
            CreateMap<ShopItemTranslatableDetailsEntry, ShopItemTranslatableDetailsEntry>();
            CreateMap<ShopItemEntry, ShopItemEntry>();
            CreateMap<Category, Category>();
            CreateMap<CategoryTranslatableDetails, CategoryTranslatableDetails>();
            CreateMap<ShopItem, ShopItem>();
            CreateMap<Image, Image>();

            //CreateMap<Category, KeyedCategory>().ForMember(p => p.Name,
            //    opt => opt.MapFrom((src, dest, destMember, context) =>
            //    {
            //        var found = src.TranslatableDetails.FirstOrDefault(prop => prop.)
            //})
        }
    }
}
