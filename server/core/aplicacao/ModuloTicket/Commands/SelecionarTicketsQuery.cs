using FluentResults;
using GestaoDeEstacionamento.Core.Dominio.ModuloHospede;
using GestaoDeEstacionamento.Core.Dominio.ModuloVaga;
using GestaoDeEstacionamento.Core.Dominio.ModuloVeiculo;
using MediatR;
using System.Collections.Immutable;

namespace GestaoDeEstacionamento.Core.Aplicacao.ModuloTicket.Commands;
public record SelecionarTicketsQuery(int? Quantidade) : IRequest<Result<SelecionarTicketsResult>>;
public record SelecionarTicketsResult(ImmutableList<SelecionarTicketsDto> Tickets);
public record SelecionarTicketsDto(
    Guid id,
    Hospede HospedeId,
    Veiculo VeiculoId,
    Vaga VagaId,
    DateTime Entrada,
    DateTime? Saida,
    string? Observacao,
    bool Aberta
    );