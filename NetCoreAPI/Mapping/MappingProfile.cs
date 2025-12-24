using AutoMapper;
using NetCoreAPI.Models;
using NetCoreAPI.Dto;

namespace NetCoreAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Entity -> DTO
            CreateMap<Campaing, CampaingCreateDTO>();
            CreateMap<Campaing, CampaingListDTO>();


            // DTO -> Entity
            CreateMap<CampaingCreateDTO, Campaing>();
        
        }
    }
}
