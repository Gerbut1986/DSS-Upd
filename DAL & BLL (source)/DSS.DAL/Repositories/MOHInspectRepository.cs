namespace DSS.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    public class MOHInspectRepository : IRepository<MOH_Inspection>
    {
        readonly MyContext db;

        public MOHInspectRepository(MyContext db) => this.db = db;

        public async Task Create(MOH_Inspection entity)
        {
            db.MOH_Inspections.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            db.MOH_Inspections.Remove(await db.MOH_Inspections.FindAsync(id));
            await db.SaveChangesAsync();
        }

        public async Task<MOH_Inspection> Get(int id)
        {
            return await db.MOH_Inspections.FindAsync(id);
        }

        public IQueryable<MOH_Inspection> GetAll()
        {
            return db.MOH_Inspections;
        }

        public async Task<System.Collections.Generic.List<MOH_Inspection>> GetAllAsync()
        {
            return await GetAll().ToListAsync();
        }

        public void Update(MOH_Inspection entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public async Task UpdateAsync(int id)
        {
            db.Entry(await db.MOH_Inspections.FindAsync(id)).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
