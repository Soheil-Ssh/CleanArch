namespace CleanArch.Api.Common.Extensions;

/// <summary>
/// Provides extension methods to convert <see cref="Result"/> and <see cref="Result{TData}"/> objects
/// into ASP.NET Core <see cref="IActionResult"/> responses.
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    /// Converts a non-generic <see cref="Result"/> to an <see cref="IActionResult"/>.
    /// Success results become HTTP 200 with a successful API response body;
    /// failure results become a problem details response with the appropriate HTTP status code.
    /// </summary>
    /// <param name="result">The result object to convert.</param>
    /// <returns>An <see cref="OkObjectResult"/> on success, or an <see cref="ObjectResult"/> containing
    /// <see cref="ApiProblemDetails"/> on failure.</returns>
    public static IActionResult ToActionResult(this Result result)
        => result.IsSuccess ? new OkObjectResult(ApiResponse.Success()) : CreateProblemDetails(result.Error);

    /// <summary>
    /// Converts a generic <see cref="Result{TData}"/> to an <see cref="IActionResult"/>.
    /// Success results return HTTP 200 with the data wrapped in an API response;
    /// failures return a problem details response with the mapped HTTP status code.
    /// </summary>
    /// <typeparam name="T">The type of the data contained in a successful result.</typeparam>
    /// <param name="result">The result object to convert.</param>
    /// <returns>An <see cref="OkObjectResult"/> containing <see cref="ApiResponse{T}"/> on success,
    /// or an <see cref="ObjectResult"/> with problem details on failure.</returns>
    public static IActionResult ToActionResult<T>(this Result<T> result)
        => result.IsSuccess ? new OkObjectResult(ApiResponse<T>.Success(result.Data)) : CreateProblemDetails(result.Error);

    /// <summary>
    /// Builds an <see cref="ObjectResult"/> with <see cref="ApiProblemDetails"/> from the given error.
    /// The HTTP status code is derived from the error’s type via <see cref="ErrorExtensions.GetStatusCode"/>.
    /// </summary>
    /// <param name="error">The error to represent as problem details.</param>
    /// <returns>An <see cref="ObjectResult"/> with the mapped status code and a problem details body.</returns>
    private static ObjectResult CreateProblemDetails(Error error)
    {
        var statusCode = error.GetStatusCode();
        return new ObjectResult(new ApiProblemDetails
        {
            Title = error.Code,
            Detail = error.Description,
            Status = statusCode
        })
        {
            StatusCode = statusCode
        };
    }
}