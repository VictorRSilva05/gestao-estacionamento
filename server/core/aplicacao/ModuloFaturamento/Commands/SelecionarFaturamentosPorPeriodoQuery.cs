using FluentResults;
using MediatR;
using System.Collections.Immutable;

namespace GestaoDeEstacionamento.Core.Aplicacao.ModuloFaturamento.Commands;
public record SelecionarFaturamentosPorPeriodoQuery(DateTime DataInicio, DateTime DataFim) : IRequest<Result<SelecionarFaturamentosResult>>;

