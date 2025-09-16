
using DotNet.Testcontainers.Containers;
using FizzWare.NBuilder;
using GestaoDeEstacionamento.Core.Dominio.ModuloFaturamento;
using GestaoDeEstacionamento.Core.Dominio.ModuloHospede;
using GestaoDeEstacionamento.Core.Dominio.ModuloTicket;
using GestaoDeEstacionamento.Core.Dominio.ModuloVaga;
using GestaoDeEstacionamento.Core.Dominio.ModuloVeiculo;
using GestaoDeEstacionamento.Infraestrutura.Orm.Compartilhado;
using GestaoDeEstacionamento.Infraestrutura.Orm.ModuloFaturamento;
using GestaoDeEstacionamento.Infraestrutura.Orm.ModuloHospede;
using GestaoDeEstacionamento.Infraestrutura.Orm.ModuloTicket;
using GestaoDeEstacionamento.Infraestrutura.Orm.ModuloVaga;
using GestaoDeEstacionamento.Infraestrutura.Orm.ModuloVeiculo;
using Testcontainers.PostgreSql;
using static System.Net.Mime.MediaTypeNames;

namespace GestaoDeEstacionamento.Testes.Integração.Compartilhado;
[TestClass]
public abstract class TestFixture
{
    protected GestaoDeEstacionamentoDbContext? dbContext;

    protected RepositorioHospedeEmOrm? repositorioHospede;
    protected RepositorioVeiculoEmOrm? repositorioVeiculo;
    protected RepositorioVagaEmOrm? repositorioVaga;
    protected RepositorioTicketEmOrm? repositorioTicket;
    protected RepositorioFaturamentoEmOrm? repositorioFaturamento;

    private static IDatabaseContainer? container;

    [AssemblyInitialize]
    public static async Task Setup(TestContext _)
    {
        container = new PostgreSqlBuilder()
            .WithImage("postgres:16")
            .WithName("gestao-estacionamento-testdb")
            .WithDatabase("GestaoDeEstacionamentoDb")
            .WithUsername("postgres")
            .WithPassword("YourStrongPassword")
            .WithCleanUp(true)
            .Build();

        await InicializarBancoDadosAsync(container);
    }

    [AssemblyCleanup]
    public static async Task Teardown()
    {
        await EncerrarBancoDadosAsync();
    }

    [TestInitialize]
    public void ConfigurarTestes()
    {
        if (container is null)
            throw new ArgumentNullException("O banco de dados não foi inicializado.");

        dbContext = GestaoDeEstacionamentoDbContextFactory.CriarDbContext(container.GetConnectionString());

        ConfigurarTabelas(dbContext);

        repositorioFaturamento = new RepositorioFaturamentoEmOrm(dbContext);
        repositorioTicket = new RepositorioTicketEmOrm(dbContext);
        repositorioVaga = new RepositorioVagaEmOrm(dbContext);
        repositorioVeiculo = new RepositorioVeiculoEmOrm(dbContext);
        repositorioHospede = new RepositorioHospedeEmOrm(dbContext);

        BuilderSetup.SetCreatePersistenceMethod<Hospede>(async hospede =>
                     await repositorioHospede.CadastrarAsync(hospede)  
                );
        BuilderSetup.SetCreatePersistenceMethod<IList<Hospede>>(async hospedes =>
                    await repositorioHospede.CadastrarEntidades(hospedes)
                );

        BuilderSetup.SetCreatePersistenceMethod<Veiculo>(async veiculos =>
                    await repositorioVeiculo.CadastrarAsync(veiculos)
                );
        BuilderSetup.SetCreatePersistenceMethod<IList<Veiculo>>(async veiculos =>
                    await repositorioVeiculo.CadastrarEntidades(veiculos)
                );

        BuilderSetup.SetCreatePersistenceMethod<Vaga>(async vaga =>
                    await repositorioVaga.CadastrarAsync(vaga)
                );
        BuilderSetup.SetCreatePersistenceMethod<IList<Vaga>>(async vagas =>
                    await repositorioVaga.CadastrarEntidades(vagas)
                );

        BuilderSetup.SetCreatePersistenceMethod<Ticket>(async ticket =>
                    await repositorioTicket.CadastrarAsync(ticket)
                );
        BuilderSetup.SetCreatePersistenceMethod<IList<Ticket>>(async tickets =>
                    await repositorioTicket.CadastrarEntidades(tickets)
                );

        BuilderSetup.SetCreatePersistenceMethod<Faturamento>(async faturamento =>
                    await repositorioFaturamento.CadastrarAsync(faturamento)
                );
        BuilderSetup.SetCreatePersistenceMethod<IList<Faturamento>>(async faturamentos =>
                    await repositorioFaturamento.CadastrarEntidades(faturamentos)
                );
    }

    private static void ConfigurarTabelas(GestaoDeEstacionamentoDbContext dbContext)
    {
        dbContext.Database.EnsureCreated();

        dbContext.Faturamentos.RemoveRange(dbContext.Faturamentos);
        dbContext.Tickets.RemoveRange(dbContext.Tickets);
        dbContext.Vagas.RemoveRange(dbContext.Vagas);
        dbContext.Veiculos.RemoveRange(dbContext.Veiculos);
        dbContext.Hospedes.RemoveRange(dbContext.Hospedes);

        dbContext.SaveChanges();
    }

    private static async Task InicializarBancoDadosAsync(IDatabaseContainer container)
    {
        await container.StartAsync();
    }

    private static async Task EncerrarBancoDadosAsync()
    {
        if (container is null)
            throw new ArgumentNullException("O Banco de dados não foi inicializado.");

        await container.StopAsync();
        await container.DisposeAsync();
    }
}