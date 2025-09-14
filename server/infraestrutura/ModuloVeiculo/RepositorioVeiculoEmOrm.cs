using GestaoDeEstacionamento.Core.Dominio.ModuloVeiculo;
using GestaoDeEstacionamento.Infraestrutura.Orm.Compartilhado;
using TesteFacil.Infraestrutura.Orm.Compartilhado;

namespace GestaoDeEstacionamento.Infraestrutura.Orm.ModuloVeiculo;
public class RepositorioVeiculoEmOrm(GestaoDeEstacionamentoDbContext contexto) : RepositorioBaseEmOrm<Veiculo>(contexto), IRepositorioVeiculo;
