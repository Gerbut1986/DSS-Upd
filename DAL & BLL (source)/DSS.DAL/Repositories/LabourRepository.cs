namespace DSS.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class LabourRepository : IRepository<Labour_Relations>
    {
        readonly MyContext db;

        public LabourRepository(MyContext db) => this.db = db;

        public async Task Create(Labour_Relations entity)
        {
            db.Relations.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            db.Relations.Remove(await db.Relations.FindAsync(id));
            await db.SaveChangesAsync();
        }

        public async Task<Labour_Relations> Get(int id)
        {
            return await db.Relations.FindAsync(id);
        }

        public IQueryable<Labour_Relations> GetAll()
        {
            return db.Relations;
        }

        public Task<List<Labour_Relations>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public void Update(Labour_Relations entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public async Task UpdateAsync(int id)
        {
            db.Entry(await db.Relations.FindAsync(id)).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
