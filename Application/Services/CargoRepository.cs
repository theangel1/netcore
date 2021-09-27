using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts;
using Application.DTOs;
using Dapper;
using Domain;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.DapperConnection;

namespace Application.Services
{
    public class CargoRepository : ICargoRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IFactoryConnection _factoryConnection;

        public CargoRepository(ApplicationDbContext db, IFactoryConnection factoryConnection)
        {
            _factoryConnection = factoryConnection;
            _db = db;
        }

        public async Task<bool> Create(Cargo entity)
        {
            entity.FechaCreacion = DateTime.Now;//no se si sea la mejor opcion implementar esta logica acá
            await _db.Cargo.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Cargo entity)
        {
            //Implementaremos esta logica? o solo lo ocultaremos?
            _db.Cargo.Remove(entity);
            return await Save();
        }

        public async Task<IList<Cargo>> FindAll()
        {
            var cargos = await _db.Cargo.ToListAsync();
            return cargos;
        }

        public async Task<Cargo> FindById(int id)
        {
            var cargo = await _db.Cargo.FirstOrDefaultAsync(c => c.Id == id);
            return cargo;
        }

        public async Task<PaginacionModel> GetPaginacion(PaginacionCursoRequestDTO request)
        {
            var storeProcedure = "sp_obtener_cargo_paginacion";
            var ordenamiento = "Nombre";//Lo ordenaré por nombre...
            var parametros = new Dictionary<string, object>();
            parametros.Add("NombreCargo", request.Titulo);
            return await DevolverPaginacion(storeProcedure, request.NumeroPagina, request.CantidadElementos, parametros, ordenamiento);
        }

        public async Task<PaginacionModel> DevolverPaginacion(string storeProcedure, int numeroPagina, int cantidadElementos, IDictionary<string, object> parametrosFiltro, string ordenamientoColumna)
        {
            var paginacionModel = new PaginacionModel();
            List<IDictionary<string, object>> listaReporte = null;
            int totalRecords = 0;
            int totalPaginas = 0;

            try
            {
                var connection = _factoryConnection.GetConnection();
                DynamicParameters parametros = new DynamicParameters();

                foreach (var param in parametrosFiltro)
                {
                    parametros.Add("@" + param.Key, param.Value);
                }

                parametros.Add("@NumeroPagina", numeroPagina);
                parametros.Add("@CantidadElementos", cantidadElementos);
                parametros.Add("@Ordenamiento", ordenamientoColumna);

                parametros.Add("@TotalRecords", totalRecords, DbType.Int32, ParameterDirection.Output);
                parametros.Add("@TotalPaginas", totalPaginas, DbType.Int32, ParameterDirection.Output);

                var result = await connection.QueryAsync(storeProcedure, parametros, commandType: CommandType.StoredProcedure);
                listaReporte = result.Select(x => (IDictionary<string, object>)x).ToList();
                
                paginacionModel.ListaRecords = listaReporte;
                paginacionModel.NumeroPaginas = parametros.Get<int>("@TotalPaginas");
                paginacionModel.TotalRecords = parametros.Get<int>("@TotalRecords");

            }
            catch (Exception e)
            {
                throw new Exception("No se pudo ejecutar el procedimiento almacenado. ", e);
            }
            finally
            {
                _factoryConnection.CloseConnection();
            }

            return paginacionModel;

        }

        public async Task<bool> IsExists(int id)
        {
            return await _db.Cargo.AnyAsync(c => c.Id == id);
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Cargo entity)
        {
            entity.FechaModificacion = DateTime.Now;
            _db.Cargo.Update(entity);
            return await Save();
        }
    }
}