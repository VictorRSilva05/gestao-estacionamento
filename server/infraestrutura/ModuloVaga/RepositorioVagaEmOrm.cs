using GestaoDeEstacionamento.Core.Dominio.ModuloVaga;
using GestaoDeEstacionamento.Infraestrutura.Orm.Compartilhado;
using TesteFacil.Infraestrutura.Orm.Compartilhado;

namespace GestaoDeEstacionamento.Infraestrutura.Orm.ModuloVaga;
public class RepositorioVagaEmOrm(GestaoDeEstacionamentoDbContext contexto) : RepositorioBaseEmOrm<Vaga>(contexto), IRepositorioVaga;
