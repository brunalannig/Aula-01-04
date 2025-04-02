using System.ComponentModel.DataAnnotations;

namespace WebApplication8.Models
{
    public class Pedido
    {
        [Key]
        public int IdPedido { get; set; }
        public int? IdCliente { get; set; }
        public float? valor { get; set; }
        public int? IdCidade { get; set; }
    }
}
