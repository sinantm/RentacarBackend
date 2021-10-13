using AutoMapper;
using Rentacar.Authentication;
using Rentacar.DTO;

namespace Rentacar.MapperProfiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<AppUser, UserDTO>();
            CreateMap<UserDTO, AppUser>();
            CreateMap<AppUser, AppUser>();
        }
    }
}