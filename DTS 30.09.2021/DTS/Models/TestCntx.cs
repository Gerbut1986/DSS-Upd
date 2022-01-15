namespace DTS.Models
{
    using System.Data.Entity;

    public class TestCntx : DbContext  // Method for the testing of insert functionality
    {
        public TestCntx(string conn) : base(conn) { }
        public DbSet<DSS.BLL.DTO.LoginSession_DTO> LoginSessions { get; set; }
    }
}