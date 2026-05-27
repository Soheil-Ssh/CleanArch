using CleanArch.Application.Abstractions.Errors.Base;

namespace CleanArch.Application.Abstractions.Result.Base;

/// <summary>
/// Serves as the base class for result types that indicate success or failure
/// and carry an associated error when unsuccessful.
/// </summary>
public abstract class BaseResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BaseResult"/> class.
    /// Ensures a consistent state: success must be paired with <see cref="Error.None"/>
    /// and failure must be paired with an actual error.
    /// </summary>
    /// <param name="isSuccess"><c>true</c> if the operation succeeded; otherwise <c>false</c>.</param>
    /// <param name="error">The error associated with the result. Must be <see cref="Error.None"/> when <paramref name="isSuccess"/> is <c>true</c> and must not be <see cref="Error.None"/> when <paramref name="isSuccess"/> is <c>false</c>.</param>
    /// <exception cref="ArgumentException">Thrown when the combination of <paramref name="isSuccess"/> and <paramref name="error"/> is invalid.</exception>
    protected BaseResult(bool isSuccess, Error error)
    {
        if ((isSuccess && error != Error.None) ||
            (!isSuccess && error == Error.None))
            throw new ArgumentException("Invalid error.", nameof(error));

        IsSuccess = isSuccess;
        Error = error;
    }

    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    /// Gets a value indicating whether the operation represented by this result was successful.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Gets a value indicating whether the operation represented by this result failed.
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// Gets the error that occurred during the operation when <see cref="IsSuccess"/> is <c>false</c>;
    /// otherwise <see cref="Error.None"/>.
    /// </summary>
    public Error Error { get; }
}