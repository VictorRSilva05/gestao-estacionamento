using GestaoDeEstacionamento.Core.Dominio.Compartilhado;

namespace GestaoDeEstacionamento.Core.Dominio.ModuloFaturamento;
public interface IRepositorioFaturamento : IRepositorio<Faturamento>
{
    Task<List<Faturamento>> SelecionarPorPeriodoAsync(DateTime dataInicio, DateTime dataFim);
}