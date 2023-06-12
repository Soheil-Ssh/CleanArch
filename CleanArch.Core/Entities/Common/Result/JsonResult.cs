namespace CleanArch.Core.Entities.Common.Result
{
    public class JsonResult
    {
        #region properties

        public bool Succeeded { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }

        #endregion
    }
}
