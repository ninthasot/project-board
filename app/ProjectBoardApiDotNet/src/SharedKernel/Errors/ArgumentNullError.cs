namespace SharedKernel.Errors;

public class ArgumentNullError : CustomError
{
    private const int STATUS_CODE = 400;

    private const string DEFAULT_CODE = "bad_request";

    private const string DEFAULT_MESSAGE = "The argument '{argumentName}' cannot be null.";

    public ArgumentNullError()
        : base(STATUS_CODE, DEFAULT_CODE, DEFAULT_MESSAGE) { }
}
