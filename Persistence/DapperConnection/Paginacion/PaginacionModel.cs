using System.Collections.Generic;

namespace Persistence.DapperConnection
{
    public class PaginacionModel
    {
        public List<IDictionary<string, object>> ListaRecords { get; set; }
        public int TotalRecords { get; set; }
        public int NumeroPaginas { get; set; }
    }
}