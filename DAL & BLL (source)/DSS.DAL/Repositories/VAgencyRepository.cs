namespace DSS.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Linq;
    using System.Threading.Tasks;

    public class VAgencyRepository : IRepository<Visits_Agency>
    {
        readonly MyContext db;

        public VAgencyRepository(MyContext db) => this.db = db;

        public async Task Create(Visits_Agency entity)
        {
            db.Visits_Agencies.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            db.Visits_Agencies.Remove(await db.Visits_Agencies.FindAsync(id));
            await db.SaveChangesAsync();
        }

        public async Task<Visits_Agency> Get(int id)
        {
            return await db.Visits_Agencies.FindAsync(id);
        }

        public IQueryable<Visits_Agency> GetAll()
        {
            return db.Visits_Agencies;
        }

        public Task<System.Collections.Generic.List<Visits_Agency>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public void Update(Visits_Agency entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public async Task UpdateAsync(int id)
        {
            db.Entry(await db.Visits_Agencies.FindAsync(id)).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
