using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Forma_1.Models;
using Forma_1.Services;
using System.Security.Claims;
using AutoMapper;
using Forma_1.ViewModels;

namespace Forma_1.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(ILogger<HomeController> logger, IMapper mapper, TeamService teamService)
        {
            _logger = logger;
            this.teamService = teamService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Team> teams = await teamService.GetTeamList();
            IEnumerable<TeamViewModel> teamsViewModel = mapper.Map<IEnumerable<TeamViewModel>>(teams);
            return View(teamsViewModel);
        }

        public IActionResult AddTeam()
        {
            TeamViewModel new_team = new TeamViewModel();
            return View("TeamForm", new_team);
        }

        public async Task<IActionResult> EditTeam(Guid Id)
        {
            Team team = await teamService.GetTeam(Id);
            TeamViewModel teamViewModel = mapper.Map<TeamViewModel>(team);
            return View("TeamForm", teamViewModel);
        }

        public async Task<IActionResult> DeleteTeam(Guid Id)
        {
            try
            {
                var UserId = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value;
                await teamService.RemoveTeam(Id);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return Content("An error occoured: " + e.Message +  " Access denied.");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveTeam(TeamViewModel teamViewModel)
        {
            try
            {
                var UserId = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value;
                Team team = mapper.Map<Team>(teamViewModel);

                if (team.Id == Guid.Empty)
                    await teamService.AddTeam(team);
                else
                    await teamService.EditTeam(team);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                return Content("An error occoured: " + e.Message +  " Access denied.");
            }
            
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

        private readonly TeamService teamService;
        private readonly IMapper mapper;
        private readonly ILogger<HomeController> _logger;
    }
}
