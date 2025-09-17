using GestaoDeEstacionamento.Core.Dominio.ModuloHospede;
using GestaoDeEstacionamento.Core.Dominio.ModuloTicket;
using GestaoDeEstacionamento.Core.Dominio.ModuloVaga;
using GestaoDeEstacionamento.Core.Dominio.ModuloVeiculo;

namespace GestaoDeEstacionamento.Testes.Unidade.ModuloTicket;

[TestClass]
[TestCategory("Testes de Unidade de Ticket")]
public sealed class TicketTests
{
    private Ticket? ticket;
    private Veiculo? veiculo;
    private Vaga? vaga;
    private Hospede? hospede;

    [TestMethod]
    public void Deve_Abrir_Ticket_Corretamente()
    {
        // Arrange
        hospede = new Hospede("Tio Guda", "999.999.999-99");
        veiculo = new Veiculo("ABC-1234", "Modelo X", "Cor Y");
        vaga = new Vaga("A1");
        var data = DateTime.UtcNow.AddDays(2);
        string observacao = "Nenhuma observação";

        // Act
        ticket = new Ticket(hospede, veiculo, vaga, data, observacao);
        ticket.AbrirTicket();

        // Assert
        Assert.IsTrue(ticket.Aberta);
        Assert.IsTrue(ticket.Vaga.Ocupada);
    }

    [TestMethod]
    public void Deve_Fechar_Ticket_Corretamente()
    {
        // Arrange
        hospede = new Hospede("Tio Guda", "999.999.999-99");
        veiculo = new Veiculo("ABC-1234", "Modelo X", "Cor Y");
        vaga = new Vaga("A1");
        var data = DateTime.UtcNow.AddDays(2);
        string observacao = "Nenhuma observação";
        ticket = new Ticket(hospede, veiculo, vaga, data, observacao);
        ticket.AbrirTicket();

        // Act
        ticket.FecharTicket();

        // Assert
        Assert.IsFalse(ticket.Aberta);
        Assert.IsFalse(ticket.Vaga.Ocupada);
    }
}
