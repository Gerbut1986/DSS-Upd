namespace DSS.DAL.Repositories
{ 
    using EF;
    using Entities;
    using Interfaces;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    public class PositionRepository : IRepository<Position>
    {
        readonly MyContext db;

        public PositionRepository(MyContext db) => this.db = db;

        public async Task Create(Position entity)
        {
            db.Positions.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            db.Positions.Remove(await db.Positions.FindAsync(id));
            await db.SaveChangesAsync();
        }

        public async Task<Position> Get(int id)
        {
            return await db.Positions.FindAsync(id);
        }

        public IQueryable<Position> GetAll()
        {
            return db.Positions;
        }

        public async Task<List<Position>> GetAllAsync()
        {
            return await GetAll().ToListAsync();
        }

        public void Update(Position entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public async Task UpdateAsync(int id)
        {
            db.Entry(await db.Positions.FindAsync(id)).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
