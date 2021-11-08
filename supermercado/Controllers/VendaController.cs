using Microsoft.AspNetCore.Mvc;
using supermercado.Models;
using System.Linq;

namespace supermercado.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendaController : ControllerBase
    {
        private readonly SUPERMERCADOContext _context;

        public VendaController(SUPERMERCADOContext context)
        {
            _context = context;
        }

        [HttpPut("realizar-venda")]
        public IActionResult RealizarVenda(int codProduto, int quantidade)//5
        {
            var produto = _context.Produtos.Find(codProduto);

            if (produto == null)
            {
                return NotFound("Produto não encontrado.");
            }

            var estoque = _context.Produtos
                .Where(x => x.CodProduto == codProduto)
                .FirstOrDefault();

            if (estoque.Quantidade < quantidade)
            {
                return BadRequest($"O produto não tem a quantidade solicitada em estoques. A quantidade disponível em estoque é de {estoque.Quantidade}");
            }

            estoque.Quantidade -= quantidade;

            _context.Update(estoque);

            _context.SaveChanges();

            return Ok("Venda cadastrada com sucesso.");
        }
    }
}
