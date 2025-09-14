namespace GestaoDeEstacionamento.WebApi.Models.ModuloVaga;

public record CadastrarVagaRequest(
    string Nome,
    bool Ocupada
    );

public record CadastrarVagaResponse(Guid Id);
