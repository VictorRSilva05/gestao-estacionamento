using GestaoDeEstacionamento.Core.Aplicacao.ModuloFaturamento.Commands;
using MediatR;
using System.Collections.Immutable;

namespace GestaoDeEstacionamento.WebApi.Models.ModuloFaturamento;

public record SelecionarFaturamentosPorPeriodoRequest(DateTime DataInicio, DateTime DataFim);

public record SelecionarFaturamentosPorPeriodoResponse(ImmutableList<SelecionarFaturamentosDto> Faturamentos);

