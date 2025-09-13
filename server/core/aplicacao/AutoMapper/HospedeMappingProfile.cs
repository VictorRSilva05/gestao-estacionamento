using AutoMapper;
using GestaoDeEstacionamento.Core.Aplicacao.ModuloHospede.Commands;
using GestaoDeEstacionamento.Core.Dominio.ModuloHospede;
using System.Collections.Immutable;

namespace GestaoDeEstacionamento.Core.Aplicacao.AutoMapper;
public class HospedeMappingProfile : Profile
{
    public HospedeMappingProfile()
    {
        CreateMap<CadastrarHospedeCommand, Hospede>();
        CreateMap<Hospede, CadastrarHospedeResult>();

        CreateMap<EditarHospedeCommand, Hospede>();
        CreateMap<Hospede, EditarHospedeResult>();

        CreateMap<Hospede, SelecionarHospedePorIdResult>()
            .ConvertUsing(src => new SelecionarHospedePorIdResult(
                src.Id,
                src.Nome
            ));

        CreateMap<Hospede, SelecionarHospedePorIdResult>()
            .ConvertUsing(src => new SelecionarHospedePorIdResult(
                src.Id,
                src.Nome
                ));

        CreateMap<Hospede, SelecionarHospedesDto>();

        CreateMap<IEnumerable<Hospede>, SelecionarHospedesResult>()
         .ConvertUsing((src, dest, ctx) =>
             new SelecionarHospedesResult(
                 src?.Select(c => ctx.Mapper.Map<SelecionarHospedesDto>(c)).ToImmutableList() ?? ImmutableList<SelecionarHospedesDto>.Empty
             )
         );
    }
}
