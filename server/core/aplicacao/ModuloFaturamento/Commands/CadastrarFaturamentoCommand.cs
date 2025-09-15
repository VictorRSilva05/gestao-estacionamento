using FluentResults;
using GestaoDeEstacionamento.Core.Dominio.ModuloTicket;
using MediatR;

namespace GestaoDeEstacionamento.Core.Aplicacao.ModuloFaturamento.Commands;
public record CadastrarFaturamentoCommand(Guid TicketId) : IRequest<Result<CadastrarFaturamentoResult>>;

public record CadastrarFaturamentoResult(Guid Id);
