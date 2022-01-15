namespace DSS.DAL.Interfaces
{
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// General CRUD operation (generic interface):
    /// </summary>
    /// <typeparam name="IEntity"> Abstraction for each models(entities) </typeparam>
    /// repository is for the methods that will have their implementation later on in the classes
    public interface IRepository<IEntity> where IEntity : class
    {
        //Task return type by itself stand for void return type
        Task Delete(int id);
        Task<IEntity> Get(int id);
        Task Create(IEntity entity);
        void Update(IEntity entity);
        Task UpdateAsync(int id);
        IQueryable<IEntity> GetAll(); 
        Task<System.Collections.Generic.List<IEntity>> GetAllAsync();
    }
}
