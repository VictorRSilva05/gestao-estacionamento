using FluentValidation;
using GestaoDeEstacionamento.Core.Aplicacao.ModuloVeiculo.Commands;
using GestaoDeEstacionamento.Core.Dominio.ModuloVeiculo;

namespace GestaoDeEstacionamento.Core.Aplicacao.FluentValidation.ModuloVeiculo;
public class EditarVeiculoCommandValidator : AbstractValidator<EditarVeiculoCommand>
{
    public EditarVeiculoCommandValidator()
    {
        RuleFor(x => x.Placa)
            .NotEmpty().WithMessage("A placa é obrigatória.");

        RuleFor(x => x.Modelo)
            .NotEmpty().WithMessage("O modelo é obrigatório.");

        RuleFor(x => x.Cor)
            .NotEmpty().WithMessage("A cor é obrigatória.");
    }
}
