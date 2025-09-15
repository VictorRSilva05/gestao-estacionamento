using AutoMapper;
using GestaoDeEstacionamento.Core.Aplicacao.ModuloVaga.Commands;
using GestaoDeEstacionamento.WebApi.Models.ModuloVaga;
using System.Collections.Immutable;

namespace GestaoDeEstacionamento.WebApi.AutoMapper;

public class VagaModelsMappingProfile : Profile
{
    public VagaModelsMappingProfile()
    {
        CreateMap<CadastrarVagaRequest, CadastrarVagaCommand>();
        CreateMap<CadastrarVagaResult, CadastrarVagaResponse>();

        CreateMap<(Guid, EditarVagaRequest), EditarVagaCommand>()
            .ConvertUsing(src => new EditarVagaCommand(
                src.Item1,
                src.Item2.Nome,
                src.Item2.Ocupada
                ));

        CreateMap<EditarVagaResult, EditarVagaResponse>();

        CreateMap<Guid, ExcluirVagaCommand>()
            .ConstructUsing(src => new ExcluirVagaCommand(src));

        CreateMap<SelecionarVagaPorPlacaRequest, SelecionarVagaPorPlacaQuery>();
        CreateMap<SelecionarVagaPorPlacaResult, SelecionarVagaPorPlacaResponse>();


        CreateMap<SelecionarVagasRequest, SelecionarVagasQuery>();

        CreateMap<SelecionarVagasResult, SelecionarVagasResponse>()
            .ConvertUsing((src, dest, ctx) => new SelecionarVagasResponse(
                src.Vagas.Count,
                src?.Vagas.Select(c => ctx.Mapper.Map<SelecionarVagasDto>(c)).ToImmutableList() ?? ImmutableList<SelecionarVagasDto>.Empty
            ));

        CreateMap<Guid, SelecionarVagaPorIdQuery>()
            .ConvertUsing(src => new SelecionarVagaPorIdQuery(src));

        CreateMap<SelecionarVagaPorIdResult, SelecionarVagaPorIdResponse>()
            .ConvertUsing(src => new SelecionarVagaPorIdResponse(
                src.Id,
                src.Nome,
                src.Ocupada
            ));
    }
}
