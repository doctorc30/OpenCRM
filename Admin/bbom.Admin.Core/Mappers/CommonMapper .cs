//using System;
//using AutoMapper;

//namespace bbom.Admin.Core.Mappers
//{
//    public class CommonMapper : IMapper
//    {
//        static CommonMapper()
//        {
//            //Mapper.Reset();
//            //Mapper.CreateMap<post, PostEditViewModel>()
//            //    .ForMember(dest => dest.autor,
//            //        opt => opt.MapFrom(src => src.autor))
//            //    .ForMember(dest => dest.title,
//            //        opt => opt.MapFrom(src => src.title))
//            //    .ForMember(dest => dest.short_story,
//            //        opt => opt.MapFrom(src => src.short_story))
//            //    .ForMember(dest => dest.full_story,
//            //        opt => opt.MapFrom(src => src.full_story))
//            //    .ForMember(dest => dest.image_link,
//            //        opt => opt.MapFrom(src => src.image_link))
//            //    .ForMember(dest => dest.C_date,
//            //        opt => opt.MapFrom(src => src.C_date))
//            //    .ForMember(dest => dest.category,
//            //        opt =>
//            //            opt.MapFrom(
//            //                src =>
//            //                    src.Categorys.Select(Category.Create).First()));
//        }

//        public T Map<T>(object source, Type sourceType)
//        {
//            return (T)Mapper.Map(source, sourceType, typeof(T));
//        }
//    }
//}