using FluentResults;
using MediatR;

namespace GestaoDeEstacionamento.Core.Aplicacao.ModuloVaga.Commands;

public record CadastrarVagaCommand(
    string Nome,
    bool Ocupada
    ) : IRequest<Result<CadastrarVagaResult>>;

public record CadastrarVagaResult(Guid Id);
