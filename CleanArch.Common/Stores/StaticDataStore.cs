namespace CleanArch.Common.Stores
{
    public static class StaticDataStore
    {
        #region Connection

        public const string SqlServerConnectionStringName = "DefaultConnection";

        #endregion

        #region Messages

        public const string CreateSucceededMessage = "Added successfully.";
        public const string EditSucceededMessage = "Edited successfully.";
        public const string DeleteSucceededMessage = "Deleted successfully.";

        #endregion
    }
}
