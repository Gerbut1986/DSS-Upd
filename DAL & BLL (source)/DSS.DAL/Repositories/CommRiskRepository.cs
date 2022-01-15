namespace DSS.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Linq;
    using System.Threading.Tasks;

    public class CommRiskRepository : IRepository<Community_Risks>
    {
        readonly MyContext db;

        public CommRiskRepository(MyContext db) => this.db = db;

        public async Task Create(Community_Risks entity)
        {
            db.Community_Risks.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            db.Community_Risks.Remove(await db.Community_Risks.FindAsync(id));
            await db.SaveChangesAsync();
        }

        public async Task<Community_Risks> Get(int id)
        {
            return await db.Community_Risks.FindAsync(id);
        }
        public IQueryable<Community_Risks> GetAll()
        {
            return db.Community_Risks;
        }

        public Task<System.Collections.Generic.List<Community_Risks>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public void Update(Community_Risks entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public async Task UpdateAsync(int id)
        {
            db.Entry(await db.Community_Risks.FindAsync(id)).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
