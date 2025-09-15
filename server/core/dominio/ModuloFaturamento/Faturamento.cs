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
        Id = Guid.NewGuid();
        Diarias = CalcularDiarias();
        Ticket = ticket;
        Total = Diarias * ValorDiaria;
    }

    public override void AtualizarRegistro(Faturamento registroEditado)
    {
        throw new NotImplementedException();
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
