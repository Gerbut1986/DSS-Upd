namespace DSS.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    public class WorkshopBCInspectRepo : IRepository<WorkshopBCInspection>
    {
        readonly MyContext db;

        public WorkshopBCInspectRepo(MyContext db) => this.db = db;

        public async Task Create(WorkshopBCInspection entity)
        {
            db.WorkshopBCInspections.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            db.WorkshopBCInspections.Remove(await db.WorkshopBCInspections.FindAsync(id));
            await db.SaveChangesAsync();
        }

        public async Task<WorkshopBCInspection> Get(int id)
        {
            return await db.WorkshopBCInspections.FindAsync(id);
        }

        public IQueryable<WorkshopBCInspection> GetAll()
        {
            return db.WorkshopBCInspections;
        }

        public async Task<System.Collections.Generic.List<WorkshopBCInspection>> GetAllAsync()
        {
            return await GetAll().ToListAsync();
        }

        public void Update(WorkshopBCInspection entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public Task UpdateAsync(WorkshopBCInspection entity)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
