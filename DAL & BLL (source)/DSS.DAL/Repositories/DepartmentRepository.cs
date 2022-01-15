namespace DSS.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Linq;
    using System.Threading.Tasks;

    public class DepartmentRepository : IRepository<Department>
    {
        readonly MyContext db;

        public DepartmentRepository(MyContext db) => this.db = db;

        public async Task Create(Department entity)
        {
            db.Departments.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            db.Departments.Remove(await db.Departments.FindAsync(id));
            await db.SaveChangesAsync();
        }

        public async Task<Department> Get(int id)
        {
            return await db.Departments.FindAsync(id);
        }

        public IQueryable<Department> GetAll()
        {
            return db.Departments;
        }

        public Task<System.Collections.Generic.List<Department>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public void Update(Department entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public async Task UpdateAsync(int id)
        {
            db.Entry(await db.Departments.FindAsync(id)).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
