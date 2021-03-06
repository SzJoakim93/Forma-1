﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Forma_1.Models;
using BLL.ServiceInterfaces;
using AutoMapper;
using Forma_1.ViewModels;
using BLL.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Forma_1.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(ILogger<HomeController> logger, IMapper mapper, ITeamService teamService)
        {
            _logger = logger;
            this.teamService = teamService;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            IEnumerable<TeamDto> teams = await teamService.GetTeamList();
            IEnumerable<TeamViewModel> teamsViewModel = mapper.Map<IEnumerable<TeamViewModel>>(teams);
            return View(teamsViewModel);
        }

        public IActionResult AddTeam()
        {
            if (!User.Identity.IsAuthenticated)
                return Content("Access denied.");

            TeamViewModel new_team = new TeamViewModel();
            return View("TeamForm", new_team);
        }

        public async Task<IActionResult> EditTeam(Guid Id)
        {
            if (!User.Identity.IsAuthenticated)
                return Content("Access denied.");

            TeamDto team = await teamService.GetTeam(Id);
            TeamViewModel teamViewModel = mapper.Map<TeamViewModel>(team);
            return View("TeamForm", teamViewModel);
        }

        public async Task<IActionResult> DeleteTeam(Guid Id)
        {
            if (!User.Identity.IsAuthenticated)
                return Content("Access denied.");

            await teamService.RemoveTeam(Id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveTeam(TeamViewModel teamViewModel)
        {
            if (!User.Identity.IsAuthenticated)
                return Content("Access denied.");

            if (!ModelState.IsValid)
                return View("TeamForm", teamViewModel);

            TeamDto team = mapper.Map<TeamDto>(teamViewModel);

            if (team.Id == Guid.Empty)
                await teamService.AddTeam(team);
            else
                await teamService.EditTeam(team);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private readonly ITeamService teamService;
        private readonly IMapper mapper;
        private readonly ILogger<HomeController> _logger;
    }
}
