using AutoMapper;
using GestaoDeEstacionamento.Core.Aplicacao.ModuloVeiculo.Commands;
using GestaoDeEstacionamento.Core.Dominio.ModuloVeiculo;
using System.Collections.Immutable;

namespace GestaoDeEstacionamento.Core.Aplicacao.AutoMapper;
public class VeiculoMappingProfile : Profile
{
    public VeiculoMappingProfile()
    {
        CreateMap<CadastrarVeiculoCommand, Veiculo>();
        CreateMap<Veiculo, CadastrarVeiculoResult>();

        CreateMap<EditarVeiculoCommand, Veiculo>();
        CreateMap<Veiculo, EditarVeiculoResult>();

        CreateMap<Veiculo, SelecionarVeiculoPorIdResult>()
            .ConvertUsing(src => new SelecionarVeiculoPorIdResult(
                src.Id,
                src.Placa,
                src.Modelo,
                src.Cor
                ));

        CreateMap<Veiculo, SelecionarVeiculosDto>();

        CreateMap<IEnumerable<Veiculo>, SelecionarVeiculosResult>()
            .ConvertUsing((src, dest, ctx) =>
                new SelecionarVeiculosResult(
                    src?.Select(c => ctx.Mapper.Map<SelecionarVeiculosDto>(c)).ToImmutableList() ?? ImmutableList<SelecionarVeiculosDto>.Empty
                )
            );
    }
}
