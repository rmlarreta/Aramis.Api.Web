namespace Aramis.Api.Commons.ModelsDto.Security
{
    public class UserInsertDto
    {
        public Guid? Id { get; set; }

        public string? UserName { get; set; }

        public string? RealName { get; set; }
        public string? PassWord { get; set; }

    }
}
