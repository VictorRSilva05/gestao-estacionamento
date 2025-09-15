using GestaoDeEstacionamento.Core.Dominio.ModuloTicket;
using GestaoDeEstacionamento.Core.Dominio.ModuloVaga;
using GestaoDeEstacionamento.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;
using TesteFacil.Infraestrutura.Orm.Compartilhado;

namespace GestaoDeEstacionamento.Infraestrutura.Orm.ModuloVaga;
public class RepositorioVagaEmOrm(GestaoDeEstacionamentoDbContext contexto) : RepositorioBaseEmOrm<Vaga>(contexto), IRepositorioVaga
{
    public override async Task<List<Vaga>> SelecionarRegistrosAsync()
    {
        return await registros
            .Include(x => x.Veiculo)
            .ToListAsync();
    }

    public override async Task<Vaga?> SelecionarRegistroPorIdAsync(Guid id)
    {
        return await registros
            .Include(x => x.Veiculo)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Vaga?> SelecionarPorPlacaAsync(string placa)
    {
        return await registros
            .Include(x => x.Veiculo)
            .FirstOrDefaultAsync(x => x.Veiculo != null && x.Veiculo.Placa == placa);
    }
}
