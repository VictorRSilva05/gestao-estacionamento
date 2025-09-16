using GestaoDeEstacionamento.Core.Dominio.ModuloHospede;
using GestaoDeEstacionamento.Core.Dominio.ModuloTicket;
using GestaoDeEstacionamento.Core.Dominio.ModuloVaga;
using GestaoDeEstacionamento.Core.Dominio.ModuloVeiculo;
using GestaoDeEstacionamento.Testes.Integração.Compartilhado;

namespace GestaoDeEstacionamento.Testes.Integração.ModuloTicket;

[TestClass]
[TestCategory("Testes de Integração de Ticket")]
public sealed class RepositorioTicketEmOrmTests : TestFixture
{
    [TestMethod]
    public async Task Deve_Cadastrar_Ticket_Corretamente()
    {
        // Arrange
        var hospede = new Hospede("Tio Guda", "999.999.999-99");
        await repositorioHospede?.CadastrarAsync(hospede)!;
        dbContext?.SaveChanges();
        var veiculo = new Veiculo("ABC-1234", "Modelo X", "Cor Y");
        await repositorioVeiculo?.CadastrarAsync(veiculo)!;
        dbContext?.SaveChanges();
        var vaga = new Vaga("A1");
        await repositorioVaga?.CadastrarAsync(vaga)!;
        dbContext?.SaveChanges();

        var data = DateTime.UtcNow;

        string observacao = "Nenhuma observação";
        var ticket = new Ticket(hospede, veiculo, vaga, data, observacao);

        // Act
        await repositorioTicket?.CadastrarAsync(ticket)!;
        dbContext?.SaveChanges();

        // Assert
        var ticketEncontrado = repositorioTicket?.SelecionarRegistroPorIdAsync(ticket.Id).Result;
        Assert.AreEqual(ticket, ticketEncontrado);
    }

    [TestMethod]
    public async Task Deve_Editar_Ticket_Corretamente()
    {
        // Arrange
        var hospede = new Hospede("Tio Guda", "999.999.999-99");
        await repositorioHospede?.CadastrarAsync(hospede)!;
        dbContext?.SaveChanges();
        var veiculo = new Veiculo("ABC-1234", "Modelo X", "Cor Y");
        await repositorioVeiculo?.CadastrarAsync(veiculo)!;
        dbContext?.SaveChanges();
        var vaga = new Vaga("A1");
        await repositorioVaga?.CadastrarAsync(vaga)!;
        dbContext?.SaveChanges();
        var data = DateTime.UtcNow;
        string observacao = "Nenhuma observação";
        var ticket = new Ticket(hospede, veiculo, vaga, data, observacao);
        await repositorioTicket?.CadastrarAsync(ticket)!;
        dbContext?.SaveChanges();

        var novoHospede = new Hospede("Tio Guda Editado", "999.999.999-99");
        await repositorioHospede?.CadastrarAsync(novoHospede)!;
        dbContext?.SaveChanges();
        var novoVeiculo = new Veiculo("DEF-5678", "Modelo Z", "Cor W");
        await repositorioVeiculo?.CadastrarAsync(novoVeiculo)!;
        dbContext?.SaveChanges();
        var novaVaga = new Vaga("B2");
        await repositorioVaga?.CadastrarAsync(novaVaga)!;
        dbContext?.SaveChanges();
        var novaData = DateTime.UtcNow.AddHours(2);
        string novaObservacao = "Observação editada";
        var ticketEditado = new Ticket(novoHospede, novoVeiculo, novaVaga, novaData, novaObservacao);

        // Act
        var conseguiuEditar = await repositorioTicket?.EditarAsync(ticket.Id, ticketEditado)!;
        dbContext?.SaveChanges();

        // Assert
        var ticketEncontrado = repositorioTicket?.SelecionarRegistroPorIdAsync(ticket.Id).Result;
        Assert.AreEqual(ticket, ticketEncontrado);
        Assert.IsTrue(conseguiuEditar);
    }

    [TestMethod]
    public async Task Deve_Excluir_Ticket_Corretamente()
    {
        // Arrange
        var hospede = new Hospede("Tio Guda", "999.999.999-99");
        await repositorioHospede?.CadastrarAsync(hospede)!;
        dbContext?.SaveChanges();
        var veiculo = new Veiculo("ABC-1234", "Modelo X", "Cor Y");
        await repositorioVeiculo?.CadastrarAsync(veiculo)!;
        dbContext?.SaveChanges();
        var vaga = new Vaga("A1");
        await repositorioVaga?.CadastrarAsync(vaga)!;
        dbContext?.SaveChanges();
        var data = DateTime.UtcNow;
        string observacao = "Nenhuma observação";
        var ticket = new Ticket(hospede, veiculo, vaga, data, observacao);
        await repositorioTicket?.CadastrarAsync(ticket)!;
        dbContext?.SaveChanges();

        // Act
        var conseguiuExcluir = await repositorioTicket?.ExcluirAsync(ticket.Id)!;
        dbContext?.SaveChanges();

        // Assert
        var ticketEncontrado = repositorioTicket?.SelecionarRegistroPorIdAsync(ticket.Id).Result;
        Assert.IsNull(ticketEncontrado);
    }

    [TestMethod]
    public async Task Deve_Selecionar_Todos_Tickets_Corretamente()
    {
        // Arrange
        var hospede1 = new Hospede("Tio Guda", "999.999.999-99");
        await repositorioHospede?.CadastrarAsync(hospede1)!;
        dbContext?.SaveChanges();
        var veiculo1 = new Veiculo("ABC-1234", "Modelo X", "Cor Y");
        await repositorioVeiculo?.CadastrarAsync(veiculo1)!;
        dbContext?.SaveChanges();
        var vaga1 = new Vaga("A1");
        await repositorioVaga?.CadastrarAsync(vaga1)!;
        dbContext?.SaveChanges();
        var data1 = DateTime.UtcNow;
        var data2 = DateTime.UtcNow;
        string observacao1 = "Nenhuma observação";
        string observacao2 = "Nenhuma observação";
        var ticket1 = new Ticket(hospede1, veiculo1, vaga1, data1, observacao1);
        var hospede2 = new Hospede("Tio Guda 2", "888.888.888-88");
        await repositorioHospede?.CadastrarAsync(hospede2)!;
        dbContext?.SaveChanges();
        var veiculo2 = new Veiculo("DEF-5678", "Modelo Z", "Cor W");
        await repositorioVeiculo?.CadastrarAsync(veiculo2)!;
        dbContext?.SaveChanges();
        var vaga2 = new Vaga("B2");
        await repositorioVaga?.CadastrarAsync(vaga2)!;
        dbContext?.SaveChanges();
        var ticket2 = new Ticket(hospede2, veiculo2, vaga2, data2, observacao2);
        await repositorioTicket?.CadastrarEntidades(new List<Ticket> { ticket1, ticket2 })!;
        dbContext?.SaveChanges();

        // Act
        var tickets = await repositorioTicket?.SelecionarRegistrosAsync()!;

        // Assert
        Assert.AreEqual(2, tickets.Count);
        CollectionAssert.AreEquivalent(new List<Ticket> { ticket1, ticket2 }, tickets.ToList());
    }

    [TestMethod]
    public async Task Deve_Selecionar_Ticket_Por_Id_Corretamente()
    {
        // Arrange
        var hospede = new Hospede("Tio Guda", "999.999.999-99");
        await repositorioHospede?.CadastrarAsync(hospede)!;
        dbContext?.SaveChanges();
        var veiculo = new Veiculo("ABC-1234", "Modelo X", "Cor Y");
        await repositorioVeiculo?.CadastrarAsync(veiculo)!;
        dbContext?.SaveChanges();
        var vaga = new Vaga("A1");
        await repositorioVaga?.CadastrarAsync(vaga)!;
        dbContext?.SaveChanges();
        var data = DateTime.UtcNow;
        string observacao = "Nenhuma observação";
        var ticket = new Ticket(hospede, veiculo, vaga, data, observacao);
        await repositorioTicket?.CadastrarAsync(ticket)!;
        dbContext?.SaveChanges();

        // Act
        var ticketEncontrado = await repositorioTicket?.SelecionarRegistroPorIdAsync(ticket.Id)!;
        
        // Assert
        Assert.IsNotNull(ticketEncontrado);
        Assert.AreEqual(ticket, ticketEncontrado);
    }

    [TestMethod]
    public async Task Deve_Selecionar_Quantidade_Especifica_De_Tickets_Corretamente()
    {
        // Arrange
        var hospede1 = new Hospede("Tio Guda", "999.999.999-99");
        await repositorioHospede?.CadastrarAsync(hospede1)!;
        dbContext?.SaveChanges();
        var veiculo1 = new Veiculo("ABC-1234", "Modelo X", "Cor Y");
        await repositorioVeiculo?.CadastrarAsync(veiculo1)!;
        dbContext?.SaveChanges();
        var vaga1 = new Vaga("A1");
        await repositorioVaga?.CadastrarAsync(vaga1)!;
        dbContext?.SaveChanges();
        var data1 = DateTime.UtcNow;
        var data2 = DateTime.UtcNow;
        string observacao1 = "Nenhuma observação";
        string observacao2 = "Nenhuma observação";
        var ticket1 = new Ticket(hospede1, veiculo1, vaga1, data1, observacao1);
        var hospede2 = new Hospede("Tio Guda 2", "888.888.888-88");
        await repositorioHospede?.CadastrarAsync(hospede2)!;
        dbContext?.SaveChanges();
        var veiculo2 = new Veiculo("DEF-5678", "Modelo Z", "Cor W");
        await repositorioVeiculo?.CadastrarAsync(veiculo2)!;
        dbContext?.SaveChanges();
        var vaga2 = new Vaga("B2");
        await repositorioVaga?.CadastrarAsync(vaga2)!;
        dbContext?.SaveChanges();
        var ticket2 = new Ticket(hospede2, veiculo2, vaga2, data2, observacao2);
        await repositorioTicket?.CadastrarEntidades(new List<Ticket> { ticket1, ticket2 })!;
        dbContext?.SaveChanges();

        // Act
        var tickets = await repositorioTicket?.SelecionarRegistrosAsync(1)!;

        // Assert
        Assert.AreEqual(1, tickets.Count);
    }
}