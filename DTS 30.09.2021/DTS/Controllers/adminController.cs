namespace DTS.Models
{
    using DSS.BLL;
    using System.Net;
    using System.Web;
    using System.Linq;
    using System.Web.Mvc;
    using DTS.Controllers;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity.Owin;

    internal enum ConfirmType
    {
        Allow,
        Decline,
        NoN
    };

    public class adminController : Controller
    {
        static IEnumerable<ApplicationUser> users = default;
        internal DSS.BLL.Services.ServiceDSS dssService = default;
        static ApplicationUser allowUser = default;
        ApplicationDbContext db { get; set; } = default;
        static ConfirmType isConfirmed = ConfirmType.NoN;
        ApplicationUser Admin { get; set; }
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        STREAM createStream = default;
        private int checkAlreadyToken = 0;
        private ApplicationUser CurrentLocalUser { get; set; }

        public adminController()
        {
            AppSettings.ListsInit(out dssService, createStream);
            createStream = new STREAM(dssService);
        }

        public ApplicationUser GetCurrUser() => AccountController.AllUsers.FirstOrDefault(n => n.Email.Equals(User.Identity.Name));

        #region Properties [SignInManager, UserManager]:
        public ApplicationSignInManager SignInManager
        {
            get => _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            private set => _signInManager = value;
        }

        public ApplicationUserManager UserManager
        {
            get => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            private set => _userManager = value;
        }
        #endregion

        [AllowAnonymous]
        public ActionResult admin(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        static bool enterAgain = false;
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> admin(LoginViewModel isAdmin, string returnUrl)
        {
            SignInStatus result = default;
            if (!ModelState.IsValid)
            {
                if (isAdmin.Email == null && isAdmin.Password == null) ViewBag.incorrect = "Fields Login & Password has to be Filled out!";
                else @ViewBag.incorrect = "Some of field is EMPTY!";
                return View(isAdmin);
            }
            else
            {
                ApplicationUser signedUser = UserManager.FindByEmail(isAdmin.Email);
                if (signedUser != null)
                    if (signedUser.Role == Role.Admin.ToString())
                        result = await SignInManager.PasswordSignInAsync(signedUser.UserName, isAdmin.Password, isAdmin.RememberMe, shouldLockout: false);
                    else result = SignInStatus.Failure;
                switch (result)
                {
                    case SignInStatus.Success:
                        return RedirectToAction("../admin/_3be122bf_a593_4a98_8154_fff1f605738f");
                    case SignInStatus.LockedOut:
                        return View("Lockout");
                    case SignInStatus.RequiresVerification:
                        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = isAdmin.RememberMe });
                    case SignInStatus.Failure:
                    default:
                        ViewBag.incorrect = "Invalid login attempt.This user does NOT exsist!";
                        ModelState.AddModelError("", "Invalid login attempt.");
                        isAdmin.Password = "Incorrect password...";
                        return View(isAdmin);
                }
            }
            #region Old solution:
            //bool exist = false;
            //if (ModelState.IsValid)
            //{
            //    foreach (var curr in db.AspNetUsers.ToList())
            //    {
            //        if (curr.Email.Equals(isAdmin.Email) && curr.PasswordHash.Equals(isAdmin.PasswordHash) && curr.Role.Equals("Admin"))
            //        {
            //            exist = true;
            //            HomeController.role = Role.Admin;
            //            HomeController.isAdmin = true;
            //            return RedirectToAction("../admin/GetAllUsers/");
            //        }
            //        else exist = false;
            //    }
            //    if (!exist)
            //    {
            //        ViewBag.DoesnExist = "Login or Password does not exist... Try again!";
            //    }
            //}
            #endregion
        }

        public ActionResult AllowDecline(string id, string[] btn)
        {
            if (!id.Equals(string.Empty))
            {
                using (db = new ApplicationDbContext())
                {
                    allowUser = db.Users.FirstOrDefault(u => u.Id == id);
                    switch (btn[0])
                    {
                        case "btn-allow":
                            isConfirmed = ConfirmType.Allow;
                            allowUser.EmailConfirmed = true;
                            db.Entry(allowUser).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("../admin/_3be122bf_a593_4a98_8154_fff1f605738f/");
                        case "btn-decline":
                            isConfirmed = ConfirmType.Decline;
                            allowUser.EmailConfirmed = false;
                            db.Entry(allowUser).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("../admin/_3be122bf_a593_4a98_8154_fff1f605738f/");
                    }
                }
            }
            return View();
        }

        #region User's C.R.U.D:

        #region Select All Users:
        /// <summary>
        /// Select All Users
        /// </summary>
        /// <returns>View with it</returns>
        public ActionResult _3be122bf_a593_4a98_8154_fff1f605738f()
        {
            try
            {
                CurrentLocalUser = GetCurrUser();
                checkAlreadyToken = CurrentLocalUser.Care_Community;
            }
            catch { return RedirectToAction($"../Account/Login"); }

            { ViewBag.Location = STREAM.GetLocNames().ToArray(); }
            { ViewBag.Position = STREAM.GetPosNames().ToArray(); }
            var strHostName = Dns.GetHostName();
            // Then using host name, get the IP address list..
            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
            IPAddress[] addr = ipEntry.AddressList;
            { @ViewBag.CurrIP1 = addr[0]; }
            { @ViewBag.CurrIP2 = addr[1]; }

            ViewBag.AlloState = isConfirmed.ToString();

            if (isConfirmed == ConfirmType.Allow)
                ViewBag.SuccessEmailConfirm = $"User {allowUser.FirstName} was Confirmed Successfuly!";
            else if (isConfirmed == ConfirmType.Decline)
                ViewBag.SuccessEmailConfirm = $"User {allowUser.FirstName} was Declined!";

            using (db = new ApplicationDbContext())
            {
                users = db.Users.OrderByDescending(x => x.DateRegister).ToList();
            }

            return View(users);
        }

        [HttpPost]
        public ActionResult _3be122bf_a593_4a98_8154_fff1f605738f(ApplicationUser obj)
        {
            { ViewBag.Location = STREAM.GetLocNames().ToArray(); }
            { ViewBag.Position = STREAM.GetPosNames().ToArray(); }
            var strHostName = Dns.GetHostName();
            // Then using host name, get the IP address list..
            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
            IPAddress[] addr = ipEntry.AddressList;
            { @ViewBag.CurrIP1 = addr[0]; }
            { @ViewBag.CurrIP2 = addr[1]; }

            ViewBag.AlloState = isConfirmed.ToString();

            if (isConfirmed == ConfirmType.Allow)
                ViewBag.SuccessEmailConfirm = $"User {allowUser.FirstName} was Confirmed Successfuly!";
            else if (isConfirmed == ConfirmType.Decline)
                ViewBag.SuccessEmailConfirm = $"User {allowUser.FirstName} was Declined!";
            string btnName =
         Request.Params.Cast<string>().Where(p => p.StartsWith("btn")).Select(p => p.Substring("btn".Length)).First();
            using (db = new ApplicationDbContext())
            {
                if (btnName.Equals("-upSort"))
                    users = db.Users.OrderBy(x => x.Email).ToList();
                else if (btnName.Equals("-downSort"))
                    users = db.Users.OrderByDescending(x => x.Email).ToList();
            }
            return View(users);
        }

        [HttpPost]
        public ActionResult GetAllUsers(string id)
        {
            { ViewBag.Location = STREAM.GetLocNames().ToArray(); }
            { ViewBag.Position = STREAM.GetPosNames().ToArray(); }
            return View();
        }
        #endregion

        #region Edit User:
        static ApplicationUser saveHash;
        [HttpGet]
        public ActionResult Edit_User(string id)
        {
            try
            {
                CurrentLocalUser = GetCurrUser();
                checkAlreadyToken = CurrentLocalUser.Care_Community;
            }
            catch { return RedirectToAction($"../Account/Login"); }

            { ViewBag.locations = HomeController.list; }
            { ViewBag.positions = HomeController.list2; }
            { ViewBag.TypeRole = new SelectList(new string[] { "Admin", "SuperUser", "RegionalUser", "User", "Unknown" }); }
            if (id.Equals(null))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var found = saveHash = UserManager.FindById(id);
            if (found != null)
                return View(found);
            return View(new ApplicationUser());
        }

        [HttpPost]
        public async Task<ActionResult> Edit_User(ApplicationUser user)
        {
            switch (user.Role)
            {
                case "RegionalUser":
                    user.Care_Community = 0;
                    break;
                case "User":
                    user.Region = 0;
                    break;
            }
            { ViewBag.locations = HomeController.list; }
            { ViewBag.positions = HomeController.list2; }
            { ViewBag.TypeRole = new SelectList(new string[] { "Admin", "SuperUser", "RegionalUser", "User", "Unknown" }); }
            using (db = new ApplicationDbContext())
            {
                user.PasswordHash = saveHash.PasswordHash;
                user.SecurityStamp = saveHash.SecurityStamp;
                user.DateRegister = saveHash.DateRegister;
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                int res = await db.SaveChangesAsync();
                return RedirectToAction("../admin/_3be122bf_a593_4a98_8154_fff1f605738f");
            }
        }
        #endregion

        #region Delete User:
        public ActionResult DeleteUser(string id)
        {
            try
            {
                CurrentLocalUser = GetCurrUser();
                checkAlreadyToken = CurrentLocalUser.Care_Community;
            }
            catch { return RedirectToAction($"../Account/Login"); }

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var found = UserManager.FindById(id);
            if (found == null) return View();
            ViewBag.locations = STREAM.GetLocNames().ToArray();
            ViewBag.positions = STREAM.GetPosNames().ToArray();
            return View(found);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteUser(string id, bool b = false)
        {
            if (ModelState.IsValid)
            {
                using (db = new ApplicationDbContext())
                {
                    var found = db.Users.FirstOrDefault(u => u.Id == id);
                    db.Users.Remove(found ?? throw new System.Exception("Something went wrong..."));
                    int res = await db.SaveChangesAsync();
                    return RedirectToAction("../admin/_3be122bf_a593_4a98_8154_fff1f605738f");
                }
            }
            return View(id);
        }
        #endregion

        #region Details User:
        public ActionResult DetailsUser(string id)
        {
            try
            {
                CurrentLocalUser = GetCurrUser();
                checkAlreadyToken = CurrentLocalUser.Care_Community;
            }
            catch { return RedirectToAction($"../Account/Login"); }

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var found = UserManager.FindById(id);
            ViewBag.locations = STREAM.GetLocNames().ToArray();
            ViewBag.positions = STREAM.GetPosNames().ToArray();
            if (found == null)
                return HttpNotFound();
            return View(found);
        }
        #endregion

        #endregion
    }
}