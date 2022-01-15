namespace DSS.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Linq;
    using System.Threading.Tasks; 

    class LicensInspecRepository : IRepository<LicensingInspection>
    {
        readonly MyContext db;

        public LicensInspecRepository(MyContext db) => this.db = db;

        public async Task Create(LicensingInspection entity)
        {
            db.LicensingInspections.Add(entity);
            int res = await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var en = await db.LicensingInspections.FindAsync(id);
            db.LicensingInspections.Remove(en);
            await db.SaveChangesAsync();
        }

        public async Task<LicensingInspection> Get(int id)
        {
            return await db.LicensingInspections.FindAsync(id);
        }

        public IQueryable<LicensingInspection> GetAll()
        {
            return db.LicensingInspections;
        }

        public Task<System.Collections.Generic.List<LicensingInspection>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public void Update(LicensingInspection entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public async Task UpdateAsync(int id)
        {
            db.Entry(await db.LicensingInspections.FindAsync(id)).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
