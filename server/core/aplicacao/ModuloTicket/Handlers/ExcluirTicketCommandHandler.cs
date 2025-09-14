using AutoMapper;
using eAgenda.Core.Aplicacao.Compartilhado;
using FluentResults;
using FluentValidation;
using GestaoDeEstacionamento.Core.Aplicacao.ModuloTicket.Commands;
using GestaoDeEstacionamento.Core.Dominio.Compartilhado;
using GestaoDeEstacionamento.Core.Dominio.ModuloHospede;
using GestaoDeEstacionamento.Core.Dominio.ModuloTicket;
using GestaoDeEstacionamento.Core.Dominio.ModuloVaga;
using GestaoDeEstacionamento.Core.Dominio.ModuloVeiculo;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GestaoDeEstacionamento.Core.Aplicacao.ModuloTicket.Handlers;
public class ExcluirTicketCommandHandler(
    IRepositorioTicket repositorioTicket,
    IUnitOfWork unitOfWork,
    ILogger<ExcluirTicketCommandHandler> logger
    ) : IRequestHandler<ExcluirTicketCommand, Result<ExcluirTicketResult>>
{
    public async Task<Result<ExcluirTicketResult>> Handle(ExcluirTicketCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await repositorioTicket.ExcluirAsync(command.Id);

            await unitOfWork.CommitAsync();

            var result = new ExcluirTicketResult();

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackAsync();

            logger.LogError(
                ex,
                "Ocorreu um erro durante a exclusão de {@Registro}.",
                command
            );

            return Result.Fail(ResultadosErro.ExcecaoInternaErro(ex));
        }
    }
}
