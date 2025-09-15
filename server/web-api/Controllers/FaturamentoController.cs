using AutoMapper;
using FluentResults;
using GestaoDeEstacionamento.Core.Aplicacao.ModuloFaturamento.Commands;
using GestaoDeEstacionamento.WebApi.Models.ModuloFaturamento;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeEstacionamento.WebApi.Controllers;

[ApiController]
[Route("Faturamentos")]
public class FaturamentoController(IMediator mediator, IMapper mapper) : ControllerBase
{
    [HttpPost("fechar-ticket")]
    public async Task<ActionResult<CadastrarFaturamentoResponse>> Cadastrar(CadastrarFaturamentoRequest request)
    {
        var command = mapper.Map<CadastrarFaturamentoCommand>(request);

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

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        var response = mapper.Map<CadastrarFaturamentoResponse>(result.Value);

        return Created(string.Empty, response);
    }

    [HttpGet("selecionar-faturamentos")]
    public async Task<ActionResult<SelecionarFaturamentosResponse>> SelecionarRegistros(
        [FromQuery] SelecionarFaturamentosRequest? request,
        CancellationToken cancellationToken
    )
    {
        var query = mapper.Map<SelecionarFaturamentosQuery>(request);

        var result = await mediator.Send(query, cancellationToken);

        if (result.IsFailed)
            return BadRequest();

        var response = mapper.Map<SelecionarFaturamentosResponse>(result.Value);

        return Ok(response);
    }

    [HttpGet("{id:guid} selecionar-faturamentos-por-id")]
    public async Task<ActionResult<SelecionarFaturamentoPorIdResponse>> SelecionarRegistroPorId(Guid id)
    {
        var query = mapper.Map<SelecionarFaturamentoPorIdQuery>(id);

        var result = await mediator.Send(query);

        if (result.IsFailed)
            return NotFound(id);

        var response = mapper.Map<SelecionarFaturamentoPorIdResponse>(result.Value);

        return Ok(response);
    }

    [HttpGet("selecionar-faturamentos-por-periodo")]
    public async Task<IActionResult> SelecionarPorPeriodo([FromQuery] DateTime dataInicio, [FromQuery] DateTime dataFim, CancellationToken cancellationToken)
    {
        var inicio = DateTime.SpecifyKind(dataInicio, DateTimeKind.Utc);
        var fim = DateTime.SpecifyKind(dataFim, DateTimeKind.Utc);

        var query = new SelecionarFaturamentosPorPeriodoQuery(inicio, fim);

        var result = await mediator.Send(query, cancellationToken);

        if (result.IsFailed)
            return BadRequest(result.Errors);

        return Ok(result.Value);
    }
}