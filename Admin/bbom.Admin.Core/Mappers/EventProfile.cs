using System;
using System.Linq;
using AutoMapper;
using bbom.Admin.Core.ViewModels.Events;
using bbom.Data.ContentModel;

namespace bbom.Admin.Core.Mappers
{
    public class EventProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Event, EventJson>()
                .ForMember(dest => dest.title,
                    opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.name,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.url,
                    opt => opt.MapFrom(src => src.Url))
                .ForMember(dest => dest.start,
                    opt => opt.MapFrom(src => src.StartDate.ToString("yyyy-MM-dd HH:mm")))
                .ForMember(dest => dest.end,
                    opt => opt.MapFrom(src => src.EndDate.ToString("yyyy-MM-dd HH:mm")))
                .ForMember(dest => dest.typeId,
                    opt => opt.MapFrom(src => src.EventType.Id.ToString()))
                .ForMember(dest => dest.userName,
                    opt => opt.MapFrom(src => src.AspNetUser.GetFio()))
                .ForMember(dest => dest.description,
                    opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.status,
                    opt => opt.MapFrom(src => src.Stats == null ? "" : src.Stats.Value.ToString()))
                .ForMember(dest => dest.isShowDate,
                    opt => opt.MapFrom(src => src.StartDate < DateTime.Now.AddHours(24)))
                .ForMember(dest => dest.isRun,
                    opt => opt.MapFrom(src => src.StartDate < DateTime.Now))
                .ForMember(dest => dest.icon,
                    opt =>
                        opt.MapFrom(
                            src => src.Spikers.FirstOrDefault() == null
                                ? GlobalConstants.ImageEventsPath + src.Icon
                                : GlobalConstants.ImageUserProfilePath + src.Spikers.FirstOrDefault().Id))
                .ForMember(dest => dest.spiker,
                    opt =>
                        opt.MapFrom(
                            src => src.Spikers.FirstOrDefault() == null
                                ? src.Spiker
                                : src.Spikers.FirstOrDefault().GetIO()))
                .ForMember(dest => dest.spikerId,
                    opt => opt.MapFrom(src => src.Spikers.FirstOrDefault() == null
                        ? src.Spiker
                        : src.Spikers.FirstOrDefault().Id));

            CreateMap<Event, EventLigthJson>()
                .ForMember(dest => dest.title,
                    opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.name,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.url,
                    opt => opt.MapFrom(src => src.Url))
                .ForMember(dest => dest.start,
                    opt => opt.MapFrom(src => src.StartDate.ToString("yyyy-MM-dd HH:mm")))
                .ForMember(dest => dest.end,
                    opt => opt.MapFrom(src => src.EndDate.ToString("yyyy-MM-dd HH:mm")));

            CreateMap<Event, EventListJson>()
                .ForMember(dest => dest.title,
                    opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.name,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.url,
                    opt => opt.MapFrom(src => src.Url))
                .ForMember(dest => dest.description,
                    opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.start,
                    opt => opt.MapFrom(src => src.StartDate.ToString("yyyy-MM-dd HH:mm")))
                .ForMember(dest => dest.icon,
                    opt => opt.MapFrom(src => src.Icon))
                .ForMember(dest => dest.spiker,
                    opt => opt.MapFrom(src => src.Spiker))
                .ForMember(dest => dest.isShowDate,
                    opt => opt.MapFrom(src => src.StartDate < DateTime.Now.AddHours(24) && src.StartDate > DateTime.Now))
                .ForMember(dest => dest.buttonColor,
                    opt => opt.MapFrom(src => GetButtonColor(src.StartDate)));
        }

        private string GetButtonColor(DateTime startDate)
        {
            if (startDate.AddMinutes(10) > DateTime.Now && startDate.AddMinutes(-10) < DateTime.Now)
            {
                return "red";
            }
            return "#949494";
        }
    }
}