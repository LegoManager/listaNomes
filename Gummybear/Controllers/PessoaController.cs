using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace Gummybear
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private static List<Pessoa> ListaPessoas = new List<Pessoa>();

        [HttpPost]
        [Route("Adicionar")]

        public IActionResult AdicionarAluno(Pessoa novaPessoa)
        {
            ListaPessoas.Add(novaPessoa);
            return Ok("Nova pessoa criada com sucesso");
        }

        [HttpPut]
        [Route("Atualizar/{cpf}")]
        public IActionResult Atualizar(string cpf, Pessoa pessoaAtualizada)
        {
            Pessoa? resultadoBusca = ListaPessoas.Where(pessoa => pessoa.cpf == cpf).FirstOrDefault();
            if (resultadoBusca is null) return NotFound($"O Cpf {cpf} não foi encontrado");

            resultadoBusca.nome = pessoaAtualizada.nome;
            resultadoBusca.peso= pessoaAtualizada.peso;
            resultadoBusca.altura = pessoaAtualizada.altura;

            return Ok("Pessoa atualizada com sucesso");
        }

        [HttpDelete]
        [Route("Remover")]
        public IActionResult Remover(string cpf)
        {
            Pessoa? resultadoBusca = ListaPessoas.Where(pessoa => pessoa.cpf == cpf).FirstOrDefault();
            if (resultadoBusca is null) return NotFound($"O Cpf {cpf} não foi encontrado");
            ListaPessoas.Remove(resultadoBusca);
            return Ok("Pessoa removido com sucesso");
        }

        [HttpGet]
        [Route("ObterTodos")]

        public IActionResult ObterTodos()
        {
            return Ok(ListaPessoas);
        }

        [HttpGet]
        [Route("ObterporCpf")]
        
        public IActionResult Obterporcpf(string cpf)
        {
            var resultadoBusca = ListaPessoas.Where(pessoa => pessoa.cpf == cpf).FirstOrDefault();
            if (resultadoBusca is null)
                return NotFound($"O cpf {cpf} não foi encontrado");

            return Ok(resultadoBusca);
        }

        [HttpGet]
        [Route("ObterporIMC")]
        public IActionResult ObterIMC()
        {
            if (ListaPessoas.Count > 0)
            {
                var resultado = new List<Pessoa>();

                for (int i = 0; i < ListaPessoas.Count; i++)
                {
                    var pesoPessoa = ListaPessoas[i].peso;
                    var alturaPessoa = ListaPessoas[i].altura;
                    var resultadoIMC = pesoPessoa / (alturaPessoa * alturaPessoa);

                    if (resultadoIMC >= 18.0 && resultadoIMC <= 24.0)
                    {
                        resultado.Add(ListaPessoas[i]);
                    }

                }
                return Ok(resultado);
            }
            else
            {
                return BadRequest("Não possui nenhum IMC com esse valor");
            }
        }
        [HttpGet]
        [Route("BuscarPorNome")]
        public IActionResult BuscarNome(string nome)
        {
            var buscaPessoa = ListaPessoas.Where(nomePessoa => nomePessoa.nome == nome).ToList();
            return Ok(buscaPessoa);
        }
    }
}
