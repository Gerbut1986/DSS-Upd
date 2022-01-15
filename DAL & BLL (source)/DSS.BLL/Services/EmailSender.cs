namespace DSS.BLL.Services
{
    using System.Net.Mail;
    using DSS.BLL.Interfaces;
    using DSS.BLL.PartialModels.Email;

    public class EmailSender : IEmailSender
    {
        public string SendMessage(string email, string fname, string lname, string attachment = null)
        {
            try
            {
                MailAddress from = new MailAddress(email, "Request for Passwords");
                MailAddress to = new MailAddress("vdinovich@hotmail.com"); // email to admin
                MailMessage m = new MailMessage(from, to);
                m.Subject = $"I Forgot My Email From '{email}'";
                m.Body = $"Hi Administrator! I'm '{fname} {lname}' and forgot my password." +
                "\nPlease provide me with my password.\n\nRegards.";
                //if (attachment != null) m. = attachment;
                m.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new System.Net.NetworkCredential(Credential.Mail, Credential.Pass);
                smtp.EnableSsl = true;
                smtp.Send(m);
                return "Your e-mail was sent successfully to Admin.";
            }
            catch(System.Exception ex)
            {
                return ex.Message;// + " | " + ex.StackTrace;
            }
        }
    }
}
