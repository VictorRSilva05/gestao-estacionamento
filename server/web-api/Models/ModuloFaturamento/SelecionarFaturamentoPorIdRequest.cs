using Microsoft.AspNetCore.Routing.Constraints;

namespace GestaoDeEstacionamento.WebApi.Models.ModuloFaturamento;

public record SelecionarFaturamentoPorIdRequest(Guid Id);

public record SelecionarFaturamentoPorIdResponse(
    Guid Id,
    Guid TicketId,
    int Diarias,
    decimal ValorDiaria,
    decimal Total
    );