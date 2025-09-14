using GestaoDeEstacionamento.Core.Dominio.ModuloHospede;
using GestaoDeEstacionamento.Core.Dominio.ModuloVaga;
using GestaoDeEstacionamento.Core.Dominio.ModuloVeiculo;

namespace GestaoDeEstacionamento.WebApi.Models.ModuloTicket;

public record EditarTicketRequest(
    Guid HospedeId,
    Guid VeiculoId,
    Guid VagaId,
    DateTime Entrada,
    DateTime? Saida,
    string? Observacao,
    bool Aberta
    );

public record EditarTicketResponse(
    Guid HospedeId,
    Guid VeiculoId,
    Guid VagaId,
    DateTime Entrada,
    DateTime? Saida,
    string? Observacao,
    bool Aberta
    );
