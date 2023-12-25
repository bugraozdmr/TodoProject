using AutoMapper;
using TodoProject.DtoLayer.LoginDto;
using TodoProject.DtoLayer.RegisterDto;
using TodoProject.EntityLayer.Concrete;

namespace TodoProject.WebApi.Mapping
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<LoginUserDto, AppUser>().ReverseMap();
            CreateMap<CreateNewUserDto, AppUser>().ReverseMap();
        }
    }
}
