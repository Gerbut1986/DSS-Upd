namespace DSS.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Linq;
    using System.Threading.Tasks;

    public class OtherRepository : Interfaces.IRepository<Other>
    {
        readonly MyContext db;

        public OtherRepository(MyContext db) => this.db = db;

        public async Task Create(Other entity)
        {
            db.Others.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            db.Others.Remove(await db.Others.FindAsync(id));
            await db.SaveChangesAsync();
        }

        public async Task<Other> Get(int id)
        {
            return await db.Others.FindAsync(id);
        }

        public IQueryable<Other> GetAll()
        {
            return db.Others;
        }

        public Task<System.Collections.Generic.List<Other>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public void Update(Other entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public async Task UpdateAsync(int id)
        {
            db.Entry(await db.Others.FindAsync(id)).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();
        }

    }
}
