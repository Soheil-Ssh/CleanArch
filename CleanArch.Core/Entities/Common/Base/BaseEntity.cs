namespace CleanArch.Core.Entities.Common.Base
{
    public class BaseEntity
    {
        #region Properties

        [Key]
        public Guid Id { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public bool IsDelete { get; set; }

        #endregion
    }
}
