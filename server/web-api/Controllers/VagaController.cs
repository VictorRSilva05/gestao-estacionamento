﻿using AutoMapper;
using FluentResults;
using GestaoDeEstacionamento.Core.Aplicacao.ModuloVaga.Commands;
using GestaoDeEstacionamento.WebApi.Models.ModuloVaga;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeEstacionamento.WebApi.Controllers;

[ApiController]
[Route("vagas")]
public class VagaController(IMediator mediator, IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CadastrarVagaResponse>> Cadastrar(CadastrarVagaRequest request)
    {
        var command = mapper.Map<CadastrarVagaCommand>(request);

        var result = await mediator.Send(command);

        if (result.IsFailed)
        {
            if (result.HasError(e => e.HasMetadata("TipoErro", m => m.Equals("RequisicaoInvalida"))))
            {
                var errosDeValidacao = result.Errors
                    .SelectMany(e => e.Reasons.OfType<IError>())
                    .Select(e => e.Message);

                return BadRequest(errosDeValidacao);
            }

            return StatusCode(StatusCodes.Status409Conflict);
        }

        var response = mapper.Map<CadastrarVagaResponse>(result.Value);

        return Created(string.Empty, response);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<EditarVagaResponse>> Editar(Guid id, EditarVagaRequest request)
    {
        var command = mapper.Map<(Guid, EditarVagaRequest), EditarVagaCommand>((id, request));

        var result = await mediator.Send(command);

        if (result.IsFailed)
            return BadRequest();

        var response = mapper.Map<EditarVagaResponse>(result.Value);

        return Ok(response);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<ExcluirVagaResponse>> Excluir(Guid id)
    {
        var command = mapper.Map<ExcluirVagaCommand>(id);

        var result = await mediator.Send(command);

        if (result.IsFailed)
            return BadRequest();

        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<SelecionarVagasResponse>> SelecionarRegistros(
        [FromQuery] SelecionarVagasRequest? request,
        CancellationToken cancellationToken
    )
    {
        var query = mapper.Map<SelecionarVagasQuery>(request);

        var result = await mediator.Send(query, cancellationToken);

        if (result.IsFailed)
            return BadRequest();

        var response = mapper.Map<SelecionarVagasResponse>(result.Value);

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SelecionarVagaPorIdResponse>> SelecionarRegistroPorId(Guid id)
    {
        var query = mapper.Map<SelecionarVagaPorIdQuery>(id);

        var result = await mediator.Send(query);

        if (result.IsFailed)
            return NotFound(id);

        var response = mapper.Map<SelecionarVagaPorIdResponse>(result.Value);

        return Ok(response);
    }

    [HttpGet("vaga-por-placa")]
    public async Task<ActionResult<SelecionarVagaPorPlacaResponse>> SelecionarRegistroPorPlaca(string placa)
    {
        var query = mapper.Map<SelecionarVagaPorPlacaQuery>(placa);

        var result = await mediator.Send(query);

        if (result.IsFailed)
            return NotFound(placa);

        var response = mapper.Map<SelecionarVagaPorPlacaResponse>(result.Value);

        return Ok(response);
    }
}