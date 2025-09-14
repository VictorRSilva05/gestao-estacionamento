using AutoMapper;
using eAgenda.Core.Aplicacao.Compartilhado;
using FluentResults;
using GestaoDeEstacionamento.Core.Aplicacao.ModuloTicket.Commands;
using GestaoDeEstacionamento.Core.Dominio.ModuloTicket;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace GestaoDeEstacionamento.Core.Aplicacao.ModuloTicket.Handlers;
public class SelecionarTicketsQueryHandler(
    IRepositorioTicket repositorioTicket,
    IMapper mapper,
    ILogger<SelecionarTicketsQueryHandler> logger
    ) : IRequestHandler<SelecionarTicketsQuery, Result<SelecionarTicketsResult>>
{
    public async Task<Result<SelecionarTicketsResult>> Handle(SelecionarTicketsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var registros = query.Quantidade.HasValue ?
                await repositorioTicket.SelecionarRegistrosAsync(query.Quantidade.Value) :
                await repositorioTicket.SelecionarRegistrosAsync();

            var result = mapper.Map<SelecionarTicketsResult>(registros);

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
