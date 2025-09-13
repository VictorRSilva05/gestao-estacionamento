using AutoMapper;
using eAgenda.Core.Aplicacao.Compartilhado;
using FluentResults;
using FluentValidation;
using GestaoDeEstacionamento.Core.Aplicacao.ModuloHospede.Commands;
using GestaoDeEstacionamento.Core.Dominio.Compartilhado;
using GestaoDeEstacionamento.Core.Dominio.ModuloHospede;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GestaoDeEstacionamento.Core.Aplicacao.ModuloHospede.Handlers;
public class EditarHospedeCommandHandler(
    IRepositorioHospede repositorioHospede,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IValidator<EditarHospedeCommand> validator,
    ILogger<EditarHospedeCommandHandler> logger
    ) : IRequestHandler<EditarHospedeCommand, Result<EditarHospedeResult>>
{
    public async Task<Result<EditarHospedeResult>> Handle(EditarHospedeCommand command, CancellationToken cancellationToken)
    {
        var resultadoValidacao = await validator.ValidateAsync(command, cancellationToken);

        if (!resultadoValidacao.IsValid)
        {
            var erros = resultadoValidacao.Errors.Select(e => e.ErrorMessage);

            var erroFormatado = ResultadosErro.RequisicaoInvalidaErro(erros);

            return Result.Fail(erroFormatado);
        }

        var registros = await repositorioHospede.SelecionarRegistrosAsync();

        if (registros.Any(i => i.Id.Equals(command.Id) && i.Nome.Equals(command.Nome)))
            return Result.Fail(ResultadosErro.RegistroDuplicadoErro("Já existe um contato registrado com este nome."));

        try
        {
            var hospedeEditado = mapper.Map<Hospede>(command);

            await repositorioHospede.EditarAsync(command.Id, hospedeEditado);

            await unitOfWork.CommitAsync();

            var result = mapper.Map<EditarHospedeResult>(hospedeEditado);

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