namespace DSS.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    public class IncidentRepository : IRepository<Critical_Incidents>
    {
        readonly MyContext db;

        public IncidentRepository(MyContext db) => this.db = db;

        public async Task Create(Critical_Incidents entity)
        {
            db.Critical_Incidents.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            db.Critical_Incidents.Remove(await db.Critical_Incidents.FindAsync(id));
            await db.SaveChangesAsync();
        }

        public async Task<Critical_Incidents> Get(int id)
        {
            return await db.Critical_Incidents.FindAsync(id);
        }

        public IQueryable<Critical_Incidents> GetAll()
        {
            return db.Critical_Incidents;
        }

        public async Task<System.Collections.Generic.List<Critical_Incidents>> GetAllAsync()
        {
            var lst = (from ci in db.Critical_Incidents select ci);
            return await lst.ToListAsync();
        }

        public void Update(Critical_Incidents entity)
        {
            db.Entry(entity).State = EntityState.Modified;
        }

        public async Task UpdateAsync(int id)
        {
            db.Entry(Get(id)).State = EntityState.Modified;
            int res = await db.SaveChangesAsync();
        }
    }
}
