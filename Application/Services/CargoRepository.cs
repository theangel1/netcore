using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Contracts;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services
{
    public class CargoRepository : ICargoRepository
    {
        private readonly ApplicationDbContext _db;
        public CargoRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        

        public async Task<bool> Create(Cargo entity)
        {
            await _db.Cargo.AddAsync(entity);
            return await Save();
        }

        public async Task<IList<Cargo>> FindAll()
        {
            var cargos = await _db.Cargo.ToListAsync();
            return cargos;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }
    }
}