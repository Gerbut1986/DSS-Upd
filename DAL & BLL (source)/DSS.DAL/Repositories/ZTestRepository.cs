namespace DSS.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Linq;
    using System.Threading.Tasks;

    public class ZTestRepository : IRepository<ZTest>
    {
        //we are just declaring db here; read-only format to protect from modifications.
        private readonly MyContext db;

        //This is a constructor written via a lambda expression
        //we are initilizing db here; it establishes a specific connection w/ Ztest interface
        public ZTestRepository(MyContext db) => this.db = db; 

        //just using Task by itself b/c it stand for void if not void then Task<int> format
        public async Task Create(ZTest entity)
        {
            db.ZTest.Add(entity);
            await db.SaveChangesAsync();
            
        }

        public async Task Delete(int id)
        {
            db.ZTest.Remove(await db.ZTest.FindAsync(id));
            await db.SaveChangesAsync();
        }

        public async Task<ZTest> Get(int id)
        {
            return await db.ZTest.FindAsync(id);
        }

        public IQueryable<ZTest> GetAll()
        {
            return db.ZTest;
        }

        //just a fake implementation for interface declared so we won't get an error message
        public Task<System.Collections.Generic.List<ZTest>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public void Update (ZTest entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public async Task UpdateAsync(int id)
        {
            db.Entry(await db.ZTest.FindAsync(id)).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}

