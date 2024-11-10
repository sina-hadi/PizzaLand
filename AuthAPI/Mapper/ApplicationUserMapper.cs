using AuthAPI.Models;
using AuthAPI.Models.Dto;

namespace AuthAPI.Mapper
{
    public static class ApplicationUserMapper
    {
        public static ApplicationUser ToApplicationUserFromRegisterationRequestDto(this RegisterationRequestDto registerationRequestDto)
        {
            return new ApplicationUser
            {
                UserName = registerationRequestDto.Email,
                Email = registerationRequestDto.Email,
                NormalizedEmail = registerationRequestDto.Email.ToUpper(),
                Name = registerationRequestDto.Name,
                PhoneNumber = registerationRequestDto.PhoneNumber
            };
        }
    }
}
