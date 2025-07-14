namespace SharedKernel.Errors;

public class ConflictError : CustomError
{
    private const int STATUS_CODE = 409;

    private const string DEFAULT_CODE = "conflict";

    private const string DEFAULT_MESSAGE = "Conflict";

    public ConflictError()
        : base(STATUS_CODE, DEFAULT_CODE, DEFAULT_MESSAGE) { }
}
