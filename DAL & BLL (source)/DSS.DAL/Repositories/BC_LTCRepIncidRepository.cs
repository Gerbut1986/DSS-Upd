namespace DSS.DAL.Repositories
{ 
    using EF;
    using Entities;
    using Interfaces;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    public class BC_LTCRepIncidRepository : IRepository<BC_LTC_Reportable_Incidents>
    {
        readonly MyContext db;

        public BC_LTCRepIncidRepository(MyContext db) => this.db = db;

        public async Task Create(BC_LTC_Reportable_Incidents entity)
        {
            db.BC_LTC_Reportable_Incidents.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            db.BC_LTC_Reportable_Incidents.Remove(await db.BC_LTC_Reportable_Incidents.FindAsync(id));
            await db.SaveChangesAsync();
        }

        public async Task<BC_LTC_Reportable_Incidents> Get(int id)
        {
            return await db.BC_LTC_Reportable_Incidents.FindAsync(id);
        }

        public IQueryable<BC_LTC_Reportable_Incidents> GetAll()
        {
            return db.BC_LTC_Reportable_Incidents;
        }

        public async Task<List<BC_LTC_Reportable_Incidents>> GetAllAsync()
        {
            return await GetAll().ToListAsync();
        }

        public void Update(BC_LTC_Reportable_Incidents entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public async Task UpdateAsync(int id)
        {
            db.Entry(await db.BC_LTC_Reportable_Incidents.FindAsync(id)).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
