using GestaoDeEstacionamento.Core.Dominio.ModuloHospede;
using GestaoDeEstacionamento.Infraestrutura.Orm.Compartilhado;
using TesteFacil.Infraestrutura.Orm.Compartilhado;

namespace GestaoDeEstacionamento.Infraestrutura.Orm.ModuloHospede;
public class RepositorioHospedeEmOrm(GestaoDeEstacionamentoDbContext contexto) : RepositorioBaseEmOrm<Hospede>(contexto), IRepositorioHospede;
