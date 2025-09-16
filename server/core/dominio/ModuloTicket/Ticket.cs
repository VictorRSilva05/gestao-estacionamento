using GestaoDeEstacionamento.Core.Dominio.Compartilhado;
using GestaoDeEstacionamento.Core.Dominio.ModuloHospede;
using GestaoDeEstacionamento.Core.Dominio.ModuloVaga;
using GestaoDeEstacionamento.Core.Dominio.ModuloVeiculo;

namespace GestaoDeEstacionamento.Core.Dominio.ModuloTicket;
public class Ticket : EntidadeBase<Ticket>
{
    public Hospede Hospede { get; set; }
    public Veiculo Veiculo { get; set; }
    public Vaga Vaga { get; set; }
    public DateTime Entrada { get; set; }
    public DateTime? Saida { get; set; }
    public string? Observacao { get; set; }
    public bool Aberta { get; set; }

    public Ticket() { }

    public Ticket(Hospede hospede, Veiculo veiculo, Vaga vaga, DateTime saida, string? observacao) : this()
    {
        Hospede = hospede;
        Veiculo = veiculo;
        Vaga = vaga;
        Entrada = DateTime.UtcNow;
        Saida = saida;
        Observacao = observacao;
        Aberta = true;
    }

    public void AbrirTicket()
    {
        Aberta = true;
        Vaga.OcuparVaga(Veiculo);
    }
    public void FecharTicket()
    {
        Aberta = false;
        Vaga.DesocuparVaga();
    }

    public override void AtualizarRegistro(Ticket registroEditado)
    {
        Hospede = registroEditado.Hospede;
        Veiculo = registroEditado.Veiculo;
        Vaga = registroEditado.Vaga;
        Entrada = registroEditado.Entrada;
        Saida = registroEditado.Saida;
        Observacao = registroEditado.Observacao;
        Aberta = registroEditado.Aberta;
    }
}
