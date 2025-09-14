using AutoMapper;
using GestaoDeEstacionamento.Core.Aplicacao.ModuloVaga.Commands;
using GestaoDeEstacionamento.Core.Dominio.ModuloVaga;
using System.Collections.Immutable;

namespace GestaoDeEstacionamento.Core.Aplicacao.AutoMapper;
public class VagaMappingProfile : Profile
{
    public VagaMappingProfile()
    {
        // Commands/Results de Cadastro
        CreateMap<CadastrarVagaCommand, Vaga>();
        CreateMap<Vaga, CadastrarVagaResult>();

        // Commands/Results de Edição
        CreateMap<EditarVagaCommand, Vaga>();
        CreateMap<Vaga, EditarVagaResult>();

        // Commands/Results de Seleção Por Id
        CreateMap<Vaga, SelecionarVagaPorIdResult>()
            .ConvertUsing(src => new SelecionarVagaPorIdResult(
                src.Id,
                src.Nome,
                src.Ocupada
            ));

        // Commands/Results de Seleção
        CreateMap<Vaga, SelecionarVagasDto>();

        CreateMap<IEnumerable<Vaga>, SelecionarVagasResult>()
            .ConvertUsing((src, dest, ctx) =>
                new SelecionarVagasResult(
                    src?.Select(c => ctx.Mapper.Map<SelecionarVagasDto>(c)).ToImmutableList() ?? ImmutableList<SelecionarVagasDto>.Empty
                )
            );
    }
}
