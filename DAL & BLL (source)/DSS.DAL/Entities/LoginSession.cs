namespace DSS.DAL.Entities
{
    public class LoginSession
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public System.DateTime DateOfEntry { get; set; }
        public string Location { get; set; }
        public System.Guid SessionId { get; set; }
    }
}
