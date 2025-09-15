using AutoMapper;
using eAgenda.Core.Aplicacao.Compartilhado;
using FluentResults;
using FluentValidation;
using GestaoDeEstacionamento.Core.Aplicacao.ModuloTicket.Commands;
using GestaoDeEstacionamento.Core.Dominio.Compartilhado;
using GestaoDeEstacionamento.Core.Dominio.ModuloHospede;
using GestaoDeEstacionamento.Core.Dominio.ModuloTicket;
using GestaoDeEstacionamento.Core.Dominio.ModuloVaga;
using GestaoDeEstacionamento.Core.Dominio.ModuloVeiculo;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace GestaoDeEstacionamento.Core.Aplicacao.ModuloTicket.Handlers;
public class CadastrarTicketCommandHandler(
    IRepositorioTicket repositorioTicket,
    IRepositorioHospede repositorioHospede,
    IRepositorioVeiculo repositorioVeiculo,
    IRepositorioVaga repositorioVaga,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IValidator<CadastrarTicketCommand> validator,
    ILogger<CadastrarTicketCommandHandler> logger
    ) : IRequestHandler<CadastrarTicketCommand, Result<CadastrarTicketResult>>
{
    public async Task<Result<CadastrarTicketResult>> Handle(CadastrarTicketCommand command, CancellationToken cancellationToken)
    {
        var resultadoValidacao = await validator.ValidateAsync(command, cancellationToken);

        if (!resultadoValidacao.IsValid)
        {
            var erros = resultadoValidacao.Errors.Select(e => e.ErrorMessage);

            var erroFormatado = ResultadosErro.RequisicaoInvalidaErro(erros);

            return Result.Fail(erroFormatado);
        }

        try
        {
            var hospedeExiste = await repositorioHospede.SelecionarRegistroPorIdAsync(command.HospedeId);
            if (hospedeExiste is null)
                return Result.Fail("Hóspede não encontrado.");

            var veiculoExiste = await repositorioVeiculo.SelecionarRegistroPorIdAsync(command.VeiculoId);
            if (veiculoExiste is null)
                return Result.Fail("Veículo não encontrado.");

            var vagaExiste = await repositorioVaga.SelecionarRegistroPorIdAsync(command.VagaId);
            if (vagaExiste is null)
                return Result.Fail("Vaga não encontrada.");

            if (vagaExiste.Ocupada)
                return Result.Fail("A vaga já está ocupada");

            var ticket = mapper.Map<Ticket>(command);

            ticket.Hospede = hospedeExiste;
            ticket.Veiculo = veiculoExiste;

            ticket.Vaga = vagaExiste;

            ticket.AbrirTicket();

            await repositorioTicket.CadastrarAsync(ticket);

            await unitOfWork.CommitAsync();

            var result = mapper.Map<CadastrarTicketResult>(ticket);

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackAsync();

            logger.LogError(
                ex,
                "Ocorreu um erro durante o registro de {@Registro}.",
                command
            );

            return Result.Fail(ResultadosErro.ExcecaoInternaErro(ex));
        }
    }
}