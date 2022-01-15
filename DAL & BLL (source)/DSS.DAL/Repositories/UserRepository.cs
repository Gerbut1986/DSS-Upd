namespace DSS.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserRepository : IRepository<Users>
    {
        readonly MyContext db;

        public UserRepository(MyContext db) => this.db = db;

        public async Task Create(Users entity)
        {
            db.Users.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            db.Users.Remove(await db.Users.FindAsync(id));
            await db.SaveChangesAsync();
        }

        public async Task<Users> Get(int id)
        {
            return await db.Users.FindAsync(id);
        }

        public IQueryable<Users> GetAll()
        {
            return db.Users;
        }

        public async Task<System.Collections.Generic.List<Users>> GetAllAsync()
        {
            return await GetAll().ToListAsync();
        }

        public void Update(Users entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public async Task UpdateAsync(int id)
        {
            db.Entry(await db.Users.FindAsync(id)).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
