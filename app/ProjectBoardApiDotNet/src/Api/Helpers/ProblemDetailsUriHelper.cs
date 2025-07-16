namespace Api.Helpers;

internal static class ProblemDetailsUriHelper
{
    public static string GetProblemTypeUri(int statusCode) =>
        statusCode switch
        {
            400 => "https://tools.ietf.org/html/rfc9110#section-15.5.1",
            401 => "https://tools.ietf.org/html/rfc9110#section-15.5.2",
            403 => "https://tools.ietf.org/html/rfc9110#section-15.5.4",
            404 => "https://tools.ietf.org/html/rfc9110#section-15.5.5",
            409 => "https://tools.ietf.org/html/rfc9110#section-15.5.8",
            500 => "https://tools.ietf.org/html/rfc9110#section-15.6.1",
            503 => "https://tools.ietf.org/html/rfc9110#section-15.6.4",
            _ => "https://tools.ietf.org/html/rfc9110",
        };
}
