
using System.ComponentModel.DataAnnotations;
using DesafioItau.Aplicacao.Servicos.Transacoes;
using DesafioItau.Domain.Models;

namespace xUnitDesafioItau;

public class TransferenciaServiceTeste
{
    [Fact]
    public void Transferir_QuandoValorEhNegativo_DeveRetornarFalse()
    {
        // Arrange
        var transacao = new Transacao()
        {
            Valor = -100,
            DataHora = DateTimeOffset.UtcNow
        };
        var servicoTransacao = new TransacaoService();

        // Act
        var resultado = servicoTransacao.Transferir(transacao);
        var listaTransacoes = servicoTransacao.ObterTransacoesDaUltimaHora();

        // Assert
        Assert.False(resultado);
        Assert.Empty(listaTransacoes);
        
    }

    [Fact]
    public void Transferir_QuandoDataHoraEhFutura_DeveRetornarFalse()
    {
        // Arrange
        var transacao = new Transacao()
        {
            DataHora = DateTimeOffset.UtcNow.AddHours(1)
        };
        var servicoTransacao = new TransacaoService();

        // Act
        var resultado = servicoTransacao.Transferir(transacao);
        var listaTransacoes = servicoTransacao.ObterTransacoesDaUltimaHora();

        // Assert
        Assert.False(resultado);
        Assert.Empty(listaTransacoes);
    }

    [Theory]
    [InlineData(-100, "2026-10-24T00:00:00Z")]
    public void ValidarTransacao_QuandoPropriedadeObrigatoriaEhInvalida_DeveLancarValidationException(
        double valor,
        string dataHora)
    {
        // Arrange
        var transacao = new Transacao()
        {
            Valor = valor,
            DataHora = DateTime.Parse(dataHora),
        };

        // Act
        var exception = Assert.Throws<ValidationException>(() =>
            Validator.ValidateObject(
                transacao,
                new ValidationContext(transacao),
                validateAllProperties: true)
        );

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void ValidarTransacao_QuandoValidateAllPropertiesEhFalse_DeveLancarValidationException()
    {
        // Arrange
        var transacao = new Transacao
        {
            Valor = 100,
            DataHora = null
        };

        var context = new ValidationContext(transacao);

        // Act
        var exception = Record.Exception(() =>
            Validator.ValidateObject(
                transacao,
                context,
                validateAllProperties: false)
        );

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void LimparTransacoes_QuandoExistemTransacoesRegistradas_DeveDeixarListaVazia()
    {
        // Arrange
        var servicoTransacao = new TransacaoService();
        var transacao = new Transacao()
        {
            Valor = 1000
        };
        servicoTransacao.Transferir(transacao);

        // Act
        servicoTransacao.LimparTransacoes();
        var transacoes = servicoTransacao.ObterTransacoesDaUltimaHora();

        // Assert
        Assert.Empty(transacoes);
    }

    [Fact]
    public void ObterTransacoesDaUltimaHora_QuandoTransacaoOcorreuNosUltimos60Minutos_DeveRetornarTransacao()
    {
        // Arrange
        var servicoTransacao = new TransacaoService();
        var transacao = new Transacao()
        {
            Valor = 1000,
            DataHora = DateTimeOffset.UtcNow.AddMinutes(-10)
        };
        servicoTransacao.Transferir(transacao);

        // Act
        var transacoes = servicoTransacao.ObterTransacoesDaUltimaHora();

        // Assert
        Assert.NotEmpty(transacoes);
    }

    [Fact]
    public void ObterTransacoesDaUltimaHora_QuandoTransacaoOcorreuHaMaisDe60Minutos_DeveRetornarListaVazia()
    {
        // Arrange
        var servicoTransacao = new TransacaoService();
        var transacao = new Transacao()
        {
            Valor = 1000,
            DataHora = DateTimeOffset.UtcNow.AddMinutes(-120)
        };
        servicoTransacao.Transferir(transacao);

        // Act
        var transacoes = servicoTransacao.ObterTransacoesDaUltimaHora();

        // Assert
        Assert.Empty(transacoes);
    }
}
