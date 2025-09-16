using GestaoDeEstacionamento.Core.Dominio.ModuloHospede;
using GestaoDeEstacionamento.Testes.Integração.Compartilhado;
using Org.BouncyCastle.Crypto;

namespace GestaoDeEstacionamento.Testes.Integração.ModuloHospede;

[TestClass]
[TestCategory("Testes de Integração de Hospede")]
public sealed class RepositorioHospedeEmOrmTests : TestFixture
{
    [TestMethod]
    public async Task Deve_Cadastrar_Hospede_Corretamente()
    {
        // Arrange
        var hospede = new Hospede("Tio Guda", "999.999.999-99");

        // Act
        await repositorioHospede?.CadastrarAsync(hospede)!;
        dbContext?.SaveChanges();

        // Assert
        var hospedeEncontrado = repositorioHospede?.SelecionarRegistroPorIdAsync(hospede.Id).Result;
        Assert.AreEqual(hospede, hospedeEncontrado);
    }

    [TestMethod]
    public async Task Deve_Editar_Hospede_Corretamente()
    {
        // Arrange
        var hospede = new Hospede("Tio Guda", "999.999.999-99");
        await repositorioHospede?.CadastrarAsync(hospede)!;
        dbContext?.SaveChanges();

        var hospedeEditado = new Hospede("Tio Guda Editado", "999.999.999-99");

        // Act
        var conseguiuEditar = await repositorioHospede?.EditarAsync(hospede.Id, hospedeEditado)!;
        dbContext?.SaveChanges();

        // Assert
        var hospedeEncontrado = repositorioHospede?.SelecionarRegistroPorIdAsync(hospede.Id).Result;
        Assert.AreEqual(hospede, hospedeEncontrado);
        Assert.IsTrue(conseguiuEditar);
    }

    [TestMethod]
    public async Task Deve_Excluir_Hospede_Corretamente()
    {
        // Arrange
        var hospede = new Hospede("Tio Guda", "999.999.999-99");
        await repositorioHospede?.CadastrarAsync(hospede)!;
        dbContext?.SaveChanges();

        // Act
        var conseguiuExcluir = await repositorioHospede?.ExcluirAsync(hospede.Id)!;
        dbContext?.SaveChanges();

        // Assert
        var hospedeEncontrado = repositorioHospede?.SelecionarRegistroPorIdAsync(hospede.Id).Result;
        Assert.IsNull(hospedeEncontrado);
        Assert.IsTrue(conseguiuExcluir);
    }

    [TestMethod]
    public void Deve_Selecionar_Todos_Hospedes_Corretamente()
    {
        // Arrange
        var hospede1 = new Hospede("Tio Guda", "999.999.999-99");
        var hospede2 = new Hospede("Tio Guda 2", "888.888.888-88");
        var hospede3 = new Hospede("Tio Guda 3", "777.777.777-77");
        var hospedes = new List<Hospede> { hospede1, hospede2, hospede3 };
        repositorioHospede?.CadastrarEntidades(hospedes).Wait();
        dbContext?.SaveChanges();

        // Act
        var hospedesEncontrados = repositorioHospede?.SelecionarRegistrosAsync().Result;

        // Assert
        CollectionAssert.AreEquivalent(hospedes, hospedesEncontrados!.ToList());
    }

    [TestMethod]
    public void Deve_Selecionar_Hospede_Por_Id_Corretamente()
    {
        // Arrange
        var hospede = new Hospede("Tio Guda", "999.999.999-99");
        repositorioHospede?.CadastrarAsync(hospede).Wait();
        dbContext?.SaveChanges();

        // Act
        var hospedeEncontrado = repositorioHospede?.SelecionarRegistroPorIdAsync(hospede.Id).Result;

        // Assert
        Assert.AreEqual(hospede, hospedeEncontrado);
    }

    [TestMethod]
    public void Deve_Selecionar_Quantidade_Especifica_De_Hospedes_Corretamente()
    {
        // Arrange
        var hospede1 = new Hospede("Tio Guda", "999.999.999-99");
        var hospede2 = new Hospede("Tio Guda 2", "888.888.888-88");
        var hospede3 = new Hospede("Tio Guda 3", "777.777.777-77");
        var hospedes = new List<Hospede> { hospede1, hospede2, hospede3 };
        repositorioHospede?.CadastrarEntidades(hospedes).Wait();
        dbContext?.SaveChanges();

        // Act
        var quantidadeParaSelecionar = 2;
        var hospedesEncontrados = repositorioHospede?.SelecionarRegistrosAsync(quantidadeParaSelecionar).Result;

        // Assert
        Assert.AreEqual(quantidadeParaSelecionar, hospedesEncontrados?.Count);
   }
}