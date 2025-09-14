using FluentValidation;
using GestaoDeEstacionamento.Core.Aplicacao.ModuloTicket.Commands;

namespace GestaoDeEstacionamento.Core.Aplicacao.FluentValidation.ModuloTicket;
public class EditarTicketCommandValidator : AbstractValidator<EditarTicketCommand>
{
    public EditarTicketCommandValidator()
    {
        RuleFor(x => x.HospedeId)
        .NotEmpty().WithMessage("É obrigatório conter um hospede.");

        RuleFor(x => x.VeiculoId)
            .NotEmpty().WithMessage("É obrigatório conter um veículo.");

        RuleFor(x => x.VagaId)
            .NotEmpty().WithMessage("É obrigatório conter uma vaga.");
    }
}
