using FluentValidation;
using GestaoDeEstacionamento.Core.Aplicacao.ModuloVeiculo.Commands;

namespace GestaoDeEstacionamento.Core.Aplicacao.FluentValidation.ModuloVeiculo;
public class CadastrarVeiculoCommandValidator : AbstractValidator<CadastrarVeiculoCommand>
{
    public CadastrarVeiculoCommandValidator()
    {
        RuleFor(x => x.Placa)
            .NotEmpty().WithMessage("A placa é obrigatória.");

        RuleFor(x => x.Modelo)
            .NotEmpty().WithMessage("O modelo é obrigatório.");

        RuleFor(x => x.Cor)
            .NotEmpty().WithMessage("A cor é obrigatória.");
    }
}
