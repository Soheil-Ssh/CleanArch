using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Web.Controllers
{
    public class HomeController : BaseController
    {
        #region Index

        public IActionResult Index() => View();

        #endregion
    }
}
