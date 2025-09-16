using GestaoDeEstacionamento.Testes.Integração.Compartilhado;

namespace GestaoDeEstacionamento.Testes.Integração.ModuloVeiculo;

[TestClass]
[TestCategory("Testes de Integração de Veiculo")]
public class RepositorioVeiculoEmOrmTests : TestFixture
{
    [TestMethod]
    public async Task Deve_Cadastrar_Veiculo_Corretamente()
    {
        // Arrange
        var veiculo = new Core.Dominio.ModuloVeiculo.Veiculo("ABC-1234", "Modelo X", "Cor Y");

        // Act
        await repositorioVeiculo?.CadastrarAsync(veiculo)!;
        dbContext?.SaveChanges();

        // Assert
        var veiculoEncontrado = repositorioVeiculo?.SelecionarRegistroPorIdAsync(veiculo.Id).Result;
        Assert.AreEqual(veiculo, veiculoEncontrado);
    }

    [TestMethod]
    public async Task Deve_Editar_Veiculo_Corretamente()
    {
        // Arrange
        var veiculo = new Core.Dominio.ModuloVeiculo.Veiculo("ABC-1234", "Modelo X", "Cor Y");
        await repositorioVeiculo?.CadastrarAsync(veiculo)!;
        dbContext?.SaveChanges();
        var veiculoEditado = new Core.Dominio.ModuloVeiculo.Veiculo("XYZ-5678", "Modelo Z", "Cor W");
        
        // Act
        var conseguiuEditar = await repositorioVeiculo?.EditarAsync(veiculo.Id, veiculoEditado)!;
        dbContext?.SaveChanges();
        
        // Assert
        var veiculoEncontrado = repositorioVeiculo?.SelecionarRegistroPorIdAsync(veiculo.Id).Result;
        Assert.AreEqual(veiculo, veiculoEncontrado);
        Assert.IsTrue(conseguiuEditar);
    }

    [TestMethod]
    public async Task Deve_Excluir_Veiculo_Corretamente()
    {
        // Arrange
        var veiculo = new Core.Dominio.ModuloVeiculo.Veiculo("ABC-1234", "Modelo X", "Cor Y");
        await repositorioVeiculo?.CadastrarAsync(veiculo)!;
        dbContext?.SaveChanges();

        // Act
        var conseguiuExcluir = await repositorioVeiculo?.ExcluirAsync(veiculo.Id)!;
        dbContext?.SaveChanges();

        // Assert
        var veiculoEncontrado = repositorioVeiculo?.SelecionarRegistroPorIdAsync(veiculo.Id).Result;
        Assert.IsNull(veiculoEncontrado);
        Assert.IsTrue(conseguiuExcluir);
    }

    [TestMethod]
    public async Task Deve_Selecionar_Veiculo_Por_Id_Corretamente()
    {
        // Arrange
        var veiculo = new Core.Dominio.ModuloVeiculo.Veiculo("ABC-1234", "Modelo X", "Cor Y");
        await repositorioVeiculo?.CadastrarAsync(veiculo)!;
        dbContext?.SaveChanges();

        // Act
        var veiculoEncontrado = await repositorioVeiculo?.SelecionarRegistroPorIdAsync(veiculo.Id)!;

        // Assert
        Assert.AreEqual(veiculo, veiculoEncontrado);
    }

    [TestMethod]
    public async Task Deve_Selecionar_Quantidade_Especifica_De_Veiculos_Corretamente()
    {
        // Arrange
        var veiculos = new List<Core.Dominio.ModuloVeiculo.Veiculo>
        {
            new Core.Dominio.ModuloVeiculo.Veiculo("ABC-1234", "Modelo X", "Cor Y"),
            new Core.Dominio.ModuloVeiculo.Veiculo("DEF-5678", "Modelo A", "Cor B"),
            new Core.Dominio.ModuloVeiculo.Veiculo("GHI-9012", "Modelo C", "Cor D")
        };
        foreach (var veiculo in veiculos)
            await repositorioVeiculo?.CadastrarAsync(veiculo)!;
        dbContext?.SaveChanges();

        // Act
        var quantidadeParaSelecionar = 2;
        var veiculosSelecionados = await repositorioVeiculo?.SelecionarRegistrosAsync(quantidadeParaSelecionar)!;

        // Assert
        Assert.AreEqual(quantidadeParaSelecionar, veiculosSelecionados.Count);
    }
}
