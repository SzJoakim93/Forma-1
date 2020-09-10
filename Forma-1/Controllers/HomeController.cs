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

namespace Forma_1.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(ILogger<HomeController> logger, TeamService teamService)
        {
            _logger = logger;
            this.teamService = teamService;
        }

        public async Task<IActionResult> Index()
        {
            var teams = await teamService.GetTeamList();
            return View(teams);
        }

        public IActionResult AddTeam()
        {
            var new_team = new Team();
            return View("TeamForm", new_team);
        }

        public async Task<IActionResult> EditTeam(Guid Id)
        {
            var team = await teamService.GetTeam(Id);
            return View("TeamForm", team);
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
        public async Task<IActionResult> SaveTeam(Team team)
        {
            try
            {
                var UserId = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value;

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

        TeamService teamService;
        private readonly ILogger<HomeController> _logger;
    }
}
