using GestaoDeEstacionamento.Core.Dominio.ModuloFaturamento;
using GestaoDeEstacionamento.Core.Dominio.ModuloHospede;
using GestaoDeEstacionamento.Core.Dominio.ModuloTicket;
using GestaoDeEstacionamento.Core.Dominio.ModuloVaga;
using GestaoDeEstacionamento.Core.Dominio.ModuloVeiculo;
using GestaoDeEstacionamento.Testes.Integração.Compartilhado;

namespace GestaoDeEstacionamento.Testes.Integração.ModuloFaturamento;

[TestClass]
[TestCategory("Testes de Integração de Faturamento")]
public sealed class RepositorioFaturamentoEmOrmTests : TestFixture
{
    [TestMethod]
    public async Task Deve_Cadastrar_Faturamento_Corretamente()
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

        var data = DateTime.UtcNow.AddDays(2);

        string observacao = "Nenhuma observação";
        var ticket = new Ticket(hospede, veiculo, vaga, data, observacao);

        var faturamento = new Faturamento(ticket);

        // Act
        await repositorioFaturamento?.CadastrarAsync(faturamento)!;
        dbContext?.SaveChanges();

        // Assert
        var faturamentoEncontrado = repositorioFaturamento?.SelecionarRegistroPorIdAsync(faturamento.Id).Result;
    }

    [TestMethod]
    public async Task Deve_Selecionar_Faturamento_Por_Id_Corretamente()
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
        var data = DateTime.UtcNow.AddDays(2);
        string observacao = "Nenhuma observação";
        var ticket = new Ticket(hospede, veiculo, vaga, data, observacao);
        await repositorioTicket?.CadastrarAsync(ticket)!;
        dbContext?.SaveChanges();

        var faturamento = new Faturamento(ticket);
        await repositorioFaturamento?.CadastrarAsync(faturamento)!;
        dbContext?.SaveChanges();

        // Act
        var faturamentoEncontrado = await repositorioFaturamento?.SelecionarRegistroPorIdAsync(faturamento.Id)!;
        
        // Assert
        Assert.AreEqual(faturamento, faturamentoEncontrado);
    }

    [TestMethod]
    public async Task Deve_Selecionar_Todos_Faturamentos_Corretamente()
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
        var data = DateTime.UtcNow.AddDays(2);
        string observacao = "Nenhuma observação";
        var ticket = new Ticket(hospede, veiculo, vaga, data, observacao);
        await repositorioTicket?.CadastrarAsync(ticket)!;
        dbContext?.SaveChanges();

        var faturamento = new Faturamento(ticket);
        await repositorioFaturamento?.CadastrarAsync(faturamento)!;
        dbContext?.SaveChanges();

        // Act
        var faturamentos = await repositorioFaturamento?.SelecionarRegistrosAsync()!;

        // Assert
        Assert.IsTrue(faturamentos.Count > 0);
        Assert.IsTrue(faturamentos.Contains(faturamento));
    }

    [TestMethod]
    public async Task Deve_Selecionar_Faturamento_Por_Periodo_Corretamente()
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
        var data1 = DateTime.UtcNow.AddDays(2);
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
        var data2 = DateTime.UtcNow.AddDays(5);
        var ticket2 = new Ticket(hospede2, veiculo2, vaga2, data2, observacao2);

        await repositorioTicket?.CadastrarEntidades(new List<Ticket> { ticket1, ticket2 })!;
        dbContext?.SaveChanges();

        var faturamento1 = new Faturamento(ticket1);
        var faturamento2 = new Faturamento(ticket2);
        await repositorioFaturamento?.CadastrarEntidades(new List<Faturamento> { faturamento1, faturamento2 })!;
        dbContext?.SaveChanges();

        // Act
        var faturamentosNoPeriodo = await repositorioFaturamento?.SelecionarPorPeriodoAsync(DateTime.UtcNow, DateTime.UtcNow.AddDays(2))!;
        // Assert
        Assert.AreEqual(1, faturamentosNoPeriodo.Count);
        Assert.IsTrue(faturamentosNoPeriodo.Contains(faturamento1));
    }
}
