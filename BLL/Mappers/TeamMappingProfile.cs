using AutoMapper;
using BLL.DTO;
using Model;

namespace BLL.Mappers
{
    public class TeamMappingProfile : Profile
    {
        public TeamMappingProfile()
        {
            CreateMap<Team, TeamDto>().ReverseMap();
        }
    }
}