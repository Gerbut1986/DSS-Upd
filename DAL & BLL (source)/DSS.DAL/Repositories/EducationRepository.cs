namespace DSS.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Linq;
    using System.Threading.Tasks;

    public class EducationRepository : IRepository<Education>
    {
        readonly  MyContext db;

        public EducationRepository(MyContext db) => this.db = db;

        public async Task Create(Education entity)
        {
            db.Educations.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            db.Educations.Remove(await db.Educations.FindAsync(id));
            await db.SaveChangesAsync();
        }

        public async Task<Education> Get(int id)
        {
            return await db.Educations.FindAsync(id);
        }

        public IQueryable<Education> GetAll()
        {
            return db.Educations;
        }

        public Task<System.Collections.Generic.List<Education>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public void Update(Education entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public async Task UpdateAsync(int id)
        {
            db.Entry(await db.Educations.FindAsync(id)).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
