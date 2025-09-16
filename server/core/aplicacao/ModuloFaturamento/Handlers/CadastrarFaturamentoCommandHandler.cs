using AutoMapper;
using eAgenda.Core.Aplicacao.Compartilhado;
using FluentResults;
using GestaoDeEstacionamento.Core.Aplicacao.ModuloFaturamento.Commands;
using GestaoDeEstacionamento.Core.Aplicacao.ModuloTicket.Commands;
using GestaoDeEstacionamento.Core.Dominio.Compartilhado;
using GestaoDeEstacionamento.Core.Dominio.ModuloAutenticacao;
using GestaoDeEstacionamento.Core.Dominio.ModuloFaturamento;
using GestaoDeEstacionamento.Core.Dominio.ModuloHospede;
using GestaoDeEstacionamento.Core.Dominio.ModuloTicket;
using GestaoDeEstacionamento.Core.Dominio.ModuloVaga;
using GestaoDeEstacionamento.Core.Dominio.ModuloVeiculo;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GestaoDeEstacionamento.Core.Aplicacao.ModuloFaturamento.Handlers;
public class CadastrarFaturamentoCommandHandler(
    IRepositorioTicket repositorioTicket,
    IRepositorioFaturamento repositorioFaturamento,
    ITenantProvider tenantProvider,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ILogger<CadastrarFaturamentoCommandHandler> logger
    ) : IRequestHandler<CadastrarFaturamentoCommand, Result<CadastrarFaturamentoResult>>
{
    public async Task<Result<CadastrarFaturamentoResult>> Handle(CadastrarFaturamentoCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var ticketExiste = await repositorioTicket.SelecionarRegistroPorIdAsync(command.TicketId);
            if (ticketExiste is null)
                return Result.Fail("Ticket não encontrado.");

            if (!ticketExiste.Aberta)
                return Result.Fail("O ticket já está fechado");

            var faturamento = mapper.Map<Faturamento>(command);

            faturamento.UsuarioId = tenantProvider.UsuarioId.GetValueOrDefault();

            faturamento.Ticket = ticketExiste;

            faturamento.Ticket.FecharTicket();
            faturamento.CalcularFaturamento();

            await repositorioFaturamento.CadastrarAsync(faturamento);

            await unitOfWork.CommitAsync();

            var result = mapper.Map<CadastrarFaturamentoResult>(faturamento);

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackAsync();

            logger.LogError(
                ex,
                "Ocorreu um erro durante o registro de {@Registro}.",
                command
            );

            return Result.Fail(ResultadosErro.ExcecaoInternaErro(ex));
        }
    }
}
