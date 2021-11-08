using Microsoft.AspNetCore.Mvc;
using supermercado.Models;
using System.Linq;

namespace supermercado.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private readonly SUPERMERCADOContext _context;

        public FuncionarioController(SUPERMERCADOContext context)
        {
            _context = context;

        }

        [HttpGet]
        public IActionResult ObterFuncionarios()
        {
            var funcionario = _context.Funcionarios
                .ToList();

            return Ok(funcionario);
        }

        [HttpGet("{codigoFuncionario}")]
        public IActionResult ObterFuncionariosPorCodigo(int codigoFuncionario)
        {
            if (codigoFuncionario == 0)
            {
                return BadRequest("codigo não pode ser 0");
            }
            var funcionarios = _context.Funcionarios
              .Where(x => x.CodFuncionario == codigoFuncionario)
              .FirstOrDefault();

            if (funcionarios == null)
            {
                return NotFound("funcionario não existe");
            }

            return Ok(funcionarios);
        }

        [HttpPost]
        public IActionResult CadastrarFuncionario(string nomeFuncionario, string setor, string cargo)
        {
            var novoFuncionario = new Funcionario
            {
                Cagor = cargo,
                Setor = setor,
                NomeFuncionario = nomeFuncionario
            };

            _context.Add(novoFuncionario);
            _context.SaveChanges();

            return Ok("funcionario cadastrado com sucesso");
        }

        [HttpPut("{codigoFuncionario}")]
        public IActionResult AtualizarFuncionario(string nomeFuncionario, string setor, string cargo, int codigoFuncionario)
        {
            var usuario = _context.Funcionarios
               .Where(x => x.CodFuncionario == codigoFuncionario)
               .FirstOrDefault();

            if (usuario == null)
            {
                return NotFound("usuario não encontrado");
            }

            usuario.Cagor = cargo;
            usuario.NomeFuncionario = nomeFuncionario;
            usuario.Setor = setor;

            _context.Update(usuario);
            _context.SaveChanges();

            return Ok("usuario atualizado com sucesso");
        }

        [HttpDelete("codigoFuncionario")]
        public IActionResult DeletarFuncionario(int codigoFuncionario)
        {
            var funcionarios = _context.Funcionarios
                .Where(x => x.CodFuncionario == codigoFuncionario)
               .FirstOrDefault();
            if (funcionarios == null)
            {
                return NotFound("usuario não encontrado");
            }

            _context.Remove(funcionarios);
            _context.SaveChanges();

            return Ok("usuario deletado com sucesso");
        }
    }
}
