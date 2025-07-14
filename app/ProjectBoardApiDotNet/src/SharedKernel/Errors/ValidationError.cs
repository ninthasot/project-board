namespace SharedKernel.Errors;

public class ValidationError : CustomError
{
    private const int STATUS_CODE = 422;
    private const string DEFAULT_CODE = "validation_error";
    private const string DEFAULT_MESSAGE = "Validation Error";

    public ValidationError()
        : base(STATUS_CODE, DEFAULT_CODE, DEFAULT_MESSAGE) { }
}
