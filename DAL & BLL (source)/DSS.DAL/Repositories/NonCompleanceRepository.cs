namespace DSS.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Linq;
    using System.Threading.Tasks;

    public class NonCompleanceRepository : IRepository<NonCompleance>
    {
        readonly MyContext db;

        public NonCompleanceRepository(MyContext db) => this.db = db;

        public async Task Create(NonCompleance entity)
        {
            db.NonCompleances.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var res = db.NonCompleances.Remove(await db.NonCompleances.FindAsync(id));
            await db.SaveChangesAsync();
        }

        public async Task<NonCompleance> Get(int id)
        {
            return await db.NonCompleances.FindAsync(id);
        }

        public IQueryable<NonCompleance> GetAll()
        {
            return db.NonCompleances;
        }

        public Task<System.Collections.Generic.List<NonCompleance>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public void Update(NonCompleance entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public async Task UpdateAsync(int id)
        {
            db.Entry(await db.NonCompleances.FindAsync(id)).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
