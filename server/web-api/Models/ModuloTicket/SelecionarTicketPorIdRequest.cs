using GestaoDeEstacionamento.Core.Dominio.ModuloHospede;
using GestaoDeEstacionamento.Core.Dominio.ModuloVaga;
using GestaoDeEstacionamento.Core.Dominio.ModuloVeiculo;

namespace GestaoDeEstacionamento.WebApi.Models.ModuloTicket;

public record SelecionarTicketPorIdRequest(Guid Id);

public record SelecionarTicketPorIdResponse(
    Guid Id,
    Guid HospedeId,
    Guid VeiculoId,
    Guid VagaId,
    DateTime Entrada,
    DateTime? Saida,
    string? Observacao,
    bool Aberta
    );
