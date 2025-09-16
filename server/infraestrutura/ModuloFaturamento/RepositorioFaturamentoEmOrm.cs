using GestaoDeEstacionamento.Core.Dominio.ModuloFaturamento;
using GestaoDeEstacionamento.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;
using TesteFacil.Infraestrutura.Orm.Compartilhado;

namespace GestaoDeEstacionamento.Infraestrutura.Orm.ModuloFaturamento;
public class RepositorioFaturamentoEmOrm(GestaoDeEstacionamentoDbContext contexto) : RepositorioBaseEmOrm<Faturamento>(contexto), IRepositorioFaturamento
{
    public override async Task<List<Faturamento>> SelecionarRegistrosAsync()
    {
        return await registros
            .Include(x => x.Ticket)
            .ToListAsync();
    }

    public override async Task<Faturamento?> SelecionarRegistroPorIdAsync(Guid id)
    {
        return await registros
            .Include(x => x.Ticket)  
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Faturamento>> SelecionarPorPeriodoAsync(DateTime dataInicio, DateTime dataFim)
    {
        return await registros
            .Include(x => x.Ticket)
            .Where(f => f.Ticket.Entrada.Date >= dataInicio.Date && f.Ticket.Saida.Value.Date <= dataFim.Date)
            .ToListAsync();
    }
}
