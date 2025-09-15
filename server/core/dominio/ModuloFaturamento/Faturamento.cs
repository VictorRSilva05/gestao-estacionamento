using GestaoDeEstacionamento.Core.Dominio.Compartilhado;
using GestaoDeEstacionamento.Core.Dominio.ModuloTicket;

namespace GestaoDeEstacionamento.Core.Dominio.ModuloFaturamento;
public class Faturamento : EntidadeBase<Faturamento>
{
    public decimal ValorDiaria = 35;
    public int Diarias;
    public Ticket Ticket { get; set; }
    public decimal Total;

    public Faturamento() { }

    public Faturamento(Ticket ticket)
    {
        Ticket = ticket;
    }

    public override void AtualizarRegistro(Faturamento registroEditado)
    {
        throw new NotImplementedException();
    }

    public void CalcularFaturamento()
    {
        Diarias = CalcularDiarias();
        Total = Diarias * ValorDiaria;
    }
    private int CalcularDiarias()
    {
        TimeSpan duracao = Ticket.Saida.Value - Ticket.Entrada;
         int diarias = (int)Math.Ceiling(duracao.TotalDays);

        if (diarias == 0)
            diarias = 1; 

        return diarias;
    }
}
