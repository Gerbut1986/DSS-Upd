namespace DSS.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Linq;
    using System.Threading.Tasks;

    public class SectionRepository : IRepository<Section>
    {
        readonly MyContext db;

        public SectionRepository(MyContext db) => this.db = db;

        public async Task Create(Section entity)
        {
            db.Sections.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var res = db.Sections.Remove(await db.Sections.FindAsync(id));
            await db.SaveChangesAsync();
        }

        public async Task<Section> Get(int id)
        {
            return await db.Sections.FindAsync(id);
        }

        public IQueryable<Section> GetAll()
        {
            return db.Sections;
        }

        public Task<System.Collections.Generic.List<Section>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public void Update(Section entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public async Task UpdateAsync(int id)
        {
            db.Entry(await db.Sections.FindAsync(id)).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
