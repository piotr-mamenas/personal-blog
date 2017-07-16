using AutoMapper;
using PersonalBlog.Web.Core.Domain;
using PersonalBlog.Web.ViewModels;

namespace PersonalBlog.Web
{
    public static class MapProfile
    {
        public static void Map()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<LoginViewModel, User>().ReverseMap();
                cfg.CreateMap<PostViewModel, Post>().ReverseMap();
                cfg.CreateMap<ProfileViewModel, Author>().ReverseMap();
            });
        }
    }
}