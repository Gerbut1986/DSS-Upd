namespace DSS.DAL.Interfaces
{
    using DSS.DAL.Entities;
    using System.Threading.Tasks;

    //to get all locations (generic of a Home type is used) in an async manner
    // only reference types (b/c class is used as a limit) can implement this interface
    public interface IAsync<IEntity> where IEntity : class
    {
        Task<System.Collections.Generic.List<Home>> GetAllAsync();
    }
}
