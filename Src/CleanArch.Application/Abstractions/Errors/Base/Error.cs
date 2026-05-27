namespace CleanArch.Application.Abstractions.Errors.Base;

/// <summary>
/// Represents an application error identified by a unique code and an optional human-readable description.
/// </summary>
/// <param name="Code">The error code that uniquely identifies the type of error. Must not be null.</param>
/// <param name="Description">An optional description providing additional details about the error.</param>
public sealed record Error(string Code, string? Description = null)
{
    /// <summary>
    /// A predefined instance representing no error, with an empty code. Safe to use as a sentinel value.
    /// </summary>
    public static readonly Error None = new Error(string.Empty);

    /// <summary>
    /// Returns the string representation of the error.
    /// If a description is provided, the format is "Code: Description"; otherwise, only the code is returned.
    /// </summary>
    /// <returns>The formatted error string.</returns>
    public override string ToString() =>
        string.IsNullOrEmpty(Description) ? Code : $"{Code}: {Description}";
}