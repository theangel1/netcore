using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Colaborador : BaseEntity
    {
        [Key]
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
        public int? CargoId { get; set; }
        public Cargo Cargo{ get; set; }
        public Uri FotoUrl { get; set; }
    }
}