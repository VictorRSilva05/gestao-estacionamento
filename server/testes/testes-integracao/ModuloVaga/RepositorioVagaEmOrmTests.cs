using GestaoDeEstacionamento.Core.Dominio.ModuloVaga;
using GestaoDeEstacionamento.Testes.Integração.Compartilhado;

namespace GestaoDeEstacionamento.Testes.Integração.ModuloVaga;

[TestClass]
[TestCategory("Testes de Integração de Vaga")]
public sealed class RepositorioVagaEmOrmTests : TestFixture
{
    [TestMethod]
    public async Task Deve_Cadastrar_Vaga_Corretamente()
    {
        // Arrange
        var vaga = new Vaga("A1");

        // Act
        await repositorioVaga?.CadastrarAsync(vaga)!;
        dbContext?.SaveChanges();

        // Assert
        var vagaEncontrada = repositorioVaga?.SelecionarRegistroPorIdAsync(vaga.Id).Result;
        Assert.AreEqual(vaga, vagaEncontrada);
    }

    [TestMethod]
    public async Task Deve_Editar_Vaga_Corretamente()
    {
        // Arrange
        var vaga = new Vaga("A1");
        await repositorioVaga?.CadastrarAsync(vaga)!;
        dbContext?.SaveChanges();
        var vagaEditada = new Vaga("A1 Editada");

        // Act
        var conseguiuEditar = await repositorioVaga?.EditarAsync(vaga.Id, vagaEditada)!;
        dbContext?.SaveChanges();

        // Assert
        var vagaEncontrada = repositorioVaga?.SelecionarRegistroPorIdAsync(vaga.Id).Result;
        Assert.AreEqual(vaga, vagaEncontrada);
        Assert.IsTrue(conseguiuEditar);
    }

    [TestMethod]
    public async Task Deve_Excluir_Vaga_Corretamente()
    {
        // Arrange
        var vaga = new Vaga("A1");
        await repositorioVaga?.CadastrarAsync(vaga)!;
        dbContext?.SaveChanges();

        // Act
        var conseguiuExcluir = await repositorioVaga?.ExcluirAsync(vaga.Id)!;
        dbContext?.SaveChanges();

        // Assert
        var vagaEncontrada = repositorioVaga?.SelecionarRegistroPorIdAsync(vaga.Id).Result;
        Assert.IsNull(vagaEncontrada);
        Assert.IsTrue(conseguiuExcluir);
    }

    [TestMethod]
    public async Task Deve_Selecionar_Todas_Vagas_Corretamente()
    {
        // Arrange
        var vaga1 = new Vaga("A1");
        var vaga2 = new Vaga("A2");
        var vaga3 = new Vaga("A3");
        await repositorioVaga?.CadastrarEntidades(new List<Vaga> { vaga1, vaga2, vaga3 })!;
        dbContext?.SaveChanges();

        // Act
        var vagas = await repositorioVaga?.SelecionarRegistrosAsync()!;

        // Assert
        Assert.AreEqual(3, vagas?.Count);
        CollectionAssert.Contains(vagas!, vaga1);
        CollectionAssert.Contains(vagas!, vaga2);
        CollectionAssert.Contains(vagas!, vaga3);
    }

    [TestMethod]
    public async Task Deve_Selecionar_Vagas_Com_Quantidade_Especifica_Corretamente()
    {
        // Arrange
        var vaga1 = new Vaga("A1");
        var vaga2 = new Vaga("A2");
        var vaga3 = new Vaga("A3");
        await repositorioVaga?.CadastrarEntidades(new List<Vaga> { vaga1, vaga2, vaga3 })!;
        dbContext?.SaveChanges();

        // Act
        var vagas = await repositorioVaga?.SelecionarRegistrosAsync(2)!;

        // Assert
        Assert.AreEqual(2, vagas?.Count);
    }

    [TestMethod]
    public async Task Deve_Selecionar_Vaga_Por_Id_Corretamente()
    {
        // Arrange
        var vaga = new Vaga("A1");
        await repositorioVaga?.CadastrarAsync(vaga)!;
        dbContext?.SaveChanges();

        // Act
        var vagaEncontrada = await repositorioVaga?.SelecionarRegistroPorIdAsync(vaga.Id)!;

        // Assert
        Assert.AreEqual(vaga, vagaEncontrada);
    }
}
