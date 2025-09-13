using FluentValidation;
using GestaoDeEstacionamento.Core.Aplicacao.ModuloHospede.Commands;

namespace GestaoDeEstacionamento.Core.Aplicacao.FluentValidation.ModuloHospede;
public class EditarHospedeCommandValidator : AbstractValidator<EditarHospedeCommand>
{
    public EditarHospedeCommandValidator()
    {
        RuleFor(x => x.Nome)
           .NotEmpty().WithMessage("O nome é obrigatório.")
           .MinimumLength(2).WithMessage("O nome deve ter pelo menos {MinLength} caracteres.")
           .MaximumLength(100).WithMessage("O nome deve conter no máximo {MaxLength} caracteres.");

    }
}
