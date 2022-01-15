namespace DSS.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Linq;
    using System.Threading.Tasks;

    public class PComplaintsRepository : IRepository<Privacy_Complaints>
    {
        readonly MyContext db;

        public PComplaintsRepository(MyContext db) => this.db = db;

        public async Task Create(Privacy_Complaints entity)
        {
            db.Privacy_Complaints.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            db.Privacy_Complaints.Remove(await db.Privacy_Complaints.FindAsync(id));
            await db.SaveChangesAsync();
        }

        public async Task<Privacy_Complaints> Get(int id)
        {
            return await db.Privacy_Complaints.FindAsync(id);
        }

        public IQueryable<Privacy_Complaints> GetAll()
        {
            return db.Privacy_Complaints;
        }

        public Task<System.Collections.Generic.List<Privacy_Complaints>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public void Update(Privacy_Complaints entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public async Task UpdateAsync(int id)
        {
            db.Entry(await db.Privacy_Complaints.FindAsync(id)).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
