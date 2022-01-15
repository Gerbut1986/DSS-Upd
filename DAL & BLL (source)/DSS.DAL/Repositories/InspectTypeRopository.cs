namespace DSS.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    public class InspectTypeRopository : IRepository<InspectType>
    {
        readonly MyContext db;

        public InspectTypeRopository(MyContext db) => this.db = db;

        public async Task Create(InspectType entity)
        {
            db.InspectTypes.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            db.InspectTypes.Remove(await db.InspectTypes.FindAsync(id));
            await db.SaveChangesAsync();
        }

        public async Task<InspectType> Get(int id)
        {
            return await db.InspectTypes.FindAsync(id);
        }

        public IQueryable<InspectType> GetAll()
        {
            return db.InspectTypes;
        }

        public async Task<System.Collections.Generic.List<InspectType>> GetAllAsync()
        {
            return await GetAll().ToListAsync();
        }

        public void Update(InspectType entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public Task UpdateAsync(InspectType entity)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
