using GestaoDeEstacionamento.Core.Dominio.Compartilhado;

namespace GestaoDeEstacionamento.Core.Dominio.ModuloVaga;
public class Vaga : EntidadeBase<Vaga>
{
    public string Nome { get; set; }
    public bool Ocupada {  get; set; }

    public Vaga() { }

    public Vaga(string nome) : this()
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Ocupada = false;
    }

    public override void AtualizarRegistro(Vaga registroEditado)
    {
        Nome = registroEditado.Nome;
        Ocupada = registroEditado.Ocupada;
    }

    public void OcuparVaga()
    {
        Ocupada = true;
    }

    public void DesocuparVaga()
    {
        Ocupada = false;
    }
}
