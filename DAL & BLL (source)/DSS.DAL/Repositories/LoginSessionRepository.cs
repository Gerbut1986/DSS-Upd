namespace DSS.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class LoginSessionRepository : IRepository<LoginSession>
    {
        //we are just declaring db here; read-only format to protect from modifications.
        private readonly MyContext db;

        //This is a constructor written via a lambda expression
        //we are initilizing db here; it establishes a specific connection w/ Ztest interface
        public LoginSessionRepository(MyContext db) => this.db = db;

        //public string Create(LoginSession entity)
        //{
        //    db.LoginSessions.Add(entity);
        //    int res =  db.SaveChanges();
        //    if (res == 0)
        //        return $"{res} record was add..";
        //    else if (res == -1) return "Something went wrong...";
        //    else return $"{res} record was added successfully!"; 
        //}

        //just using Task by itself b/c it stand for void if not void then Task<int> format
        public async Task Create(LoginSession entity)
        {
            db.LoginSessions.Add(entity);
            int res = await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            db.LoginSessions.Remove(await db.LoginSessions.FindAsync(id));
            await db.SaveChangesAsync();
        }

        public async Task<LoginSession> Get(int id)
        {
            return await db.LoginSessions.FindAsync(id);
        }

        public IQueryable<LoginSession> GetAll()
        {
            return db.LoginSessions;
        }

        //just a fake implementation for interface declared so we won't get an error message
        public Task<List<LoginSession>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public void Update(LoginSession entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public async Task UpdateAsync(int id)
        {
            db.Entry(await db.LoginSessions.FindAsync(id)).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
