using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Web.Controllers
{
    public class BaseController : Controller
    {
        #region Alert

        protected void Alert(string message, NotificationType notificationType)
        {
            TempData["notification"] = $"toastr.{notificationType.ToString().ToLower()}('{message}');";
        }

        #endregion
    }
}
