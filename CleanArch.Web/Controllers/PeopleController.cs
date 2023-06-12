using CleanArch.Application.DTOs.People;
using CleanArch.Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Web.Controllers
{
    public class PeopleController : BaseController
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

        public async Task<IActionResult> Index()
        {
            var result = await _peopleService.GetAllPeopleAsync();

            if (!result.Succeeded)
            {
                _logger.LogError(result.Message, result.Type, result.Time);

                if (result.Data == null)
                    return NotFound();

                return StatusCode(500);
            }

            return View(result.Data);
        }

        #endregion

        #region Create person

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePersonDTO model)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _peopleService.CreatePersonAsync(model);

            if (!result.Succeeded)
            {
                _logger.LogError(result.Message, result.Type, result.Time);
                return StatusCode(500);
            }

            Alert(StaticDataStore.CreateSucceededMessage, NotificationType.Success);
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Edit person

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _peopleService.GetPersonByIdAsync(id);

            if (!result.Succeeded)
            {
                _logger.LogError(result.Message, result.Type, result.Time);

                if (result.Data == null)
                    return NotFound();

                return StatusCode(500);
            }

            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditPersonDTO model)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _peopleService.EditPersonAsync(model);

            if (!result.Succeeded)
            {
                _logger.LogError(result.Message, result.Type, result.Time);
                return StatusCode(500);
            }

            Alert(StaticDataStore.EditSucceededMessage, NotificationType.Success);
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Detials

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _peopleService.GetPersonDetailsByIdAsync(id);

            if (!result.Succeeded)
            {
                _logger.LogError(result.Message, result.Type, result.Time);

                if (result.Data == null)
                    return NotFound();

                return StatusCode(500);
            }

            return View(result.Data);
        }

        #endregion

        #region Delete

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _peopleService.DeletePersonByIdAsync(id);

            if (!result.Succeeded)
            {
                _logger.LogError(result.Message, result.Type, result.Time);

                if (result.Type == ResultType.NotFoundError)
                {
                    return Json(JsonHelper.MessageResult(JsonResultType.NotFoundError));
                }

                return Json(JsonHelper.MessageResult(JsonResultType.DeleteFailed));
            }

            return Json(JsonHelper.MessageResult(JsonResultType.DeleteSucceeded));
        }

        #endregion
    }
}
