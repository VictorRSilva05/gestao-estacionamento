using FluentResults;
using MediatR;

namespace GestaoDeEstacionamento.Core.Aplicacao.ModuloVaga.Commands;
public record EditarVagaCommand(
    Guid Id,
    string Nome,
    bool Ocupada
    ) : IRequest<Result<EditarVagaResult>>;

public record EditarVagaResult(
    string Nome,
    bool Ocupada
    );