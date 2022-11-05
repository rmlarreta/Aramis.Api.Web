namespace Aramis.Api.Commons.ModelsDto.Security
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string UserName { get; set; } = null!;

        public string RealName { get; set; } = null!;

        public Guid Role { get; set; }

        public string RoleName { get; set; } = null!;

        public DateTime EndOfLife { get; set; }

        public bool Active { get; set; }
    }
}
