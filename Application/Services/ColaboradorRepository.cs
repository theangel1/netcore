using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Contracts;
using Application.DTOs;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.DapperConnection;

namespace Application.Services
{
    public class ColaboradorRepository : IColaboradorRepository
    {
        private readonly ApplicationDbContext _db;
        public ColaboradorRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Colaborador entity)
        {
            await _db.Colaborador.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Colaborador entity)
        {
            _db.Colaborador.Remove(entity);
            return await Save();
        }

        public async Task<IList<Colaborador>> FindAll()
        {
            var colaboradores = await _db.Colaborador.Include(c => c.Cargo).ToListAsync();
            return colaboradores;
        }

        public async Task<Colaborador> FindById(int id)
        {
            var colaborador = await _db.Colaborador.Include(c => c.Cargo).FirstOrDefaultAsync();
            return colaborador;
        }      

        public Task<PaginacionModel> GetPaginacion(PaginacionCursoRequestDTO request)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> IsExists(int rut)
        {
            var isExists = await _db.Colaborador.AnyAsync(c => c.Rut == rut.ToString());
            return isExists;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Colaborador entity)
        {
            _db.Colaborador.Update(entity);
            return await Save();
        }
    }
}