using GuideTI_Variacao_do_Ativo.Models;
using Microsoft.EntityFrameworkCore;

namespace GuideTI_Variacao_do_Ativo.Context
{
    public class VariacaoDoAtivoContext : DbContext
    {
        public VariacaoDoAtivoContext(DbContextOptions<VariacaoDoAtivoContext> options) : base(options)
        {

        }

        public DbSet<Variacao> Variacoes => Set<Variacao>();
    }
}
