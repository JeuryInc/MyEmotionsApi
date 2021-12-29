using AutoMapper;
using MyEmotionsApi.API.ViewModels;
using MyEmotionsApi.Entities;

namespace MyEmotionsApi.ViewModels.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Emotion, EmotionViewModel>()
                .ForMember(s => s.OwnerUsername, map => map.MapFrom(s => s.Owner.Username));
        }
    }
}