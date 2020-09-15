using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTO;

namespace BLL.ServiceInterfaces
{
    public interface ITeamService
    {
        Task AddTeam(TeamDto teamDto);
        Task EditTeam(TeamDto team);
        Task RemoveTeam(Guid id);
        Task<List<TeamDto>> GetTeamList();
        Task<TeamDto> GetTeam(Guid id);
    }
}