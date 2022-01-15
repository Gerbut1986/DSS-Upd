namespace DSS.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    public class QualityReviewRepo : IRepository<QualityReview>
    {
        readonly MyContext db;

        public QualityReviewRepo(MyContext db) => this.db = db;

        public async Task Create(QualityReview entity)
        {
            db.QualityReviews.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            db.QualityReviews.Remove(await db.QualityReviews.FindAsync(id));
            await db.SaveChangesAsync();
        }

        public async Task<QualityReview> Get(int id)
        {
            return await db.QualityReviews.FindAsync(id);
        }

        public IQueryable<QualityReview> GetAll()
        {
            return db.QualityReviews;
        }

        public async Task<List<QualityReview>> GetAllAsync()
        {
            return await GetAll().ToListAsync();
        }

        public void Update(QualityReview entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public Task UpdateAsync(QualityReview entity)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
