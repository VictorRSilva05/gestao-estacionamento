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
using Microsoft.Extensions.Logging;
using System.Net.Sockets;

namespace GestaoDeEstacionamento.Core.Aplicacao.ModuloTicket.Handlers;
public class EditarTicketCommandHandler(
    IRepositorioTicket repositorioTicket,
    IRepositorioHospede repositorioHospede,
    IRepositorioVeiculo repositorioVeiculo,
    IRepositorioVaga repositorioVaga,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IValidator<EditarTicketCommand> validator,
    ILogger<EditarTicketCommandHandler> logger
    ) : IRequestHandler<EditarTicketCommand, Result<EditarTicketResult>>
{
    public async Task<Result<EditarTicketResult>> Handle(EditarTicketCommand command, CancellationToken cancellationToken)
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

            var ticketEditado = mapper.Map<Ticket>(command);

            ticketEditado.Hospede = hospedeExiste;
            ticketEditado.Veiculo = veiculoExiste;
            ticketEditado.Vaga = vagaExiste;

            await repositorioTicket.EditarAsync(command.Id, ticketEditado);

            await unitOfWork.CommitAsync();

            var result = mapper.Map<EditarTicketResult>(ticketEditado);

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackAsync();

            logger.LogError(
                ex,
                "Ocorreu um erro durante a edição de {@Registro}.",
                command
            );

            return Result.Fail(ResultadosErro.ExcecaoInternaErro(ex));
        }
    }
}
