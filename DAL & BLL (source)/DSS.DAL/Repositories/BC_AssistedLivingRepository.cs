namespace DSS.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Linq;
    using System.Data.Entity;
    using System.Threading.Tasks;

    public class BC_AssistedLivingRepository : IRepository<BC_Assisted_Living_Reportable_Incidents>
    {
        readonly MyContext db;

        public BC_AssistedLivingRepository(MyContext db) => this.db = db;

        public async Task Create(BC_Assisted_Living_Reportable_Incidents entity)
        {
            db.BC_LTC_Assisted_Incidents.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            db.BC_LTC_Assisted_Incidents.Remove(await db.BC_LTC_Assisted_Incidents.FindAsync(id));
            await db.SaveChangesAsync();
        }

        public async Task<BC_Assisted_Living_Reportable_Incidents> Get(int id)
        {
            return await db.BC_LTC_Assisted_Incidents.FindAsync(id);
        }

        public IQueryable<BC_Assisted_Living_Reportable_Incidents> GetAll()
        {
            return db.BC_LTC_Assisted_Incidents;
        }

        public async Task<System.Collections.Generic.List<BC_Assisted_Living_Reportable_Incidents>> GetAllAsync()
        {
            var meth = GetAll().ToListAsync();
            return await meth;
        }

        public void Update(BC_Assisted_Living_Reportable_Incidents entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public Task UpdateAsync(BC_Assisted_Living_Reportable_Incidents entity)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
