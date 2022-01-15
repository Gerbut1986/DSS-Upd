namespace DSS.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Linq;
    using System.Threading.Tasks;

    public class OutbreakeRepository : IRepository<Outbreaks>
    {
        readonly MyContext db;

        public OutbreakeRepository(MyContext db) => this.db = db;

        public async Task Create(Outbreaks entity)
        {
            db.Outbreaks.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var res = db.Outbreaks.Remove(await db.Outbreaks.FindAsync(id));
            await db.SaveChangesAsync();
        }

        public async Task<Outbreaks> Get(int id)
        {
            return await db.Outbreaks.FindAsync(id);
        }

        public IQueryable<Outbreaks> GetAll()
        {
            return db.Outbreaks;
        }

        public Task<System.Collections.Generic.List<Outbreaks>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public void Update(Outbreaks entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public async Task UpdateAsync(int id)
        {
            db.Entry(await db.Outbreaks.FindAsync(id)).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
