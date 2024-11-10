namespace AuthAPI.Models.Dto
{
    public class RegisterationResponseDto
    {
        public UserDto? User { get; set; } = null;
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = "";
    }
}
