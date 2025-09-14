using GestaoDeEstacionamento.Core.Dominio.ModuloTicket;
using GestaoDeEstacionamento.Infraestrutura.Orm.Compartilhado;
using TesteFacil.Infraestrutura.Orm.Compartilhado;

namespace GestaoDeEstacionamento.Infraestrutura.Orm.ModuloTicket;
public class RepositorioTicketEmOrm(GestaoDeEstacionamentoDbContext contexto) : RepositorioBaseEmOrm<Ticket>(contexto), IRepositorioTicket;

