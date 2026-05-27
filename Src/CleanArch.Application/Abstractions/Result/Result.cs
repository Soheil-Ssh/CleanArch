using CleanArch.Application.Abstractions.Result.Base;
using System.Diagnostics.CodeAnalysis;
using CleanArch.Application.Abstractions.Errors.Base;

namespace CleanArch.Application.Abstractions.Result;

/// <summary>
/// Represents the outcome of an operation that either succeeds or fails, without a return value.
/// </summary>
public class Result : BaseResult
{
    private Result(bool isSuccess, Error error)
        : base(isSuccess, error) { }

    /// <summary>
    /// Creates a successful result indicating that the operation completed without errors.
    /// </summary>
    /// <returns>A new <see cref="Result"/> representing success.</returns>
    public static Result Success() => new(true, Error.None);

    /// <summary>
    /// Creates a failure result with the specified error indicating why the operation failed.
    /// </summary>
    /// <param name="error">The error that describes the failure. Must not be <see cref="Error.None"/>.</param>
    /// <returns>A new <see cref="Result"/> representing failure.</returns>
    public static Result Failure(Error error) => new(false, error);
}

/// <summary>
/// Represents the outcome of an operation that either succeeds and returns data,
/// or fails with an error.
/// </summary>
/// <typeparam name="TData">The type of data returned on success.</typeparam>
public class Result<TData> : BaseResult
{
    private Result(TData? data, bool isSuccess, Error error) : base(isSuccess, error)
    {
        Data = data;
    }

    /// <summary>
    /// The data returned by a successful operation. Guaranteed to be non-null
    /// when <see cref="BaseResult.IsSuccess"/> is <c>true</c>, as enforced by
    /// <see cref="System.Diagnostics.CodeAnalysis.MemberNotNullWhenAttribute"/>.
    /// </summary>
    [MemberNotNullWhen(true, nameof(Data))]
    public TData? Data { get; }

    /// <summary>
    /// Creates a successful result containing the provided data.
    /// </summary>
    /// <param name="data">The data to be returned with the success result.</param>
    /// <returns>A new <see cref="Result{TData}"/> instance representing success with the given data.</returns>
    public static Result<TData> Success(TData data) => new(data, true, Error.None);

    /// <summary>
    /// Creates a failure result with the specified error.
    /// </summary>
    /// <param name="error">The error that describes the failure. Must not be <see cref="Error.None"/>.</param>
    /// <returns>A new <see cref="Result{TData}"/> instance representing failure with the given error.</returns>
    public static Result<TData> Failure(Error error) => new(default, false, error);
}