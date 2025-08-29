namespace SchoolManagement.Application.DTOs.Auth
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public string? access_token { get; set; }
    }
    public class RefreshTokenRequest
    {

        public string RefreshToken { get; set; }
    }
}
