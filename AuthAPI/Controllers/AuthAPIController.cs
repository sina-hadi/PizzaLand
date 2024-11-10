using AuthAPI.Models.Dto;
using AuthAPI.Service.IService;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IAuthService _authService;
        protected ResponseDto _response;
        public AuthAPIController(IJwtTokenGenerator jwtTokenGenerator, IAuthService authService)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _authService = authService;
            _response = new ResponseDto();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            LoginResponseDto loginResponse = await _authService.Login(loginRequest);

            if (loginResponse.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = loginResponse.Message;
                return BadRequest(_response);
            }

            return Ok(loginResponse);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterationRequestDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            RegisterationResponseDto registerationResponse = await _authService.Register(requestDto);

            if (registerationResponse.IsSuccess == false)
            {
                _response.IsSuccess = false;
                _response.Message = registerationResponse.Message;
                return BadRequest(_response);
            }

            _response.Result = registerationResponse.User;
            _response.Message = "Progress was successful!";

            return Ok(_response);
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var role = String.Empty;
            switch (model.RoleName.ToUpper())
            {
                case "BOSS":
                    role = "BOSS";
                    break;
                case "ADMIN":
                    role = "ADMIN";
                    break;
                case "CUSTOMER":
                    role = "CUSTOMER";
                    break;
                default:
                    return BadRequest("Role is not accurate! 1.BOSS  2.ADMIN  3.CUSTOMER");
            }

            RegisterationResponseDto assignRoleSuccessful = await _authService.AssignRole(model.Email, role);

            if (!assignRoleSuccessful.IsSuccess)
            {
                return BadRequest(assignRoleSuccessful);
            }

            return Ok(assignRoleSuccessful);
        }
    }
}
