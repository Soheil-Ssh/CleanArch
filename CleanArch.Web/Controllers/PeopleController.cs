using CleanArch.Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Web.Controllers
{
    public class PeopleController : Controller
    {
        #region Ctor

        private readonly IPeopleService _peopleService;
        private readonly ILogger<PeopleController> _logger;

        public PeopleController(IPeopleService peopleService,
            ILogger<PeopleController> logger)
        {
            _peopleService = peopleService;
            _logger = logger;
        }

        #endregion

        #region Index

        public IActionResult Index() => View();

        #endregion
    }
}
