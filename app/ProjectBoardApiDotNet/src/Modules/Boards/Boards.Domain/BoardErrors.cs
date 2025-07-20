using Common.Domain.Errors;

namespace Boards.Domain;

public static class BoardErrors
{
    public static CustomError NotFound() =>
        CommonErrorFactory.NotFound(
            Resources.Errors.BoardErrors.Board_NotFound_Title,
            Resources.Errors.BoardErrors.Board_NotFound_Detail
        );
}
