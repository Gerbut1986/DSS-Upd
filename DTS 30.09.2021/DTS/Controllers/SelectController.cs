namespace DTS.Controllers
{
    using DSS.BLL;
    using DTS.Models;
    using DSS.BLL.DTO;
    using System.Linq;
    using System.Web.Mvc;
    using DSS.BLL.Services;
    using System.Collections.Generic;

    public class SelectController : Controller
    {
        int localLocation;
        internal STREAM str;
        readonly ServiceDSS db;
        public static bool flag;
        private ApplicationUser CurrentLocalUser { get; set; }

        public SelectController()
        {
            AppSettings.ListsInit(out db, str);
            var TotalMemoryHeap = System.GC.GetTotalMemory(true);
        }

        public ApplicationUser GetCurrUser() => AccountController.AllUsers.FirstOrDefault(n => n.Email.Equals(User.Identity.Name));

        public ActionResult Select_Incidents()
        {
            try
            {
                CurrentLocalUser = GetCurrUser();
                localLocation = CurrentLocalUser.Care_Community;
            }
            catch
            {
                string unAuthMsg = "First you should to SignIn or go to Register Form!"; // this is un use text 
                return RedirectToAction($"../Account/Login/{unAuthMsg}");
            }

            int id_loc = 0;
            IEnumerable<CI_Category_Type_DTO> ll = AppSettings.ci_types;
            var isadmin = HomeController.isAdmin;
            if (CurrentLocalUser.Role == Role.User.ToString()) // if user role
            {
                id_loc = AccountController.AllUsers.FirstOrDefault(n => n.Email.Equals(User.Identity.Name)).Care_Community;  /// Get id location from Sign_In_Main page HomeControllers'
                TablesContainer.list1 = db.ReadIncidents().Where(l => l.Location == id_loc).ToList(); // get list of records from Critical_Incidents(Location) 
            }
            else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                AppSettings.IsRegionalUser(new Critical_Incidents_DTO(), CurrentLocalUser.Region, Range.NoN, System.DateTime.MinValue, System.DateTime.MinValue);
            else
            {
                TablesContainer.list1 = db.ReadIncidents().ToList();
            }
            if (TablesContainer.list1.Count == 0)
            {
                ViewBag.err = flag = false;
                ViewBag.emptyMsg = "The form - Critical Incidents has no records.";
                return View();
            }
            else
            {
                if (CurrentLocalUser.Role == Role.User.ToString()) // if user role
                {
                    ViewBag.err = flag = true;
                    var cc = db.ReadHomes().Where(i => i.Id == id_loc).SingleOrDefault();
                    { ViewBag.List1 = STREAM.GetCINames().ToArray(); }
                    { ViewBag.list = STREAM.GetLocNames().ToArray(); }
                    return View(TablesContainer.list1);
                }
                else // admin or superus
                {
                    ViewBag.err = flag = true;
                    List<Critical_Incidents_DTO> kk = TablesContainer.list1.ToList();
                    for (int i = 0; i < kk.Count; i++)
                    {
                        int id = kk[i].CI_Category_Type;
                        var f = db.ReadCICategory().Where(ii => ii.Id == id).FirstOrDefault();
                        string name = f.Name;
                    }
                    { ViewBag.IsAdmin = HomeController.isAdmin; }
                    { ViewBag.List1 = STREAM.GetCINames().ToArray(); }
                    { ViewBag.list = STREAM.GetLocNames().ToArray(); }
                    return View(TablesContainer.list1);
                }
            }
        }

        [HttpPost]
        public ActionResult Select_Incidents(object o)
        {
            string btnName =
                Request.Params.Cast<string>().Where(p => p.StartsWith("btn")).Select(p => p.Substring("btn".Length)).First();
            TablesContainer.list1 = TablesContainer.list1.OrderBy(x => x.Date).ToList();
            int id_loc = 0;
            IEnumerable<CI_Category_Type_DTO> ll = AppSettings.ci_types;
            var isadmin = HomeController.isAdmin;
            if (btnName.Equals("-upSort"))
                TablesContainer.list1 = TablesContainer.list1.OrderBy(x => x.Date).ToList();
            else if (btnName.Equals("-downSort"))
                TablesContainer.list1 = TablesContainer.list1.OrderByDescending(x => x.Date).ToList();
            if (TablesContainer.list1.Count == 0)
            {
                ViewBag.err = flag = false;
                ViewBag.emptyMsg = "The form - Critical Incidents has no records.";
                return View();
            }
            else
            {
                ViewBag.err = flag = true;
                List<Critical_Incidents_DTO> kk = TablesContainer.list1.ToList();
                for (int i = 0; i < kk.Count; i++)
                {
                    int id = kk[i].CI_Category_Type;
                    var f = db.ReadCICategory().Where(ii => ii.Id == id).FirstOrDefault();
                    string name = f.Name;
                }
                { ViewBag.IsAdmin = HomeController.isAdmin; }
                { ViewBag.List1 = STREAM.GetCINames().ToArray(); }
                { ViewBag.list = STREAM.GetLocNames().ToArray(); }
            }
            return View(TablesContainer.list1);
        }

        public ActionResult Select_Complaints()
        {
            try
            {
                CurrentLocalUser = GetCurrUser();
                localLocation = CurrentLocalUser.Care_Community;
            }
            catch
            {
                string unAuthMsg = "First you should to SignIn or go to Register Form!";
                return RedirectToAction($"../Account/Login/{unAuthMsg}");
            }
            CurrentLocalUser = GetCurrUser();
            int id_loc = 0;
            if (CurrentLocalUser.Role == Role.User.ToString()) // if user role
            {
                id_loc = AccountController.AllUsers.FirstOrDefault(n => n.Email.Equals(User.Identity.Name)).Care_Community;  /// Get id location from Sign_In_Main page HomeControllers'
                TablesContainer.list2 = db.ReadComplaints().Where(l => l.Location == id_loc).ToList(); // get list of records from Critical_Incidents(Location) 
            }
            else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                AppSettings.IsRegionalUser(new Complaint_DTO(), CurrentLocalUser.Region, Range.NoN, System.DateTime.MinValue, System.DateTime.MinValue);
            else
            {
                TablesContainer.list2 = db.ReadComplaints().ToList();
            }
            if (TablesContainer.list2.Count() == 0)
            {
                ViewBag.err = flag = false;
                ViewBag.emptyMsg = "The form - Complaints has no records.";
                return View();
            }
            else
            {
                if (CurrentLocalUser.Role == Role.User.ToString()) // if user role
                {
                    ViewBag.err = flag = true;
                    var cc = db.ReadHomes().Where(i => i.Id == id_loc).SingleOrDefault();
                    { ViewBag.list = STREAM.GetLocNames().ToArray(); }
                    return View(TablesContainer.list2);
                }
                else // admin
                {
                    ViewBag.err = flag = true;
                    List<Complaint_DTO> kk = TablesContainer.list2.ToList();
                    //{ ViewBag.IsAdmin = HomeController.isAdmin; }
                    { ViewBag.list = STREAM.GetLocNames().ToArray(); }
                    return View(TablesContainer.list2);
                }
            }
        }

        [HttpPost]
        public ActionResult Select_Complaints(object o)
        {
            string btnName =
                  Request.Params.Cast<string>().Where(p => p.StartsWith("btn")).Select(p => p.Substring("btn".Length)).First();
            TablesContainer.list2 = TablesContainer.list2.OrderBy(x => x.DateReceived).ToList();
            var isadmin = HomeController.isAdmin;
            if (btnName.Equals("-upSort"))
                TablesContainer.list2 = TablesContainer.list2.OrderBy(x => x.DateReceived).ToList();
            else if (btnName.Equals("-downSort"))
                TablesContainer.list2 = TablesContainer.list2.OrderByDescending(x => x.DateReceived).ToList();
            if (TablesContainer.list2.Count() == 0)
            {
                ViewBag.err = flag = false;
                ViewBag.emptyMsg = "The form - Complaints has no records.";
                return View();
            }
            else
            {
                ViewBag.err = flag = true;
                List<Complaint_DTO> kk = TablesContainer.list2.ToList();
                { ViewBag.list = STREAM.GetLocNames().ToArray(); }
            }
            return View(TablesContainer.list2);
        }

        public ActionResult Select_Community()
        {
            try
            {
                CurrentLocalUser = GetCurrUser();
                localLocation = CurrentLocalUser.Care_Community;
            }
            catch
            {
                string unAuthMsg = "First you should to SignIn or go to Register Form!";
                return RedirectToAction($"../Account/Login/{unAuthMsg}");
            }
            CurrentLocalUser = GetCurrUser();
            int id_loc = 0;
            if (CurrentLocalUser.Role == Role.User.ToString()) // if user role
            {
                id_loc = AccountController.AllUsers.FirstOrDefault(n => n.Email.Equals(User.Identity.Name)).Care_Community;  /// Get id location from Sign_In_Main page HomeControllers'
                TablesContainer.list5 = db.ReadRisks().Where(l => l.Location == id_loc).ToList(); // get list of records from Critical_Incidents(Location) 
            }
            else if (CurrentLocalUser.Role == Role.RegionalUser.ToString()) // if Regionaluser role
                AppSettings.IsRegionalUser(new Community_Risks_DTO(), CurrentLocalUser.Region, Range.NoN, System.DateTime.MinValue, System.DateTime.MinValue);
            else // admin
                TablesContainer.list5 = db.ReadRisks().ToList();

            if (TablesContainer.list5.Count() == 0)
            {
                ViewBag.err = flag = false;
                ViewBag.emptyMsg = "The form - Community Risks has no records.";
                return View();
            }
            else
            {
                if (CurrentLocalUser.Role == Role.User.ToString()) // if user role
                {
                    ViewBag.err = flag = true;
                    var cc = db.ReadHomes().Where(i => i.Id == id_loc).SingleOrDefault();
                    { ViewBag.list = STREAM.GetLocNames().ToArray(); }
                    return View(TablesContainer.list5);
                }
                else if (CurrentLocalUser.Role == Role.RegionalUser.ToString()) // if user role
                {
                    if (TablesContainer.list5 != null)
                        TablesContainer.list5 = TablesContainer.list5;
                    ViewBag.err = flag = true;
                    var kk = TablesContainer.list5.ToList();
                    { ViewBag.IsAdmin = HomeController.isAdmin; }
                    { ViewBag.list = STREAM.GetLocNames().ToArray(); }
                    return View(TablesContainer.list5);
                }
                else // admin
                {
                    ViewBag.err = flag = true;
                    var kk = TablesContainer.list5.ToList();
                    { ViewBag.IsAdmin = HomeController.isAdmin; }
                    { ViewBag.list = STREAM.GetLocNames().ToArray(); }
                    return View(TablesContainer.list5);
                }
            }
        }

        [HttpPost]
        public ActionResult Select_Community(object o)
        {
            string btnName =
                  Request.Params.Cast<string>().Where(p => p.StartsWith("btn")).Select(p => p.Substring("btn".Length)).First();
            TablesContainer.list5 = TablesContainer.list5.OrderBy(x => x.Date).ToList();
            var isadmin = HomeController.isAdmin;
            if (btnName.Equals("-upSort"))
                TablesContainer.list5 = TablesContainer.list5.OrderBy(x => x.Date).ToList();
            else if (btnName.Equals("-downSort"))
                TablesContainer.list5 = TablesContainer.list5.OrderByDescending(x => x.Date).ToList();
            if (TablesContainer.list5.Count() == 0)
            {
                ViewBag.err = flag = false;
                ViewBag.emptyMsg = "The form - Community Risks has no records.";
                return View();
            }
            else
            {
                ViewBag.err = flag = true;
                var kk = TablesContainer.list5.ToList();
                { ViewBag.IsAdmin = HomeController.isAdmin; }
                { ViewBag.list = STREAM.GetLocNames().ToArray(); }
            }
            return View(TablesContainer.list5);
        }

        public ActionResult Select_GoodNews()
        {
            try
            {
                CurrentLocalUser = GetCurrUser();
                localLocation = CurrentLocalUser.Care_Community;
            }
            catch
            {
                string unAuthMsg = "First you should to SignIn or go to Register Form!";
                return RedirectToAction($"../Account/Login/{unAuthMsg}");
            }
            int id_loc = 0;
            if (CurrentLocalUser.Role == Role.User.ToString()) // if user role
            {
                id_loc = AccountController.AllUsers.FirstOrDefault(n => n.Email.Equals(User.Identity.Name)).Care_Community;  /// Get id location from Sign_In_Main page HomeControllers'
                TablesContainer.list3 = db.ReadNews().Where(l => l.Location == id_loc).ToList(); // get list of records from Critical_Incidents(Location) 
            }
            else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                AppSettings.IsRegionalUser(new Good_News_DTO(), CurrentLocalUser.Region, Range.NoN, System.DateTime.MinValue, System.DateTime.MinValue);
            else
                TablesContainer.list3 = db.ReadNews().ToList();
            if (TablesContainer.list3.Count() == 0)
            {
                ViewBag.err = flag = false;
                ViewBag.emptyMsg = "The form - Good News has no records.";
                return View();
            }
            else
            {
                if (CurrentLocalUser.Role == Role.User.ToString()) // if user role
                {
                    ViewBag.err = flag = true;
                    var cc = db.ReadHomes().Where(i => i.Id == id_loc).SingleOrDefault();
                    { ViewBag.list = STREAM.GetLocNames().ToArray(); }
                    return View(TablesContainer.list3);
                }
                else // admin
                {
                    ViewBag.err = flag = true;
                    var kk = TablesContainer.list3.ToList();
                    { ViewBag.IsAdmin = HomeController.isAdmin; }
                    { ViewBag.list = STREAM.GetLocNames().ToArray(); }
                    return View(TablesContainer.list3);
                }
            }
        }

        [HttpPost]
        public ActionResult Select_GoodNews(object o)
        {
            string btnName =
                  Request.Params.Cast<string>().Where(p => p.StartsWith("btn")).Select(p => p.Substring("btn".Length)).First();
            TablesContainer.list3 = TablesContainer.list3.OrderBy(x => x.DateNews).ToList();
            var isadmin = HomeController.isAdmin;
            if (btnName.Equals("-upSort"))
                TablesContainer.list3 = TablesContainer.list3.OrderBy(x => x.DateNews).ToList();
            else if (btnName.Equals("-downSort"))
                TablesContainer.list3 = TablesContainer.list3.OrderByDescending(x => x.DateNews).ToList();
            if (TablesContainer.list3.Count() == 0)
            {
                ViewBag.err = flag = false;
                ViewBag.emptyMsg = "The form - Good News has no records.";
                return View();
            }
            else
            {
                ViewBag.err = flag = true;
                var kk = TablesContainer.list3.ToList();
                { ViewBag.IsAdmin = HomeController.isAdmin; }
                { ViewBag.list = STREAM.GetLocNames().ToArray(); }
            }
            return View(TablesContainer.list3);
        }

        public ActionResult Select_WSIB()
        {
            try
            {
                CurrentLocalUser = GetCurrUser();
                localLocation = CurrentLocalUser.Care_Community;
            }
            catch
            {
                string unAuthMsg = "First you should to SignIn or go to Register Form!";
                return RedirectToAction($"../Account/Login/{unAuthMsg}");
            }
            int id_loc = 0;
            if (CurrentLocalUser.Role == Role.User.ToString()) // if user role
            {
                id_loc = AccountController.AllUsers.FirstOrDefault(n => n.Email.Equals(User.Identity.Name)).Care_Community;  /// Get id location from Sign_In_Main page HomeControllers'
                TablesContainer.list13 = db.ReadWSiBs().Where(l => l.Location == id_loc).ToList(); // get list of records from Critical_Incidents(Location) 
            }
            else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                AppSettings.IsRegionalUser(new WSIB_DTO(), CurrentLocalUser.Region, Range.NoN, System.DateTime.MinValue, System.DateTime.MinValue);
            else
                TablesContainer.list13 = db.ReadWSiBs().ToList();
            if (TablesContainer.list13.Count() == 0)
            {
                ViewBag.err = flag = false;
                ViewBag.emptyMsg = "The form - WSIB has no records.";
                return View();
            }
            else
            {
                if (CurrentLocalUser.Role == Role.User.ToString()) // if user role
                {
                    ViewBag.err = flag = true;
                    var cc = db.ReadHomes().Where(i => i.Id == id_loc).SingleOrDefault();
                    { ViewBag.list = STREAM.GetLocNames().ToArray(); }
                    return View(TablesContainer.list13);
                }
                else // admin
                {
                    ViewBag.err = flag = true;
                    var kk = TablesContainer.list13.ToList();
                    { ViewBag.IsAdmin = HomeController.isAdmin; }
                    { ViewBag.list = STREAM.GetLocNames().ToArray(); }
                    return View(TablesContainer.list13);
                }
            }
        }

        [HttpPost]
        public ActionResult Select_WSIB(object o)
        {
            string btnName =
                  Request.Params.Cast<string>().Where(p => p.StartsWith("btn")).Select(p => p.Substring("btn".Length)).First();
            TablesContainer.list13 = TablesContainer.list13.OrderBy(x => x.Date_Accident).ToList();
            var isadmin = HomeController.isAdmin;
            if (btnName.Equals("-upSort"))
                TablesContainer.list13 = TablesContainer.list13.OrderBy(x => x.Date_Accident).ToList();
            else if (btnName.Equals("-downSort"))
                TablesContainer.list13 = TablesContainer.list13.OrderByDescending(x => x.Date_Accident).ToList();
            if (TablesContainer.list13.Count() == 0)
            {
                ViewBag.err = flag = false;
                ViewBag.emptyMsg = "The form - WSIB has no records.";
                return View();
            }
            else
            {
                ViewBag.err = flag = true;
                { ViewBag.IsAdmin = HomeController.isAdmin; }
                { ViewBag.list = STREAM.GetLocNames().ToArray(); }
            }
            return View(TablesContainer.list13);
        }

        public ActionResult Select_Not_WSIB()
        {
            try
            {
                CurrentLocalUser = GetCurrUser();
                localLocation = CurrentLocalUser.Care_Community;
            }
            catch
            {
                string unAuthMsg = "First you should to SignIn or go to Register Form!";
                return RedirectToAction($"../Account/Login/{unAuthMsg}");
            }
            int id_loc = 0;
            if (CurrentLocalUser.Role == Role.User.ToString()) // if user role
            {
                id_loc = AccountController.AllUsers.FirstOrDefault(n => n.Email.Equals(User.Identity.Name)).Care_Community;  /// Get id location from Sign_In_Main page HomeControllers'
                TablesContainer.list14 = db.ReadNotWSIBs().Where(l => l.Location == id_loc).ToList(); // get list of records from Critical_Incidents(Location) 
            }
            else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                AppSettings.IsRegionalUser(new Not_WSIBs_DTO(), CurrentLocalUser.Region, Range.NoN, System.DateTime.MinValue, System.DateTime.MinValue);
            else
                TablesContainer.list14 = db.ReadNotWSIBs().ToList();
            if (TablesContainer.list14.Count() == 0)
            {
                ViewBag.err = flag = false;
                ViewBag.emptyMsg = "The form - Internal has no records.";
                return View();
            }
            else
            {
                if (CurrentLocalUser.Role == Role.User.ToString()) // if user role
                {
                    ViewBag.err = flag = true;
                    var cc = db.ReadHomes().Where(i => i.Id == id_loc).SingleOrDefault();
                    { ViewBag.list = STREAM.GetLocNames().ToArray(); }
                    return View(TablesContainer.list14);
                }
                else // admin
                {
                    ViewBag.err = flag = true;
                    var kk = TablesContainer.list14.ToList();
                    { ViewBag.IsAdmin = HomeController.isAdmin; }
                    { ViewBag.list = STREAM.GetLocNames().ToArray(); }
                    return View(TablesContainer.list14);
                }
            }
        }

        [HttpPost]
        public ActionResult Select_Not_WSIB(object o)
        {
            string btnName =
                  Request.Params.Cast<string>().Where(p => p.StartsWith("btn")).Select(p => p.Substring("btn".Length)).First();
            TablesContainer.list14 = TablesContainer.list14.OrderBy(x => x.Date_of_Incident).ToList();
            var isadmin = HomeController.isAdmin;
            if (btnName.Equals("-upSort"))
                TablesContainer.list14 = TablesContainer.list14.OrderBy(x => x.Date_of_Incident).ToList();
            else if (btnName.Equals("-downSort"))
                TablesContainer.list14 = TablesContainer.list14.OrderByDescending(x => x.Date_of_Incident).ToList();
            if (TablesContainer.list14.Count() == 0)
            {
                ViewBag.err = flag = false;
                ViewBag.emptyMsg = "The form - Internal has no records.";
                return View();
            }
            else
            {
                ViewBag.err = flag = true;
                { ViewBag.IsAdmin = HomeController.isAdmin; }
                { ViewBag.list = STREAM.GetLocNames().ToArray(); }
            }
            return View(TablesContainer.list14);
        }

        public ActionResult Outbreaks()
        {
            try
            {
                CurrentLocalUser = GetCurrUser();
                localLocation = CurrentLocalUser.Care_Community;
            }
            catch
            {
                string unAuthMsg = "First you should to SignIn or go to Register Form!";
                return RedirectToAction($"../Account/Login/{unAuthMsg}");
            }
            int id_loc = 0;
            if (CurrentLocalUser.Role == Role.User.ToString()) // if user role
            {
                id_loc = AccountController.AllUsers.FirstOrDefault(n => n.Email.Equals(User.Identity.Name)).Care_Community;  /// Get id location from Sign_In_Main page HomeControllers'
                TablesContainer.list12 = db.ReadOutbreaks().Where(l => l.Location == id_loc).ToList(); // get list of records from Critical_Incidents(Location) 
            }
            else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                AppSettings.IsRegionalUser(new Outbreaks_DTO(), CurrentLocalUser.Region, Range.NoN, System.DateTime.MinValue, System.DateTime.MinValue);
            else
                TablesContainer.list12 = db.ReadOutbreaks().ToList();
            if (TablesContainer.list12.Count() == 0)
            {
                ViewBag.err = flag = false;
                ViewBag.emptyMsg = "The form - Outbreakes has no records.";
                return View();
            }
            else
            {
                if (CurrentLocalUser.Role == Role.User.ToString()) // if user role
                {
                    ViewBag.err = flag = true;
                    var cc = db.ReadHomes().Where(i => i.Id == id_loc).SingleOrDefault();
                    { ViewBag.list = STREAM.GetLocNames().ToArray(); }
                    return View(TablesContainer.list12);
                }
                else // admin
                {
                    ViewBag.err = flag = true;
                    var kk = TablesContainer.list12.ToList();
                    { ViewBag.IsAdmin = HomeController.isAdmin; }
                    { ViewBag.list = STREAM.GetLocNames().ToArray(); }
                    return View(TablesContainer.list12);
                }
            }
        }

        [HttpPost]
        public ActionResult Outbreaks(object o)
        {
            string btnName =
                Request.Params.Cast<string>().Where(p => p.StartsWith("btn")).Select(p => p.Substring("btn".Length)).First();
            TablesContainer.list12 = TablesContainer.list12.OrderBy(x => x.Date_Declared).ToList();
            var isadmin = HomeController.isAdmin;
            if (btnName.Equals("-upSort"))
                TablesContainer.list12 = TablesContainer.list12.OrderBy(x => x.Date_Declared).ToList();
            else if (btnName.Equals("-downSort"))
                TablesContainer.list12 = TablesContainer.list12.OrderByDescending(x => x.Date_Declared).ToList();
            if (TablesContainer.list12.Count() == 0)
            {
                ViewBag.err = flag = false;
                ViewBag.emptyMsg = "The form - Outbreakes has no records.";
                return View();
            }
            else
            {
                ViewBag.err = flag = true;
                var kk = TablesContainer.list12.ToList();
                { ViewBag.IsAdmin = HomeController.isAdmin; }
                { ViewBag.list = STREAM.GetLocNames().ToArray(); }
            }
            return View(TablesContainer.list12);
        }


        public ActionResult Privacy_Breaches()
        {
            try
            {
                CurrentLocalUser = GetCurrUser();
                localLocation = CurrentLocalUser.Care_Community;
            }
            catch
            {
                string unAuthMsg = "First you should to SignIn or go to Register Form!";
                return RedirectToAction($"../Account/Login/{unAuthMsg}");
            }
            int id_loc = 0;
            if (CurrentLocalUser.Role == Role.User.ToString()) // if user role
            {
                id_loc = AccountController.AllUsers.FirstOrDefault(n => n.Email.Equals(User.Identity.Name)).Care_Community;  /// Get id location from Sign_In_Main page HomeControllers'
                TablesContainer.list7 = db.ReadBreaches().Where(l => l.Location == id_loc).ToList(); // get list of records from Critical_Incidents(Location) 
            }
            else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                AppSettings.IsRegionalUser(new Privacy_Breaches_DTO(), CurrentLocalUser.Region, Range.NoN, System.DateTime.MinValue, System.DateTime.MinValue);
            else TablesContainer.list7 = db.ReadBreaches().ToList();
            if (TablesContainer.list7.Count() == 0)
            {
                ViewBag.err = flag = false;
                ViewBag.emptyMsg = "The form - Privacy Breaches has no records.";
                return View();
            }
            else
            {
                if (CurrentLocalUser.Role == Role.User.ToString()) // if user role
                {
                    ViewBag.err = flag = true;
                    var cc = db.ReadHomes().Where(i => i.Id == id_loc).SingleOrDefault();
                    { ViewBag.list = STREAM.GetLocNames().ToArray(); }

                    return View(TablesContainer.list7);
                }
                else // admin
                {
                    ViewBag.err = flag = true;
                    var kk = TablesContainer.list7.ToList();
                    { ViewBag.IsAdmin = HomeController.isAdmin; }
                    { ViewBag.list = STREAM.GetLocNames().ToArray(); }
                    return View(TablesContainer.list7);
                }
            }
        }

        [HttpPost]
        public ActionResult Privacy_Breaches(object o)
        {
            string btnName =
                  Request.Params.Cast<string>().Where(p => p.StartsWith("btn")).Select(p => p.Substring("btn".Length)).First();
            TablesContainer.list7 = TablesContainer.list7.OrderBy(x => x.Date_Breach_Occurred).ToList();
            var isadmin = HomeController.isAdmin;
            if (btnName.Equals("-upSort"))
                TablesContainer.list7 = TablesContainer.list7.OrderBy(x => x.Date_Breach_Occurred).ToList();
            else if (btnName.Equals("-downSort"))
                TablesContainer.list7 = TablesContainer.list7.OrderByDescending(x => x.Date_Breach_Occurred).ToList();
            if (TablesContainer.list7.Count() == 0)
            {
                ViewBag.err = flag = false;
                ViewBag.emptyMsg = "The form - Privacy Breaches has no records.";
                return View();
            }
            else
            {
                    ViewBag.err = flag = true;
                    { ViewBag.list = STREAM.GetLocNames().ToArray(); }                    
            }
            return View(TablesContainer.list7);
        }

        public ActionResult Privacy_Complaints()
        {
            try
            {
                CurrentLocalUser = GetCurrUser();
                localLocation = CurrentLocalUser.Care_Community;
            }
            catch
            {
                string unAuthMsg = "First you should to SignIn or go to Register Form!";
                return RedirectToAction($"../Account/Login/{unAuthMsg}");
            }
            int id_loc = 0;
            if (CurrentLocalUser.Role == Role.User.ToString()) // if user role
            {
                id_loc = AccountController.AllUsers.FirstOrDefault(n => n.Email.Equals(User.Identity.Name)).Care_Community;  /// Get id location from Sign_In_Main page HomeControllers'
                TablesContainer.list8 = db.ReadPComplaints().Where(l => l.Location == id_loc).ToList(); // get list of records from Critical_Incidents(Location) 
            }
            else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                AppSettings.IsRegionalUser(new Privacy_Complaints_DTO(), CurrentLocalUser.Region, Range.NoN, System.DateTime.MinValue, System.DateTime.MinValue); 
            else
                TablesContainer.list8 = db.ReadPComplaints().ToList();
            if (TablesContainer.list8.Count() == 0)
            {
                ViewBag.err = flag = false;
                ViewBag.emptyMsg = "The form - Privacy Complaints has no records.";
                return View();
            }
            else
            {
                if (CurrentLocalUser.Role == Role.User.ToString()) // if user role
                {
                    ViewBag.err = flag = true;
                    var cc = db.ReadHomes().Where(i => i.Id == id_loc).SingleOrDefault();
                    { ViewBag.list = STREAM.GetLocNames().ToArray(); }
                    return View(TablesContainer.list8);
                }
                else // admin
                {
                    ViewBag.err = flag = true;
                    var kk = TablesContainer.list8.ToList();
                    { ViewBag.IsAdmin = HomeController.isAdmin; }
                    { ViewBag.list = STREAM.GetLocNames().ToArray(); }
                    return View(TablesContainer.list8);
                }
            }
        }

        [HttpPost]
        public ActionResult Privacy_Complaints(object o)
        {
            string btnName =
                  Request.Params.Cast<string>().Where(p => p.StartsWith("btn")).Select(p => p.Substring("btn".Length)).First();
            TablesContainer.list8 = TablesContainer.list8.OrderBy(x => x.Date_Complain_Received).ToList();
            var isadmin = HomeController.isAdmin;
            if (btnName.Equals("-upSort"))
                TablesContainer.list8 = TablesContainer.list8.OrderBy(x => x.Date_Complain_Received).ToList();
            else if (btnName.Equals("-downSort"))
                TablesContainer.list8 = TablesContainer.list8.OrderByDescending(x => x.Date_Complain_Received).ToList();
            if (TablesContainer.list8.Count() == 0)
            {
                ViewBag.err = flag = false;
                ViewBag.emptyMsg = "The form - Privacy Complaints has no records.";
                return View();
            }
            else
            {
                ViewBag.err = flag = true;
                { ViewBag.list = STREAM.GetLocNames().ToArray(); }
            }
            return View(TablesContainer.list8);
        }

        public ActionResult Select_Emergency_Prep()
        {
            try
            {
                CurrentLocalUser = GetCurrUser();
                localLocation = CurrentLocalUser.Care_Community;
            }
            catch
            {
                string unAuthMsg = "First you should to SignIn or go to Register Form!";
                return RedirectToAction($"../Account/Login/{unAuthMsg}");
            }
            int id_loc = 0;
            if (CurrentLocalUser.Role == Role.User.ToString()) // if user role
            {
                id_loc = AccountController.AllUsers.FirstOrDefault(n => n.Email.Equals(User.Identity.Name)).Care_Community;  /// Get id location from Sign_In_Main page HomeControllers'
                TablesContainer.list4 = db.ReadEmergency().Where(l => l.Location == id_loc).ToList(); // get list of records from Critical_Incidents(Location) 
            }
            else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                AppSettings.IsRegionalUser(new Emergency_Prep_DTO(), CurrentLocalUser.Region, Range.NoN, System.DateTime.MinValue, System.DateTime.MinValue);
            else 
                TablesContainer.list4 = db.ReadEmergency().ToList();
            if (TablesContainer.list4.Count() == 0)
            {
                ViewBag.err = flag = false;
                ViewBag.emptyMsg = "The form - Emergency Prep has no records.";
                return View();
            }
            else
            {
                if (CurrentLocalUser.Role == Role.User.ToString()) // if user role
                {
                    ViewBag.err = flag = true;
                    var cc = db.ReadHomes().Where(i => i.Id == id_loc).SingleOrDefault();
                    { ViewBag.list = STREAM.GetLocNames().ToArray(); }
                    return View(TablesContainer.list4);
                }
                else // admin
                {
                    ViewBag.err = flag = true;
                    { ViewBag.IsAdmin = HomeController.isAdmin; }
                    { ViewBag.list = STREAM.GetLocNames().ToArray(); }
                    return View(TablesContainer.list4);
                }
            }
        }

        [HttpPost]
        public ActionResult Select_Emergency_Prep(object o)
        {
            string btnName =
                  Request.Params.Cast<string>().Where(p => p.StartsWith("btn")).Select(p => p.Substring("btn".Length)).First();
            TablesContainer.list4 = TablesContainer.list4.OrderBy(x => x.Date).ToList();
            var isadmin = HomeController.isAdmin;
            if (btnName.Equals("-upSort"))
                TablesContainer.list4 = TablesContainer.list4.OrderBy(x => x.Date).ToList();
            else if (btnName.Equals("-downSort"))
                TablesContainer.list4 = TablesContainer.list4.OrderByDescending(x => x.Date).ToList();
            if (TablesContainer.list4.Count() == 0)
            {
                ViewBag.err = flag = false;
                ViewBag.emptyMsg = "The form - Good News has no records.";
                return View();
            }
            else
            {
                ViewBag.err = flag = true;
                var kk = TablesContainer.list4.ToList();
                { ViewBag.IsAdmin = HomeController.isAdmin; }
                { ViewBag.list = STREAM.GetLocNames().ToArray(); }
            }
            return View(TablesContainer.list4);
        }

        public ActionResult Select_BC_LTC()
        {
            try
            {
                CurrentLocalUser = GetCurrUser();
                localLocation = CurrentLocalUser.Care_Community;
            }
            catch
            {
                string unAuthMsg = "First you should to SignIn or go to Register Form!";
                return RedirectToAction($"../Account/Login/{unAuthMsg}");
            }
            int id_loc = 0;
            if (CurrentLocalUser.Role == Role.User.ToString()) // if user role
            {
                id_loc = AccountController.AllUsers.FirstOrDefault(n => n.Email.Equals(User.Identity.Name)).Care_Community;  /// Get id location from Sign_In_Main page HomeControllers'
                TablesContainer.list19 = db.ReadBC_LTC().Where(l => l.CareCommName == id_loc).ToList(); // get list of records from Critical_Incidents(Location) 
            }
            else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                AppSettings.IsRegionalUser(new BC_LTC_Reportable_Incidents_DTO(), CurrentLocalUser.Region, Range.NoN, System.DateTime.MinValue, System.DateTime.MinValue);
            else // admin
                TablesContainer.list19 = db.ReadBC_LTC().ToList();
            if (TablesContainer.list19.Count() == 0)
            {
                ViewBag.err = flag = false;
                ViewBag.emptyMsg = "The form - Reportable Incidents LTC has no records.";
                return View();
            }
            else
            {
                if (CurrentLocalUser.Role == Role.User.ToString()) // if user role
                {
                    ViewBag.err = flag = true;
                    var cc = db.ReadHomes().Where(i => i.Id == id_loc).SingleOrDefault();
                    { ViewBag.list = STREAM.GetLocNames().ToArray(); }
                    return View(TablesContainer.list19);
                }
                else // admin
                {
                    ViewBag.err = flag = true;
                    var kk = TablesContainer.list19.ToList();
                    { ViewBag.IsAdmin = HomeController.isAdmin; }
                    { ViewBag.list = STREAM.GetLocNames().ToArray(); }
                    return View(TablesContainer.list19);
                }
            }
        }

        [HttpPost]
        public ActionResult Select_BC_LTC(object o)
        {
            string btnName =
                  Request.Params.Cast<string>().Where(p => p.StartsWith("btn")).Select(p => p.Substring("btn".Length)).First();
            TablesContainer.list19 = TablesContainer.list19.OrderBy(x => x.DateIncident).ToList();
            var isadmin = HomeController.isAdmin;
            if (btnName.Equals("-upSort"))
                TablesContainer.list19 = TablesContainer.list19.OrderBy(x => x.DateIncident).ToList();
            else if (btnName.Equals("-downSort"))
                TablesContainer.list19 = TablesContainer.list19.OrderByDescending(x => x.DateIncident).ToList();
            if (TablesContainer.list19.Count() == 0)
            {
                ViewBag.err = flag = false;
                ViewBag.emptyMsg = "The form - Reportable Incidents LTC has no records.";
                return View();
            }
            else
            {
                ViewBag.err = flag = true;
                var kk = TablesContainer.list19.ToList();
                { ViewBag.IsAdmin = HomeController.isAdmin; }
                { ViewBag.list = STREAM.GetLocNames().ToArray(); }
            }
            return View(TablesContainer.list19);
        }

        public ActionResult Select_BC_LTC_Assisted()
        {
            try
            {
                CurrentLocalUser = GetCurrUser();
                localLocation = CurrentLocalUser.Care_Community;
            }
            catch
            {
                string unAuthMsg = "First you should to SignIn or go to Register Form!";
                return RedirectToAction($"../Account/Login/{unAuthMsg}");
            }
            int id_loc = 0;

            if (CurrentLocalUser.Role == Role.User.ToString()) // if user role
            {
                id_loc = AccountController.AllUsers.FirstOrDefault(n => n.Email.Equals(User.Identity.Name)).Care_Community;  /// Get id location from Sign_In_Main page HomeControllers'
                TablesContainer.list20 = db.ReadBC_LTCAssisted().Where(l => l.NameCareCommu == id_loc).ToList(); // get list of records from Critical_Incidents(Location) 
            }
            else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                AppSettings.IsRegionalUser(new BC_Assisted_Living_Reportable_Incidents_DTO(),
                    CurrentLocalUser.Region, Range.NoN, System.DateTime.MinValue, System.DateTime.MinValue);
            else
                TablesContainer.list20 = db.ReadBC_LTCAssisted().ToList();
            if (TablesContainer.list20.Count() == 0)
            {
                ViewBag.err = flag = false;
                ViewBag.emptyMsg = "The form - Assisted Living Reportable Incidents has no records.";
                return View();
            }
            else
            {
                if (CurrentLocalUser.Role == Role.User.ToString()) // if user role
                {
                    ViewBag.err = flag = true;
                    var cc = db.ReadHomes().Where(i => i.Id == id_loc).SingleOrDefault();
                    { ViewBag.list = STREAM.GetLocNames().ToArray(); }
                    return View(TablesContainer.list20);
                }
                else // if admin role
                {
                    ViewBag.err = flag = true;
                    var kk = TablesContainer.list20.ToList();
                    { ViewBag.IsAdmin = HomeController.isAdmin; }
                    { ViewBag.list = STREAM.GetLocNames().ToArray(); }
                    return View(TablesContainer.list20);
                }
            }
        }

        [HttpPost]
        public ActionResult Select_BC_LTC_Assisted(object o)
        {
            string btnName =
                  Request.Params.Cast<string>().Where(p => p.StartsWith("btn")).Select(p => p.Substring("btn".Length)).First();
            TablesContainer.list20 = TablesContainer.list20.OrderBy(x => x.DateIncident).ToList();
            var isadmin = HomeController.isAdmin;
            if (btnName.Equals("-upSort"))
                TablesContainer.list20 = TablesContainer.list20.OrderBy(x => x.DateIncident).ToList();
            else if (btnName.Equals("-downSort"))
                TablesContainer.list20 = TablesContainer.list20.OrderByDescending(x => x.DateIncident).ToList();
            if (TablesContainer.list20.Count() == 0)
            {
                ViewBag.err = flag = false;
                ViewBag.emptyMsg = "The form - Assisted Living Reportable Incidents has no records.";
                return View();
            }
            else
            {
                ViewBag.err = flag = true;
                var kk = TablesContainer.list20.ToList();
                { ViewBag.IsAdmin = HomeController.isAdmin; }
                { ViewBag.list = STREAM.GetLocNames().ToArray(); }
            }
            return View(TablesContainer.list20);
        }

        public ActionResult Select_LiceInspect()
        {
            try
            {
                CurrentLocalUser = GetCurrUser();
                localLocation = CurrentLocalUser.Care_Community;
            }
            catch
            {
                string unAuthMsg = "First you should to SignIn or go to Register Form!";
                return RedirectToAction($"../Account/Login/{unAuthMsg}");
            }
            int id_loc = 0;
            if (CurrentLocalUser.Role == Role.User.ToString()) // if user role
            {
                id_loc = AccountController.AllUsers.FirstOrDefault(n => n.Email.Equals(User.Identity.Name)).Care_Community;  /// Get id location from Sign_In_Main page HomeControllers'
                TablesContainer.list15 = db.ReadLiceInspect().Where(l => l.CareComName == id_loc).ToList(); // get list of records from Critical_Incidents(Location) 
            }
            else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                AppSettings.IsRegionalUser(new LicensingInspectionDTO(), CurrentLocalUser.Region, Range.NoN, System.DateTime.MinValue, System.DateTime.MinValue);
            else // admin
                TablesContainer.list15 = db.ReadLiceInspect().ToList();
            if (TablesContainer.list15.Count() == 0)
            {
                ViewBag.err = flag = false;
                ViewBag.emptyMsg = "The form - Licensing Inspections has no records.";
                return View();
            }
            else
            {
                if (CurrentLocalUser.Role == Role.User.ToString()) // if user role
                {
                    ViewBag.err = flag = true;
                    var cc = db.ReadHomes().Where(i => i.Id == id_loc).SingleOrDefault();
                    { ViewBag.list = STREAM.GetLocNames().ToArray(); }
                    return View(TablesContainer.list15);
                }
                else // if admin role
                {
                    ViewBag.err = flag = true;
                    var kk = TablesContainer.list15.ToList();
                    { ViewBag.IsAdmin = HomeController.isAdmin; }
                    { ViewBag.list = STREAM.GetLocNames().ToArray(); }
                    return View(TablesContainer.list15);
                }
            }
        }

        [HttpPost]
        public ActionResult Select_LiceInspect(object o)
        {
            string btnName =
                  Request.Params.Cast<string>().Where(p => p.StartsWith("btn")).Select(p => p.Substring("btn".Length)).First();
            TablesContainer.list15 = TablesContainer.list15.OrderBy(x => x.Date).ToList();
            var isadmin = HomeController.isAdmin;
            if (btnName.Equals("-upSort"))
                TablesContainer.list15 = TablesContainer.list15.OrderBy(x => x.Date).ToList();
            else if (btnName.Equals("-downSort"))
                TablesContainer.list15 = TablesContainer.list15.OrderByDescending(x => x.Date).ToList();
            if (TablesContainer.list15.Count() == 0)
            {
                ViewBag.err = flag = false;
                ViewBag.emptyMsg = "The form - Licensing Inspections has no records.";
                return View();
            }
            else
            {
                ViewBag.err = flag = true;
                var kk = TablesContainer.list15.ToList();
                { ViewBag.list = STREAM.GetLocNames().ToArray(); }
            }
            return View(TablesContainer.list15);
        }

        public ActionResult Select_AssistLivInspect()
        {
            try
            {
                CurrentLocalUser = GetCurrUser();
                localLocation = CurrentLocalUser.Care_Community;
            }
            catch
            {
                string unAuthMsg = "First you should to SignIn or go to Register Form!";
                return RedirectToAction($"../Account/Login/{unAuthMsg}");
            }
            int id_loc = 0;
            if (CurrentLocalUser.Role == Role.User.ToString()) // if user role
            {
                id_loc = AccountController.AllUsers.FirstOrDefault(n => n.Email.Equals(User.Identity.Name)).Care_Community;  /// Get id location from Sign_In_Main page HomeControllers'
                TablesContainer.list16 = db.ReadAssLivInspect().Where(l => l.CareComName == id_loc).ToList(); // get list of records from Critical_Incidents(Location) 
            }
            else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                AppSettings.IsRegionalUser(new AssistedLivingInspectionDTO(), CurrentLocalUser.Region, Range.NoN, System.DateTime.MinValue, System.DateTime.MinValue);
            else
                TablesContainer.list16 = db.ReadAssLivInspect().ToList();
            if (TablesContainer.list16.Count() == 0)
            {
                ViewBag.err = flag = false;
                ViewBag.emptyMsg = "The form - Assisted Liviving Inspection has no records.";
                return View();
            }
            else
            {
                if (CurrentLocalUser.Role == Role.User.ToString()) // if user role
                {
                    ViewBag.err = flag = true;
                    var cc = db.ReadHomes().Where(i => i.Id == id_loc).SingleOrDefault();
                    { ViewBag.list = STREAM.GetLocNames().ToArray(); }
                    return View(TablesContainer.list16);
                }
                else // if admin role
                {
                    ViewBag.err = flag = true;
                    var kk = TablesContainer.list16.ToList();
                    { ViewBag.IsAdmin = HomeController.isAdmin; }
                    { ViewBag.list = STREAM.GetLocNames().ToArray(); }
                    return View(TablesContainer.list16);
                }
            }
        }

        [HttpPost]
        public ActionResult Select_AssistLivInspect(object o)
        {
            string btnName =
                  Request.Params.Cast<string>().Where(p => p.StartsWith("btn")).Select(p => p.Substring("btn".Length)).First();
            TablesContainer.list16 = TablesContainer.list16.OrderBy(x => x.Date).ToList();
            var isadmin = HomeController.isAdmin;
            if (btnName.Equals("-upSort"))
                TablesContainer.list16 = TablesContainer.list16.OrderBy(x => x.Date).ToList();
            else if (btnName.Equals("-downSort"))
                TablesContainer.list16 = TablesContainer.list16.OrderByDescending(x => x.Date).ToList();
            if (TablesContainer.list16.Count() == 0)
            {
                ViewBag.err = flag = false;
                ViewBag.emptyMsg = "The form - Assisted Liviving Inspection has no records.";
                return View();
            }
            else
            {
                ViewBag.err = flag = true;
                var kk = TablesContainer.list16.ToList();
                { ViewBag.list = STREAM.GetLocNames().ToArray(); }
            }
            return View(TablesContainer.list16);
        }

        public ActionResult Select_WorksaveBC()
        {
            try
            {
                CurrentLocalUser = GetCurrUser();
                localLocation = CurrentLocalUser.Care_Community;
            }
            catch
            {
                string unAuthMsg = "First you should to SignIn or go to Register Form!";
                return RedirectToAction($"../Account/Login/{unAuthMsg}");
            }
            int id_loc = 0;
            if (CurrentLocalUser.Role == Role.User.ToString()) // if user role
            {
                id_loc = AccountController.AllUsers.FirstOrDefault(n => n.Email.Equals(User.Identity.Name)).Care_Community;  /// Get id location from Sign_In_Main page HomeControllers'
                TablesContainer.list17 = db.ReadWorkshopBCInspect().Where(l => l.CareComName == id_loc).ToList(); // get list of records from Critical_Incidents(Location) 
            }
            else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                AppSettings.IsRegionalUser(new WorkshopBCInspection_DTO(), CurrentLocalUser.Region, Range.NoN, System.DateTime.MinValue, System.DateTime.MinValue);
            else
                TablesContainer.list17 = db.ReadWorkshopBCInspect().ToList();
            if (TablesContainer.list17.Count() == 0)
            {
                ViewBag.err = flag = false;
                ViewBag.emptyMsg = "The form - Worksafe BC Inspection has no records.";
                return View();
            }
            else
            {
                if (CurrentLocalUser.Role == Role.User.ToString()) // if user role
                {
                    ViewBag.err = flag = true;
                    var cc = db.ReadHomes().Where(i => i.Id == id_loc).SingleOrDefault();
                    { ViewBag.list = STREAM.GetLocNames().ToArray(); }
                    return View(TablesContainer.list17);
                }
                else // if admin role
                {
                    ViewBag.err = flag = true;
                    var kk = TablesContainer.list17.ToList();
                    { ViewBag.IsAdmin = HomeController.isAdmin; }
                    { ViewBag.list = STREAM.GetLocNames().ToArray(); }
                    return View(TablesContainer.list17);
                }
            }
        }

        [HttpPost]
        public ActionResult Select_WorksaveBC(object o)
        {
            string btnName =
                  Request.Params.Cast<string>().Where(p => p.StartsWith("btn")).Select(p => p.Substring("btn".Length)).First();
            TablesContainer.list17 = TablesContainer.list17.OrderBy(x => x.Date).ToList();
            var isadmin = HomeController.isAdmin;
            if (btnName.Equals("-upSort"))
                TablesContainer.list17 = TablesContainer.list17.OrderBy(x => x.Date).ToList();
            else if (btnName.Equals("-downSort"))
                TablesContainer.list17 = TablesContainer.list17.OrderByDescending(x => x.Date).ToList();
            if (TablesContainer.list17.Count() == 0)
            {
                ViewBag.err = flag = false;
                ViewBag.emptyMsg = "The form - Worksafe BC Inspection has no records.";
                return View();
            }
            else
            {
                ViewBag.err = flag = true;
                { ViewBag.list = STREAM.GetLocNames().ToArray(); }
            }
            return View(TablesContainer.list17);
        }

        public ActionResult Select_QualityReview()
        {
            try
            {
                CurrentLocalUser = GetCurrUser();
                localLocation = CurrentLocalUser.Care_Community;
            }
            catch
            {
                string unAuthMsg = "First you should to SignIn or go to Register Form!";
                return RedirectToAction($"../Account/Login/{unAuthMsg}");
            }
            int id_loc = 0;
            if (CurrentLocalUser.Role == Role.User.ToString()) // if user role
            {
                id_loc = AccountController.AllUsers.FirstOrDefault(n => n.Email.Equals(User.Identity.Name)).Care_Community;  /// Get id location from Sign_In_Main page HomeControllers'
                TablesContainer.list18 = db.ReadQualityReviews().Where(l => l.CareComName == id_loc).ToList(); // get list of records from Critical_Incidents(Location) 
            }
            else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                AppSettings.IsRegionalUser(new QualityReview_DTO(), CurrentLocalUser.Region, Range.NoN, System.DateTime.MinValue, System.DateTime.MinValue);
            else
                TablesContainer.list18 = db.ReadQualityReviews().ToList();
            if (TablesContainer.list18.Count() == 0)
            {
                ViewBag.err = flag = false;
                ViewBag.emptyMsg = "The form - Quality Reviews / QACR has no records.";
                return View();
            }
            else
            {
                if (CurrentLocalUser.Role == Role.User.ToString()) // if user role
                {
                    ViewBag.err = flag = true;
                    var cc = db.ReadHomes().Where(i => i.Id == id_loc).SingleOrDefault();
                    { ViewBag.list = STREAM.GetLocNames().ToArray(); }
                    return View(TablesContainer.list18);
                }
                else // if admin role
                {
                    ViewBag.err = flag = true;
                    var kk = TablesContainer.list18.ToList();
                    { ViewBag.IsAdmin = HomeController.isAdmin; }
                    { ViewBag.list = STREAM.GetLocNames().ToArray(); }
                    return View(TablesContainer.list18);
                }
            }
        }

        [HttpPost]
        public ActionResult Select_QualityReview(object o)
        {
            string btnName =
                  Request.Params.Cast<string>().Where(p => p.StartsWith("btn")).Select(p => p.Substring("btn".Length)).First();
            TablesContainer.list18 = TablesContainer.list18.OrderBy(x => x.Date).ToList();
            var isadmin = HomeController.isAdmin;
            if (btnName.Equals("-upSort"))
                TablesContainer.list18 = TablesContainer.list18.OrderBy(x => x.Date).ToList();
            else if (btnName.Equals("-downSort"))
                TablesContainer.list18 = TablesContainer.list18.OrderByDescending(x => x.Date).ToList();
            if (TablesContainer.list18.Count() == 0)
            {
                ViewBag.err = flag = false;
                ViewBag.emptyMsg = "The form - Quality Reviews / QACR has no records.";
                return View();
            }
            else
            {
                ViewBag.err = flag = true;
                { ViewBag.list = STREAM.GetLocNames().ToArray(); }
            }
            return View(TablesContainer.list18);
        }

        public ActionResult Select_MOHInspections()
        {
            try
            {
                CurrentLocalUser = GetCurrUser();
                localLocation = CurrentLocalUser.Care_Community;
            }
            catch
            {
                string unAuthMsg = "First you should to SignIn or go to Register Form!";
                return RedirectToAction($"../Account/Login/{unAuthMsg}");
            }
            int id_loc = 0;
            IEnumerable<MOH_Inspection_DTO> list = default;
            if (CurrentLocalUser.Role == Role.User.ToString()) // if user role
            {
                id_loc = AccountController.AllUsers.FirstOrDefault(n => n.Email.Equals(User.Identity.Name)).Care_Community;
                list = db.ReadMOHInspections().Where(l => l.Location == id_loc);
            }
            else // admin
                list = db.ReadMOHInspections().ToList();

            if (list.Count() == 0) // when table is empty
            {
                ViewBag.err = flag = false;
                ViewBag.emptyMsg = "The form - MOH Inspections  has no records.";
                return View();
            }
            else
            {
                if (CurrentLocalUser.Role == Role.User.ToString()) // if user role
                {
                    ViewBag.err = flag = true;
                    var cc = db.ReadHomes().Where(i => i.Id == id_loc).SingleOrDefault();
                    { ViewBag.list = STREAM.GetLocNames().ToArray(); }
                    return View(list);
                }
                else // if admin role
                {
                    ViewBag.err = flag = true;
                    var kk = list.ToList();
                    { ViewBag.IsAdmin = HomeController.isAdmin; }
                    { ViewBag.list = STREAM.GetLocNames().ToArray(); }
                    return View(list);
                }
            }
        }
    }
}
