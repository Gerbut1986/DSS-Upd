namespace DTS.Controllers
{
    using System;
    using System.IO;
    using DTS.Models;
    using System.Web;
    using System.Linq;
    using System.Web.Mvc;
    using DSS.BLL.Services;
    using System.Threading.Tasks;
    using Microsoft.Owin.Security;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using System.Collections.Generic;

    //public delegate ApplicationUser GetCurrentUser();

    [Authorize]
    public class AccountController : Controller
    {
        #region Fields:
        public static List<ApplicationUser> AllUsers { get; set; }
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly ServiceDSS db;
        //public GetCurrentUser UserDeleg;
        public static int CurrentUser { get; set; }
        #endregion

        #region Constructors:
        public AccountController() => db = new ServiceDSS(Init.ConnectionStrAdm);

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        #endregion

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

        #region VerifyCode:
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }
        #endregion

        #region Login:
        //
        // GET: /Account/Login
        static bool enterAgain;
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
           // if (!enterAgain)
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            var procc = new DSS.BLL.STREAM(db);
            if (!ModelState.IsValid)
            {
                if (model.Email == null && model.Password == null) ViewBag.incorrect = "Login and Password Fields have to be Filled out!";
                else @ViewBag.incorrect = "Some of the fields are EMPTY!";
                return View(model);
            }
            else
            {
                AllUsers = UserManager.Users.ToList(); 
                ApplicationUser signedUser = UserManager.FindByEmail(model.Email);

                if(signedUser == null) // if login and password  doesn't exists
                {
                    ViewBag.incorrect = "Your Login and/or Password is Incorrect!";
                    return View(model);
                }

                SignInStatus result = await SignInManager.PasswordSignInAsync(signedUser.UserName, model.Password, model.RememberMe, shouldLockout: false);


                switch (result)
                {
                    case SignInStatus.Success:                
                        new AppSettings().ClearAllSummary(true);
                        HomeController.checkView = "none";
                        switch (signedUser.Role)
                        {
                            case "Admin":
                                break;
                            case "User":
                                await AddSession(new DSS.BLL.DTO.LoginSession_DTO
                                {
                                    UserName = signedUser.FirstName + " | " + signedUser.LastName,
                                    Email = signedUser.Email,
                                    SessionId = Guid.NewGuid(),
                                    DateOfEntry = DateTime.Now
                                });
                                //db.Insert(entity);
                                break;
                            case "SuperUser":
                                await AddSession(new DSS.BLL.DTO.LoginSession_DTO
                                {
                                    UserName = signedUser.FirstName + " | " + signedUser.LastName,
                                    Email = signedUser.Email,
                                    SessionId = Guid.NewGuid(),
                                    DateOfEntry = DateTime.Now
                                });
                                break;
                            case "RegionalUser":
                                //UserDeleg = delegate { return AllUsers.FirstOrDefault(n => n.Email.Equals(User.Identity.Name)); };
                               //var current = AccountController.AllUsers.FirstOrDefault(n => n.Email.Equals(User.Identity.Name));
                               int region = Models.RegionLogic.RegionService.GetRegion(signedUser);
                                HomeController.SelectRegions = 
                                    Models.RegionLogic.RegionFolder.Extract
                                    (AppDomain.CurrentDomain.BaseDirectory + "Regions" + 
                                    $"\\Region{region}.txt");
                                await AddSession(new DSS.BLL.DTO.LoginSession_DTO
                                {
                                    UserName = signedUser.FirstName + " | " + signedUser.LastName,
                                    Email = signedUser.Email,
                                    SessionId = Guid.NewGuid(),
                                    DateOfEntry = DateTime.Now
                                });
                                break;
                            default: break;
                        }
                        //HomeController.isSignIn = true;
                        return RedirectToLocal(returnUrl);
                    case SignInStatus.LockedOut:
                        return View("Lockout");
                    case SignInStatus.RequiresVerification:
                        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                    case SignInStatus.Failure:
                    default:
                        ViewBag.incorrect = "This user or password does not exist or the user is not confirmed!";
                        // ModelState.AddModelError("", "Invalid login attempt.");
                        return View(model);
                }
            }
        }

        private async Task AddSession(DSS.BLL.DTO.LoginSession_DTO model)
        {
            await Task.Run(() =>
            {
                using (var cntx = new TestCntx(Init.ConnectionStrAdm))
                {
                    cntx.LoginSessions.Add(model);
                    int res = cntx.SaveChanges();
                }
            });
        }
        #endregion

        #region Register:
        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register() => View();

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    LastName = model.LastName,
                    FirstName = model.FirstName,
                    Region = 0,
                    Role = Role.Unknown.ToString(),
                    DateRegister = DateTime.Now
                };
                using (var sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "StoragePass.txt", true))
                    sw.WriteLine(DateTime.Now + " => " + model.Email + " | " + model.Password);
                user.Email = model.Email;
                user.ConfirmedEmail = false;
                user.EmailConfirmed = false; // Allows user register.
                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                { 
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    AddErrors(result);
                    ViewBag.incorrect = result.Errors.ToArray()[0];
                    return View();
                }
            }
            return View(model);
        }
        #endregion

        #region Confirm Email:
        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public string ConfirmEmail(string email) => "To the postal address " + email + " further instructions for completing registration";
        #endregion

        #region Forgot / Confirm / Reset / ResetConfirmation - Password:
        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    ViewBag.IncorrectEmail = "Incorrect Email address or this user doesn't exist"; 
                    return View(model);
                }
                else
                {
                    ViewBag.ResultMsg =
                        new EmailSender().SendMessage(model.Email, user.FirstName, user.LastName)+"\n"+" Please wait.";
                }

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }
        #endregion

        #region External Login / External Login Callback / External Login Confirmation / External Login Failure:
        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }
        #endregion

        #region LogOFF:
        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account");
        }
        #endregion

        #region Dispose:
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }
        #endregion

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Wor_Tabs", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}