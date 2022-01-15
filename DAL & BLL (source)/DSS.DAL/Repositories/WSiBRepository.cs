namespace DSS.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Linq;
    using System.Threading.Tasks;

    public class WSiBRepository : IRepository<WSIB>
    {
        readonly MyContext db;

        public WSiBRepository(MyContext db) => this.db = db;

        public async Task Create(WSIB entity)
        {
            db.WSIBs.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            db.WSIBs.Remove(await db.WSIBs.FindAsync(id));
            await db.SaveChangesAsync();
        }

        public async Task<WSIB> Get(int id)
        {
            return await db.WSIBs.FindAsync(id);
        }

        public IQueryable<WSIB> GetAll()
        {
            return db.WSIBs;
        }

        public Task<System.Collections.Generic.List<WSIB>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public void Update(WSIB entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public async Task UpdateAsync(int id)
        {
            db.Entry(await db.WSIBs.FindAsync(id)).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
