using AuthAPI.Data;
using AuthAPI.Mapper;
using AuthAPI.Models;
using AuthAPI.Models.Dto;
using AuthAPI.Service.IService;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(AppDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<RegisterationResponseDto> AssignRole(string email, string role)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(role).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, role);
                return new RegisterationResponseDto() { User = user.toUserDtoFromApplicationUser(), IsSuccess = true, Message = "Progress done successfully!!" };
            }
            return new RegisterationResponseDto() { User = null, IsSuccess = false, Message = "There is no use with this email or there is a error!!" };
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            ApplicationUser? user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToUpper() == loginRequestDto.UserName.ToUpper());

            if (user == null)
            {
                return new LoginResponseDto() { User = null, Token = "", Message = "There is no user with this username!!" };
            }

            bool isValidPassword = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (isValidPassword == false)
            {
                return new LoginResponseDto() { User = null, Token = "", Message = "Wrong password!!" };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);

            return new LoginResponseDto() { User = user.toUserDtoFromApplicationUser(), Token = token };
        }

        public async Task<RegisterationResponseDto> Register(RegisterationRequestDto registerationRequestDto)
        {
            ApplicationUser user = registerationRequestDto.ToApplicationUserFromRegisterationRequestDto();

            try
            {
                var result = await _userManager.CreateAsync(user, registerationRequestDto.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _db.ApplicationUsers.First(u => u.UserName == registerationRequestDto.Email);

                    return new RegisterationResponseDto() { User = user.toUserDtoFromApplicationUser(), IsSuccess = true, Message = "Progress was successful!" };
                }
                else
                {
                    return new RegisterationResponseDto() { User = null, IsSuccess = false, Message = result.Errors.FirstOrDefault().Description };
                }
            }
            catch (Exception ex)
            {
                return new RegisterationResponseDto() { User = null, IsSuccess = false, Message = ex.Message };
            }
        }
    }
}
