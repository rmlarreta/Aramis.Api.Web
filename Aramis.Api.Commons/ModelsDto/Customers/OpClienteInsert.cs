namespace Aramis.Api.Commons.ModelsDto.Customers
{
    public class OpClienteInsert
    {
        public string? Id { get; set; } = null!;
        public string Cui { get; set; } = null!;
        public string Resp { get; set; } = null!; 
        public string Razon { get; set; } = null!;
        public string Gender { get; set; } = null!; 
        public string Domicilio { get; set; } = null!;
        public string? Pais { get; set; } 
        public string? Contacto { get; set; }
        public string? Mail { get; set; }
    }
}
