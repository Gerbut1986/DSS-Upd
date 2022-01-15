namespace DSS.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    public class InspectInfoRepository : IRepository<InspectionInfo>
    {
        readonly MyContext db;

        public InspectInfoRepository(MyContext db) => this.db = db;

        public async Task Create(InspectionInfo entity)
        {
            db.InspectionInfos.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            db.InspectionInfos.Remove(await db.InspectionInfos.FindAsync(id));
            await db.SaveChangesAsync();
        }

        public async Task<InspectionInfo> Get(int id)
        {
            return await db.InspectionInfos.FindAsync(id);
        }

        public IQueryable<InspectionInfo> GetAll()
        {
            return db.InspectionInfos;
        }

        public async Task<System.Collections.Generic.List<InspectionInfo>> GetAllAsync()
        {
            return await GetAll().ToListAsync(); 
        }

        public void Update(InspectionInfo entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public Task UpdateAsync(InspectionInfo entity)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
