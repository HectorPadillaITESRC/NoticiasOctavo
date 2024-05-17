using AutoMapper;
using NoticiasOctavoAPI.Models.DTOs;
using NoticiasOctavoAPI.Models.Entities;

namespace NoticiasOctavoAPI.Helpers
{
    public class AutomapperProfile:Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Usuarios, PeriodistaDTO>();
            CreateMap<PeriodistaDTO, Usuarios>();

            CreateMap<Usuarios, Periodista2DTO>();
            CreateMap<Periodista2DTO, Usuarios>();

        }
    }
}
