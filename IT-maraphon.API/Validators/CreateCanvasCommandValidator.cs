using FluentValidation;
using IT_maraphon.Application.Commands;

namespace IT_maraphon.API.Validators;

public class CreateCanvasCommandValidator : AbstractValidator<CreateCanvasCommand>
{
    public CreateCanvasCommandValidator()
    {
        RuleForEach(x => x.Figures)
            .ChildRules(figure =>
            {
                figure.RuleFor(f => f.FigureType)
                .IsInEnum();
            });
    }
}