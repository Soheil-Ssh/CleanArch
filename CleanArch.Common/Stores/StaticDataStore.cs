namespace CleanArch.Common.Stores
{
    public static class StaticDataStore
    {
        #region Connection

        public const string SqlServerConnectionStringName = "DefaultConnection";

        #endregion

        #region Messages

        public const string CreateSucceededMessage = "Added successfully.";
        public const string CreateFailedMessage = "Error! Could not be added.";
        public const string EditSucceededMessage = "Edited successfully.";
        public const string EditFailedMessage = "Error! Could not be edited.";
        public const string DeleteSucceededMessage = "Deleted successfully.";
        public const string DeleteFailedMessage = "Error! Could not be deleted.";
        public const string ErrorMessage = "Error! Please try again later.";
        public const string ValidationErrorMessage = "Error! Please enter the correct information.";
        public const string NotFoundErrorMessage = "Error! Not found.";
        public const string ImageSecurityErrorMessage = "Error! Uploaded image is dangerous or not an image file.";

        #endregion
    }
}
