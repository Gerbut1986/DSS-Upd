namespace DSS.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Linq;
    using System.Threading.Tasks;

    public class GoodNewsRepository : IRepository<Good_News>
    {
        readonly MyContext db;

        public GoodNewsRepository(MyContext db) => this.db = db;

        public async Task Create(Good_News entity)
        {
            db.Good_News.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            db.Good_News.Remove(await db.Good_News.FindAsync(id));
            await db.SaveChangesAsync();
        }

        public async Task<Good_News> Get(int id)
        {
            return await db.Good_News.FindAsync(id);
        }

        public IQueryable<Good_News> GetAll()
        {
            return db.Good_News;
        }

        public Task<System.Collections.Generic.List<Good_News>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public void Update(Good_News entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public async Task UpdateAsync(int id)
        {
            db.Entry(await db.Good_News.FindAsync(id)).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
