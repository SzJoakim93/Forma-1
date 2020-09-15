using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTO;
using DAL;
using Model;
using Microsoft.EntityFrameworkCore;
using BLL.ServiceInterfaces;

namespace BLL.Services
{
    public class TeamService : ITeamService
    {
        public TeamService(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task AddTeam(TeamDto teamDto)
        {
            Team team = mapper.Map<Team>(teamDto);
            await dbContext.Teams.AddAsync(team);
            await dbContext.SaveChangesAsync();
        }

        public async Task EditTeam(TeamDto team)
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

        public async Task<List<TeamDto>> GetTeamList()
        {
            List<TeamDto> teams = mapper.Map<List<TeamDto>>(await dbContext.Teams.ToListAsync());
            return teams;
        }

        public async Task<TeamDto> GetTeam(Guid id)
        {
            TeamDto team = mapper.Map<TeamDto>(await dbContext.Teams.FirstOrDefaultAsync(x => x.Id == id));
            return team;
        }

        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
    }    
} 
