using System;

namespace Domain
{
    public class BaseEntity
    {
        public DateTime? FechaCreacion { get; set; } 
        public DateTime? FechaModificacion{ get; set; }
    }
}