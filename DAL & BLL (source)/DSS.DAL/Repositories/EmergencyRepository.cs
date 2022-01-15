namespace DSS.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    public class EmergencyRepository : IRepository<Emergency_Prep>
    {
        readonly MyContext db;

        public EmergencyRepository(MyContext db) => this.db = db;

        public async Task Create(Emergency_Prep entity)
        {
            db.Emergency_Prep.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            db.Emergency_Prep.Remove(await db.Emergency_Prep.FindAsync(id));
            await db.SaveChangesAsync();
        }

        public async Task<Emergency_Prep> Get(int id)
        {
            return await db.Emergency_Prep.FindAsync(id);
        }

        public IQueryable<Emergency_Prep> GetAll()
        {
            return db.Emergency_Prep;
        }

        public async Task<System.Collections.Generic.List<Emergency_Prep>> GetAllAsync()
        {
            return await GetAll().ToListAsync();
        }

        public void Update(Emergency_Prep entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public async Task UpdateAsync(int id)
        {
            db.Entry(await db.Emergency_Prep.FindAsync(id)).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
