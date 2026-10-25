using System.ComponentModel.DataAnnotations;
using DesafioItau.Aplicacao.Servicos.Transacoes;
using DesafioItau.Domain.Models;

namespace xUnitDesafioItau;

public class TransferenciaServiceTeste
{
    [Fact]
    public void Transferir_QuandoSaldoEhNegativo_RejeitarTransacaoComFalse()
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
        // Assert
        Assert.False(resultado);
        
    }
    [Fact]
    public void Transferir_QuandoTransacaoAconteceNoFuturo_RejeitarTransacaoComFalse()
    {
        // Arrange
        var transacao = new Transacao()
        {
            DataHora = DateTimeOffset.UtcNow.AddHours(1)
        };
        var servicoTransacao = new TransacaoService();
        // Act
        var resultado = servicoTransacao.Transferir(transacao);
        // Assert
        Assert.False(resultado);
    }
    [Theory]
    [InlineData(-100, "2026-10-24T00:00:00Z")] 
    public void Transferir_ComPropriedadeObrigatoriaInvalida_DeveLancarValidationException(double valor, string dataHora)
    {
        // Arrange
        var transacao = new Transacao()
        {
            Valor = valor,
            DataHora = DateTime.Parse(dataHora),
        };
        
        // Act
        var exception = Assert.Throws<ValidationException>(() =>
            Validator.ValidateObject(transacao,
                new ValidationContext(transacao),
                validateAllProperties: true)
            );
        
        // Assert
        Assert.NotNull(exception);
    }
    
    [Fact]
    public void Estatistica_ValidatorComValidateAllPropertiesFalse_NaoPodeIgnorarPoisTodosCamposTransacaoSaoRequired()
    {
        // Arrange
        var transacao = new Transacao
        {
        Valor = 100,
        DataHora = null
        };

        var context = new ValidationContext(transacao);

        // Act 
        // Como validateAllProperties é false, ele não lança ValidationException.
        var exception = Record.Exception(() => // Grava qualquer tipo de exceção que a expressão abaixo possa lançar.
            Validator.ValidateObject(transacao, context, validateAllProperties: false)
        );

        // Assert 
        // Como todos os campos de transacao são validos, de qualquer forma haverá exception.
        Assert.NotNull(exception);
    }
    
    
}