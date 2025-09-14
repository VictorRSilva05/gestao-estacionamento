using FluentResults;
using GestaoDeEstacionamento.Core.Dominio.ModuloHospede;
using GestaoDeEstacionamento.Core.Dominio.ModuloVaga;
using GestaoDeEstacionamento.Core.Dominio.ModuloVeiculo;
using MediatR;

namespace GestaoDeEstacionamento.Core.Aplicacao.ModuloTicket.Commands;
public record CadastrarTicketCommand(
    Hospede HospedeId,
    Veiculo VeiculoId,
    Vaga VagaId,
    DateTime Entrada,
    DateTime? Saida,
    string? Observacao,
    bool Aberta
    ) : IRequest<Result<CadastrarTicketResult>>;

public record CadastrarTicketResult(Guid Id);
