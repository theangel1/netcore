using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class Usuario : IdentityUser
    {
         public string Rut { get; set; }

        public int RutCuerpo { get { return int.Parse(Rut.Substring(0, Rut.Length - 2)); } }
        public string DV { get { return Rut.Substring(Rut.Length - 1, 1); } }
    }
}