using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cargo
{
    public class CargoCreateDTO
    {
        [Required(ErrorMessage = ("Nombre no encontrado"))]        
        public string Nombre { get; set; }

        [Required(ErrorMessage = ("Descripcion no encontrada"))]
        public string Descripcion { get; set; }
    }
}