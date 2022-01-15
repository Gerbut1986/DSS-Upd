namespace DSS.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    public class ComplaintRepository : IRepository<Complaint>
    {
        readonly MyContext db;

        public ComplaintRepository(MyContext db) => this.db = db;

        public async Task Create(Complaint entity)
        {
            db.Complaints.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var en = await db.Complaints.FindAsync(id);
            db.Complaints.Remove(en);
            await db.SaveChangesAsync();
        }

        public async Task<Complaint> Get(int id)
        {
            return await db.Complaints.FindAsync(id);
        }

        public IQueryable<Complaint> GetAll()
        {
            return db.Complaints;
        }

        public async Task<System.Collections.Generic.List<Complaint>> GetAllAsync()
        {
            return await GetAll().ToListAsync();
        }

        public void Update(Complaint entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public async Task UpdateAsync(int id)
        {
            db.Entry(await db.Complaints.FindAsync(id)).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
