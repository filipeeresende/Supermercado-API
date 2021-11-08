using Microsoft.AspNetCore.Mvc;
using supermercado.Models;
using System.Linq;

namespace supermercado.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly SUPERMERCADOContext _context;

        public ProdutoController(SUPERMERCADOContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult ObterProduto()
        {
            var produtos = _context.Produtos
             .ToList();

            return Ok(produtos);
        }

        [HttpGet("{codigoProduto}")]
        public IActionResult ObterProdutoPorCodigo(int codigoProduto)
        {
            if (codigoProduto == 0)
            {
                return BadRequest("codigo não pode ser 0");
            }
            var produto = _context.Produtos
                .Where(x => x.CodProduto == codigoProduto)
                .FirstOrDefault();
            if (produto == null)
            {
                return NotFound("produto não existe");
            }
            return Ok(produto);
        }

        [HttpPost]
        public IActionResult CadastrarProduto(string nomeProduto, int valorProduto, int quantidadeProduto)

        {
            var novoPorduto = new Produto
            {
                NomeProduto = nomeProduto,
                Valor = valorProduto,
                Quantidade = quantidadeProduto

            };

            _context.Add(novoPorduto);
            _context.SaveChanges();

            return Ok("produto cadastrado com sucesso");
        }

        [HttpPut("{codigoProduto}")]
        public IActionResult AtualizarProduto(string nomeProduto, int valorProduto, int codigoProduto)
        {
            var produto = _context.Produtos
                 .Where(x => x.CodProduto == codigoProduto)
                 .FirstOrDefault();

            if (produto == null)
            {
                return BadRequest("produto nao encontrado");
            }

            produto.NomeProduto = nomeProduto;
            produto.CodProduto = codigoProduto;
            produto.Valor = valorProduto;

            _context.Update(produto);
            _context.SaveChanges();

            return Ok("produto atualizado com sucesso");
        }

        [HttpDelete("{codigoProduto}")]
        public IActionResult DeletarProduto(int codigoProduto)
        {
            var produto = _context.Produtos
               .Where(x => x.CodProduto == codigoProduto)
               .FirstOrDefault();

            if (produto == null)
            {
                return NotFound("produto não encontrado");
            }
            _context.Remove(produto);
            _context.SaveChanges();

            return Ok("usuario deletado com sucesso");
        }
    }
}
