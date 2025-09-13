using AutoMapper;
using GestaoDeEstacionamento.Core.Aplicacao.ModuloHospede.Commands;
using GestaoDeEstacionamento.WebApi.Models.ModuloHospede;

namespace GestaoDeEstacionamento.WebApi.AutoMapper;

public class HospedeModelsMappingProfile : Profile
{
    public HospedeModelsMappingProfile()
    {
        CreateMap<CadastrarHospedeRequest, CadastrarHospedeCommand>();
        CreateMap<CadastrarHospedeResult, CadastrarHospedeResponse>();

        CreateMap<(Guid, EditarHospedeRequest), EditarHospedeCommand>()
            .ConvertUsing(src => new EditarHospedeCommand(
                src.Item1,
                src.Item2.Nome
            ));

        CreateMap<EditarHospedeResult, EditarHospedeResponse>();

        CreateMap<Guid, ExcluirHospedeCommand>()
            .ConstructUsing(src => new ExcluirHospedeCommand(src));

        CreateMap<SelecionarHospedePorIdResult, SelecionarHospedePorIdResponse>()
            .ConvertUsing(src => new SelecionarHospedePorIdResponse(
                src.Id,
                src.Nome
            ));

        CreateMap<SelecionarHospedesRequest, SelecionarHospedesQuery>();
    }
}
