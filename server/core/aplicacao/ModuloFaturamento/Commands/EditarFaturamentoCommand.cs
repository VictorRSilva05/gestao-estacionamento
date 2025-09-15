using FluentResults;
using MediatR;

namespace GestaoDeEstacionamento.Core.Aplicacao.ModuloFaturamento.Commands;
public record EditarFaturamentoCommand(Guid Id, Guid TicketId) : IRequest<Result<EditarFaturamentoResult>>;

public record EditarFaturamentoResult(Guid TicketId);