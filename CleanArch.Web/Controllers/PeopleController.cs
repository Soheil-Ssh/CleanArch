﻿using CleanArch.Application.IServices;
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
    }
}
