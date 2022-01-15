namespace DSS.BLL.Interfaces
{
    public interface IEmailSender
    {
        string SendMessage(string email, string fname, string lname, string attachment);
    }
}
