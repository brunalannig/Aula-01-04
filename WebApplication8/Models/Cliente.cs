using System.ComponentModel.DataAnnotations;

namespace WebApplication8.Models
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }
        public string? nomeCliente { get; set; }
    }
}
