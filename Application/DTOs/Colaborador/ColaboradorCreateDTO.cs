using System;
using Application.DTOs.Cargo;

namespace Application.DTOs.Colaborador
{
    public class ColaboradorCreateDTO
    {
        public string Rut { get; set; }
        public string DV { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPat { get; set; }
        public string ApellidoMat { get; set; }
        public string Email { get; set; }
        public string Fono { get; set; }
        public string ContactoEmerg { get; set; }
        public string EmailCorp { get; set; }
        public string FonoCorp { get; set; }
        public string Direccion { get; set; }
        public int? CargoId { get; set; } //Opcional, pero puede cambiar segun la regla de negocio
        public CargoDTO Cargo{ get; set; }
        public Uri FotoUrl { get; set; }
    }
}