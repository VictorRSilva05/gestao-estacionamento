using GestaoDeEstacionamento.Core.Dominio.ModuloTicket;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoDeEstacionamento.Infraestrutura.Orm.ModuloTicket;
public class MapeadorTicketEmOrm : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.HasOne(x => x.Hospede);

        builder.HasOne(x => x.Veiculo);

        builder.HasOne(x => x.Vaga);

        builder.Property(x => x.Entrada)
            .IsRequired();

        builder.Property(x => x.Saida)
            .IsRequired();

        builder.Property(x => x.Observacao);

        builder.Property(x => x.Aberta)
            .IsRequired();
    }
}
