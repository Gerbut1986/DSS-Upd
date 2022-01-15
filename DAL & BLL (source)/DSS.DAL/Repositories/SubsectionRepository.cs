namespace DSS.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Linq;
    using System.Threading.Tasks;

    public class SubsectionRepository : IRepository<Subsection>
    {
        readonly MyContext db;

        public SubsectionRepository(MyContext db) => this.db = db;

        public async Task Create(Subsection entity)
        {
            db.Subsections.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var res = db.Subsections.Remove(await db.Subsections.FindAsync(id));
            await db.SaveChangesAsync();
        }

        public async Task<Subsection> Get(int id)
        {
            return await db.Subsections.FindAsync(id);
        }

        public IQueryable<Subsection> GetAll()
        {
            return db.Subsections;
        }

        public Task<System.Collections.Generic.List<Subsection>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public void Update(Subsection entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public async Task UpdateAsync(int id)
        {
            db.Entry(await db.Subsections.FindAsync(id)).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
