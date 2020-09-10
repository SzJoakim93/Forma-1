using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Forma_1.Data;
using Forma_1.Models;
using Microsoft.EntityFrameworkCore;

namespace Forma_1.Services
{
    public class TeamService
    {
        public TeamService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddTeam(Team new_team)
        {
            await dbContext.Teams.AddAsync(new_team);
            await dbContext.SaveChangesAsync();
        }

        public async Task EditTeam(Team team)
        {
            var editTeam = await dbContext.Teams.FirstOrDefaultAsync(x => x.Id == team.Id);
            editTeam.Name = team.Name;
            editTeam.Wins = team.Wins;
            editTeam.IsPaid = team.IsPaid;
            editTeam.Founded = team.Founded;
            await dbContext.SaveChangesAsync();
        }

        public async Task RemoveTeam(Guid id)
        {
            Team to_delete = await dbContext.Teams.FirstOrDefaultAsync(x => x.Id == id);
            dbContext.Teams.Remove(to_delete);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Team>> GetTeamList()
        {
            return await dbContext.Teams.ToListAsync();
        }

        public async Task<Team> GetTeam(Guid id)
        {
            return await dbContext.Teams.FirstOrDefaultAsync(x => x.Id == id);
        }

        private readonly ApplicationDbContext dbContext;
    }    
} 
