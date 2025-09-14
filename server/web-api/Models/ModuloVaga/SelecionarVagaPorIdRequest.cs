namespace GestaoDeEstacionamento.WebApi.Models.ModuloVaga;

public record SelecionarVagaPorIdRequest(Guid Id);

public record SelecionarVagaPorIdResponse(
    Guid Id,
    string Nome,
    bool Ocupada
    );
