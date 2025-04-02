using System.ComponentModel.DataAnnotations;

namespace WebApplication8.Models
{
    public class Cidade
    {
        [Key]
        public int IdCidade { get; set; }
        public string? nomeCidade { get; set; }
        public string? uf { get; set; }
        public string? pais { get; set; }
    }
}
