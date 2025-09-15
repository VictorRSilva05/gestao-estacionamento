using GestaoDeEstacionamento.Core.Dominio.Compartilhado;
using GestaoDeEstacionamento.Core.Dominio.ModuloVeiculo;

namespace GestaoDeEstacionamento.Core.Dominio.ModuloVaga;
public class Vaga : EntidadeBase<Vaga>
{
    public string Nome { get; set; }
    public bool Ocupada {  get; set; }
    public Veiculo? Veiculo { get; set; }

    public Vaga() { }

    public Vaga(string nome) : this()
    {
        Nome = nome;
        Ocupada = false;
        Veiculo = null;
    }

    public override void AtualizarRegistro(Vaga registroEditado)
    {
        Nome = registroEditado.Nome;
        Ocupada = registroEditado.Ocupada;
    }

    public void OcuparVaga(Veiculo veiculo)
    {
        Ocupada = true;
        Veiculo = veiculo;
    }

    public void DesocuparVaga()
    {
        Ocupada = false;
        Veiculo = null;
    }
}
