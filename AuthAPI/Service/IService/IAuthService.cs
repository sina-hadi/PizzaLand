using AuthAPI.Models.Dto;

namespace AuthAPI.Service.IService
{
    public interface IAuthService
    {
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<RegisterationResponseDto> Register(RegisterationRequestDto registerationRequestDto);
        Task<RegisterationResponseDto> AssignRole(string email, string role);
    }
}
