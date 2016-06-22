using AutoMapper;
using bbom.Admin.Core.ViewModels.Post;
using bbom.Data.ContentModel;

namespace bbom.Admin.Core.Mappers
{
    public class PostProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Post, PostJson>()
                .ForMember(dest => dest.title,
                    opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.text,
                    opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.typeId,
                    opt => opt.MapFrom(src => src.TypeId))
                .ForMember(dest => dest.date,
                    opt => opt.MapFrom(src => src.Date.ToString("d")));
        }
    }
}