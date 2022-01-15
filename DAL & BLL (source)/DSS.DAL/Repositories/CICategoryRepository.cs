namespace DSS.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    public class CICategoryRepository : IRepository<CI_Category_Type>
    {
        readonly MyContext db;

        public CICategoryRepository(MyContext db) => this.db = db;

        public async Task Create(CI_Category_Type entity)
        {
            db.CI_Category_Types.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            db.Critical_Incidents.Remove(await db.Critical_Incidents.FindAsync(id));
            await db.SaveChangesAsync();
        }

        public async Task<CI_Category_Type> Get(int id)
        {
            return await db.CI_Category_Types.FindAsync(id);
        }

        public IQueryable<CI_Category_Type> GetAll()
        {
            return db.CI_Category_Types;
        }

        public async Task<System.Collections.Generic.List<CI_Category_Type>> GetAllAsync()
        {
            return await GetAll().ToListAsync();
        }

        public void Update(CI_Category_Type entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public Task UpdateAsync(CI_Category_Type entity)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
