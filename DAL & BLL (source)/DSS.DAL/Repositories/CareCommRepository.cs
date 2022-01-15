namespace DSS.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    public class CareCommRepository : IRepository<Home>
    {
        readonly MyContext db;

        public CareCommRepository(MyContext db) => this.db = db;

        public async Task Create(Home entity)
        {
            db.Homes.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            db.Homes.Remove(await db.Homes.FindAsync(id));
            await db.SaveChangesAsync();
        }

        public async Task<Home> Get(int id)
        {
            return await db.Homes.FindAsync(id);
        }

        public IQueryable<Home> GetAll()
        {
            return db.Homes;
        }

        public async Task<System.Collections.Generic.List<Home>> GetAllAsync()
        {
            return await GetAll().ToListAsync();
        }

        public void Update(Home entity)
        {
            db.Entry(entity).State = EntityState.Modified;
        }

        public async Task UpdateAsync(int id)
        {
            db.Entry(await db.Homes.FindAsync(id)).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
