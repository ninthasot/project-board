namespace SharedKernel.Errors;

public class ValidationError : CustomError
{
    private const int STATUS_CODE = 422;
    private const string DEFAULT_CODE = "validation_error";
    private const string DEFAULT_MESSAGE = "Validation Error";

    public ValidationError(string msg)
        : base(STATUS_CODE, DEFAULT_CODE, msg != null ? msg : DEFAULT_MESSAGE) { }
}
