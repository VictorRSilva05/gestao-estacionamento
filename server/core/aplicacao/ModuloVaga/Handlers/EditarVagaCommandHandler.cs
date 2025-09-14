using AutoMapper;
using eAgenda.Core.Aplicacao.Compartilhado;
using FluentResults;
using FluentValidation;
using GestaoDeEstacionamento.Core.Aplicacao.ModuloVaga.Commands;
using GestaoDeEstacionamento.Core.Dominio.Compartilhado;
using GestaoDeEstacionamento.Core.Dominio.ModuloVaga;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GestaoDeEstacionamento.Core.Aplicacao.ModuloVaga.Handlers;
public class EditarVagaCommandHandler(
    IRepositorioVaga repositorioVaga,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ILogger<EditarVagaCommandHandler> logger
    ) : IRequestHandler<EditarVagaCommand, Result<EditarVagaResult>>
{
    public async Task<Result<EditarVagaResult>> Handle(EditarVagaCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var VagaEditado = mapper.Map<Vaga>(command);

            await repositorioVaga.EditarAsync(command.Id, VagaEditado);

            await unitOfWork.CommitAsync();

            var result = mapper.Map<EditarVagaResult>(VagaEditado);

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
