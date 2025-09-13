using FluentValidation;
using GestaoDeEstacionamento.Core.Aplicacao.ModuloHospede.Commands;

namespace GestaoDeEstacionamento.Core.Aplicacao.FluentValidation.ModuloHospede;
public class CadastrarHospedeCommandValidator : AbstractValidator<CadastrarHospedeCommand>
{
    public CadastrarHospedeCommandValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O Nome é obrigatório.")
            .MinimumLength(2).WithMessage("O nome deve ter pelo menos {MinLength} caracteres.")
            .MaximumLength(100).WithMessage("O nome deve conter no máximo {MaxLength} caracteres.");
    }
}
