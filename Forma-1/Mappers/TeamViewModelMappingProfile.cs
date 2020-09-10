using AutoMapper;
using Forma_1.Models;
using Forma_1.ViewModels;

namespace Forma_1.Mappers
{
    public class TeamViewModelMappingProfile : Profile
    {
        public TeamViewModelMappingProfile()
        {
            CreateMap<Team, TeamViewModel>().ReverseMap();
        }
    }
}