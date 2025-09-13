namespace GestaoDeEstacionamento.WebApi.Models.ModuloHospede;

public record CadastrarHospedeRequest(
    string Nome
    );

public record CadastrarHospedeResponse(Guid Id);
