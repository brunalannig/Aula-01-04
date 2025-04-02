using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication8.Data;
using WebApplication8.Models;

namespace WebApplication8.Controllers
{
    public class PedidoController : Controller
    {
        private readonly WebApplication8Context _context;

        public PedidoController(WebApplication8Context context)
        {
            _context = context;
        }

        // GET: Pedido
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pedido.ToListAsync());
        }
        public async Task<IActionResult> IndexFiltro
                        (string p_nome, string criteriobusca)
        {   // parte do código que preenche o list da cidade
            var v_cidades = from x in _context.Cidade select x.nomeCidade;
            //a variavel x é um apelido para a tabela cidade
            @ViewBag.pnomeCidade = new SelectList(await v_cidades.ToListAsync());
            //viewBag pega do controller e joga pra Views

            var pedidosfiltro = from x in _context.Pedido select x;
            //carrega todos os pedidos

            if (!String.IsNullOrEmpty(p_nome))
            {
                pedidosfiltro = from x in _context.Pedido
                                join y in _context.Cidade on x.IdCidade equals y.IdCidade
                                where y.nomeCidade == p_nome
                                select x;//carrega pedidos com o filtro usado pelo usuario
            }
            if (!String.IsNullOrEmpty(criteriobusca))
            {
                pedidosfiltro = pedidosfiltro.Where(zz => zz.valor == int.Parse(criteriobusca));
            }

            return View(await pedidosfiltro.ToListAsync());
        }

        public async Task<IActionResult> IndexPedidoCli
                        (string p_nome, string criteriobusca)
        {   // parte do código que preenche o list da cidade
            var v_clientes = from x in _context.Cliente select x.nomeCliente;
            //a variavel x é um apelido para a tabela cidade
            @ViewBag.pnomeCliente = new SelectList(await v_clientes.ToListAsync());
            //viewBag pega do controller e joga pra Views

            var pedidosfiltro = from x in _context.Pedido select x;
            //carrega todos os pedidos

            if (!String.IsNullOrEmpty(p_nome))
            {
                pedidosfiltro = from x in _context.Pedido
                                join y in _context.Cliente on x.IdCliente equals y.IdCliente
                                where y.nomeCliente == p_nome
                                select x;//carrega pedidos com o filtro usado pelo usuario
            }
            if (!String.IsNullOrEmpty(criteriobusca))
            {
                pedidosfiltro = pedidosfiltro.Where(zz => zz.valor == int.Parse(criteriobusca));
            }

            return View(await pedidosfiltro.ToListAsync());
        }

        // GET: Pedido/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido
                .FirstOrDefaultAsync(m => m.IdPedido == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET: Pedido/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pedido/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPedido,IdCliente,valor,IdCidade")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pedido);
        }

        // GET: Pedido/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            return View(pedido);
        }

        // POST: Pedido/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPedido,IdCliente,valor,IdCidade")] Pedido pedido)
        {
            if (id != pedido.IdPedido)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.IdPedido))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pedido);
        }

        // GET: Pedido/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido
                .FirstOrDefaultAsync(m => m.IdPedido == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Pedido/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedido = await _context.Pedido.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedido.Remove(pedido);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedido.Any(e => e.IdPedido == id);
        }
    }
}
