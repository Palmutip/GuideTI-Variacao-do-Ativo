using System.ComponentModel.DataAnnotations;

namespace GuideTI_Variacao_do_Ativo.Models
{
    public class Variacao
    {
        [Key]
        public int Dia { get; set; }
        public string Data { get; set; } = string.Empty;
        public string Valor { get; set; } = string.Empty;
        public string VaricaoRelacaoD1 { get; set; } = string.Empty;
        public string VariacaoRelacaoPrimeiraData { get; set; } = string.Empty;
    }
}
