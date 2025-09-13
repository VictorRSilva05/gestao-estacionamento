using Microsoft.EntityFrameworkCore;

namespace GestaoDeEstacionamento.Infraestrutura.Orm.Compartilhado;
public static class GestaoDeEstacionamentoDbContextFactory
{
    public static GestaoDeEstacionamentoDbContext CriarDbContext(string connectionString)
    {
        var options = new DbContextOptionsBuilder<GestaoDeEstacionamentoDbContext>()
            .UseNpgsql(connectionString)
            .Options;

        var dbContext = new GestaoDeEstacionamentoDbContext(options);

        return dbContext;
    }
}
