using DesafioItau.Aplicacao.Servicos.Estatisticas;
using DesafioItau.Aplicacao.Servicos.Transacoes;

namespace xUnitDesafioItau;

public class EstatisticaServiceTeste
{
    [Fact]
    public void Estatistica_QuandoNaoExisteTransacoesEm60Minutos_DeveConsiderarValoresComoZero()
    {
        
        // Arrange
        var servicoTransacao = new TransacaoService();
        var servicoEstatistica = new EstatisticasService(servicoTransacao);
        
        // Act 
        var estatisticas = servicoEstatistica.ObterEstatisticasDaUltimaHora();
        var resultadoAvg = estatisticas.Avg;
        var resultadoMax = estatisticas.Max;
        var resultadoMin = estatisticas.Min;
        var resultadoQuantia = estatisticas.Count;
        var resultadoSum =  estatisticas.Sum;
        
        // Assert
        Assert.Equal(0, resultadoAvg);
        Assert.Equal(0, resultadoMin);
        Assert.Equal(0, resultadoMax);
        Assert.Equal(0, resultadoSum);
        Assert.Equal(0, resultadoQuantia);
    }
}