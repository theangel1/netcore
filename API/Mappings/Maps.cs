using Application.DTOs.Cargo;
using Application.DTOs.Colaborador;
using AutoMapper;
using Domain;

namespace API.Mappings
{   
    public class Maps : Profile
    {
        public Maps()
        {
            //Mapeo de cargo
            CreateMap<Cargo, CargoDTO>().ReverseMap();
            CreateMap<Cargo, CargoCreateDTO>().ReverseMap();
            CreateMap<Cargo, CargoUpdateDTO>().ReverseMap();

            //Mapeo de colaborador
            CreateMap<Colaborador, ColaboradorDTO>().ReverseMap();
            CreateMap<Colaborador, ColaboradorCreateDTO>().ReverseMap();
            CreateMap<Colaborador, ColaboradorUpdateDTO>().ReverseMap();
        }
    }
}