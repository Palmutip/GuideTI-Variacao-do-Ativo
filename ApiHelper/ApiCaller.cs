using GuideTI_Variacao_do_Ativo.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System;

namespace GuideTI_Variacao_do_Ativo
{
    public class ApiCaller
    {
        private readonly HttpClient _httpClient;

        public ApiCaller(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Variacao>> ChamaApiESalvaNoBancoDeDados(string identificacaoAtivo)
        {
            try
            {
                DateTime data = new DateTime(2023, 01, 01, 0, 1, 0); // Cria um objeto DateTime com a data/hora especificada
                TimeZoneInfo fusoHorario = TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo"); // Obtém uma instância do fuso horário -03:00
                DateTime dataFusoHorario = TimeZoneInfo.ConvertTime(data, fusoHorario); // Converte a data/hora para o fuso horário -03:00
                DateTimeOffset dateTimeOffset = new DateTimeOffset(dataFusoHorario, fusoHorario.GetUtcOffset(dataFusoHorario)); // Cria um objeto DateTimeOffset com a data/hora e o fuso horário especificados
                long timestamp = dateTimeOffset.ToUnixTimeSeconds();
                //var timestamp = ((DateTimeOffset)DateTime.UtcNow.AddDays(-30)).ToUnixTimeSeconds();
                var response = await _httpClient.GetAsync($"https://query2.finance.yahoo.com/v8/finance/chart/{identificacaoAtivo}?period1={timestamp}&period2=9999999999&interval=1d");
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Ativo>(content);

                if (result == null)
                {
                    throw new ArgumentNullException(nameof(result), "Retorno da API foi nulo");
                }

                var chart = result.chart?.result?.FirstOrDefault();
                if (chart == null)
                {
                    return new List<Variacao>();
                }

                var periodos = chart.timestamp;

                var numPeriodos = periodos.Count;
                if (numPeriodos < 30)
                {
                    return new List<Variacao>();
                }

                var openList = chart.indicators?.quote?.FirstOrDefault()?.open;

                var variacoes = openList?
                    .Skip(Math.Max(0, openList.Count() - 30))
                    .Select((open, index) =>
                    {
                        var variacao = new Variacao
                        {
                            Dia = index + 1,
                            Data = DateTimeOffset.FromUnixTimeSeconds(periodos[periodos.Count() - 30 + index]).LocalDateTime.ToString("dd/MM/yyyy"),
                            Valor = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", open)
                        };

                        if (index == 0)
                        {
                            variacao.VaricaoRelacaoD1 = "-";
                            variacao.VariacaoRelacaoPrimeiraData = "-";
                        }
                        else
                        {
                            var openD1 = chart.indicators?.quote?.FirstOrDefault()?.open?[periodos.Count - 31 + index];
                            var PorcentagemDiaMenosUm = ((open * 100) / openD1) - 100;
                            var PorcentagemEmRelacaoAPrimeiraData = ((open * 100) / chart.indicators?.quote?.FirstOrDefault()?.open?[periodos.Count() - 30]) - 100;
                            variacao.VaricaoRelacaoD1 = (Convert.ToDecimal(PorcentagemDiaMenosUm)/100).ToString("P2", CultureInfo.GetCultureInfo("pt-BR")) ?? "-";
                            variacao.VariacaoRelacaoPrimeiraData = (Convert.ToDecimal(PorcentagemEmRelacaoAPrimeiraData) / 100).ToString("P2", CultureInfo.GetCultureInfo("pt-BR")) ?? "-";
                        }

                        return variacao;
                    })
                    .ToList();

                return variacoes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
