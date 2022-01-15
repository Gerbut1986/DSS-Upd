namespace DSS.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Linq;
    using System.Threading.Tasks;

    public class PBreachesRepository : IRepository<Privacy_Breaches>
    {
        readonly MyContext db;

        public PBreachesRepository(MyContext db) => this.db = db;

        public async Task Create(Privacy_Breaches entity)
        {
            db.Privacy_Breaches.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            db.Privacy_Breaches.Remove(await db.Privacy_Breaches.FindAsync(id));
            await db.SaveChangesAsync();
        }

        public async Task<Privacy_Breaches> Get(int id)
        {
            return await db.Privacy_Breaches.FindAsync(id);
        }
        public IQueryable<Privacy_Breaches> GetAll()
        {
            return db.Privacy_Breaches;
        }

        public Task<System.Collections.Generic.List<Privacy_Breaches>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public void Update(Privacy_Breaches entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public async Task UpdateAsync(int id)
        {
            db.Entry(await db.Privacy_Breaches.FindAsync(id)).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
