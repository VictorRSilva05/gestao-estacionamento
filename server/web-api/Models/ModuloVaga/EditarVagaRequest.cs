namespace GestaoDeEstacionamento.WebApi.Models.ModuloVaga;

public record EditarVagaRequest(
    string Nome, 
    bool Ocupada
    );

public record EditarVagaResponse(
    string Nome,
    string Ocupada
    );