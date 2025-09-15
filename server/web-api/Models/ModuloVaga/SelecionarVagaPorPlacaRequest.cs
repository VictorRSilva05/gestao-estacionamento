﻿namespace GestaoDeEstacionamento.WebApi.Models.ModuloVaga;

public record SelecionarVagaPorPlacaRequest(string placa);

public record SelecionarVagaPorPlacaResponse(
    Guid Id,
    string Nome,
    string Placa
    );
