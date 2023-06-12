using CleanArch.Common.Stores;
using CleanArch.Core.Entities.Common.Enums;
using CleanArch.Core.Entities.Common.Result;

namespace CleanArch.Common.Helpers
{
    public class JsonHelper
    {
        #region Message result 

        public static JsonResult MessageResult(JsonResultType jsonResultType) =>
            jsonResultType switch
            {
                JsonResultType.CreateSucceeded => new JsonResult() { Succeeded = true, Message = StaticDataStore.CreateSucceededMessage },
                JsonResultType.CreateFailed => new JsonResult() { Succeeded = false, Message = StaticDataStore.CreateFailedMessage },
                JsonResultType.EditSucceeded => new JsonResult() { Succeeded = true, Message = StaticDataStore.EditSucceededMessage },
                JsonResultType.EditFailed => new JsonResult() { Succeeded = false, Message = StaticDataStore.EditFailedMessage },
                JsonResultType.DeleteSucceeded => new JsonResult() { Succeeded = true, Message = StaticDataStore.DeleteSucceededMessage },
                JsonResultType.DeleteFailed => new JsonResult() { Succeeded = false, Message = StaticDataStore.DeleteFailedMessage },
                JsonResultType.Error => new JsonResult() { Succeeded = false, Message = StaticDataStore.ErrorMessage },
                JsonResultType.ValidationError => new JsonResult() { Succeeded = false, Message = StaticDataStore.ValidationErrorMessage },
                JsonResultType.NotFoundError => new JsonResult() { Succeeded = false, Message = StaticDataStore.NotFoundErrorMessage },
                JsonResultType.ImageSecurityError => new JsonResult() { Succeeded = false, Message = StaticDataStore.ImageSecurityErrorMessage },
                _ => throw new ArgumentException("invalid enum value", nameof(jsonResultType))
            };

        public static JsonResult MessageResult(string message, bool succeeded)
            => new JsonResult() { Succeeded = succeeded, Message = message };

        #endregion

        #region Data result

        public static JsonResult DataResult(object data, bool succeeded = true)
        {
            return new JsonResult() { Succeeded = succeeded, Data = data };
        }

        #endregion
    }
}
