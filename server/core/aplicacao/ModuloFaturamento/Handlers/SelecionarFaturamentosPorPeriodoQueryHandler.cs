using AutoMapper;
using eAgenda.Core.Aplicacao.Compartilhado;
using FluentResults;
using GestaoDeEstacionamento.Core.Aplicacao.ModuloFaturamento.Commands;
using GestaoDeEstacionamento.Core.Dominio.ModuloFaturamento;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Immutable;

namespace GestaoDeEstacionamento.Core.Aplicacao.ModuloFaturamento.Handlers;
public class SelecionarFaturamentosPorPeriodoQueryHandler(
    IRepositorioFaturamento repositorioFaturamento,
    IMapper mapper,
    ILogger<SelecionarFaturamentosPorPeriodoQueryHandler> logger
    ) : IRequestHandler<SelecionarFaturamentosPorPeriodoQuery, Result<SelecionarFaturamentosResult>>
{
    public async Task<Result<SelecionarFaturamentosResult>> Handle(SelecionarFaturamentosPorPeriodoQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var faturamentos = await repositorioFaturamento
                .SelecionarPorPeriodoAsync(query.DataInicio, query.DataFim);

            var dtoList = mapper.Map<List<SelecionarFaturamentosDto>>(faturamentos);

            return Result.Ok(new SelecionarFaturamentosResult(dtoList.ToImmutableList()));
        }
        catch (Exception ex)
        {
            logger.LogError(ex,
                "Erro ao selecionar faturamentos no período de {DataInicio} a {DataFim}",
                query.DataInicio, query.DataFim);

            return Result.Fail(ResultadosErro.ExcecaoInternaErro(ex));
        }
    }
}
