using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using SistemaGestao.Data;
using SistemaGestao.Models;

namespace SistemaGestao.Controllers
{
    [Authorize]
    public class PedidoController : Controller
    {
        private readonly AppDbContext _context;

        public PedidoController(AppDbContext context)
        {
            _context = context;
        }

        
        public IActionResult Index()
        {
            var pedidos = _context.Pedidos.Include(p => p.Itens).ThenInclude(i => i.Produto).ToList();
            return View(pedidos);
        }

       
        public IActionResult Create()
        {
            ViewBag.Produtos = _context.Produtos.ToList();
            return View("CriarPedido");
        }

 
        [HttpPost]
        public IActionResult Create(Pedido pedido, List<int> produtoIds, List<int> quantidades)
        {
            pedido.Itens = new List<ItemPedido>();
            for (int i = 0; i < produtoIds.Count; i++)
            {
                pedido.Itens.Add(new ItemPedido { ProdutoId = produtoIds[i], Quantidade = quantidades[i] });
            }

            _context.Pedidos.Add(pedido);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Historico()
        {
            var pedidos = _context.Pedidos
                .Include(p => p.Itens).ThenInclude(i => i.Produto)
                .Where(p => p.Status == StatusPedido.Entregue)
                .ToList();

            return View(pedidos);
        }

        public IActionResult Cozinha()
        {
            var pedidos = _context.Pedidos
                .Include(p => p.Itens)
                .ThenInclude(i => i.Produto)
                .Where(p => p.Itens.Any(i => i.Produto.Tipo == TipoProduto.Prato))
                .ToList();

            return View(pedidos);
        }

        public IActionResult Copa()
        {
            var pedidos = _context.Pedidos
                .Include(p => p.Itens)
                .ThenInclude(i => i.Produto)
                .Where(p => p.Itens.Any(i => i.Produto.Tipo == TipoProduto.Bebida))
                .ToList();

            return View(pedidos);
        }

        [HttpPost]
        public IActionResult MarcarPronto(int id, string origem)
        {
            var pedido = _context.Pedidos
                .Include(p => p.Itens)
                .ThenInclude(i => i.Produto)
                .FirstOrDefault(p => p.Id == id);

            if (pedido != null)
            {
                pedido.Status = StatusPedido.Pronto;
                _context.SaveChanges();

                return origem switch
                {
                    "cozinha" => RedirectToAction(nameof(Cozinha)),
                    "copa" => RedirectToAction(nameof(Copa)),
                    _ => RedirectToAction(nameof(Index))
                };
            }

            return NotFound();
        }
    }
}