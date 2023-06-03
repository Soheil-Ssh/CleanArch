namespace CleanArch.Core.Entities.Common.Result;

#region Result

public class Result : BaseResult
{
    #region Ctor

    public Result() : base()
    { }

    #endregion
}

#endregion

#region Result with data

public class Result<TData> : BaseResult
{
    #region Properties

    public TData Data { get; set; }

    #endregion

    #region Ctor

    public Result() : base()
    { }

    #endregion
}

#endregion