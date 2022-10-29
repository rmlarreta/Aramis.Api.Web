namespace Aramis.Api.SecurityService.ModelsDto
{
    public class UserAuth
    {
        public string? UserName { get; set; }
        public Guid? Role { get; set; }
        public string? Token { get; set; }
    }
}
