using AuthAPI.Models;
using AuthAPI.Models.Dto;

namespace AuthAPI.Mapper
{
    public static class UserDtoMapper
    {
        public static UserDto toUserDtoFromApplicationUser(this ApplicationUser applicationUser)
        {
            return new UserDto
            {
                Email = applicationUser.Email,
                ID = applicationUser.Id,
                Name = applicationUser.Name,
                PhoneNumber = applicationUser.PhoneNumber
            };
        }
    }
}
