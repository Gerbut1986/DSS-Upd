namespace DSS.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Linq;
    using System.Threading.Tasks;

    public class SWordsRepository : IRepository<Search_Word>
    {
        readonly MyContext db;

        public SWordsRepository(MyContext db) => this.db = db;

        public async Task Create(Search_Word entity)
        {
            db.Search_Words.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            db.Search_Words.Remove(await db.Search_Words.FindAsync(id));
            await db.SaveChangesAsync();
        }

        public async Task<Search_Word> Get(int id)
        {
            return await db.Search_Words.FindAsync(id);
        }

        public IQueryable<Search_Word> GetAll()
        {
            return db.Search_Words;
        }

        public Task<System.Collections.Generic.List<Search_Word>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public void Update(Search_Word entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public async Task UpdateAsync(int id)
        {
            db.Entry(await db.Search_Words.FindAsync(id)).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
