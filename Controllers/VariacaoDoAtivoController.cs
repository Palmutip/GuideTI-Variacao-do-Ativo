using GuideTI_Variacao_do_Ativo.Context;
using GuideTI_Variacao_do_Ativo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GuideTI_Variacao_do_Ativo.Controllers
{
    [Route("api/[controller]"), ApiController]
    public class VariacaoDoAtivoController : ControllerBase
    {
        private static HttpClient httpClient = new HttpClient();

        private readonly VariacaoDoAtivoContext _context;

        public VariacaoDoAtivoController(VariacaoDoAtivoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Variacao>>> GetAllVariacoes()
        {
            return Ok(await _context.Variacoes.ToListAsync());
        }

        [HttpGet("{dia}")]
        public async Task<ActionResult<List<Variacao>>> GetVariacao(int dia)
        {
            var registro = await _context.Variacoes.FindAsync(dia);

            if (null == registro)
            {
                return BadRequest("Registro não encontrado");
            }

            return Ok(registro);
        }

        [HttpPost]
        public async Task<ActionResult<List<Variacao>>> AddAtivo(string IdentificacaoAtivo)
        {
            var apiCaller = new ApiCaller(httpClient);
            var retornoApi = await apiCaller.ChamaApiESalvaNoBancoDeDados(IdentificacaoAtivo);

            foreach (var variacao in retornoApi)
            {
                _context.Variacoes.Add(variacao);
            }

            await _context.SaveChangesAsync();

            return Ok(await _context.Variacoes.ToListAsync());
        }

        [HttpPatch("{dia}")]
        public async Task<IActionResult> Update(int dia, [FromBody] Variacao variacaoDto)
        {
            var variacao = await _context.Variacoes.FindAsync(dia);

            if (variacao == null)
            {
                return NotFound();
            }

            variacao.Data = variacaoDto.Data;
            variacao.Valor = variacaoDto.Valor;
            variacao.VaricaoRelacaoD1 = variacaoDto.VaricaoRelacaoD1;
            variacao.VariacaoRelacaoPrimeiraData = variacaoDto.VariacaoRelacaoPrimeiraData;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAll()
        {
            var variacoes = await _context.Variacoes.ToListAsync();

            _context.Variacoes.RemoveRange(variacoes);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{dia}")]
        public async Task<ActionResult> Delete(int dia)
        {
            var variacao = await _context.Variacoes.FindAsync(dia);

            if (variacao == null)
            {
                return NotFound();
            }

            _context.Variacoes.Remove(variacao);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
