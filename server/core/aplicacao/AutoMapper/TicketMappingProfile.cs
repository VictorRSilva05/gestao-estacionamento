using AutoMapper;
using GestaoDeEstacionamento.Core.Aplicacao.ModuloTicket.Commands;
using GestaoDeEstacionamento.Core.Dominio.ModuloTicket;
using System.Collections.Immutable;

namespace GestaoDeEstacionamento.Core.Aplicacao.AutoMapper;
public class TicketMappingProfile : Profile
{
    public TicketMappingProfile()
    {
        CreateMap<CadastrarTicketCommand, Ticket>();
        CreateMap<Ticket, CadastrarTicketResult>();

        CreateMap<EditarTicketCommand, Ticket>();
        CreateMap<Ticket, EditarTicketResult>();

        CreateMap<Ticket, SelecionarTicketPorIdResult>()
            .ConvertUsing(src => new SelecionarTicketPorIdResult(
                src.Id,
                src.Hospede,
                src.Veiculo,
                src.Vaga,
                src.Entrada,
                src.Saida,
                src.Observacao,
                src.Aberta
            ));

        CreateMap<Ticket, SelecionarTicketsDto>()
           .ConvertUsing(src => new SelecionarTicketsDto(
                src.Id,
                src.Hospede,
                src.Veiculo,
                src.Vaga,
                src.Entrada,
                src.Saida,
                src.Observacao,
                src.Aberta
            ));

        CreateMap<IEnumerable<Ticket>, SelecionarTicketsResult>()
         .ConvertUsing((src, dest, ctx) =>
             new SelecionarTicketsResult(
                 src?.Select(c => ctx.Mapper.Map<SelecionarTicketsDto>(c)).ToImmutableList() ?? ImmutableList<SelecionarTicketsDto>.Empty
             )
         );
    }
}
