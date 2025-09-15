using GestaoDeEstacionamento.Core.Dominio.ModuloFaturamento;
using GestaoDeEstacionamento.Infraestrutura.Orm.Compartilhado;
using TesteFacil.Infraestrutura.Orm.Compartilhado;

namespace GestaoDeEstacionamento.Infraestrutura.Orm.ModuloFaturamento;
public class RepositorioFaturamentoEmOrm(GestaoDeEstacionamentoDbContext contexto) : RepositorioBaseEmOrm<Faturamento>(contexto), IRepositorioFaturamento;
