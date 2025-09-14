using FluentResults;
using GestaoDeEstacionamento.Core.Dominio.ModuloHospede;
using GestaoDeEstacionamento.Core.Dominio.ModuloVaga;
using GestaoDeEstacionamento.Core.Dominio.ModuloVeiculo;
using MediatR;

namespace GestaoDeEstacionamento.Core.Aplicacao.ModuloTicket.Commands;
public record SelecionarTicketPorIdQuery(Guid Id) : IRequest<Result<SelecionarTicketPorIdResult>>;

public record SelecionarTicketPorIdResult(
    Guid Id,
    Guid HospedeId,
    Guid VeiculoId,
    Guid VagaId,
    DateTime Entrada,
    DateTime? Saida,
    string? Observacao,
    bool Aberta
    );
