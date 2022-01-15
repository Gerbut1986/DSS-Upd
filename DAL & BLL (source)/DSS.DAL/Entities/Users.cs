namespace DSS.DAL.Entities
{
    public class Users
    {
        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public int Position { get; set; }
        public int Care_Community { get; set; }
        public System.DateTime Date { get; set; }
        public string Week { get; set; }
        public string User_Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string ConfirmedEmail { get; set; }
        public System.DateTime Date_Register { get; set; } = System.DateTime.Now;
        public string Role { get; set; }
        public int Region { get; set; }
    }
}