namespace Application.DTOs
{
    public class PaginacionCursoRequestDTO
    {
        public string Titulo { get; set; }
        public int NumeroPagina { get; set; }
        public int CantidadElementos { get; set; }
    }
}