namespace SharedKernel.Errors;

public class NotFoundError : CustomError
{
    private const int STATUS_CODE = 404;

    private const string DEFAULT_CODE = "not_found";

    private const string DEFAULT_MESSAGE = "The requested resource was not found.";

    public NotFoundError()
        : base(STATUS_CODE, DEFAULT_CODE, DEFAULT_MESSAGE) { }
}
