using AutoMapper;
using eAgenda.Core.Aplicacao.Compartilhado;
using FluentResults;
using GestaoDeEstacionamento.Core.Aplicacao.ModuloFaturamento.Commands;
using GestaoDeEstacionamento.Core.Dominio.ModuloFaturamento;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace GestaoDeEstacionamento.Core.Aplicacao.ModuloFaturamento.Handlers;
public class SelecionarFaturamentosQueryHandler(
    IRepositorioFaturamento repositorioFaturamento,
    IMapper mapper,
    ILogger<SelecionarFaturamentosQueryHandler> logger
    ) : IRequestHandler<SelecionarFaturamentosQuery, Result<SelecionarFaturamentosResult>>
{
    public async Task<Result<SelecionarFaturamentosResult>> Handle(SelecionarFaturamentosQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var registros = query.Quantidade.HasValue ?
                await repositorioFaturamento.SelecionarRegistrosAsync(query.Quantidade.Value) :
                await repositorioFaturamento.SelecionarRegistrosAsync();

            var result = mapper.Map<SelecionarFaturamentosResult>(registros);

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Ocorreu um erro durante a seleção de {@Registros}.",
                query
            );

            return Result.Fail(ResultadosErro.ExcecaoInternaErro(ex));
        }
    }
}
