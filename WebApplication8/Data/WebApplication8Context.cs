using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication8.Models;

namespace WebApplication8.Data
{
    public class WebApplication8Context : DbContext
    {
        public WebApplication8Context (DbContextOptions<WebApplication8Context> options)
            : base(options)
        {
        }

        public DbSet<WebApplication8.Models.Cidade> Cidade { get; set; } = default!;
        public DbSet<WebApplication8.Models.Cliente> Cliente { get; set; } = default!;
        public DbSet<WebApplication8.Models.Pedido> Pedido { get; set; } = default!;
    }
}
