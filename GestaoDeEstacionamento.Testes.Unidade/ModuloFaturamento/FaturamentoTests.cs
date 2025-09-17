using GestaoDeEstacionamento.Core.Dominio.ModuloFaturamento;
using GestaoDeEstacionamento.Core.Dominio.ModuloHospede;
using GestaoDeEstacionamento.Core.Dominio.ModuloTicket;
using GestaoDeEstacionamento.Core.Dominio.ModuloVaga;
using GestaoDeEstacionamento.Core.Dominio.ModuloVeiculo;

namespace GestaoDeEstacionamento.Testes.Unidade.ModuloFaturamento;

[TestClass]
[TestCategory("Testes de Unidade de Faturamento")]
public sealed class FaturamentoTests
{
    private Faturamento? faturamento;
    private Ticket? ticket;
    private Veiculo? veiculo;
    private Vaga? vaga;
    private Hospede? hospede;

    [TestMethod]
    public void Deve_Calcular_Valor_Corretamente()
    {
        // Arrange
        hospede = new Hospede("Tio Guda", "999.999.999-99");
        veiculo = new Veiculo("ABC-1234", "Modelo X", "Cor Y");
        vaga = new Vaga("A1");

        var dataSaida = DateTime.UtcNow.AddDays(2);
        ticket = new Ticket(hospede, veiculo, vaga, dataSaida, "Nenhuma observação");
        var faturamento = new Faturamento(ticket);

        // Act
        faturamento.CalcularFaturamento();
        var valorCalculado = faturamento.Total;

        // Assert
        decimal valorEsperado = 70m;
        Assert.AreEqual(valorEsperado, valorCalculado);
    }
}
