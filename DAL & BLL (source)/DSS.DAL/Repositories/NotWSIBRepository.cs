namespace DSS.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Linq;
    using System.Threading.Tasks;

    public class NotWSiBRepository : IRepository<Not_WSIBs>
    {
        readonly MyContext db;

        public NotWSiBRepository(MyContext db) => this.db = db;

        public async Task Create(Not_WSIBs entity)
        {
            db.Not_WSIBs.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            db.Not_WSIBs.Remove(await db.Not_WSIBs.FindAsync(id));
            await db.SaveChangesAsync();
        }

        public async Task<Not_WSIBs> Get(int id)
        {
            return await db.Not_WSIBs.FindAsync(id);
        }

        public IQueryable<Not_WSIBs> GetAll()
        {
            return db.Not_WSIBs;
        }

        public Task<System.Collections.Generic.List<Not_WSIBs>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public void Update(Not_WSIBs entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public async Task UpdateAsync(int id)
        {
            db.Entry(await db.Not_WSIBs.FindAsync(id)).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
