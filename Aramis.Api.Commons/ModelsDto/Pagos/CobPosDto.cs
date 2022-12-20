namespace Aramis.Api.Commons.ModelsDto.Pagos
{
    public class CobPosDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string? DeviceId { get; set; }

        public string? Token { get; set; }
    }
}
