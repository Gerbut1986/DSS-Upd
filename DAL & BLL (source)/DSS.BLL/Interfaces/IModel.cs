namespace DSS.BLL.Interfaces
{
    //IModel is implemented in every entity so that the implementing classes have proper flexibility
    public interface IModel
    {
        int Id { get; set; }
    }
}
