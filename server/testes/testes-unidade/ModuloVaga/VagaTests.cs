using GestaoDeEstacionamento.Core.Dominio.ModuloVaga;
using GestaoDeEstacionamento.Core.Dominio.ModuloVeiculo;

namespace GestaoDeEstacionamento.Testes.Unidade.ModuloVaga;

[TestClass]
[TestCategory("Testes de Unidade de Vaga")]
public sealed class VagaTests
{
    private Veiculo? veiculo;
    private Vaga? vaga;

    [TestMethod]
    public void Deve_Ocupar_Vaga_Corretamente()
    {
        // Arrange
        veiculo = new Veiculo("ABC-1234", "Modelo X", "Cor Y");
        vaga = new Vaga("A1");

        // Act
        vaga.OcuparVaga(veiculo);

        // Assert
        Assert.IsTrue(vaga.Ocupada);
        Assert.AreEqual(veiculo, vaga.Veiculo);
    }

    [TestMethod]
    public void Deve_Desocupar_Vaga_Corretamente()
    {
        // Arrange
        veiculo = new Veiculo("ABC-1234", "Modelo X", "Cor Y");
        vaga = new Vaga("A1");
        vaga.OcuparVaga(veiculo);

        // Act
        vaga.DesocuparVaga();

        // Assert
        Assert.IsFalse(vaga.Ocupada);
        Assert.IsNull(vaga.Veiculo);
    }
}
