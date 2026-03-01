using Application.Common.Results;

namespace PresentationAPI.Endpoints;

public static class ResultHttpExtensions
{
    public static IResult ToHttpResult(this Result result)
        => result.ErrorType switch
        {
            ErrorTypes.NotFound => Results.NotFound(result.ErrorMessage),
            ErrorTypes.Conflict => Results.Conflict(result.ErrorMessage),
            ErrorTypes.BadRequest => Results.BadRequest(result.ErrorMessage),
            ErrorTypes.Unexpected => Results.Problem(result.ErrorMessage),
            _ => Results.Problem("An unknown error occurred.")
        };

    public static IResult ToHttpResult<T>(this Result<T> result)
        => result.ErrorType switch
        {
            ErrorTypes.NotFound => Results.NotFound(result.ErrorMessage),
            ErrorTypes.Conflict => Results.Conflict(result.ErrorMessage),
            ErrorTypes.BadRequest => Results.BadRequest(result.ErrorMessage),
            ErrorTypes.Unexpected => Results.Problem(result.ErrorMessage),
            _ => Results.Problem("An unknown error occurred.")
        };
}