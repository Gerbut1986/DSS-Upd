namespace DSS.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Linq;
    using System.Threading.Tasks;

    public class OtherOptionRepository : IRepository<OtherOption>
    {
        readonly MyContext db;

        public OtherOptionRepository(MyContext db) => this.db = db;

        public async Task Create(OtherOption entity)
        {
            db.OtherOptions.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var res = db.OtherOptions.Remove(await db.OtherOptions.FindAsync(id));
            await db.SaveChangesAsync();
        }

        public async Task<OtherOption> Get(int id)
        {
            return await db.OtherOptions.FindAsync(id);
        }

        public IQueryable<OtherOption> GetAll()
        {
            return db.OtherOptions;
        }

        public Task<System.Collections.Generic.List<OtherOption>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public void Update(OtherOption entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public async Task UpdateAsync(int id)
        {
            db.Entry(await db.OtherOptions.FindAsync(id)).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
