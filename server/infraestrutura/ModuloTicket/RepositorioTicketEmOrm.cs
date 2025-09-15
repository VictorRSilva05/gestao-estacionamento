using GestaoDeEstacionamento.Core.Dominio.ModuloTicket;
using GestaoDeEstacionamento.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;
using TesteFacil.Infraestrutura.Orm.Compartilhado;

namespace GestaoDeEstacionamento.Infraestrutura.Orm.ModuloTicket;
public class RepositorioTicketEmOrm(GestaoDeEstacionamentoDbContext contexto) : RepositorioBaseEmOrm<Ticket>(contexto), IRepositorioTicket
{
    public override async Task<List<Ticket>> SelecionarRegistrosAsync()
    {
        return await registros.Include(x => x.Vaga).ToListAsync();
    }

    public override async Task<Ticket?> SelecionarRegistroPorIdAsync(Guid id)
    {
        return await registros.Include(x => x.Vaga).FirstOrDefaultAsync(x => x.Id == id);
    }
}

