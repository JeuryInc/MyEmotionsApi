using AutoMapper;
using MyEmotions.Core.Entities;
using MyEmotionsApi.API.ViewModels;

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