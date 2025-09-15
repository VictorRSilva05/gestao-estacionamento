namespace GestaoDeEstacionamento.WebApi.Models.ModuloFaturamento;

public record EditarFaturamentoRequest(Guid TicketId);

public record EditarFaturamentoResponse(Guid TicketId);