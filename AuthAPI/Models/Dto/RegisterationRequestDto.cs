﻿namespace AuthAPI.Models.Dto
{
    public class RegisterationRequestDto
    {
        public required string Email { get; set; }
        public required string Name { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Password { get; set; }
    }
}
