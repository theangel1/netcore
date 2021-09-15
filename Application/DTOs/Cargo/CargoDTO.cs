using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cargo
{
    public class CargoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public class CargoCreateDTO
    {
        [Required(ErrorMessage = ("Nombre no encontrado"))]
        [StringLength(5, ErrorMessage = ("Maximo 5 caracteres"))]
        public string Nombre { get; set; }
        
        [Required(ErrorMessage = ("Descripcion no encontrada"))]
        public string Descripcion { get; set; }
    }
}