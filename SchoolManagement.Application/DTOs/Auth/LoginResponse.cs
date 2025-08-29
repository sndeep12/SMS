namespace SchoolManagement.Application.DTOs.Auth
{
    public class LoginResponse
    {
        public object user { get; set; }

        public string token { get; set; }
        public string refreshToken { get; set; }
    }
    public class LoginUser
    {
        public string role { get; set; }
        public string displayName { get; set; }
        public string email { get; set; }
    }
}
