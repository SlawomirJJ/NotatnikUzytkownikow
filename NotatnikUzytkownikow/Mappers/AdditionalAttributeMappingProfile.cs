using NotatnikUzytkownikow.Entities;
using NotatnikUzytkownikow.Requests;
using AutoMapper;

namespace NotatnikUzytkownikow.Mappers
{
    public class AdditionalAttributeMappingProfile : Profile
    {
        public AdditionalAttributeMappingProfile()
        {
            CreateMap<AdditionalAttribute, AdditionalAttributeRequest>()
                .ForMember(m => m.AttributeName, a => a.MapFrom(x => x.AttributeName))
                .ForMember(m => m.Value, a => a.MapFrom(x => x.Value));
        }
        
        

    }
}
