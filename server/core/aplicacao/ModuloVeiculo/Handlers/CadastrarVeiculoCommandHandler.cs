using AutoMapper;
using eAgenda.Core.Aplicacao.Compartilhado;
using FluentResults;
using FluentValidation;
using GestaoDeEstacionamento.Core.Aplicacao.ModuloVeiculo.Commands;
using GestaoDeEstacionamento.Core.Dominio.Compartilhado;
using GestaoDeEstacionamento.Core.Dominio.ModuloVeiculo;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace GestaoDeEstacionamento.Core.Aplicacao.ModuloVeiculo.Handlers;
public class CadastrarVeiculoCommandHandler(
    IRepositorioVeiculo repositorioVeiculo,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IValidator<CadastrarVeiculoCommand> validator,
    ILogger<CadastrarVeiculoCommandHandler> logger
    ) : IRequestHandler<CadastrarVeiculoCommand, Result<CadastrarVeiculoResult>>
{
    public async Task<Result<CadastrarVeiculoResult>> Handle(CadastrarVeiculoCommand command, CancellationToken cancellationToken)
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
            var veiculo = mapper.Map<Veiculo>(command);

            await repositorioVeiculo.CadastrarAsync(veiculo);

            await unitOfWork.CommitAsync();

            var result = mapper.Map<CadastrarVeiculoResult>(veiculo);

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
