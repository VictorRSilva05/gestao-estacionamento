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
public class CadastrarVagaCommandHandler(
    IRepositorioVaga repositorioVaga,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ILogger<CadastrarVagaCommandHandler> logger
    ) : IRequestHandler<CadastrarVagaCommand, Result<CadastrarVagaResult>>
{
    public async Task<Result<CadastrarVagaResult>> Handle(CadastrarVagaCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var Vaga = mapper.Map<Vaga>(command);

            await repositorioVaga.CadastrarAsync(Vaga);

            await unitOfWork.CommitAsync();

            var result = mapper.Map<CadastrarVagaResult>(Vaga);

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