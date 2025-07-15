using Boards.Resources.Validators;
using FluentValidation;

namespace Boards.Application.Commands.CreateBoard;

public class CreateBoardValidator : AbstractValidator<CreateBoardCommand>
{
    public CreateBoardValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage(BoardValidators.CreateBoard_TitleRequired)
            .MaximumLength(200)
            .WithMessage(BoardValidators.CreateBoard_TitleMaxLength);

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage(BoardValidators.CreateBoard_DescriptionRequired)
            .MaximumLength(1000)
            .WithMessage(BoardValidators.CreateBoard_DescriptionMaxLength);
    }
}
