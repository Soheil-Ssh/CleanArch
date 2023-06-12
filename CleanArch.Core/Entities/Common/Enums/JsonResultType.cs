namespace CleanArch.Core.Entities.Common.Enums
{
    public enum JsonResultType
    {
        CreateSucceeded,
        CreateFailed,
        EditSucceeded,
        EditFailed,
        DeleteSucceeded,
        DeleteFailed,
        Error,
        ValidationError,
        NotFoundError,
        ImageSecurityError,
    }
}
