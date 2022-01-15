namespace DSS.DAL.Repositories
{ 
    using EF;
    using Entities;
    using Interfaces;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    public class ImmunizationRepository : IRepository<Immunization>
    {
        readonly MyContext db;

        public ImmunizationRepository(MyContext db) => this.db = db;

        public async Task Create(Immunization entity)
        {
            db.Immunizations.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            db.Immunizations.Remove(await db.Immunizations.FindAsync(id));
            await db.SaveChangesAsync();
        }

        public async Task<Immunization> Get(int id)
        {
            return await db.Immunizations.FindAsync(id);
        }

        public IQueryable<Immunization> GetAll()
        {
            return db.Immunizations;
        }

        public async Task<System.Collections.Generic.List<Immunization>> GetAllAsync()
        {
            return await GetAll().ToListAsync();
        }

        public void Update(Immunization entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public async Task UpdateAsync(int id)
        {
            db.Entry(await db.Immunizations.FindAsync(id)).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
