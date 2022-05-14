using AutoMapper;
using Crud_API_Fixed.Models;
using Models;

namespace Crud_API_Fixed
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<UserModel, User>();
        }
    }
}
