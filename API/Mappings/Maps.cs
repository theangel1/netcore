using Application.DTOs.Cargo;
using AutoMapper;
using Domain;

namespace API.Mappings
{   
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<Cargo, CargoDTO>().ReverseMap();
            CreateMap<Cargo, CargoCreateDTO>().ReverseMap();
        }
    }
}