namespace DSS.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Linq;
    using System.Threading.Tasks;

    public class LTCHARegRepository : IRepository<LTCHAReg>
    {
        readonly MyContext db;

        public LTCHARegRepository(MyContext db) => this.db = db;

        public async Task Create(LTCHAReg entity)
        {
            db.LTCHARegs.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var res = db.LTCHARegs.Remove(await db.LTCHARegs.FindAsync(id));
            await db.SaveChangesAsync();
        }

        public async Task<LTCHAReg> Get(int id)
        {
            return await db.LTCHARegs.FindAsync(id);
        }

        public IQueryable<LTCHAReg> GetAll()
        {
            return db.LTCHARegs;
        }

        public Task<System.Collections.Generic.List<LTCHAReg>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public void Update(LTCHAReg entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public async Task UpdateAsync(int id)
        {
            db.Entry(await db.LTCHARegs.FindAsync(id)).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
