namespace DSS.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Linq;
    using System.Threading.Tasks;

    public class VOthersRepository : IRepository<Visits_Others>
    {
        readonly MyContext db;

        public VOthersRepository(MyContext db) => this.db = db;

        public async Task Create(Visits_Others entity)
        {
            db.Visits_Others.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            db.Visits_Others.Remove(await db.Visits_Others.FindAsync(id));
            await db.SaveChangesAsync();
        }

        public async Task<Visits_Others> Get(int id)
        {
            return await db.Visits_Others.FindAsync(id);
        }

        public IQueryable<Visits_Others> GetAll()
        {
            return db.Visits_Others;
        }

        public Task<System.Collections.Generic.List<Visits_Others>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public void Update(Visits_Others entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public Task UpdateAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
