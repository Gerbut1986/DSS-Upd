namespace DSS.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Linq;
    using System.Threading.Tasks;

    public class AssistLivInspecRepository : IRepository<AssistedLivingInspection>
    {
        readonly MyContext db;

        public AssistLivInspecRepository(MyContext db) => this.db = db;

        public async Task Create(AssistedLivingInspection entity)
        {
            db.AssistedLivingInspections.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var en = await db.AssistedLivingInspections.FindAsync(id);
            db.AssistedLivingInspections.Remove(en);
            await db.SaveChangesAsync();
        }

        public async Task<AssistedLivingInspection> Get(int id)
        {
            return await db.AssistedLivingInspections.FindAsync(id);
        }

        public IQueryable<AssistedLivingInspection> GetAll()
        {
            return db.AssistedLivingInspections;
        }

        public Task<System.Collections.Generic.List<AssistedLivingInspection>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public void Update(AssistedLivingInspection entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public async Task UpdateAsync(int id)
        {
            db.Entry(await db.AssistedLivingInspections.FindAsync(id)).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
