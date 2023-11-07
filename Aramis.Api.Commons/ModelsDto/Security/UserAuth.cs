namespace Aramis.Api.Commons.ModelsDto.Security
{
    public class UserAuth : UserBaseDto
    {
        public string? Token { get; set; }
         public string RoleName { get; set; } = null!;
    }
}
