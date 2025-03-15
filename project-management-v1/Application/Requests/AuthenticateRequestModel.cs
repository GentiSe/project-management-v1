namespace project_management_v1.Application.Requests
{
    public class AuthenticateRequestModel
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public required string Password { get; set; }
    }
}
