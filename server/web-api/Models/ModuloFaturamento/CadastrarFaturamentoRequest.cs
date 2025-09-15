namespace GestaoDeEstacionamento.WebApi.Models.ModuloFaturamento;

public record CadastrarFaturamentoRequest(Guid TicketId);

public record CadastrarFaturamentoResponse(Guid Id);
