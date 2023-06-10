using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Index

        public IActionResult Index() => View();

        #endregion
    }
}
