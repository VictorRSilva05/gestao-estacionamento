using GestaoDeEstacionamento.Core.Dominio.ModuloFaturamento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoDeEstacionamento.Infraestrutura.Orm.ModuloFaturamento;
public class MapeadorFaturamentoEmOrm : IEntityTypeConfiguration<Faturamento>
{
    public void Configure(EntityTypeBuilder<Faturamento> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.Diarias)
            .IsRequired();

        builder.Property(x => x.Total)
            .IsRequired();

        builder.HasOne(x => x.Ticket);

        builder.Property(x => x.ValorDiaria)
            .IsRequired();
    }
}
