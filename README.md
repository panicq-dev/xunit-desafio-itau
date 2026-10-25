# Testes Unitários — Desafio Itaú

Este repositório é uma continuação do projeto [Desafio Itaú](https://github.com/panicq-dev/desafio-itau), com foco exclusivo na implementação de testes unitários utilizando C# e xUnit.

Enquanto o repositório original concentra-se na implementação das funcionalidades do desafio, este projeto busca verificar seu comportamento por meio de testes automatizados, contemplando regras de negócio, validação de dados de entrada e verificação dos resultados produzidos pelas operações.

O objetivo é aplicar, na prática, conceitos de **Qualidade de Software**, desenvolvendo uma abordagem de testes que contribua para a confiabilidade, a manutenção e a prevenção de regressões na aplicação.

> **Nota:** este é um projeto de caráter recreativo e educacional, desenvolvido para praticar testes automatizados e consolidar conhecimentos de desenvolvimento back-end. Os cenários implementados contemplam as principais validações e comportamentos identificados para as funcionalidades testadas, mas não representam uma cobertura exaustiva de todas as situações possíveis.

## Objetivos

- Implementar testes unitários para as funcionalidades de transação.
- Verificar o cumprimento das regras de negócio.
- Validar o tratamento de dados de entrada inválidos.
- Testar pré-condições e pós-condições das operações.
- Verificar os estados da aplicação após a execução dos métodos.
- Exercitar diferentes cenários de sucesso e falha.
- Aplicar boas práticas de organização e estruturação de testes.

## Tecnologias utilizadas

- **C#**
- **.NET**
- **xUnit**
- **DataAnnotations**
- **System.ComponentModel.DataAnnotations.Validator**
- **Git e GitHub**

## Cobertura dos cenários de teste

Os testes foram desenvolvidos para cobrir os principais comportamentos esperados das funcionalidades implementadas no serviço de transações.

| Funcionalidade | Cenários contemplados |
|---|---|
| `Transferir()` | Rejeição de transações com valor negativo |
| `Transferir()` | Rejeição de transações com data e hora futuras |
| Validação de `Transacao` | Verificação de propriedades inválidas e lançamento de `ValidationException` |
| Validação de `Transacao` | Verificação do comportamento com `validateAllProperties` habilitado ou desabilitado |
| `LimparTransacoes()` | Remoção das transações registradas |
| `ObterTransacoesDaUltimaHora()` | Retorno de transações ocorridas dentro dos últimos 60 minutos |
| `ObterTransacoesDaUltimaHora()` | Exclusão de transações ocorridas há mais de 60 minutos |

### Validações realizadas

**1. Dados de entrada e regras de negócio**

Foram contemplados cenários de entrada inválida, como valores negativos e datas futuras, verificando se o serviço rejeita as operações e mantém a lista de transações vazia.

**2. Pré-condições**

Foram considerados os requisitos que os dados precisam atender para que uma operação seja aceita, incluindo as validações aplicáveis às propriedades do modelo `Transacao`.

**3. Pós-condições**

Após a execução dos métodos, os testes verificam os efeitos esperados, como a ausência de registros após operações rejeitadas, a remoção de transações e o conteúdo retornado pelas consultas.

**4. Validação de propriedades**

O uso de `Validator.ValidateObject`, `ValidationContext` e `ValidationException` permite exercitar as validações declaradas no modelo, verificando o comportamento diante de propriedades inválidas.

**5. Consulta temporal**

Foram contemplados cenários em que as transações estão dentro ou fora da janela de 60 minutos, verificando o comportamento esperado da consulta.

## Padrão de organização

Os testes utilizam o padrão **Arrange, Act, Assert (AAA)**:

- **Arrange:** preparação dos dados e dos objetos necessários.
- **Act:** execução do método que será testado.
- **Assert:** verificação do resultado e dos efeitos observáveis.

Também são utilizados `[Fact]` para cenários individuais e `[Theory]` com `[InlineData]` para testes parametrizados.

## Limitações e escopo

A suíte contempla os principais cenários identificados para as funcionalidades testadas, mas isso não significa que todas as possibilidades tenham sido cobertas.

Ainda existem oportunidades para ampliar os testes com:

- Valores nulos e limites numéricos.
- Transações exatamente no limite de 60 minutos.
- Datas inválidas ou outros limites temporais.
- Cenários de sucesso adicionais para `Transferir()`.
- Verificações mais detalhadas de cada resultado e do estado observável da aplicação.
- Testes de integração e análise de cobertura de código.

Esses pontos representam possíveis evoluções, sem impedir que o projeto cumpra sua finalidade atual: praticar testes unitários e validar os principais comportamentos implementados.

**O foco não é afirmar uma cobertura absoluta, mas demonstrar a aplicação prática de testes relevantes para as regras de negócio existentes.**

## Como executar os testes

Com o SDK do .NET instalado, execute os comandos no diretório do projeto de testes:

```bash
dotnet restore
dotnet test
```

## Projeto relacionado

[**Desafio Itaú — Repositório principal**](https://github.com/panicq-dev/desafio-itau)

Este repositório complementa o projeto original, concentrando-se na verificação automatizada do comportamento das funcionalidades implementadas.

## Autor

Desenvolvido por [panicq-dev](https://github.com/panicq-dev), como exercício recreativo e educacional de C#, .NET e Qualidade de Software.
