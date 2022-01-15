namespace DSS.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Linq;
    using System.Threading.Tasks;

    public class SignInMainRepository : IRepository<Sign_in_Main>
    {
        readonly MyContext db;

        public SignInMainRepository(MyContext db) => this.db = db;

        public async Task Create(Sign_in_Main entity)
        {
            db.Sign_in_Mains.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            db.Sign_in_Mains.Remove(await db.Sign_in_Mains.FindAsync(id));
            await db.SaveChangesAsync();
        }

        public async Task<Sign_in_Main> Get(int id)
        {
            return await db.Sign_in_Mains.FindAsync(id);
        }

        public IQueryable<Sign_in_Main> GetAll()
        {
            return db.Sign_in_Mains;
        }

        public Task<System.Collections.Generic.List<Sign_in_Main>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public void Update(Sign_in_Main entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public async Task UpdateAsync(int id)
        {
            db.Entry(await db.Sign_in_Mains.FindAsync(id)).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
