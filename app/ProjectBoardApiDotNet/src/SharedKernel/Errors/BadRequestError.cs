namespace SharedKernel.Errors;

public class BadRequestError : CustomError
{
    private const int STATUS_CODE = 400;

    private const string DEFAULT_CODE = "bad_request";

    private const string DEFAULT_MESSAGE = "Bad Request.";

    public BadRequestError()
        : base(STATUS_CODE, DEFAULT_CODE, DEFAULT_MESSAGE) { }
}
