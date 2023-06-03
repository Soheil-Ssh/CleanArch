namespace CleanArch.Core.Entities.Common.Base
{
    public abstract class BaseResult
    {
        #region Ctor

        protected BaseResult()
        {
            this.Succeeded = true;
            this.Type = ResultType.Success;
            this.Time = DateTime.Now;
        }

        #endregion

        #region Properties

        public bool Succeeded { get; set; }

        public string Message { get; set; }

        public List<string> Messages { get; set; }

        public ResultType Type { get; set; }

        public DateTime Time { get; set; }

        #endregion

        #region Set type result

        public void SetType(ResultType type = ResultType.Error, string message = "", params string[] messages)
        {
            this.Type = type;
            this.Message = message;
            this.Messages.AddRange(messages ?? Array.Empty<string>());
            this.Time = DateTime.Now;

            if (type != ResultType.Success)
            {
                this.Succeeded = false;
            }
        }

        #endregion
    }
}
