namespace DTS.Models
{
    using DTS.Controllers;

    public class RoleCheck
    {
        public static string WhoIs(Role role)
        {
            if (role == Role.SuperUser)
                return Role.SuperUser.ToString();
            else if (role == Role.User) return Role.User.ToString();
            else if (role == Role.RegionalUser) return Role.RegionalUser.ToString();
            else if (role == Role.Admin) return Role.Admin.ToString();
            else return Role.Unknown.ToString();
        }
    }
}