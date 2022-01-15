namespace DTS.Controllers
{
    #region Namespaces:
    using Logger;
    using System;
    using DSS.BLL;
    using System.IO;
    using DTS.Models;
    using System.Web;
    using System.Linq;
    using System.Text;
    using DSS.BLL.DTO;
    using System.Web.Mvc;
    using DSS.BLL.Services;
    using DSS.BLL.Interfaces;
    using System.Collections;
    using System.Threading.Tasks;
    using DTS.Models.RegionLogic;
    using System.Collections.Generic;
    #endregion

    #region Enums Region, Role, Range:
    public enum RegionPath
    {
        Region1,
        Region2,
        Region3,
        Region4,
        Region5,
        Region6,
        Region7
    };

    public enum Region
    {
        First = 1,
        Second = 2,
        Third = 3,
        Fourth = 4,
        Fifth = 5,
        Sixth = 6,
        Seventh = 7,
        Tenth = 10,
        Eleventh = 11,
        Twelvth = 12,
        NoN = 0
    };

    public enum Role { Admin, SuperUser, RegionalUser, User, Unknown };

    public enum Range { With, Without, NoN };
    #endregion

    public class HomeController : Controller
    {
        #region Fields:
        internal STREAM str;
        private int localLocation = 0;
        protected static ServiceDSS db;
        private ApplicationUser CurrentLocalUser { get; set; }

        #region Static members(fields):
        public static bool IsInit { get; set; }
        public static bool IsStart { get; set; }
        public static bool isSignIn { get; set; } = false;
        // This fieald need to know all memory to the heap
        public static long TotalMemoryHeap;
        static string notsel;
        //public static Role role;  // oldest field func-ty
        public static Range range;
        //public static int region; // oldest field func-ty
        // public static int userLocation { get; set; }// oldest field func-ty
        public static bool isAdmin = false, isEmpty = false, b = false;
        static string model_name { get; set; }
        static string w_without = string.Empty;
        static string radioNameForms = string.Empty;
        static string mirrorWout = null;
        static bool withRange = false;
        public static string path;
        public static string path2;
        public static int[] counts;
        public static string[] AllLocations;
        public static string[] SelectRegions;
        public static int num_tbl;
        public static string checkView = "none";
        public static List<string> strs, strN;
        public static List<CI_Category_Type_DTO> categories;
        public static List<Department_DTO> departments;
        public static List<Position_DTO> positions;
        public static List<Home_DTO> communities;
        public static SelectList
            list2, list, list3, list4, list5, list6, list7, list8, list9, list10,
            list11, list12, list13, list14, list15, list16, list17, list18, list19, list20, list21, list22, list23,
            list24, list25, list26, list27, list28, list29; //needed for front end drop down list
        List<object> both;
        public static string[] SelectYesNo, visit, written, direct, resident, resolved, ministry, categoryGoodNews, departmentGoodNews,
            sourceGoodNews, department2, receiveFrom, picture, risk_list, visitAgency, visitnumbers, bc_ttc1, bc_ltc2, bc_ltc3, bc_ltc4,
            bc_ltc_assist, exercises, methods, codes, contraventions, inspectType, scopeOfInspects;
        #endregion
        #endregion

        #region Constructor:
        public HomeController()
        {
            #region Old f-lty:
            //IsStart = true;
            //string mail = User.Identity.Name;
            // = GetCurrUser();
            //AccountController.AllUsers.FirstOrDefault(n => n.Email.Equals(mail));
            //if (CurrentLocalUser.Role == Role.User.ToString())
            //ViewBag.IsUser = true;
            //else ViewBag.IsUser = false;
            //if (!IsInit)
            #endregion
            AppSettings.ListsInit(out db, str);
            TotalMemoryHeap = GC.GetTotalMemory(true);
        }
        #endregion

        #region Interrupt when we try to enter via 'url-route':
        public object CheckAlreadyUser()
        {
            try
            {
                CurrentLocalUser = GetCurrUser();
                return localLocation = CurrentLocalUser.Care_Community;
            }
            catch
            {
                string unAuthMsg = "First you should to SignIn or go to Register Form!";
                return RedirectToAction($"../Account/Login/{unAuthMsg}");
            }
        }
        #endregion

        #region Complaints(Insert):
        public ActionResult Complaint_Insert()
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
            object[] objs = new object[] { list, list3, list4, list5, list6, list7, list8, list9, list10, list11 };
            ViewBag.locations = objs;
            return View();
        }

        [HttpPost]
        public ActionResult Complaint_Insert(Complaint_DTO entity)
        {
            object[] objs = new object[] { list, list3, list4, list5, list6, list7, list8, list9, list10, list11 };
            ViewBag.locations = objs;
            if (entity.DateReceived == null && entity.Location == 0 && entity.WritenOrVerbal == null && entity.Receive_Directly == null &&
               entity.FromResident == null && entity.ResidentName == null && entity.Department == null && entity.BriefDescription == null &&
               entity.IsAdministration == false && entity.CareServices == false && entity.PalliativeCare == false && entity.Dietary == false && entity.Housekeeping == false &&
               entity.Laundry == false && entity.Maintenance == false && entity.Programs == false && entity.Physician == false && entity.Other == false && entity.MOHLTCNotified == null && entity.CopyToVP == null &&
               entity.ResponseSent == null && entity.ActionToken == null && entity.Resolved == null && entity.MinistryVisit == null)
            {
                ViewBag.Empty = "All fields have to be filled.";
                return View();
            }
            else if (entity.DateReceived == null || entity.Location == 0 || entity.WritenOrVerbal == null || entity.Receive_Directly == null ||
               entity.FromResident == null || entity.ResidentName == null || entity.Department == null || entity.BriefDescription == null ||
               entity.IsAdministration == false || entity.CareServices == false || entity.PalliativeCare == false || entity.Dietary == false || entity.Housekeeping == false ||
               entity.Laundry == false || entity.Maintenance == false || entity.Programs == false || entity.Physician == false || entity.Other == false || entity.MOHLTCNotified == null || entity.CopyToVP == null ||
               entity.ResponseSent == null || entity.ActionToken == null || entity.Resolved == null || entity.MinistryVisit == null)
            {

                try
                {
                    //userLocation = entity.Location;
                    db.Insert(entity);

                    return RedirectToAction("../Select/Select_Complaints");
                }
                catch (Exception ex) { return Json("Error occurred. Error details: " + ex.Message); }
                // ViewBag.Empty = "Some fields are empty. Please fill it out and try again!";
                // return View();
            }
            else
            {
                try
                {
                    //userLocation = entity.Location;
                    db.Insert(entity);

                    return RedirectToAction("../Select/Select_Complaints");
                }
                catch (Exception ex) { return HttpNotFound(ex.Message); }
            }
        }
        #endregion

        #region Index(redirect to login page after logoff):
        [HttpGet]
        public ActionResult Index()
        {
            if (isSignIn)
            {
                isSignIn = false;
                return RedirectToAction("../Home/WOR_Tabs");
            }
            else
                return RedirectToAction("../Account/Login");
        }
        #endregion

        #region Upload files:
        public ActionResult Files()
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
            both = new List<object> { list, list2 };
            ViewBag.listing = both;
            { ViewBag.IsAdmin = isAdmin; }
            var mainModel = new Sign_in_Main_DTO();
            return View(mainModel);
        }

        [HttpPost]
        public ActionResult Uploded()
        {// Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        // Get the complete folder path and store the file inside it.  
                        path = Path.Combine(Server.MapPath($"~/Uploaded_Files/{fname}"));
                        file.SaveAs(path);
                    }
                    // Returns message that successfully uploaded  
                    return Json("Your file was uploaded successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("There was no file selected. Please try again.");
            }
        }

        public ActionResult AllFiles()
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
            List<string> names = new List<string>();
            path = Server.MapPath("~/Uploaded_Files");
            string[] files_names = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
            //if(files_names == null || files_names.Length == 0)
            //{
            //    ViewBag.Empty = "There is no uploaded file here. Please go to the upload area.";
            //    return View();
            //}

            for (int i = 0; i < files_names.Length; i++)
                names.Add(Path.GetFileName(files_names[i]));

            return View(names);
        }
        #endregion

        #region Delete file:
        public ActionResult DeleteFile(string item)
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
            bool flag = false;
            if (item != null)
            {
                flag = true;
                string path = Path.Combine(Server.MapPath($"~/Uploaded_Files/{item}"));
                System.IO.File.Delete(path);
            }
            if (!flag) return Json("There is nothing to delete...Please upload a file first.");
            else return RedirectToAction("../Home/AllFiles");
        }
        #endregion

        #region Radio settings:
        string ReInitRadios(string btnName)
        {
            if (w_without == null)
                w_without = mirrorWout;

            if (btnName.Equals("-upSort") || btnName.Equals("-downSort"))
            {
                if (!withRange)
                    w_without = "-without";
                else w_without = "-with";
            }

            #region Change a table:
            if (radioNameForms == string.Empty)
            {
                radioNameForms = Request.Form["formRadio"];
                return radioNameForms;
            }
            if (radioNameForms != string.Empty && (btnName.Equals("-insert") ||
                btnName.Equals("-summary") || btnName.Equals("-list") || btnName.Equals("-export")))
            {
                radioNameForms = Request.Form["formRadio"];
                return radioNameForms;
            }
            else return radioNameForms;
            #endregion
        }
        #endregion

        public ApplicationUser GetCurrUser() => AccountController.AllUsers.FirstOrDefault(n => n.Email.Equals(User.Identity.Name));

        #region WOR Tabs(Get):
        [HttpGet]
        public ActionResult WOR_Tabs()
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

            ViewBag.AllCI = STREAM.listCI;
            { ViewBag.AllLocs = STREAM.list; }
            if (w_without != null)
                mirrorWout = w_without;
            WorTabs tabs = null;
            { ViewBag.Welcome = AppSettings.GetRoleString(CurrentLocalUser.Role); }
            object[] vbs = default;
            if (b)
            {
                vbs = AppSettings.FillVievBags(num_tbl, db, localLocation, CurrentLocalUser);
                { ViewBag.Count = vbs[0]; }
                { ViewBag.GN_Found = vbs[1]; }
                { ViewBag.Entity = vbs[2]; }
                { ViewBag.Check1 = vbs[3]; }
                { ViewBag.Locations = vbs[4]; }
                { ViewBag.IsAdmin = vbs[5]; }
                { ViewBag.TotalSummary = vbs[6]; }
                { ViewBag.Check = vbs[7]; }
                { ViewBag.EmptLocation = vbs[8]; }
                { ViewBag.LocInfo = vbs[9]; }
            }

            tabs = new WorTabs();
            tabs.ListForms = GetFormNames();
            return View(tabs);
        }
        #endregion

        #region WOR Tabs(Post):
        private ArrayList res = default;
        [HttpPost]
        public ActionResult WOR_Tabs(WorTabs Value)
        {
            new TablesContainer().ResetAllTabls();
            try
            {
                CurrentLocalUser = GetCurrUser();
                localLocation = CurrentLocalUser.Care_Community;
            }
            catch { return RedirectToAction($"../Account/Login"); }

            ViewBag.AllLocs = STREAM.list;
            ViewBag.AllCI = STREAM.listCI;

            string btnName =
                Request.Params.Cast<string>().Where(p => p.StartsWith("btn")).Select(p => p.Substring("btn".Length)).First();

            try
            {
                w_without = Request.Form["range"];
                Value.Name = ReInitRadios(btnName);
                if (w_without.Equals("-with")) { range = Range.With; withRange = true; }
                else if (w_without.Equals("-without")) { range = Range.Without; withRange = false; }
            }
            catch (Exception ex) { var _4test = ex.Message + " | " + ex.StackTrace; }

            ViewBag.ListCI = list3;
            { ViewBag.Welcome = AppSettings.GetRoleString(CurrentLocalUser.Role); }
            ViewBag.IsAdmin = isAdmin;
            DateTime start = DateTime.MinValue, end = DateTime.MinValue;
            start = Value.Start;
            end = Value.End;
            string errorMsg = string.Empty;
            if (Value != null && Value.Name != null)  // If we select anythng table
            {
                #region For Showing List (With and Without Range):
                if (btnName.Equals("-list") || btnName.Equals("-upSort") || btnName.Equals("-downSort"))
                {
                    if (w_without.Equals(""))
                    {
                        ViewBag.ErrorMsg = "Radio button and date range are NOT selected";
                        WorTabs tabs = new WorTabs();
                        tabs.ListForms = GetFormNames();
                        return View(tabs);
                    }
                    else
                    {
                        res = start != DateTime.MinValue && end != DateTime.MinValue ?
                             AppSettings.ShowingList(Value.Name, btnName, range, db, localLocation, CurrentLocalUser.Role,
                             RegionService.GetRegion(AccountController.AllUsers.FirstOrDefault(n => n.Email.Equals(User.Identity.Name))),
                             start, end) :
                             AppSettings.ShowingList(Value.Name, btnName, range, db, localLocation, CurrentLocalUser.Role,
                             RegionService.GetRegion(AccountController.AllUsers.FirstOrDefault(n => n.Email.Equals(User.Identity.Name))));
                        var tbl_list = FillOutTableById(int.Parse(Value.Name), true).ToArray().ToList();
                        { ViewBag.Check = checkView = "list"; }
                        if (tbl_list.Count == 0)
                        {
                            ViewBag.IsEmpty = isEmpty = false;
                            ViewBag.ErrorMsg = $"The form - {GetModelNameByNum(int.Parse(Value.Name))} has no records.";
                            WorTabs tabs = new WorTabs();
                            tabs.ListForms = GetFormNames();
                            return View(tabs);
                        }
                        { ViewBag.Tbl = Value.Name; }

                        if (res != null) // List`1
                        {
                            if (Value.Name.Equals("1")) { ViewBag.CiNames = STREAM.GetCINames().ToArray(); }
                            if (Value.Name.Equals("2")) { ViewBag.Department = list20; }
                            ViewBag.Names = STREAM.GetLocNames().ToArray();
                            ViewBag.List = res[0];//.ToList();
                            WorTabs tabs = new WorTabs();
                            tabs.ListForms = GetFormNames();
                            return View(tabs);
                        }
                        else
                        {
                            ViewBag.IsEmpty = isEmpty = false;
                            ViewBag.ErrorMsg = errorMsg = "This date range doesn't contain any records.";
                            WorTabs tabs = new WorTabs();
                            tabs.ListForms = GetFormNames();
                            return View(tabs);
                        }
                    }
                }
                #endregion

                #region For Inserted:
                else if (btnName.Equals("-insert"))
                {
                    checkView = "insert";
                    ViewBag.Check = checkView;
                    int id = int.Parse(Value.Name);
                    return RedirectToAction($"../Home/GoToSelectForm/{id}");
                }
                #endregion

                #region For Export to .csv file (with and without Range):
                else if (btnName.Equals("-export"))
                {
                    new TablesContainer().ResetAllTabls();
                    if (w_without.Equals(""))
                    {
                        ViewBag.ErrorMsg = "Radio button and date range are NOT selected";
                        WorTabs tabs = new WorTabs();
                        tabs.ListForms = GetFormNames();
                        return View(tabs);
                    }
                    else
                    {
                        ActionResult resEx = null;
                        if (/*start != DateTime.MinValue && end != DateTime.MinValue && */range == Range.With)
                            resEx = Export(Value.Name, Range.With, start, end);
                        else if (range == Range.Without)
                            resEx = Export(Value.Name, Range.Without);
                        if (resEx == null)
                        {
                            ViewBag.ErrorMsg = "There are no records in this form.";
                            WorTabs tabs = new WorTabs();
                            tabs.ListForms = GetFormNames();
                            return View(tabs);
                        }
                        return RedirectToAction("../Home/ExportToCSV");
                    }
                }
                #endregion

                #region For Summary (with and without Range):
                else if (btnName.Equals("-summary"))
                {
                    checkView = "summary";
                    ViewBag.Check = checkView;
                    int id = num_tbl = int.Parse(Value.Name);
                    var tbl_list = FillOutTableById(id).ToArray().ToList();
                    if (tbl_list.Count() == 0)
                    {
                        ViewBag.IsEmpty = isEmpty = false;
                        ViewBag.ErrorMsg = $"This form {GetModelNameByNum(id)} has no records.";
                        WorTabs tabs = new WorTabs();
                        tabs.ListForms = GetFormNames();
                        return View(tabs);
                    }
                    Type type = tbl_list[0].GetType();
                    string entity = type.Name;
                    if (!entity.Equals(string.Empty))
                    {
                        ViewBag.TableName = entity;
                    }
                    if (range == Range.With)
                    {
                        if (start == DateTime.MinValue && end == DateTime.MinValue)
                        {
                            ViewBag.ErrorMsg = "No date range was chosen. Please choose the date range.";
                            WorTabs tabs = new WorTabs();
                            tabs.ListForms = GetFormNames();
                            return View(tabs);
                        }
                        else if (start == DateTime.MinValue || end == DateTime.MinValue)
                        {
                            if (start == DateTime.MinValue)
                            {
                                ViewBag.ErrorMsg = "No date 'Start' was chosen. Please choose the 'Start' date.";
                                WorTabs tabs = new WorTabs();
                                tabs.ListForms = GetFormNames();
                                return View(tabs);
                            }
                            else
                            {
                                ViewBag.ErrorMsg = "No date 'End' was chosen. Please choose the 'End' date.";
                                WorTabs tabs = new WorTabs();
                                tabs.ListForms = GetFormNames();
                                return View(tabs);
                            }
                        }
                    }
                    #region Switch to show all object's Statistic:
                    string[] regions = default;
                    switch (entity)
                    {
                        #region Critical_Incident:
                        case "Critical_Incidents_DTO":
                            IncidentSummaryLogic.ClearAllStatic();
                            IEnumerable<Critical_Incidents_DTO> list = default;
                            if (CurrentLocalUser.Role == Role.User.ToString())
                            {
                                if (range == Range.With)
                                    list = TablesContainer.list1 =
                                    (from ent in db.ReadIncidents() where ent.Date >= start && ent.Date <= end select ent).Where(i => i.Location == localLocation).ToList();
                                else if (range == Range.Without)
                                    list = TablesContainer.list1 = db.ReadIncidents().Where(l => l.Location == localLocation).ToList();
                            }
                            else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list1 =
                                    (from ent in TablesContainer.list1 where ent.Date >= start && ent.Date <= end select ent).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list1 = TablesContainer.list1;
                            }
                            else // if roleAdmin or SuperUser then it'll be executed:
                            {
                                if (range == Range.With)
                                    list = TablesContainer.list1 = (from ent in db.ReadIncidents() where ent.Date >= start && ent.Date <= end select ent).ToList();
                                else if (range == Range.Without)
                                    list = TablesContainer.list1 = db.ReadIncidents().ToList();
                            }
                            if (!IncidentSummaryLogic.checkRepead)
                            {
                                IncidentSummaryLogic.CheckLocation();
                                IncidentSummaryLogic.GetDistinctList(db.ReadHomes().ToList());
                                IncidentSummaryLogic.FillOutLists();
                                IncidentSummaryLogic.AddCntLoc();

                                if (TablesContainer.list1.Count() == 0)
                                {
                                    { ViewBag.ObjName = entity; }
                                    ViewBag.ErrorMsg = errorMsg = "This date range doesn't contain any records.";
                                    WorTabs tabs = new WorTabs();
                                    tabs.ListForms = GetFormNames();
                                    return View(tabs);
                                }
                                IncidentSummaryLogic.AllStatIncident();
                            }

                            #region Create ViewBAgs:
                            { ViewBag.TotalSummary = IncidentSummaryLogic.allSummary1; }

                            b = true;
                            if (IncidentSummaryLogic.foundSummary1.Count == 0) { b = false; ViewBag.EmptLocation = b; }
                            { ViewBag.Count = TablesContainer.list1.Count; }
                            { ViewBag.GN_Found = IncidentSummaryLogic.foundSummary1; }
                            { ViewBag.Entity = "Critical_Incidents"; }
                            if (IncidentSummaryLogic.locList.Count != 0) isEmpty = true;
                            { ViewBag.Check1 = isEmpty; }
                            { ViewBag.Locations = IncidentSummaryLogic.locList; }
                            { ViewBag.LocInfo = WhoAdmin(); }
                            #endregion
                            break;
                        #endregion

                        #region Complaints:
                        case "Complaint_DTO":
                            ComplaintSummaryLogic.ClearAllStatic();
                            if (CurrentLocalUser.Role == Role.SuperUser.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list2 = (from ent in db.ReadComplaints() where ent.DateReceived >= start && ent.DateReceived <= end select ent).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list2 = db.ReadComplaints().ToList();
                            }
                            else if (CurrentLocalUser.Role == Role.User.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list2 =
                                    (from ent in db.ReadComplaints() where ent.DateReceived >= start && ent.DateReceived <= end select ent).Where(i => i.Location == localLocation).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list2 = db.ReadComplaints().Where(l => l.Location == localLocation).ToList();
                            }
                            else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list2 =
                                    (from ent in TablesContainer.list2 where ent.DateReceived >= start && ent.DateReceived <= end select ent).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list2 = TablesContainer.list2;
                            }
                            else // admin
                            {
                                if (range == Range.With)
                                    TablesContainer.list2 =
                                    (from ent in db.ReadComplaints() where ent.DateReceived >= start && ent.DateReceived <= end select ent).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list2 = db.ReadComplaints().ToList();
                            }
                            if (!ComplaintSummaryLogic.checkRepead)
                            {
                                ComplaintSummaryLogic.CheckLocation();
                                ComplaintSummaryLogic.GetDistinctList(AppSettings.homes.ToList());
                                ComplaintSummaryLogic.FillOutLists();
                                ComplaintSummaryLogic.AddCntLoc();

                                if (TablesContainer.list2.Count() == 0)
                                {
                                    { ViewBag.ObjName = entity; }
                                    ViewBag.ErrorMsg = errorMsg = $"This date range doesn't contain any records.";// Logger.Write($"This date range doesn't contain any records.");
                                    WorTabs tabs = new WorTabs();
                                    tabs.ListForms = GetFormNames();
                                    return View(tabs);
                                }
                                ComplaintSummaryLogic.AllStatIncident();
                            }

                            #region Create ViewBAgs:
                            { ViewBag.TotalSummary = ComplaintSummaryLogic.allSummary; }

                            b = true;
                            if (ComplaintSummaryLogic.foundSummary.Count == 0) { b = false; ViewBag.EmptLocation = b; }
                            { ViewBag.Count = TablesContainer.list2.Count; }
                            { ViewBag.GN_Found = ComplaintSummaryLogic.foundSummary; }
                            { ViewBag.Entity = "Complaints"; }
                            if (ComplaintSummaryLogic.locList.Count != 0) isEmpty = true;
                            { ViewBag.Check1 = isEmpty; }
                            { ViewBag.Locations = ComplaintSummaryLogic.locList; }
                            { ViewBag.LocInfo = WhoAdmin(); }
                            #endregion
                            break;
                        #endregion

                        #region Good_News:
                        case "Good_News_DTO":
                            GoodNewsSummaryLogic.ClearAllStatic();
                            if (CurrentLocalUser.Role == Role.SuperUser.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list3 = (from ent in db.ReadNews() where ent.DateNews >= start && ent.DateNews <= end select ent).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list3 = db.ReadNews().ToList();
                            }
                            else if (CurrentLocalUser.Role == Role.User.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list3 =
                                        (from ent in db.ReadNews() where ent.DateNews >= start && ent.DateNews <= end select ent).Where(i => i.Location == localLocation).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list3 = db.ReadNews().Where(i => i.Location == localLocation).ToList();
                            }
                            else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list3 =
                                   TablesContainer.list3.Where(ent => ent.DateNews >= start && ent.DateNews <= end).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list3 = TablesContainer.list3;
                            }
                            else // admin
                            {
                                if (range == Range.With)
                                    TablesContainer.list3 =
                                        (from ent in db.ReadNews() where ent.DateNews >= start && ent.DateNews <= end select ent).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list3 = db.ReadNews().ToList();
                            }
                            if (!GoodNewsSummaryLogic.checkRepead)
                            {
                                GoodNewsSummaryLogic.CheckLocation();
                                GoodNewsSummaryLogic.GetDistinctList(AppSettings.homes.ToList());
                                GoodNewsSummaryLogic.FillOutLists();
                                GoodNewsSummaryLogic.AddCntLoc();

                                if (TablesContainer.list3.Count() == 0)
                                {
                                    { ViewBag.ObjName = entity; }
                                    ViewBag.ErrorMsg = errorMsg = $"This date range doesn't contain any records.";
                                    WorTabs tabs = new WorTabs();
                                    tabs.ListForms = GetFormNames();
                                    return View(tabs);
                                }
                                GoodNewsSummaryLogic.AllStatIncident();
                            }

                            #region Create ViewBAgs:
                            { ViewBag.TotalSummary = GoodNewsSummaryLogic.allSummary; }

                            b = true;
                            if (GoodNewsSummaryLogic.foundSummary.Count == 0) { b = false; ViewBag.EmptLocation = b; }
                            { ViewBag.Count = TablesContainer.list3.Count; }
                            { ViewBag.GN_Found = GoodNewsSummaryLogic.foundSummary; }
                            { ViewBag.Entity = "Good_News"; }
                            if (GoodNewsSummaryLogic.locList.Count != 0) isEmpty = true;
                            { ViewBag.Check1 = isEmpty; }
                            { ViewBag.Locations = GoodNewsSummaryLogic.locList; }
                            { ViewBag.LocInfo = WhoAdmin(); }
                            #endregion
                            break;
                        #endregion

                        #region Emergency Prep:
                        case "Emergency_Prep_DTO":
                            return RedirectToAction("../AdminLTE/EMERGENCY_CODE_TESTING");
                        //EmergencySummaryLogic.ClearAllStatic();
                        //if (CurrentLocalUser.Role == Role.SuperUser)
                        //    TablesContainer.list4 = db.ReadEmergency().ToList();
                        //else if (CurrentLocalUser.Role == Role.User.ToString())
                        //    TablesContainer.list4 = db.ReadEmergency().Where(l => l.Location == userLocation).ToList();
                        //else
                        //{
                        //    TablesContainer.list4 = db.ReadEmergency().ToList();
                        //}

                        //if (!EmergencySummaryLogic.checkRepead)
                        //{
                        //    EmergencySummaryLogic.CheckLocation();
                        //    EmergencySummaryLogic.GetDistinctList(AppSettings.homes.ToList());
                        //    EmergencySummaryLogic.FillOutLists();
                        //    EmergencySummaryLogic.AddCntLoc();

                        //    if (TablesContainer.list4.Count() == 0)
                        //    {
                        //        { ViewBag.ObjName = entity; }
                        //        ViewBag.ErrorMsg = errorMsg = Logger.Write($"This date range doesn't contain any records.");
                        //        WorTabs tabs = new WorTabs();
                        //        tabs.ListForms = GetFormNames();
                        //        return View(tabs);
                        //    }
                        //    EmergencySummaryLogic.AllStatIncident();
                        //}

                        //#region Create ViewBAgs:
                        //{ ViewBag.TotalSummary = EmergencySummaryLogic.allSummary; }

                        //b = true;
                        //if (EmergencySummaryLogic.foundSummary.Count == 0) { b = false; ViewBag.EmptLocation = b; }
                        //{ ViewBag.Count = TablesContainer.list4.Count; }
                        //{ ViewBag.GN_Found = EmergencySummaryLogic.foundSummary; }
                        //{ ViewBag.Entity = "Good_News"; }
                        //if (EmergencySummaryLogic.locList.Count != 0) isEmpty = true;
                        //{ ViewBag.Check1 = isEmpty; }
                        //{ ViewBag.Locations = EmergencySummaryLogic.locList; }
                        //{ ViewBag.LocInfo = WhoAdmin(); }
                        #endregion

                        #region Community_Risks
                        case "Community_Risks_DTO":
                            CommRiskSummaryLogic.ClearAllStatic();
                            if (CurrentLocalUser.Role == Role.SuperUser.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list5 = (from ent in db.ReadRisks() where ent.Date >= start && ent.Date <= end select ent).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list5 = db.ReadRisks().ToList();
                            }
                            else if (CurrentLocalUser.Role == Role.User.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list5 = (from ent in db.ReadRisks() where ent.Date >= start && ent.Date <= end select ent).Where(l => l.Location == localLocation).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list5 = (from ent in db.ReadRisks() where ent.Location == localLocation select ent).ToList();
                            }
                            else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list5 =
                                   TablesContainer.list5.Where(ent => ent.Date >= start && ent.Date <= end).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list5 = TablesContainer.list5;
                            }
                            else
                            {
                                if (range == Range.With)
                                    TablesContainer.list5 = (from ent in db.ReadRisks() where ent.Date >= start && ent.Date <= end select ent).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list5 = db.ReadRisks().ToList();
                            }
                            if (!CommRiskSummaryLogic.checkRepead)
                            {
                                CommRiskSummaryLogic.CheckLocation();
                                CommRiskSummaryLogic.GetDistinctList(AppSettings.homes.ToList());
                                CommRiskSummaryLogic.FillOutLists();
                                CommRiskSummaryLogic.AddCntLoc();

                                if (TablesContainer.list5.Count() == 0)
                                {
                                    { ViewBag.ObjName = entity; }
                                    ViewBag.ErrorMsg = errorMsg = $"This date range doesn't contain any records.";
                                    WorTabs tabs = new WorTabs();
                                    tabs.ListForms = GetFormNames();
                                    return View(tabs);
                                }
                                CommRiskSummaryLogic.AllStatIncident();
                            }

                            #region Create ViewBAgs:
                            { ViewBag.TotalSummary = CommRiskSummaryLogic.allSummary; }

                            b = true;
                            if (CommRiskSummaryLogic.foundSummary.Count == 0) { b = false; ViewBag.EmptLocation = b; }
                            { ViewBag.Count = TablesContainer.list5.Count; }
                            { ViewBag.GN_Found = CommRiskSummaryLogic.foundSummary; }
                            { ViewBag.Entity = "Community_Risks"; }
                            if (CommRiskSummaryLogic.locList.Count != 0) isEmpty = true;
                            { ViewBag.Check1 = isEmpty; }
                            { ViewBag.Locations = CommRiskSummaryLogic.locList; }
                            { ViewBag.LocInfo = WhoAdmin(); }
                            #endregion
                            break;
                        #endregion

                        #region Privacy Breaches:
                        case "Privacy_Breaches_DTO":
                            BreachesSummuryLogic.ClearAllStatic();
                            if (CurrentLocalUser.Role == Role.SuperUser.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list7 = (from ent in db.ReadBreaches() where ent.Date_Breach_Occurred >= start && ent.Date_Breach_Occurred <= end select ent).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list7 = db.ReadBreaches().ToList();
                            }
                            else if (CurrentLocalUser.Role == Role.User.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list7 = (from ent in db.ReadBreaches() where ent.Date_Breach_Occurred >= start && ent.Date_Breach_Occurred <= end select ent).Where(l => l.Location == localLocation).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list7 = db.ReadBreaches().Where(l => l.Location == localLocation).ToList();
                            }
                            else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list7 = TablesContainer.list7.Where(ent => ent.Date_Breach_Occurred >= start && ent.Date_Breach_Occurred <= end).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list7 = TablesContainer.list7;
                            }
                            else
                            {
                                if (range == Range.With)
                                    TablesContainer.list7 = (from ent in db.ReadBreaches() where ent.Date_Breach_Occurred >= start && ent.Date_Breach_Occurred <= end select ent).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list7 = db.ReadBreaches().ToList();
                            }
                            if (!BreachesSummuryLogic.checkRepead)
                            {
                                BreachesSummuryLogic.CheckLocation();
                                BreachesSummuryLogic.GetDistinctList(AppSettings.homes.ToList());
                                BreachesSummuryLogic.FillOutLists();
                                BreachesSummuryLogic.AddCntLoc();

                                if (TablesContainer.list7.Count() == 0)
                                {
                                    { ViewBag.ObjName = entity; }
                                    ViewBag.ErrorMsg = errorMsg = $"This date range doesn't contain any records.";
                                    WorTabs tabs = new WorTabs();
                                    tabs.ListForms = GetFormNames();
                                    return View(tabs);
                                }
                                BreachesSummuryLogic.AllStatIncident();
                            }

                            #region Create ViewBAgs:
                            { ViewBag.TotalSummary = BreachesSummuryLogic.allSummary; }

                            b = true;
                            if (BreachesSummuryLogic.foundSummary.Count == 0) { b = false; ViewBag.EmptLocation = b; }
                            { ViewBag.Count = TablesContainer.list7.Count; }
                            { ViewBag.GN_Found = BreachesSummuryLogic.foundSummary; }
                            { ViewBag.Entity = "Privacy_Breaches"; }
                            if (BreachesSummuryLogic.locList.Count != 0) isEmpty = true;
                            { ViewBag.Check1 = isEmpty; }
                            { ViewBag.Locations = BreachesSummuryLogic.locList; }
                            { ViewBag.LocInfo = WhoAdmin(); }
                            #endregion
                            break;
                        #endregion

                        #region Privacy Complaints:
                        case "Privacy_Complaints_DTO":
                            PComplaintSummaryLogic.ClearAllStatic();
                            if (CurrentLocalUser.Role == Role.SuperUser.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list8 = (from ent in db.ReadPComplaints() where ent.Date_Complain_Received >= start && ent.Date_Complain_Received <= end select ent).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list8 = db.ReadPComplaints().ToList();
                            }
                            else if (CurrentLocalUser.Role == Role.User.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list8 = (from ent in db.ReadPComplaints() where ent.Date_Complain_Received >= start && ent.Date_Complain_Received <= end select ent).Where(l => l.Location == localLocation).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list8 = db.ReadPComplaints().Where(l => l.Location == localLocation).ToList();
                            }
                            else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list8 =
                                   TablesContainer.list8.Where(ent => ent.Date_Complain_Received >= start && ent.Date_Complain_Received <= end).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list8 = TablesContainer.list8;
                            }
                            else
                            {
                                if (range == Range.With)
                                    TablesContainer.list8 = (from ent in db.ReadPComplaints() where ent.Date_Complain_Received >= start && ent.Date_Complain_Received <= end select ent).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list8 = db.ReadPComplaints().ToList();
                            }
                            if (!PComplaintSummaryLogic.checkRepead)
                            {
                                PComplaintSummaryLogic.CheckLocation();
                                PComplaintSummaryLogic.GetDistinctList(AppSettings.homes.ToList());
                                PComplaintSummaryLogic.FillOutLists();
                                PComplaintSummaryLogic.AddCntLoc();

                                if (TablesContainer.list8.Count() == 0)
                                {
                                    { ViewBag.ObjName = entity; }
                                    ViewBag.ErrorMsg = errorMsg = $"This date range doesn't contain any records.";
                                    WorTabs tabs = new WorTabs();
                                    tabs.ListForms = GetFormNames();
                                    return View(tabs);
                                }
                                PComplaintSummaryLogic.AllStatIncident();
                            }

                            #region Create ViewBAgs:
                            { ViewBag.TotalSummary = PComplaintSummaryLogic.allSummary; }

                            b = true;
                            if (PComplaintSummaryLogic.foundSummary.Count == 0) { b = false; ViewBag.EmptLocation = b; }
                            { ViewBag.Count = TablesContainer.list8.Count; }
                            { ViewBag.GN_Found = PComplaintSummaryLogic.foundSummary; }
                            { ViewBag.Entity = "Privacy_Complaints"; }
                            if (PComplaintSummaryLogic.locList.Count != 0) isEmpty = true;
                            { ViewBag.Check1 = isEmpty; }
                            { ViewBag.Locations = PComplaintSummaryLogic.locList; }
                            { ViewBag.LocInfo = WhoAdmin(); }
                            #endregion
                            break;
                        #endregion

                        #region Educations:
                        case "Education_DTO":
                            EducationSummaryLogic.ClearAllStatic();
                            if (CurrentLocalUser.Role == Role.SuperUser.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list9 = (from ent in db.ReadEducation() where ent.DateStart >= start && ent.DateStart <= end select ent).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list9 = db.ReadEducation().ToList();
                            }
                            else if (CurrentLocalUser.Role == Role.User.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list9 = (from ent in db.ReadEducation() where ent.DateStart >= start && ent.DateStart <= end select ent).Where(l => l.Location == localLocation).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list9 = db.ReadEducation().Where(l => l.Location == localLocation).ToList();
                            }
                            else
                            {
                                if (range == Range.With)
                                    TablesContainer.list9 = (from ent in db.ReadEducation() where ent.DateStart >= start && ent.DateStart <= end select ent).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list9 = db.ReadEducation().ToList();
                            }
                            if (!EducationSummaryLogic.checkRepead)
                            {
                                EducationSummaryLogic.CheckLocation();
                                EducationSummaryLogic.GetDistinctList(AppSettings.homes.ToList());
                                EducationSummaryLogic.FillOutLists();
                                EducationSummaryLogic.AddCntLoc();

                                if (TablesContainer.list9.Count() == 0)
                                {
                                    { ViewBag.ObjName = entity; }
                                    ViewBag.ErrorMsg = errorMsg = $"This date range doesn't contain any records.";
                                    WorTabs tabs = new WorTabs();
                                    tabs.ListForms = GetFormNames();
                                    return View(tabs);
                                }
                                EducationSummaryLogic.AllStatIncident();
                            }

                            #region Create ViewBAgs:
                            { ViewBag.TotalSummary = EducationSummaryLogic.allSummary; }

                            b = true;
                            if (EducationSummaryLogic.foundSummary.Count == 0) { b = false; ViewBag.EmptLocation = b; }
                            { ViewBag.Count = TablesContainer.list9.Count; }
                            { ViewBag.GN_Found = EducationSummaryLogic.foundSummary; }
                            { ViewBag.Entity = "Educations"; }
                            if (EducationSummaryLogic.locList.Count != 0) isEmpty = true;
                            { ViewBag.Check1 = isEmpty; }
                            { ViewBag.Locations = EducationSummaryLogic.locList; }
                            { ViewBag.LocInfo = WhoAdmin(); }
                            #endregion
                            break;
                        #endregion

                        #region Labour Relations:
                        case "Labour_Relations_DTO":
                            LabourRelationSummaryLogic.ClearAllStatic();
                            if (CurrentLocalUser.Role == Role.SuperUser.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list10 = (from ent in db.ReadRelations() where ent.Date >= start && ent.Date <= end select ent).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list10 = db.ReadRelations().ToList();
                            }
                            else if (CurrentLocalUser.Role == Role.User.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list10 = (from ent in db.ReadRelations() where ent.Date >= start && ent.Date <= end select ent).Where(l => l.Location == localLocation).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list10 = db.ReadRelations().Where(l => l.Location == localLocation).ToList();
                            }
                            else
                            {
                                if (range == Range.With)
                                    TablesContainer.list10 = (from ent in db.ReadRelations() where ent.Date >= start && ent.Date <= end select ent).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list10 = db.ReadRelations().ToList();
                            }
                            if (!LabourRelationSummaryLogic.checkRepead)
                            {
                                LabourRelationSummaryLogic.CheckLocation();
                                LabourRelationSummaryLogic.GetDistinctList(AppSettings.homes.ToList());
                                LabourRelationSummaryLogic.FillOutLists();
                                LabourRelationSummaryLogic.AddCntLoc();

                                if (TablesContainer.list10.Count() == 0)
                                {
                                    { ViewBag.ObjName = entity; }
                                    ViewBag.ErrorMsg = errorMsg = Logger.Write($"Please select a date range or the form has no records.");
                                    WorTabs tabs = new WorTabs();
                                    tabs.ListForms = GetFormNames();
                                    return View(tabs);
                                }
                                LabourRelationSummaryLogic.AllStatIncident();
                            }

                            #region Create ViewBAgs:
                            { ViewBag.TotalSummary = LabourRelationSummaryLogic.allSummary; }

                            b = true;
                            if (LabourRelationSummaryLogic.foundSummary.Count == 0) { b = false; ViewBag.EmptLocation = b; }
                            { ViewBag.Count = TablesContainer.list10.Count; }
                            { ViewBag.GN_Found = LabourRelationSummaryLogic.foundSummary; }
                            { ViewBag.Entity = "Labour_Relations"; }
                            if (LabourRelationSummaryLogic.locList.Count != 0) isEmpty = true;
                            { ViewBag.Check1 = isEmpty; }
                            { ViewBag.Locations = LabourRelationSummaryLogic.locList; }
                            { ViewBag.LocInfo = WhoAdmin(); }
                            #endregion
                            break;
                        #endregion

                        #region Immunizations:
                        case "Immunization_DTO":
                            LabourRelationSummaryLogic.ClearAllStatic();
                            if (CurrentLocalUser.Role == Role.SuperUser.ToString())
                                TablesContainer.list11 = db.ReadImmunizations().ToList();
                            else if (CurrentLocalUser.Role == Role.User.ToString())
                                TablesContainer.list11 = db.ReadImmunizations().Where(l => l.Location == localLocation).ToList();

                            if (!ImmunizationSummaryLogic.checkRepead)
                            {
                                ImmunizationSummaryLogic.CheckLocation();
                                ImmunizationSummaryLogic.GetDistinctList(AppSettings.homes.ToList());
                                ImmunizationSummaryLogic.FillOutLists();
                                ImmunizationSummaryLogic.AddCntLoc();

                                if (TablesContainer.list11.Count() == 0)
                                {
                                    { ViewBag.ObjName = entity; }
                                    ViewBag.ErrorMsg = errorMsg = Logger.Write($"This date range doesn't contain any records.");
                                    WorTabs tabs = new WorTabs();
                                    tabs.ListForms = GetFormNames();
                                    return View(tabs);
                                }
                                ImmunizationSummaryLogic.AllStatIncident();
                            }

                            #region Create ViewBAgs:
                            { ViewBag.TotalSummary = ImmunizationSummaryLogic.allSummary; }

                            b = true;
                            if (ImmunizationSummaryLogic.foundSummary.Count == 0) { b = false; ViewBag.EmptLocation = b; }
                            { ViewBag.Count = TablesContainer.list11.Count; }
                            { ViewBag.GN_Found = ImmunizationSummaryLogic.foundSummary; }
                            { ViewBag.Entity = "Immunization"; }
                            if (ImmunizationSummaryLogic.locList.Count != 0) isEmpty = true;
                            { ViewBag.Check1 = isEmpty; }
                            { ViewBag.Locations = ImmunizationSummaryLogic.locList; }
                            { ViewBag.LocInfo = WhoAdmin(); }
                            #endregion
                            break;
                        #endregion

                        #region Outbreaks:
                        case "Outbreaks_DTO":
                            OutbreaksSummaryLogic.ClearAllStatic();
                            if (CurrentLocalUser.Role == Role.SuperUser.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list12 = (from d in db.ReadOutbreaks() where d.Date_Declared >= start && d.Date_Declared <= end select d).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list12 = db.ReadOutbreaks().ToList();
                            }
                            else if (CurrentLocalUser.Role == Role.User.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list12 = (from d in db.ReadOutbreaks() where d.Date_Declared >= start && d.Date_Declared <= end select d)
                                       .Where(i => i.Location == localLocation).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list12 = db.ReadOutbreaks().Where(l => l.Location == localLocation).ToList();
                            }
                            else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list12 =
                                 TablesContainer.list12.Where(ent => ent.Date_Declared >= start && ent.Date_Declared <= end).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list12 = TablesContainer.list12;
                            }
                            else
                            {
                                if (range == Range.With)
                                    TablesContainer.list12 = (from d in db.ReadOutbreaks() where d.Date_Declared >= start && d.Date_Declared <= end select d).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list12 = db.ReadOutbreaks().ToList();
                            }
                            if (!OutbreaksSummaryLogic.checkRepead)
                            {
                                OutbreaksSummaryLogic.CheckLocation();
                                OutbreaksSummaryLogic.GetDistinctList(AppSettings.homes.ToList());
                                OutbreaksSummaryLogic.FillOutLists();
                                OutbreaksSummaryLogic.AddCntLoc();

                                if (TablesContainer.list12.Count() == 0)
                                {
                                    { ViewBag.ObjName = entity; }
                                    ViewBag.ErrorMsg = errorMsg = $"This date range doesn't contain any records.";
                                    WorTabs tabs = new WorTabs();
                                    tabs.ListForms = GetFormNames();
                                    return View(tabs);
                                }
                                OutbreaksSummaryLogic.AllStatIncident();
                            }

                            #region Create ViewBAgs:
                            { ViewBag.TotalSummary = OutbreaksSummaryLogic.allSummary; }

                            b = true;
                            if (OutbreaksSummaryLogic.foundSummary.Count == 0) { b = false; ViewBag.EmptLocation = b; }
                            { ViewBag.Count = TablesContainer.list12.Count; }
                            { ViewBag.GN_Found = OutbreaksSummaryLogic.foundSummary; }
                            { ViewBag.Entity = "Outbreaks"; }
                            if (OutbreaksSummaryLogic.locList.Count != 0) isEmpty = true;
                            { ViewBag.Check1 = isEmpty; }
                            { ViewBag.Locations = OutbreaksSummaryLogic.locList; }
                            { ViewBag.LocInfo = WhoAdmin(); }
                            #endregion
                            break;
                        #endregion

                        #region WSiB:
                        case "WSIB_DTO":
                            WSIBSummaryLogic.ClearAllStatic();
                            if (CurrentLocalUser.Role == Role.SuperUser.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list13 = (from d in db.ReadWSiBs() where d.Date_Accident >= start && d.Date_Accident <= end select d).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list13 = db.ReadWSiBs().ToList();
                            }
                            else if (CurrentLocalUser.Role == Role.User.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list13 = (from d in db.ReadWSiBs() where d.Date_Accident >= start && d.Date_Accident <= end select d).
                                    Where(l => l.Location == localLocation).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list13 = db.ReadWSiBs().Where(l => l.Location == localLocation).ToList();
                            }
                            else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list13 =
                                  TablesContainer.list13.Where(ent => ent.Date_Accident >= start && ent.Date_Accident <= end).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list13 = TablesContainer.list13;
                            }
                            else
                            {
                                if (range == Range.With)
                                    TablesContainer.list13 = (from d in db.ReadWSiBs() where d.Date_Accident >= start && d.Date_Accident <= end select d).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list13 = db.ReadWSiBs().ToList();
                            }
                            if (!WSIBSummaryLogic.checkRepead)
                            {
                                WSIBSummaryLogic.CheckLocation();
                                WSIBSummaryLogic.GetDistinctList(AppSettings.homes.ToList());
                                WSIBSummaryLogic.FillOutLists();
                                WSIBSummaryLogic.AddCntLoc();

                                if (TablesContainer.list13.Count() == 0)
                                {
                                    { ViewBag.ObjName = entity; }
                                    ViewBag.ErrorMsg = errorMsg = $"This date range doesn't contain any records.";
                                    WorTabs tabs = new WorTabs();
                                    tabs.ListForms = GetFormNames();
                                    return View(tabs);
                                }
                                WSIBSummaryLogic.AllStatIncident();
                            }

                            #region Create ViewBAgs:
                            { ViewBag.TotalSummary = WSIBSummaryLogic.allSummary; }

                            b = true;
                            if (WSIBSummaryLogic.foundSummary.Count == 0) { b = false; ViewBag.EmptLocation = b; }
                            { ViewBag.Count = TablesContainer.list13.Count; }
                            { ViewBag.GN_Found = WSIBSummaryLogic.foundSummary; }
                            { ViewBag.Entity = "WSIB"; }
                            if (WSIBSummaryLogic.locList.Count != 0) isEmpty = true;
                            { ViewBag.Check1 = isEmpty; }
                            { ViewBag.Locations = WSIBSummaryLogic.locList; }
                            { ViewBag.LocInfo = WhoAdmin(); }
                            #endregion
                            break;
                        #endregion

                        #region Not WSIB:
                        case "Not_WSIBs_DTO":
                            NotWSIBSummaryLogic.ClearAllStatic();
                            if (CurrentLocalUser.Role == Role.SuperUser.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list14 = (from d in db.ReadNotWSIBs() where d.Date_of_Incident >= start && d.Date_of_Incident <= end select d).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list14 = db.ReadNotWSIBs().ToList();
                            }
                            else if (CurrentLocalUser.Role == Role.User.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list14 = (from d in db.ReadNotWSIBs() where d.Date_of_Incident >= start && d.Date_of_Incident <= end select d).
                                    Where(l => l.Location == localLocation).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list14 = db.ReadNotWSIBs().Where(l => l.Location == localLocation).ToList();
                            }
                            else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list14 =
                                    TablesContainer.list14.Where(ent => ent.Date_of_Incident >= start && ent.Date_of_Incident <= end).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list14 = TablesContainer.list14;
                            }
                            else
                            {
                                if (range == Range.With)
                                    TablesContainer.list14 = (from d in db.ReadNotWSIBs() where d.Date_of_Incident >= start && d.Date_of_Incident <= end select d).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list14 = db.ReadNotWSIBs().ToList();
                            }
                            if (!NotWSIBSummaryLogic.checkRepead)
                            {
                                NotWSIBSummaryLogic.CheckLocation();
                                NotWSIBSummaryLogic.GetDistinctList(AppSettings.homes.ToList());
                                NotWSIBSummaryLogic.FillOutLists();
                                NotWSIBSummaryLogic.AddCntLoc();

                                if (TablesContainer.list14.Count() == 0)
                                {
                                    { ViewBag.ObjName = entity; }
                                    ViewBag.ErrorMsg = errorMsg = $"This date range doesn't contain any records.";
                                    WorTabs tabs = new WorTabs();
                                    tabs.ListForms = GetFormNames();
                                    return View(tabs);
                                }
                                NotWSIBSummaryLogic.AllStatIncident();
                            }

                            #region Create ViewBAgs:
                            { ViewBag.TotalSummary = NotWSIBSummaryLogic.allSummary; }

                            b = true;
                            if (NotWSIBSummaryLogic.foundSummary.Count == 0) { b = false; ViewBag.EmptLocation = b; }
                            { ViewBag.Count = TablesContainer.list14.Count; }
                            { ViewBag.GN_Found = NotWSIBSummaryLogic.foundSummary; }
                            { ViewBag.Entity = "Not_WSIB"; }
                            if (NotWSIBSummaryLogic.locList.Count != 0) isEmpty = true;
                            { ViewBag.Check1 = isEmpty; }
                            { ViewBag.Locations = NotWSIBSummaryLogic.locList; }
                            { ViewBag.LocInfo = WhoAdmin(); }
                            #endregion
                            break;
                        #endregion

                        #region Licensing Inspection:
                        case "LicensingInspectionDTO":
                            LiceInspecSummaryLogic.ClearAllStatic();
                            if (CurrentLocalUser.Role == Role.SuperUser.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list15 =
                                        (from d in db.ReadLiceInspect()
                                         where d.Date >= start
                               && d.Date <= end
                                         select d).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list15 = db.ReadLiceInspect().ToList();
                            }
                            else if (CurrentLocalUser.Role == Role.User.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list15 = (from d in db.ReadLiceInspect() where d.Date >= start && d.Date <= end select d).
                                    Where(l => l.CareComName == localLocation).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list15 =
                                        db.ReadLiceInspect().Where(l => l.CareComName == localLocation).ToList();
                            }
                            else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list15 =
                                    TablesContainer.list15.Where(ent => ent.Date >= start && ent.Date <= end).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list15 = TablesContainer.list15;
                            }
                            else
                            {
                                if (range == Range.With)
                                    TablesContainer.list15 =
                                        (from d in db.ReadLiceInspect() where d.Date >= start && d.Date <= end select d).
                                    ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list15 =
                                        db.ReadLiceInspect().ToList();
                            }
                            if (!LiceInspecSummaryLogic.checkRepead)
                            {
                                LiceInspecSummaryLogic.CheckLocation();
                                LiceInspecSummaryLogic.GetDistinctList(AppSettings.homes.ToList());
                                LiceInspecSummaryLogic.FillOutLists();
                                LiceInspecSummaryLogic.AddCntLoc();

                                if (TablesContainer.list15.Count() == 0)
                                {
                                    { ViewBag.ObjName = entity; }
                                    ViewBag.ErrorMsg = errorMsg = $"This date range doesn't contain any records.";
                                    WorTabs tabs = new WorTabs();
                                    tabs.ListForms = GetFormNames();
                                    return View(tabs);
                                }
                                LiceInspecSummaryLogic.AllStatIncident();
                            }

                            #region Create ViewBAgs:
                            { ViewBag.TotalSummary = LiceInspecSummaryLogic.allSummary; }

                            b = true;
                            if (LiceInspecSummaryLogic.foundSummary.Count == 0) { b = false; ViewBag.EmptLocation = b; }
                            { ViewBag.Count = TablesContainer.list15.Count; }
                            { ViewBag.GN_Found = LiceInspecSummaryLogic.foundSummary; }
                            { ViewBag.Entity = "LicensingInspection"; }
                            if (LiceInspecSummaryLogic.locList.Count != 0) isEmpty = true;
                            { ViewBag.Check1 = isEmpty; }
                            { ViewBag.Locations = LiceInspecSummaryLogic.locList; }
                            { ViewBag.LocInfo = WhoAdmin(); }
                            #endregion
                            break;
                        #endregion

                        #region Assisted Living Inspection:
                        case "AssistedLivingInspectionDTO":
                            AssistLivInspectSummaryLogic.ClearAllStatic();
                            if (CurrentLocalUser.Role == Role.SuperUser.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list16 =
                                        (from d in db.ReadAssLivInspect()
                                         where d.Date >= start
                               && d.Date <= end
                                         select d).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list16 = db.ReadAssLivInspect().ToList();
                            }
                            else if (CurrentLocalUser.Role == Role.User.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list16 = (from d in db.ReadAssLivInspect() where d.Date >= start && d.Date <= end select d).
                                    Where(l => l.CareComName == localLocation).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list16 =
                                        db.ReadAssLivInspect().Where(l => l.CareComName == localLocation).ToList();
                            }
                            else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list16 =
                                      TablesContainer.list16.Where(ent => ent.Date >= start && ent.Date <= end).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list16 = TablesContainer.list16;
                            }
                            else
                            {
                                if (range == Range.With)
                                    TablesContainer.list16 =
                                        (from d in db.ReadAssLivInspect() where d.Date >= start && d.Date <= end select d).
                                    ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list16 =
                                        db.ReadAssLivInspect().ToList();
                            }
                            if (!AssistLivInspectSummaryLogic.checkRepead)
                            {
                                AssistLivInspectSummaryLogic.CheckLocation();
                                AssistLivInspectSummaryLogic.GetDistinctList(AppSettings.homes.ToList());
                                AssistLivInspectSummaryLogic.FillOutLists();
                                AssistLivInspectSummaryLogic.AddCntLoc();

                                if (TablesContainer.list16.Count() == 0)
                                {
                                    { ViewBag.ObjName = entity; }
                                    ViewBag.ErrorMsg = errorMsg = $"This date range doesn't contain any records.";
                                    WorTabs tabs = new WorTabs();
                                    tabs.ListForms = GetFormNames();
                                    return View(tabs);
                                }
                                AssistLivInspectSummaryLogic.AllStatIncident();
                            }

                            #region Create ViewBAgs:
                            { ViewBag.TotalSummary = AssistLivInspectSummaryLogic.allSummary; }

                            b = true;
                            if (AssistLivInspectSummaryLogic.foundSummary.Count == 0) { b = false; ViewBag.EmptLocation = b; }
                            { ViewBag.Count = TablesContainer.list16.Count; }
                            { ViewBag.GN_Found = AssistLivInspectSummaryLogic.foundSummary; }
                            { ViewBag.Entity = "AssistLivInspect"; }
                            if (AssistLivInspectSummaryLogic.locList.Count != 0) isEmpty = true;
                            { ViewBag.Check1 = isEmpty; }
                            { ViewBag.Locations = AssistLivInspectSummaryLogic.locList; }
                            { ViewBag.LocInfo = WhoAdmin(); }
                            #endregion
                            break;
                        #endregion

                        #region Workshop BC Inspection:
                        case "WorkshopBCInspection_DTO":
                            WorksaveBCSummaryLogic.ClearAllStatic();
                            if (CurrentLocalUser.Role == Role.SuperUser.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list17 =
                                        (from d in db.ReadWorkshopBCInspect()
                                         where d.Date >= start && d.Date <= end
                                         select d).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list17 = db.ReadWorkshopBCInspect().ToList();
                            }
                            else if (CurrentLocalUser.Role == Role.User.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list17 = (from d in db.ReadWorkshopBCInspect() where d.Date >= start && d.Date <= end select d).
                                    Where(l => l.CareComName == localLocation).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list17 =
                                        db.ReadWorkshopBCInspect().Where(l => l.CareComName == localLocation).ToList();
                            }
                            else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list17 =
                                    TablesContainer.list17.Where(ent => ent.Date >= start && ent.Date <= end).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list17 = TablesContainer.list17;
                            }
                            else
                            {
                                if (range == Range.With)
                                    TablesContainer.list17 =
                                        (from d in db.ReadWorkshopBCInspect() where d.Date >= start && d.Date <= end select d).
                                    ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list17 =
                                        db.ReadWorkshopBCInspect().ToList();
                            }
                            if (!WorksaveBCSummaryLogic.checkRepead)
                            {
                                WorksaveBCSummaryLogic.CheckLocation();
                                WorksaveBCSummaryLogic.GetDistinctList(AppSettings.homes.ToList());
                                WorksaveBCSummaryLogic.FillOutLists();
                                WorksaveBCSummaryLogic.AddCntLoc();

                                if (TablesContainer.list17.Count() == 0)
                                {
                                    { ViewBag.ObjName = entity; }
                                    ViewBag.ErrorMsg = errorMsg = $"This date range doesn't contain any records.";
                                    WorTabs tabs = new WorTabs();
                                    tabs.ListForms = GetFormNames();
                                    return View(tabs);
                                }
                                WorksaveBCSummaryLogic.AllStatIncident();
                            }

                            #region Create ViewBAgs:
                            { ViewBag.TotalSummary = WorksaveBCSummaryLogic.allSummary; }

                            b = true;
                            if (WorksaveBCSummaryLogic.foundSummary.Count == 0) { b = false; ViewBag.EmptLocation = b; }
                            { ViewBag.Count = TablesContainer.list17.Count; }
                            { ViewBag.GN_Found = WorksaveBCSummaryLogic.foundSummary; }
                            { ViewBag.Entity = "WorksaveBC"; }
                            if (WorksaveBCSummaryLogic.locList.Count != 0) isEmpty = true;
                            { ViewBag.Check1 = isEmpty; }
                            { ViewBag.Locations = WorksaveBCSummaryLogic.locList; }
                            { ViewBag.LocInfo = WhoAdmin(); }
                            #endregion
                            break;
                        #endregion

                        #region Quality Review:
                        case "QualityReview_DTO":
                            QltyRevSummaryLogic.ClearAllStatic();
                            if (CurrentLocalUser.Role == Role.SuperUser.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list18 =
                                        (from d in db.ReadQualityReviews()
                                         where d.Date >= start && d.Date <= end
                                         select d).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list18 = db.ReadQualityReviews().ToList();
                            }
                            else if (CurrentLocalUser.Role == Role.User.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list18 = (from d in db.ReadQualityReviews() where d.Date >= start && d.Date <= end select d).
                                    Where(l => l.CareComName == localLocation).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list18 =
                                        db.ReadQualityReviews().Where(l => l.CareComName == localLocation).ToList();
                            }
                            else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list18 = TablesContainer.list18.Where(ent => ent.Date >= start && ent.Date <= end).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list18 = TablesContainer.list18;
                            }
                            else
                            {
                                if (range == Range.With)
                                    TablesContainer.list18 =
                                        (from d in db.ReadQualityReviews() where d.Date >= start && d.Date <= end select d).
                                    ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list18 =
                                        db.ReadQualityReviews().ToList();
                            }
                            if (!QltyRevSummaryLogic.checkRepead)
                            {
                                QltyRevSummaryLogic.CheckLocation();
                                QltyRevSummaryLogic.GetDistinctList(AppSettings.homes.ToList());
                                QltyRevSummaryLogic.FillOutLists();
                                QltyRevSummaryLogic.AddCntLoc();

                                if (TablesContainer.list18.Count() == 0)
                                {
                                    { ViewBag.ObjName = entity; }
                                    ViewBag.ErrorMsg = errorMsg =
                                        $"This date range doesn't contain any records.";
                                    WorTabs tabs = new WorTabs();
                                    tabs.ListForms = GetFormNames();
                                    return View(tabs);
                                }
                                QltyRevSummaryLogic.AllStatIncident();
                            }

                            #region Create ViewBAgs:
                            { ViewBag.TotalSummary = QltyRevSummaryLogic.allSummary; }

                            b = true;
                            if (QltyRevSummaryLogic.foundSummary.Count == 0) { b = false; ViewBag.EmptLocation = b; }
                            { ViewBag.Count = TablesContainer.list18.Count; }
                            { ViewBag.GN_Found = QltyRevSummaryLogic.foundSummary; }
                            { ViewBag.Entity = "BC_LTCReport"; }
                            if (QltyRevSummaryLogic.locList.Count != 0) isEmpty = true;
                            { ViewBag.Check1 = isEmpty; }
                            { ViewBag.Locations = QltyRevSummaryLogic.locList; }
                            { ViewBag.LocInfo = WhoAdmin(); }
                            #endregion
                            break;
                        #endregion

                        #region BC LTC Reportable:
                        case "BC_LTC_Reportable_Incidents_DTO":
                            BC_LTCSummaryLogic.ClearAllStatic();
                            if (CurrentLocalUser.Role == Role.SuperUser.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list19 =
                                        (from d in db.ReadBC_LTC()
                                         where d.DateIncident >= start && d.DateIncident <= end
                                         select d).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list19 = db.ReadBC_LTC().ToList();
                            }
                            else if (CurrentLocalUser.Role == Role.User.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list19 =
                                        (from d in db.ReadBC_LTC() where d.DateIncident >= start && d.DateIncident <= end select d).
                                    Where(l => l.CareCommName == localLocation).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list19 =
                                        db.ReadBC_LTC().Where(l => l.CareCommName == localLocation).ToList();
                            }
                            else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list19 =
                                    TablesContainer.list19.Where(ent => ent.DateIncident >= start && ent.DateIncident <= end).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list19 = TablesContainer.list19;
                            }
                            else
                            {
                                if (range == Range.With)
                                    TablesContainer.list19 =
                                        (from d in db.ReadBC_LTC() where d.DateIncident >= start && d.DateIncident <= end select d).
                                    ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list19 =
                                        db.ReadBC_LTC().ToList();
                            }
                            if (!BC_LTCSummaryLogic.checkRepead)
                            {
                                BC_LTCSummaryLogic.CheckLocation();
                                BC_LTCSummaryLogic.GetDistinctList(AppSettings.homes.ToList());
                                BC_LTCSummaryLogic.FillOutLists();
                                BC_LTCSummaryLogic.AddCntLoc();

                                if (TablesContainer.list19.Count() == 0)
                                {
                                    { ViewBag.ObjName = entity; }
                                    ViewBag.ErrorMsg = errorMsg =
                                        $"This date range doesn't contain any records.";
                                    WorTabs tabs = new WorTabs();
                                    tabs.ListForms = GetFormNames();
                                    return View(tabs);
                                }
                                BC_LTCSummaryLogic.AllStatIncident();
                            }

                            #region Create ViewBAgs:
                            { ViewBag.TotalSummary = BC_LTCSummaryLogic.allSummary; }

                            b = true;
                            if (BC_LTCSummaryLogic.foundSummary.Count == 0) { b = false; ViewBag.EmptLocation = b; }
                            { ViewBag.Count = TablesContainer.list19.Count; }
                            { ViewBag.GN_Found = BC_LTCSummaryLogic.foundSummary; }
                            { ViewBag.Entity = "BC_LTCReport"; }
                            if (BC_LTCSummaryLogic.locList.Count != 0) isEmpty = true;
                            { ViewBag.Check1 = isEmpty; }
                            { ViewBag.Locations = BC_LTCSummaryLogic.locList; }
                            { ViewBag.LocInfo = WhoAdmin(); }
                            #endregion
                            break;
                        #endregion

                        #region BC Assisted Living Report:
                        case "BC_Assisted_Living_Reportable_Incidents_DTO":
                            BC_AssistSummaryLogic.ClearAllStatic();
                            if (CurrentLocalUser.Role == Role.SuperUser.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list20 =
                                        (from d in db.ReadBC_LTCAssisted()
                                         where d.DateIncident >= start && d.DateIncident <= end
                                         select d).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list20 = db.ReadBC_LTCAssisted().ToList();
                            }
                            else if (CurrentLocalUser.Role == Role.User.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list20 =
                                        (from d in db.ReadBC_LTCAssisted() where d.DateIncident >= start && d.DateIncident <= end select d).
                                    Where(l => l.NameCareCommu == localLocation).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list20 =
                                        db.ReadBC_LTCAssisted().Where(l => l.NameCareCommu == localLocation).ToList();
                            }
                            else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                            {
                                if (range == Range.With)
                                    TablesContainer.list20 =
                                    TablesContainer.list20.Where(ent => ent.DateIncident >= start && ent.DateIncident <= end).ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list20 = TablesContainer.list20;
                            }
                            else
                            {
                                if (range == Range.With)
                                    TablesContainer.list20 =
                                        (from d in db.ReadBC_LTCAssisted() where d.DateIncident >= start && d.DateIncident <= end select d).
                                    ToList();
                                else if (range == Range.Without)
                                    TablesContainer.list20 =
                                        db.ReadBC_LTCAssisted().ToList();
                            }
                            if (!BC_AssistSummaryLogic.checkRepead)
                            {
                                BC_AssistSummaryLogic.CheckLocation();
                                BC_AssistSummaryLogic.GetDistinctList(AppSettings.homes.ToList());
                                BC_AssistSummaryLogic.FillOutLists();
                                BC_AssistSummaryLogic.AddCntLoc();

                                if (TablesContainer.list20.Count() == 0)
                                {
                                    { ViewBag.ObjName = entity; }
                                    ViewBag.ErrorMsg = errorMsg =
                                      $"This date range doesn't contain any records.";
                                    WorTabs tabs = new WorTabs();
                                    tabs.ListForms = GetFormNames();
                                    return View(tabs);
                                }
                                BC_AssistSummaryLogic.AllStatIncident();
                            }

                            #region Create ViewBAgs:
                            { ViewBag.TotalSummary = BC_AssistSummaryLogic.allSummary; }

                            b = true;
                            if (BC_AssistSummaryLogic.foundSummary.Count == 0) { b = false; ViewBag.EmptLocation = b; }
                            { ViewBag.Count = TablesContainer.list20.Count; }
                            { ViewBag.GN_Found = BC_AssistSummaryLogic.foundSummary; }
                            { ViewBag.Entity = "BC_AssisLivRep"; }
                            if (BC_AssistSummaryLogic.locList.Count != 0) isEmpty = true;
                            { ViewBag.Check1 = isEmpty; }
                            { ViewBag.Locations = BC_AssistSummaryLogic.locList; }
                            { ViewBag.LocInfo = WhoAdmin(); }
                            #endregion
                            break;
                            #endregion
                    }
                    #endregion
                }
                #endregion

                #region For Searching:
                else if (btnName.Equals("-search"))
                {
                    checkView = "search";
                    { ViewBag.Check = checkView; }
                    string searchName = Value.Id.ToString();
                    ViewBag.Check1 = "search" + Value.Name;

                    #region Critical Incidents Searching:
                    if (searchName != "0" && Value.Name == "1")
                    {
                        if (TablesContainer.list1.Count != 0)
                        {
                            //CI_Category_Type ci_found = db.CI_Category_Types.Where(n => n.Id == Value.Id).SingleOrDefault();
                            //if(Value.Id != 0)
                            //{
                            List<Critical_Incidents_DTO> found =
                                TablesContainer.list1.Where(ci => ci.CI_Category_Type == Value.Id).ToList();

                            { ViewBag.Names = STREAM.GetLocNames().ToArray(); }
                            ViewBag.List = found;
                            WorTabs tabs = new WorTabs();
                            tabs.ListForms = GetFormNames();
                            return View(tabs);
                            //}
                            //else
                            //{
                            //    ViewBag.ErrorMsg = $"There was nothing found for '{searchName}'.. Please try input correct CI Category Name!";
                            //    WorTabs tabs = new WorTabs();
                            //    tabs.ListForms = GetFormNames();
                            //    return View(tabs);
                            //}
                        }
                    }
                    #endregion

                    #region Complaints Searching:
                    else if (searchName != "0" && Value.Name == "2")
                    {
                        if (TablesContainer.list2.Count != 0)
                        {
                            //List<Complaint> found =
                            //    TablesContainer.list2.Where(ci => ci.Department == Value.Id).ToList();

                            { ViewBag.Names = STREAM.GetLocNames().ToArray(); }
                            //ViewBag.List = found;
                            WorTabs tabs = new WorTabs();
                            tabs.ListForms = GetFormNames();
                            return View(tabs);
                        }
                    }
                    #endregion
                }
                #endregion
            }

            #region if you didn't select anything from the list on the left
            else
            {
                ViewBag.ErrorMsg = errorMsg = "Please select a form from the list on the left.";
                WorTabs tabs = new WorTabs();
                tabs.ListForms = GetFormNames();
                return View(tabs);
            }
            #endregion

            return RedirectToAction("../Home/WOR_Tabs");
        }
        #endregion

        #region Who Admin:
        public string WhoAdmin()
        {
            if (CurrentLocalUser.Role == Role.User.ToString())
            {
                var found = db.ReadHomes().Where(f => f.Id == localLocation).SingleOrDefault();
                return found.Full_Home_Name;
            }
            else
            {
                var found = db.ReadHomes().Where(f => f.Id == 1).SingleOrDefault();
                return found.Full_Home_Name;
            }
        }
        #endregion

        #region GoToListForm:
        public ActionResult GoToListForm(object name)
        {
            var list = TablesContainer.list3;
            return View(list);
        }
        #endregion

        #region Export ActionResult method:
        public ActionResult Export(string tblName, Range range, DateTime start = default, DateTime end = default)
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
            int id = int.Parse(tblName);
            var tbl_list = FillOutTableById(id).ToArray().ToList();
            if (tbl_list.Count == 0)
            {
                return null;
            }
            Type type = tbl_list[0].GetType();
            string entity = type.Name;
            object model = Searcher.FindObjByName(entity);
            model_name = model.GetType().Name;
            #region Getting concrete (for each entity) object using typeof function:
            if (model.GetType() == typeof(Critical_Incidents_DTO))
            {
                TablesContainer.list1 = new List<Critical_Incidents_DTO>();
                foreach (var obj in tbl_list)
                    TablesContainer.list1.Add((Critical_Incidents_DTO)obj);
                switch (AppSettings.GetRoleEnum(CurrentLocalUser.Role))
                {
                    case Role.User:
                        if (range == Range.Without)
                            TablesContainer.list1 = TablesContainer.list1;
                        else
                            TablesContainer.list1 = (from ent in TablesContainer.list1 where ent.Date >= start && ent.Date <= end select ent).ToList();
                        break;
                    case Role.RegionalUser:
                        if (range == Range.Without)
                            TablesContainer.list1 = TablesContainer.list1;
                        else
                            TablesContainer.list1 = (from ent in TablesContainer.list1 where ent.Date >= start && ent.Date <= end select ent).ToList();
                        break;
                    case Role.SuperUser:
                        if (range == Range.Without)
                            TablesContainer.list1 = TablesContainer.list1;
                        else
                            TablesContainer.list1 = (from ent in TablesContainer.list1 where ent.Date >= start && ent.Date <= end select ent).ToList();
                        break;
                    case Role.Admin:
                        if (range == Range.Without)
                            TablesContainer.list1 = TablesContainer.list1.ToList();
                        else
                            TablesContainer.list1 = (from ent in TablesContainer.list1 where ent.Date >= start && ent.Date <= end select ent).ToList();
                        break;
                    case Role.Unknown:
                        TablesContainer.list1 = new List<Critical_Incidents_DTO>();
                        break;
                    default: throw new Exception("Export was faild... Something went wrong..Try to see StackTrace, and try again!");
                }

                // new STREAM().WriteToCSV(query1); // to be continue..
                return RedirectToAction("../Home/ExportToCSV");
            }
            else if (model.GetType() == typeof(Good_News_DTO))
            {
                TablesContainer.list3 = new List<Good_News_DTO>();
                foreach (var obj in tbl_list)
                    TablesContainer.list3.Add((Good_News_DTO)obj);
                switch (AppSettings.GetRoleEnum(CurrentLocalUser.Role))
                {
                    case Role.User:
                        if (range == Range.Without)
                            TablesContainer.list3 = TablesContainer.list3;
                        else
                            TablesContainer.list3 = (from ent in TablesContainer.list3 where ent.DateNews >= start && ent.DateNews <= end select ent).ToList();
                        break;
                    case Role.RegionalUser:
                        if (range == Range.Without)
                            TablesContainer.list3 = TablesContainer.list3;
                        else
                            TablesContainer.list3 = (from ent in TablesContainer.list3 where ent.DateNews >= start && ent.DateNews <= end select ent).ToList();
                        break;
                    case Role.SuperUser:
                        if (range == Range.Without)
                            TablesContainer.list3 = TablesContainer.list3;
                        else
                            TablesContainer.list3 = (from ent in TablesContainer.list3 where ent.DateNews >= start && ent.DateNews <= end select ent).ToList();
                        break;
                    case Role.Admin:
                        if (range == Range.Without)
                            TablesContainer.list3 = TablesContainer.list3.ToList();
                        else
                            TablesContainer.list3 = (from ent in TablesContainer.list3 where ent.DateNews >= start && ent.DateNews <= end select ent).ToList();
                        break;
                    case Role.Unknown:
                        TablesContainer.list3 = new List<Good_News_DTO>();
                        break;
                    default: throw new Exception("Export was faild... Something went wrong..Try to see StackTrace, and try again!");
                }

                return RedirectToAction("../Home/ExportToCSV");
            }
            else if (model.GetType() == typeof(Complaint_DTO))
            {
                TablesContainer.list2 = new List<Complaint_DTO>();
                foreach (var obj in tbl_list)
                    TablesContainer.list2.Add((Complaint_DTO)obj);
                switch (AppSettings.GetRoleEnum(CurrentLocalUser.Role))
                {
                    case Role.User:
                        if (range == Range.Without)
                            TablesContainer.list2 = TablesContainer.list2;
                        else
                            TablesContainer.list2 = (from ent in TablesContainer.list2 where ent.DateReceived >= start && ent.DateReceived <= end select ent).ToList();
                        break;
                    case Role.RegionalUser:
                        if (range == Range.Without)
                            TablesContainer.list2 = TablesContainer.list2;
                        else
                            TablesContainer.list2 = (from ent in TablesContainer.list2 where ent.DateReceived >= start && ent.DateReceived <= end select ent).ToList();
                        break;
                    case Role.SuperUser:
                        if (range == Range.Without)
                            TablesContainer.list2 = TablesContainer.list2;
                        else
                            TablesContainer.list2 = (from ent in TablesContainer.list2 where ent.DateReceived >= start && ent.DateReceived <= end select ent).ToList();
                        break;
                    case Role.Admin:
                        if (range == Range.Without)
                            TablesContainer.list2 = TablesContainer.list2.ToList();
                        else
                            TablesContainer.list2 = (from ent in TablesContainer.list2 where ent.DateReceived >= start && ent.DateReceived <= end select ent).ToList();
                        break;
                    case Role.Unknown:
                        TablesContainer.list2 = new List<Complaint_DTO>();
                        break;
                    default: throw new Exception("Export was faild... Something went wrong..Try to see StackTrace, and try again!");
                }

                return RedirectToAction("../Home/ExportToCSV");
            }
            else if (model.GetType() == typeof(Community_Risks_DTO))
            {
                TablesContainer.list5 = new List<Community_Risks_DTO>();
                foreach (var obj in tbl_list)
                    TablesContainer.list5.Add((Community_Risks_DTO)obj);
                switch (AppSettings.GetRoleEnum(CurrentLocalUser.Role))
                {
                    case Role.User:
                        if (range == Range.Without)
                            TablesContainer.list5 = TablesContainer.list5;
                        else
                            TablesContainer.list5 = (from ent in TablesContainer.list5 where ent.Date >= start && ent.Date <= end select ent).ToList();
                        break;
                    case Role.RegionalUser:
                        if (range == Range.Without)
                            TablesContainer.list5 = TablesContainer.list5;
                        else
                            TablesContainer.list5 = (from ent in TablesContainer.list5 where ent.Date >= start && ent.Date <= end select ent).ToList();
                        break;
                    case Role.SuperUser:
                        if (range == Range.Without)
                            TablesContainer.list5 = TablesContainer.list5;
                        else
                            TablesContainer.list5 = (from ent in TablesContainer.list5 where ent.Date >= start && ent.Date <= end select ent).ToList();
                        break;
                    case Role.Admin:
                        if (range == Range.Without)
                            TablesContainer.list5 = TablesContainer.list5.ToList();
                        else
                            TablesContainer.list5 = (from ent in TablesContainer.list5 where ent.Date >= start && ent.Date <= end select ent).ToList();
                        break;
                    case Role.Unknown:
                        TablesContainer.list5 = new List<Community_Risks_DTO>();
                        break;
                    default: throw new Exception("Export was faild... Something went wrong..Try to see StackTrace, and try again!");
                }

                return RedirectToAction("../Home/ExportToCSV");
            }
            else if (model.GetType() == typeof(Labour_Relations_DTO))
            {
                TablesContainer.list10 = new List<Labour_Relations_DTO>();
                foreach (var obj in tbl_list)
                    TablesContainer.list10.Add((Labour_Relations_DTO)obj);
                switch (AppSettings.GetRoleEnum(CurrentLocalUser.Role))
                {
                    case Role.User:
                        if (range == Range.Without)
                            TablesContainer.list10 = TablesContainer.list10;
                        else
                            TablesContainer.list10 = (from ent in TablesContainer.list10 where ent.Date >= start && ent.Date <= end select ent).ToList();
                        break;
                    case Role.RegionalUser:
                        if (range == Range.Without)
                            TablesContainer.list10 = TablesContainer.list10;
                        else
                            TablesContainer.list10 = (from ent in TablesContainer.list10 where ent.Date >= start && ent.Date <= end select ent).ToList();
                        break;
                    case Role.SuperUser:
                        if (range == Range.Without)
                            TablesContainer.list10 = TablesContainer.list10;
                        else
                            TablesContainer.list10 = (from ent in TablesContainer.list10 where ent.Date >= start && ent.Date <= end select ent).ToList();
                        break;
                    case Role.Admin:
                        if (range == Range.Without)
                            TablesContainer.list10 = TablesContainer.list10.ToList();
                        else
                            TablesContainer.list10 = (from ent in TablesContainer.list10 where ent.Date >= start && ent.Date <= end select ent).ToList();
                        break;
                    case Role.Unknown:
                        TablesContainer.list10 = new List<Labour_Relations_DTO>();
                        break;
                    default: throw new Exception("Export was faild... Something went wrong..Try to see StackTrace, and try again!");
                }

                return RedirectToAction("../Home/ExportToCSV");
            }
            else if (model.GetType() == typeof(Emergency_Prep_DTO))
            {
                model_name = model.GetType().Name;
                if (CurrentLocalUser.Role == Role.User.ToString())
                {
                    if (range == Range.With)
                        TablesContainer.list4 = (from c in db.ReadEmergency() where c.Date >= start && c.Date <= end select c).Where(l => l.Location == localLocation).ToList();
                    else TablesContainer.list4 = db.ReadEmergency().Where(l => l.Location == localLocation).ToList();
                }
                else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                    AppSettings.IsRegionalUser(new Emergency_Prep_DTO(), CurrentLocalUser.Region, range, start, end);
                else
                {
                    if (range == Range.With)
                        TablesContainer.list4 = (from c in db.ReadEmergency() where c.Date >= start && c.Date <= end select c).ToList();
                    else TablesContainer.list4 = db.ReadEmergency().ToList();
                }
                return RedirectToAction("../Home/ExportToCSV");
            }
            else if (model.GetType() == typeof(Visits_Others_DTO))
            {
                List<Visits_Others_DTO> lst6 = db.ReadOthers().ToList();
                model_name = model.GetType().Name;
                if (CurrentLocalUser.Role == Role.SuperUser.ToString())
                {
                    if (range == Range.Without)
                        TablesContainer.list6 = lst6;
                    else
                        TablesContainer.list6 = (from ent in lst6 where ent.Date_of_Visit >= start && ent.Date_of_Visit <= end select ent).ToList();
                }
                else
                {
                    if (range == Range.Without)
                        TablesContainer.list6 = lst6.Where(l => l.Location == localLocation).ToList();
                    else
                        TablesContainer.list6 = (from ent in lst6 where ent.Date_of_Visit >= start && ent.Date_of_Visit <= end select ent).Where(i => i.Location == localLocation).ToList();
                }
                return RedirectToAction("../Home/ExportToCSV");
            }
            else if (model.GetType() == typeof(Privacy_Breaches_DTO))
            {
                TablesContainer.list7 = new List<Privacy_Breaches_DTO>();
                foreach (var obj in tbl_list)
                    TablesContainer.list7.Add((Privacy_Breaches_DTO)obj);
                switch (AppSettings.GetRoleEnum(CurrentLocalUser.Role))
                {
                    case Role.User:
                        if (range == Range.Without)
                            TablesContainer.list7 = TablesContainer.list7;
                        else
                            TablesContainer.list7 = (from ent in TablesContainer.list7 where ent.Date_Breach_Occurred >= start && ent.Date_Breach_Occurred <= end select ent).ToList();
                        break;
                    case Role.RegionalUser:
                        if (range == Range.Without)
                            TablesContainer.list7 = TablesContainer.list7;
                        else
                            TablesContainer.list7 = (from ent in TablesContainer.list7 where ent.Date_Breach_Occurred >= start && ent.Date_Breach_Occurred <= end select ent).ToList();
                        break;
                    case Role.SuperUser:
                        if (range == Range.Without)
                            TablesContainer.list7 = TablesContainer.list7;
                        else
                            TablesContainer.list7 = (from ent in TablesContainer.list7 where ent.Date_Breach_Occurred >= start && ent.Date_Breach_Occurred <= end select ent).ToList();
                        break;
                    case Role.Admin:
                        if (range == Range.Without)
                            TablesContainer.list7 = TablesContainer.list7.ToList();
                        else
                            TablesContainer.list7 = (from ent in TablesContainer.list7 where ent.Date_Breach_Occurred >= start && ent.Date_Breach_Occurred <= end select ent).ToList();
                        break;
                    case Role.Unknown:
                        TablesContainer.list7 = new List<Privacy_Breaches_DTO>();
                        break;
                    default: throw new Exception("Export was faild... Something went wrong..Try to see StackTrace, and try again!");
                }

                return RedirectToAction("../Home/ExportToCSV");
            }
            else if (model.GetType() == typeof(Privacy_Complaints_DTO))
            {
                TablesContainer.list8 = new List<Privacy_Complaints_DTO>();
                foreach (var obj in tbl_list)
                    TablesContainer.list8.Add((Privacy_Complaints_DTO)obj);
                switch (AppSettings.GetRoleEnum(CurrentLocalUser.Role))
                {
                    case Role.User:
                        if (range == Range.Without)
                            TablesContainer.list8 = TablesContainer.list8;
                        else
                            TablesContainer.list8 = (from ent in TablesContainer.list8 where ent.Date_Complain_Received >= start && ent.Date_Complain_Received <= end select ent).ToList();
                        break;
                    case Role.RegionalUser:
                        if (range == Range.Without)
                            TablesContainer.list8 = TablesContainer.list8;
                        else
                            TablesContainer.list8 = (from ent in TablesContainer.list8 where ent.Date_Complain_Received >= start && ent.Date_Complain_Received <= end select ent).ToList();
                        break;
                    case Role.SuperUser:
                        if (range == Range.Without)
                            TablesContainer.list8 = TablesContainer.list8;
                        else
                            TablesContainer.list8 = (from ent in TablesContainer.list8 where ent.Date_Complain_Received >= start && ent.Date_Complain_Received <= end select ent).ToList();
                        break;
                    case Role.Admin:
                        if (range == Range.Without)
                            TablesContainer.list8 = TablesContainer.list8.ToList();
                        else
                            TablesContainer.list8 = (from ent in TablesContainer.list8 where ent.Date_Complain_Received >= start && ent.Date_Complain_Received <= end select ent).ToList();
                        break;
                    case Role.Unknown:
                        TablesContainer.list8 = new List<Privacy_Complaints_DTO>();
                        break;
                    default: throw new Exception("Export was faild... Something went wrong..Try to see StackTrace, and try again!");
                }

                return RedirectToAction("../Home/ExportToCSV");
            }
            else if (model.GetType() == typeof(Education_DTO))
            {
                TablesContainer.list9 = new List<Education_DTO>();
                foreach (var obj in tbl_list)
                    TablesContainer.list9.Add((Education_DTO)obj);
                switch (AppSettings.GetRoleEnum(CurrentLocalUser.Role))
                {
                    case Role.User:
                        if (range == Range.Without)
                            TablesContainer.list9 = TablesContainer.list9;
                        else
                            TablesContainer.list9 = (from ent in TablesContainer.list9 where ent.DateStart >= start && ent.DateStart <= end select ent).ToList();
                        break;
                    case Role.RegionalUser:
                        if (range == Range.Without)
                            TablesContainer.list9 = TablesContainer.list9;
                        else
                            TablesContainer.list9 = (from ent in TablesContainer.list9 where ent.DateStart >= start && ent.DateStart <= end select ent).ToList();
                        break;
                    case Role.SuperUser:
                        if (range == Range.Without)
                            TablesContainer.list9 = TablesContainer.list9;
                        else
                            TablesContainer.list9 = (from ent in TablesContainer.list9 where ent.DateStart >= start && ent.DateStart <= end select ent).ToList();
                        break;
                    case Role.Admin:
                        if (range == Range.Without)
                            TablesContainer.list9 = TablesContainer.list9.ToList();
                        else
                            TablesContainer.list9 = (from ent in TablesContainer.list9 where ent.DateStart >= start && ent.DateStart <= end select ent).ToList();
                        break;
                    case Role.Unknown:
                        TablesContainer.list9 = new List<Education_DTO>();
                        break;
                    default: throw new Exception("Export was faild... Something went wrong..Try to see StackTrace, and try again!");
                }

                return RedirectToAction("../Home/ExportToCSV");
            }
            else if (model.GetType() == typeof(Immunization_DTO))
            {
                model_name = model.GetType().Name;
                if (CurrentLocalUser.Role == Role.SuperUser.ToString())
                    TablesContainer.list11 = db.ReadImmunizations().ToList();
                else
                    TablesContainer.list11 = db.ReadImmunizations().Where(i => i.Location == localLocation).ToList();
                return RedirectToAction("../Home/ExportToCSV");
            }
            else if (model.GetType() == typeof(Outbreaks_DTO))
            {
                TablesContainer.list12 = new List<Outbreaks_DTO>();
                foreach (var obj in tbl_list)
                    TablesContainer.list12.Add((Outbreaks_DTO)obj);
                switch (AppSettings.GetRoleEnum(CurrentLocalUser.Role))
                {
                    case Role.User:
                        if (range == Range.Without)
                            TablesContainer.list12 = TablesContainer.list12;
                        else
                            TablesContainer.list12 = (from ent in TablesContainer.list12 where ent.Date_Declared >= start && ent.Date_Declared <= end select ent).ToList();
                        break;
                    case Role.RegionalUser:
                        if (range == Range.Without)
                            TablesContainer.list12 = TablesContainer.list12;
                        else
                            TablesContainer.list12 = (from ent in TablesContainer.list12 where ent.Date_Declared >= start && ent.Date_Declared <= end select ent).ToList();
                        break;
                    case Role.SuperUser:
                        if (range == Range.Without)
                            TablesContainer.list12 = TablesContainer.list12;
                        else
                            TablesContainer.list12 = (from ent in TablesContainer.list12 where ent.Date_Declared >= start && ent.Date_Declared <= end select ent).ToList();
                        break;
                    case Role.Admin:
                        if (range == Range.Without)
                            TablesContainer.list12 = TablesContainer.list12.ToList();
                        else
                            TablesContainer.list12 = (from ent in TablesContainer.list12 where ent.Date_Declared >= start && ent.Date_Declared <= end select ent).ToList();
                        break;
                    case Role.Unknown:
                        TablesContainer.list12 = new List<Outbreaks_DTO>();
                        break;
                    default: throw new Exception("Export was faild... Something went wrong..Try to see StackTrace, and try again!");
                }

                return RedirectToAction("../Home/ExportToCSV");
            }
            else if (model.GetType() == typeof(WSIB_DTO))
            {
                TablesContainer.list13 = new List<WSIB_DTO>();
                foreach (var obj in tbl_list)
                    TablesContainer.list13.Add((WSIB_DTO)obj);
                switch (AppSettings.GetRoleEnum(CurrentLocalUser.Role))
                {
                    case Role.User:
                        if (range == Range.Without)
                            TablesContainer.list13 = TablesContainer.list13;
                        else
                            TablesContainer.list13 = (from ent in TablesContainer.list13 where ent.Date_Accident >= start && ent.Date_Accident <= end select ent).ToList();
                        break;
                    case Role.RegionalUser:
                        if (range == Range.Without)
                            TablesContainer.list13 = TablesContainer.list13;
                        else
                            TablesContainer.list13 = (from ent in TablesContainer.list13 where ent.Date_Accident >= start && ent.Date_Accident <= end select ent).ToList();
                        break;
                    case Role.SuperUser:
                        if (range == Range.Without)
                            TablesContainer.list13 = TablesContainer.list13;
                        else
                            TablesContainer.list13 = (from ent in TablesContainer.list13 where ent.Date_Accident >= start && ent.Date_Accident <= end select ent).ToList();
                        break;
                    case Role.Admin:
                        if (range == Range.Without)
                            TablesContainer.list13 = TablesContainer.list13.ToList();
                        else
                            TablesContainer.list13 = (from ent in TablesContainer.list13 where ent.Date_Accident >= start && ent.Date_Accident <= end select ent).ToList();
                        break;
                    case Role.Unknown:
                        TablesContainer.list13 = new List<WSIB_DTO>();
                        break;
                    default: throw new Exception("Export was faild... Something went wrong..Try to see StackTrace, and try again!");
                }

                return RedirectToAction("../Home/ExportToCSV");
            }
            else if (model.GetType() == typeof(Not_WSIBs_DTO))
            {
                TablesContainer.list14 = new List<Not_WSIBs_DTO>();
                foreach (var obj in tbl_list)
                    TablesContainer.list14.Add((Not_WSIBs_DTO)obj);
                switch (AppSettings.GetRoleEnum(CurrentLocalUser.Role))
                {
                    case Role.User:
                        if (range == Range.Without)
                            TablesContainer.list14 = TablesContainer.list14;
                        else
                            TablesContainer.list14 = (from ent in TablesContainer.list14 where ent.Date_of_Incident >= start && ent.Date_of_Incident <= end select ent).ToList();
                        break;
                    case Role.RegionalUser:
                        if (range == Range.Without)
                            TablesContainer.list14 = TablesContainer.list14;
                        else
                            TablesContainer.list14 = (from ent in TablesContainer.list14 where ent.Date_of_Incident >= start && ent.Date_of_Incident <= end select ent).ToList();
                        break;
                    case Role.SuperUser:
                        if (range == Range.Without)
                            TablesContainer.list14 = TablesContainer.list14;
                        else
                            TablesContainer.list14 = (from ent in TablesContainer.list14 where ent.Date_of_Incident >= start && ent.Date_of_Incident <= end select ent).ToList();
                        break;
                    case Role.Admin:
                        if (range == Range.Without)
                            TablesContainer.list14 = TablesContainer.list14.ToList();
                        else
                            TablesContainer.list14 = (from ent in TablesContainer.list14 where ent.Date_of_Incident >= start && ent.Date_of_Incident <= end select ent).ToList();
                        break;
                    case Role.Unknown:
                        TablesContainer.list14 = new List<Not_WSIBs_DTO>();
                        break;
                    default: throw new Exception("Export was faild... Something went wrong..Try to see StackTrace, and try again!");
                }

                return RedirectToAction("../Home/ExportToCSV");
            }
            else if (model.GetType() == typeof(LicensingInspectionDTO))
            {
                new TablesContainer().ResetAllTabls();
                List<LicensingInspectionDTO> lst15 = new List<LicensingInspectionDTO>();
                foreach (var obj in tbl_list)
                    TablesContainer.list15.Add((LicensingInspectionDTO)obj);

                switch (AppSettings.GetRoleEnum(CurrentLocalUser.Role))
                {
                    case Role.User:
                        if (range == Range.Without)
                            TablesContainer.list15 = TablesContainer.list15;
                        else
                            TablesContainer.list15 = (from ent in TablesContainer.list15 where ent.Date >= start && ent.Date <= end select ent).ToList();
                        break;
                    case Role.RegionalUser:
                        if (range == Range.Without)
                            TablesContainer.list15 = TablesContainer.list15;
                        else
                            TablesContainer.list15 = (from ent in TablesContainer.list15 where ent.Date >= start && ent.Date <= end select ent).ToList();
                        break;
                    case Role.SuperUser:
                        if (range == Range.Without)
                            TablesContainer.list15 = TablesContainer.list15;
                        else
                            TablesContainer.list15 = (from ent in TablesContainer.list15 where ent.Date >= start && ent.Date <= end select ent).ToList();
                        break;
                    case Role.Admin:
                        if (range == Range.Without)
                            TablesContainer.list15 = TablesContainer.list15.ToList();
                        else
                            TablesContainer.list15 = (from ent in TablesContainer.list15 where ent.Date >= start && ent.Date <= end select ent).ToList();
                        break;
                    case Role.Unknown:
                        TablesContainer.list15 = new List<LicensingInspectionDTO>();
                        break;
                    default: throw new Exception("Your data export failed! This table does not exist in the database.");
                }

                return RedirectToAction("../Home/ExportToCSV");
            }
            else if (model.GetType() == typeof(AssistedLivingInspectionDTO))
            {
                new TablesContainer().ResetAllTabls();
                List<AssistedLivingInspectionDTO> lst16 = new List<AssistedLivingInspectionDTO>();
                foreach (var obj in tbl_list)
                    TablesContainer.list16.Add((AssistedLivingInspectionDTO)obj);

                switch (AppSettings.GetRoleEnum(CurrentLocalUser.Role))
                {
                    case Role.User:
                        if (range == Range.Without)
                            TablesContainer.list16 = TablesContainer.list16;
                        else
                            TablesContainer.list16 = (from ent in TablesContainer.list16 where ent.Date >= start && ent.Date <= end select ent).ToList();
                        break;
                    case Role.RegionalUser:
                        if (range == Range.Without)
                            TablesContainer.list16 = TablesContainer.list16;
                        else
                            TablesContainer.list16 = (from ent in TablesContainer.list16 where ent.Date >= start && ent.Date <= end select ent).ToList();
                        break;
                    case Role.SuperUser:
                        if (range == Range.Without)
                            TablesContainer.list16 = TablesContainer.list16;
                        else
                            TablesContainer.list16 = (from ent in TablesContainer.list16 where ent.Date >= start && ent.Date <= end select ent).ToList();
                        break;
                    case Role.Admin:
                        if (range == Range.Without)
                            TablesContainer.list16 = TablesContainer.list16.ToList();
                        else
                            TablesContainer.list16 = (from ent in TablesContainer.list16 where ent.Date >= start && ent.Date <= end select ent).ToList();
                        break;
                    case Role.Unknown:
                        TablesContainer.list16 = new List<AssistedLivingInspectionDTO>();
                        break;
                    default: throw new Exception("Your data export failed! This table does not exist in the database.");
                }

                return RedirectToAction("../Home/ExportToCSV");
            }
            else if (model.GetType() == typeof(WorkshopBCInspection_DTO))
            {
                new TablesContainer().ResetAllTabls();
                List<WorkshopBCInspection_DTO> lst17 = new List<WorkshopBCInspection_DTO>();
                foreach (var obj in tbl_list)
                    TablesContainer.list17.Add((WorkshopBCInspection_DTO)obj);

                switch (AppSettings.GetRoleEnum(CurrentLocalUser.Role))
                {
                    case Role.User:
                        if (range == Range.Without)
                            TablesContainer.list17 = TablesContainer.list17;
                        else
                            TablesContainer.list17 = (from ent in TablesContainer.list17 where ent.Date >= start && ent.Date <= end select ent).ToList();
                        break;
                    case Role.RegionalUser:
                        if (range == Range.Without)
                            TablesContainer.list17 = TablesContainer.list17;
                        else
                            TablesContainer.list17 = (from ent in TablesContainer.list17 where ent.Date >= start && ent.Date <= end select ent).ToList();
                        break;
                    case Role.SuperUser:
                        if (range == Range.Without)
                            TablesContainer.list17 = TablesContainer.list17;
                        else
                            TablesContainer.list17 = (from ent in TablesContainer.list17 where ent.Date >= start && ent.Date <= end select ent).ToList();
                        break;
                    case Role.Admin:
                        if (range == Range.Without)
                            TablesContainer.list17 = TablesContainer.list17.ToList();
                        else
                            TablesContainer.list17 = (from ent in TablesContainer.list17 where ent.Date >= start && ent.Date <= end select ent).ToList();
                        break;
                    case Role.Unknown:
                        TablesContainer.list17 = new List<WorkshopBCInspection_DTO>();
                        break;
                    default: throw new Exception("Your data export failed! This table does not exist in the database.");
                }

                return RedirectToAction("../Home/ExportToCSV");
            }
            else if (model.GetType() == typeof(QualityReview_DTO))
            {
                new TablesContainer().ResetAllTabls();
                List<QualityReview_DTO> lst18 = new List<QualityReview_DTO>();
                foreach (var obj in tbl_list)
                    TablesContainer.list18.Add((QualityReview_DTO)obj);

                switch (AppSettings.GetRoleEnum(CurrentLocalUser.Role))
                {
                    case Role.User:
                        if (range == Range.Without)
                            TablesContainer.list18 = TablesContainer.list18;
                        else
                            TablesContainer.list18 = (from ent in TablesContainer.list18 where ent.Date >= start && ent.Date <= end select ent).ToList();
                        break;
                    case Role.RegionalUser:
                        if (range == Range.Without)
                            TablesContainer.list18 = TablesContainer.list18;
                        else
                            TablesContainer.list18 = (from ent in TablesContainer.list18 where ent.Date >= start && ent.Date <= end select ent).ToList();
                        break;
                    case Role.SuperUser:
                        if (range == Range.Without)
                            TablesContainer.list18 = TablesContainer.list18;
                        else
                            TablesContainer.list18 = (from ent in TablesContainer.list18 where ent.Date >= start && ent.Date <= end select ent).ToList();
                        break;
                    case Role.Admin:
                        if (range == Range.Without)
                            TablesContainer.list18 = TablesContainer.list18.ToList();
                        else
                            TablesContainer.list18 = (from ent in TablesContainer.list18 where ent.Date >= start && ent.Date <= end select ent).ToList();
                        break;
                    case Role.Unknown:
                        TablesContainer.list18 = new List<QualityReview_DTO>();
                        break;
                    default: throw new Exception("Your data export failed! This table does not exist in the database.");
                }

                return RedirectToAction("../Home/ExportToCSV");
            }
            else if (model.GetType() == typeof(BC_LTC_Reportable_Incidents_DTO))
            {
                new TablesContainer().ResetAllTabls();
                List<BC_LTC_Reportable_Incidents_DTO> lst19 = new List<BC_LTC_Reportable_Incidents_DTO>();
                foreach (var obj in tbl_list)
                    TablesContainer.list19.Add((BC_LTC_Reportable_Incidents_DTO)obj);

                switch (AppSettings.GetRoleEnum(CurrentLocalUser.Role))
                {
                    case Role.User:
                        if (range == Range.Without)
                            TablesContainer.list19 = TablesContainer.list19;
                        else
                            TablesContainer.list19 = (from ent in TablesContainer.list19 where ent.DateIncident >= start && ent.DateIncident <= end select ent).ToList();
                        break;
                    case Role.RegionalUser:
                        if (range == Range.Without)
                            TablesContainer.list19 = TablesContainer.list19;
                        else
                            TablesContainer.list19 = (from ent in TablesContainer.list19 where ent.DateIncident >= start && ent.DateIncident <= end select ent).ToList();
                        break;
                    case Role.SuperUser:
                        if (range == Range.Without)
                            TablesContainer.list19 = TablesContainer.list19;
                        else
                            TablesContainer.list19 = (from ent in TablesContainer.list19 where ent.DateIncident >= start && ent.DateIncident <= end select ent).ToList();
                        break;
                    case Role.Admin:
                        if (range == Range.Without)
                            TablesContainer.list19 = TablesContainer.list19.ToList();
                        else
                            TablesContainer.list19 = (from ent in TablesContainer.list19 where ent.DateIncident >= start && ent.DateIncident <= end select ent).ToList();
                        break;
                    case Role.Unknown:
                        TablesContainer.list19 = new List<BC_LTC_Reportable_Incidents_DTO>();
                        break;
                    default: throw new Exception("Your data export failed! This table does not exist in the database.");
                }

                return RedirectToAction("../Home/ExportToCSV");
            }
            else if (model.GetType() == typeof(BC_Assisted_Living_Reportable_Incidents_DTO))
            {
                new TablesContainer().ResetAllTabls();
                List<BC_Assisted_Living_Reportable_Incidents_DTO> lst19 = new List<BC_Assisted_Living_Reportable_Incidents_DTO>();
                foreach (var obj in tbl_list)
                    TablesContainer.list20.Add((BC_Assisted_Living_Reportable_Incidents_DTO)obj);

                switch (AppSettings.GetRoleEnum(CurrentLocalUser.Role))
                {
                    case Role.User:
                        if (range == Range.Without)
                            TablesContainer.list20 = TablesContainer.list20;
                        else
                            TablesContainer.list20 = (from ent in TablesContainer.list20 where ent.DateIncident >= start && ent.DateIncident <= end select ent).ToList();
                        break;
                    case Role.RegionalUser:
                        if (range == Range.Without)
                            TablesContainer.list20 = TablesContainer.list20;
                        else
                            TablesContainer.list20 = (from ent in TablesContainer.list20 where ent.DateIncident >= start && ent.DateIncident <= end select ent).ToList();
                        break;
                    case Role.SuperUser:
                        if (range == Range.Without)
                            TablesContainer.list20 = TablesContainer.list20;
                        else
                            TablesContainer.list20 = (from ent in TablesContainer.list20 where ent.DateIncident >= start && ent.DateIncident <= end select ent).ToList();
                        break;
                    case Role.Admin:
                        if (range == Range.Without)
                            TablesContainer.list20 = TablesContainer.list20.ToList();
                        else
                            TablesContainer.list20 = (from ent in TablesContainer.list20 where ent.DateIncident >= start && ent.DateIncident <= end select ent).ToList();
                        break;
                    case Role.Unknown:
                        TablesContainer.list20 = new List<BC_Assisted_Living_Reportable_Incidents_DTO>();
                        break;
                    default: throw new Exception("Your data export failed! This table does not exist in the database.");
                }

                return RedirectToAction("../Home/ExportToCSV");
            }
            else
            {
                return null;
            }
            #endregion
        }
        #endregion

        #region Export to CSV:
        [HttpGet]
        public FileResult ExportToCSV()
        {
            #region Unused entities: 
            //else if (model_name.Equals("Labour_Relations_DTO")) // Not usage..
            //{
            //    var lst = TablesContainer.list10.ToList<object>();

            //    string[] names = typeof(Labour_Relations_DTO).GetProperties().Select(property => property.Name).ToArray();

            //    lst.Insert(0, names.Where(x => x != names[0]).ToArray());

            //    #region Generate CSV

            //    StringBuilder sb = new StringBuilder();
            //    for (var i = 0; i < lst.Count; i++)
            //    {
            //        if (i == 0)
            //        {
            //            string[] arrStudents = (string[])lst[0];
            //            foreach (var data in arrStudents)
            //            {
            //                //Append data with comma(,) separator.
            //                sb.Append(data + ',');
            //            }
            //        }
            //        else
            //        {
            //            sb.Append(lst[i]);
            //        }
            //        //Append new line character.
            //        sb.Append("\r\n");
            //    }

            //    #endregion

            //    #region Download CSV

            //    return File(Encoding.ASCII.GetBytes(sb.ToString()), "text/csv", "Labour Relations.csv");

            //    #endregion
            //}
            //         else if (model_name.Equals("Visits_Others_DTO"))
            //         {
            //             var lst = TablesContainer.list6.ToList<object>();

            //             string[] names = typeof(Visits_Others_DTO).GetProperties().Select(property => property.Name).ToArray();
            //lst.Insert(0, names.Where(x => x != names[0]).ToArray());

            //             #region Generate CSV

            //             StringBuilder sb = new StringBuilder();
            //             for (var i = 0; i < lst.Count; i++)
            //             {
            //                 if (i == 0)
            //                 {
            //                     string[] arrStudents = (string[])lst[0];
            //                     foreach (var data in arrStudents)
            //                     {
            //                         //Append data with comma(,) separator.
            //                         sb.Append(data + ',');
            //                     }
            //                 }
            //                 else
            //                 {
            //                     sb.Append(lst[i]);
            //                 }
            //                 //Append new line character.
            //                 sb.Append("\r\n");
            //             }

            //             #endregion

            //             #region Download CSV

            //             return File(Encoding.ASCII.GetBytes(sb.ToString()), "text/csv", "Visits_Others.csv");

            //             #endregion
            //         }       
            //else if (model_name.Equals("Education_DTO"))
            //{
            //    var lst = TablesContainer.list9.ToList<object>();

            //    string[] names = typeof(Education_DTO).GetProperties().Select(property => property.Name).ToArray();

            //    lst.Insert(0, names.Where(x => x != names[0]).ToArray());

            //    #region Generate CSV

            //    StringBuilder sb = new StringBuilder();
            //    for (var i = 0; i < lst.Count; i++)
            //    {
            //        if (i == 0)
            //        {
            //            string[] arrStudents = (string[])lst[0];
            //            foreach (var data in arrStudents)
            //            {
            //                //Append data with comma(,) separator.
            //                sb.Append(data + ',');
            //            }
            //        }
            //        else
            //        {
            //            sb.Append(lst[i]);
            //        }
            //        //Append new line character.
            //        sb.Append("\r\n");
            //    }

            //    #endregion

            //    #region Download CSV

            //    return File(Encoding.ASCII.GetBytes(sb.ToString()), "text/csv", "Education.csv");

            //    #endregion
            //}
            //else if (model_name.Equals("Labour_Relations_DTO"))
            //{
            //    var lst = TablesContainer.list10.ToList<object>();

            //    string[] names = typeof(Labour_Relations_DTO).GetProperties().Select(property => property.Name).ToArray();

            //    lst.Insert(0, names.Where(x => x != names[0]).ToArray());

            //    #region Generate CSV

            //    StringBuilder sb = new StringBuilder();
            //    for (var i = 0; i < lst.Count; i++)
            //    {
            //        if (i == 0)
            //        {
            //            var headers = new string[]
            //                            {
            //                "Location","Date","Union","Category","Details",
            //                "Status","Accruals","Outcome","Lessons Learned"
            //                            };
            //            foreach (var data in headers)
            //            {
            //                //Append data with comma(,) separator.
            //                sb.Append(data + ',');
            //            }
            //        }
            //        else
            //        {
            //            sb.Append(lst[i]);
            //        }
            //        //Append new line character.
            //        sb.Append("\r\n");
            //    }

            //    #endregion

            //    #region Download CSV

            //    return File(Encoding.ASCII.GetBytes(sb.ToString()), "text/csv", "Labour Relations.csv");

            //    #endregion
            //}
            //else if (model_name.Equals("Immunization_DTO"))
            //{
            //    var lst = TablesContainer.list11.ToList<object>();

            //    string[] names = typeof(Immunization_DTO).GetProperties().Select(property => property.Name).ToArray();

            //    lst.Insert(0, names.Where(x => x != names[0]).ToArray());

            //    #region Generate CSV

            //    StringBuilder sb = new StringBuilder();
            //    for (var i = 0; i < lst.Count; i++)
            //    {
            //        if (i == 0)
            //        {
            //            string[] arrStudents = (string[])lst[0];
            //            foreach (var data in arrStudents)
            //            {
            //                //Append data with comma(,) separator.
            //                sb.Append(data + ',');
            //            }
            //        }
            //        else
            //        {
            //            sb.Append(lst[i]);
            //        }
            //        //Append new line character.
            //        sb.Append("\r\n");
            //    }

            //    #endregion

            //    #region Download CSV

            //    return File(Encoding.ASCII.GetBytes(sb.ToString()), "text/csv", "Immunization.csv");

            //    #endregion
            //}
            #endregion
            if (model_name.Equals("WSIB_DTO"))
            {
                var lst = TablesContainer.list13.ToList<object>();
                var names = new string[]
                {
                    "Location","Date of Accident","Employee Initials/SSLI","Cause of Accident","Date Modified Duties Commenced",
                    "Actual Date Employee Returned to Regular Duties","Number of Lost Days",
                    "Number of Modified Days (Not Shadowed)","Number of Modified Days (Shadowed)",
                    "Complete and File Form 7? Reed Group, if Appplicable (Yes/No)"
                };
                lst.Insert(0, names);

                #region Generate CSV
                StringBuilder sb = new StringBuilder();
                for (var i = 0; i < lst.Count; i++)
                {
                    if (i == 0)
                        foreach (var data in names)
                            sb.Append(data + ',');
                    else sb.Append(lst[i]);
                    sb.Append("\r\n");
                }
                #endregion

                #region Download CSV
                return File(Encoding.ASCII.GetBytes(sb.ToString()), "text/csv", "WSIB.csv");
                #endregion
            }
            else if (model_name.Equals("Good_News_DTO"))
            {
                var lst = TablesContainer.list3.ToList<object>();

                string[] titles =
{
                    "Location",
                    "Date of News",
                    "Category",
                    "Department",
                    "Source of Compliment",
                    "Received From",
                    "Description of Compliment",
                    "Respect",
                    "Passion",
                    "Teamwork",
                    "Responsibility",
                    "Growth",
                    "Compliment",
                    "Spot Awards",
                    "Awards Details",
                    "Name of Awards",
                    "Awards Received",
                    "Community Inititives"
                };

                lst.Insert(0, titles);

                #region Generate CSV

                StringBuilder sb = new StringBuilder();
                for (var i = 0; i < lst.Count; i++)
                {
                    if (i == 0)
                    {
                        foreach (var data in titles)
                        {
                            //Append data with comma(,) separator.
                            sb.Append(data + ',');
                        }
                    }
                    else
                    {
                        sb.Append(lst[i]);
                    }
                    //Append new line character.
                    sb.Append("\r\n");
                }

                #endregion

                #region Download CSV

                return File(Encoding.ASCII.GetBytes(sb.ToString()), "text/csv", "Good News.csv");

                #endregion
            }
            else if (model_name.Equals("Complaint_DTO"))
            {
                var lst = TablesContainer.list2.ToList<object>();

                string[] titles = new string[]
               {
                    "Date Received",
                    "Location",
                    "Writen Or Verbal",
                    "Receive Directly",
                    "From Resident",
                    "Resident Name",
                    "Department", "Home Area",
                    "Brief Description", "Is Administration",
                    "Care Services", "Palliative Care",
                    "Dietary", "Housekeeping",
                    "Laundry", "Maintenance",
                    "Programs", "Admissions",
                    "Physician", "Other",
                    "MOHLTC Notified", "Copy To VP",
                    "Response Sent", "Action Taken",
                    "Resolved", "Ministry Visit"
               };

                lst.Insert(0, titles);

                #region Generate CSV

                StringBuilder sb = new StringBuilder();
                for (var i = 0; i < lst.Count; i++)
                {
                    if (i == 0)
                    {
                        foreach (var data in titles)
                        {
                            //Append data with comma(,) separator.
                            sb.Append(data + ',');
                        }
                    }
                    else
                    {
                        sb.Append(lst[i]);
                    }
                    //Append new line character.
                    sb.Append("\r\n");
                }

                #endregion

                #region Download CSV

                return File(Encoding.ASCII.GetBytes(sb.ToString()), "text/csv", "Complaints.csv");

                #endregion
            }
            else if (model_name.Equals("Outbreaks_DTO"))
            {
                var lst = TablesContainer.list12.ToList<object>();

                var headers = new string[]
                {
                            "Date Declared","Location","Date Concluded","Type of Outbreak","Total Days Closed",
                            "Total Residents Affected","Total Staff Affected","Strain Identified","Deaths Due",
                            "CI Report Submitted","Notify MOL if Staff has Become Ill due to Outbreak",
                            "Credit for Lost Days","LHIN Letter Received","PH Letter Received","Tracking Sheet Completed",
                            "Docs Submitted Finance"
                };

                lst.Insert(0, headers);

                #region Generate CSV

                StringBuilder sb = new StringBuilder();
                for (var i = 0; i < lst.Count; i++)
                {
                    if (i == 0)
                    {
                        foreach (var data in headers)
                        {
                            //Append data with comma(,) separator.
                            sb.Append(data + ',');
                        }
                    }
                    else
                    {
                        sb.Append(lst[i]);
                    }
                    //Append new line character.
                    sb.Append("\r\n");
                }

                #endregion

                #region Download CSV

                return File(Encoding.ASCII.GetBytes(sb.ToString()), "text/csv", "Outbreaks.csv");

                #endregion
            }
            else if (model_name.Equals("Not_WSIBs_DTO"))
            {
                var lst = TablesContainer.list14.ToList<object>();

                string[] names = typeof(Not_WSIBs_DTO).GetProperties().Select(property => property.Name).ToArray();



                lst.Insert(0, new string[names.Length]);
                lst[0] = names;

                #region Generate CSV

                StringBuilder sb = new StringBuilder();
                for (var i = 0; i < lst.Count; i++)
                {
                    if (i == 0)
                    {
                        var headers = new string[]
                          {
                            "Location","Date of Incident","Employee Initials","Position","Time of Incident",
                            "Shift","Home Area","Injury Related","Type of Injury",
                            "Details of Incident"
                          };
                        foreach (var data in headers)
                        {
                            //Append data with comma(,) separator.
                            sb.Append(data + ',');
                        }
                    }
                    else
                    {
                        sb.Append(lst[i]);
                    }
                    //Append new line character.
                    sb.Append("\r\n");
                }

                #endregion

                #region Download CSV

                return File(Encoding.ASCII.GetBytes(sb.ToString()), "text/csv", "Internal.csv");

                #endregion
            }
            else if (model_name.Equals("QualityReview_DTO"))
            {
                var lst = TablesContainer.list18.ToList<object>();

                var headers = new string[]
{
                            "Location","Date","Standard",
                            "Brief Description of the Findings",
                            "Brief Description of the Recommendations","Action Plan",
                            "Responsibility", "Action Date"
};

                lst.Insert(0, headers);

                #region Generate CSV

                StringBuilder sb = new StringBuilder();
                for (var i = 0; i < lst.Count; i++)
                {
                    if (i == 0)
                    {
                        foreach (var data in headers)
                        {
                            //Append data with comma(,) separator.
                            sb.Append(data + ',');
                        }
                    }
                    else
                    {
                        sb.Append(lst[i]);
                    }
                    //Append new line character.
                    sb.Append("\r\n");
                }

                #endregion

                #region Download CSV

                return File(Encoding.ASCII.GetBytes(sb.ToString()), "text/csv", "Quality Reviews.csv");

                #endregion
            }
            else if (model_name.Equals("Emergency_Prep_DTO"))
            {
                var lst = TablesContainer.list4.ToList<object>();
                string[] title = new string[]
                {
                    "Location","Code","Exercise","Method","Date"
                };
                lst.Insert(0, title);

                #region Generate CSV

                StringBuilder sb = new StringBuilder();
                for (var i = 0; i < lst.Count; i++)
                {
                    if (i == 0)
                    {
                        foreach (var data in title)
                        {
                            //Append data with comma(,) separator.
                            sb.Append(data + ',');
                        }
                    }
                    else
                    {
                        sb.Append(lst[i]);
                    }
                    //Append new line character.
                    sb.Append("\r\n");
                }

                #endregion

                #region Download CSV

                return File(Encoding.ASCII.GetBytes(sb.ToString()), "text/csv", "Emergency Prep.csv");

                #endregion
            }
            else if (model_name.Equals("Community_Risks_DTO"))
            {
                var lst = TablesContainer.list5.ToList<object>();

                string[] headers = new string[]
{
                            "Date","Location","Type Of Risk","Descriptions","Potential Risk",
                            "MOH Visit","Risk Legal Action","Hot Alert","Status Update","Resolved"
};

                lst.Insert(0, headers);

                #region Generate CSV

                StringBuilder sb = new StringBuilder();
                for (var i = 0; i < lst.Count; i++)
                {
                    if (i == 0)
                    {
                        foreach (var data in headers)
                        {
                            //Append data with comma(,) separator.
                            sb.Append(data + ',');
                        }
                    }
                    else
                    {
                        sb.Append(lst[i]);
                    }
                    //Append new line character.
                    sb.Append("\r\n");
                }

                #endregion

                #region Download CSV

                return File(Encoding.ASCII.GetBytes(sb.ToString()), "text/csv", "Community Risks.csv");

                #endregion
            }
            else if (model_name.Equals("Privacy_Breaches_DTO"))
            {
                var lst = TablesContainer.list7.ToList<object>();

                var headers = new string[]
        {
                            "Location","Status","Date Breach Occurred","Date Breach Reported",
                            "Breach Reported By","Type of Breach","Type of PHI Involved","Number of Individuals Affected",
                            "Description Outcome","Risk Level"
        };

                lst.Insert(0, headers);

                #region Generate CSV

                StringBuilder sb = new StringBuilder();
                for (var i = 0; i < lst.Count; i++)
                {
                    if (i == 0)
                    {
                        foreach (var data in headers)
                        {
                            //Append data with comma(,) separator.
                            sb.Append(data + ',');
                        }
                    }
                    else
                    {
                        sb.Append(lst[i]);
                    }
                    //Append new line character.
                    sb.Append("\r\n");
                }

                #endregion

                #region Download CSV

                var str = sb.ToString();
                return File(Encoding.ASCII.GetBytes(str), "text/csv", "Privacy Breaches.csv");

                #endregion
            }
            else if (model_name.Equals("Critical_Incidents_DTO"))
            {
                var lst = TablesContainer.list1.ToList<object>();

                string[] titles = new string[]
{
                            "Date","CI Form Number","CI Category Type","Location","Brief Description",
                            "MOH Notified","Police Notified","POAS Notified","Care Plan Updated","Quality Improvement Actions",
                            "MOHLTC Follow Up","CIS Initiated","Follow Up Amendments","Risk Locked" ,"File Complete"
};

                lst.Insert(0, titles);

                #region Generate CSV

                StringBuilder sb = new StringBuilder();
                for (var i = 0; i < lst.Count; i++)
                {
                    if (i == 0)
                    {
                        foreach (string data in titles)
                        {
                            //Append data with comma(,) separator.
                            sb.Append(data + ',');
                        }
                    }
                    else
                    {
                        sb.Append(lst[i]);
                    }
                    //Append new line character.
                    sb.Append("\r\n");
                }

                #endregion

                #region Download CSV

                return File(Encoding.ASCII.GetBytes(sb.ToString()), "text/csv", "Critical Incidents.csv");

                #endregion
            }
            else if (model_name.Equals("Privacy_Complaints_DTO"))
            {
                var lst = TablesContainer.list8.ToList<object>();

                var headers = new string[]
 {
                            "Location","Status","Date Complain Received","Complain Filed By","Type of Complaint",
                            "Is Complaint Resolved","Description Outcome"
 };

                lst.Insert(0, headers);

                #region Generate CSV

                StringBuilder sb = new StringBuilder();
                for (var i = 0; i < lst.Count; i++)
                {
                    if (i == 0)
                    {
                        foreach (var data in headers)
                        {
                            //Append data with comma(,) separator.
                            sb.Append(data + ',');
                        }
                    }
                    else
                    {
                        sb.Append(lst[i]);
                    }
                    //Append new line character.
                    sb.Append("\r\n");
                }

                #endregion

                #region Download CSV

                return File(Encoding.ASCII.GetBytes(sb.ToString()), "text/csv", "Privacy Complaints.csv");

                #endregion
            }
            else if (model_name.Equals("LicensingInspectionDTO"))
            {
                var lst = TablesContainer.list15.ToList<object>();

                var headers = new string[]
{
                            "Location","Date","Inspection/Complaint #", "Inspection Type/Reason",
                               "No Findings",
                            "Contraventions", "Community Care Living Action", "Residential Care Regulation Section", "Residential Care Regulation Subsection",
                            "Brief Description of Contravention","Action Plan","Responsibility","Action Date"
};

                lst.Insert(0, headers);

                #region Generate CSV

                StringBuilder sb = new StringBuilder();
                for (var i = 0; i < lst.Count; i++)
                {
                    if (i == 0)
                    {
                        foreach (var data in headers)
                        {
                            //Append data with comma(,) separator.
                            sb.Append(data + ',');
                        }
                    }
                    else
                    {
                        sb.Append(lst[i]);
                    }
                    //Append new line character.
                    sb.Append("\r\n");
                }

                #endregion

                #region Download CSV

                return File(Encoding.ASCII.GetBytes(sb.ToString()), "text/csv", "Licensing Inspections.csv");

                #endregion
            }
            else if (model_name.Equals("WorkshopBCInspection_DTO"))
            {
                var lst = TablesContainer.list17.ToList<object>();

                var headers = new string[]
{
                            "Location","Date","Inspection Report #", "Scope Of Inspections","No Orders", "Regulations Cited",
                            "Brief Description of the Findings of the Order", "Action Plan",
                            "Responsibility","Action Date", "Status of the Order"
};

                lst.Insert(0, headers);

                #region Generate CSV

                StringBuilder sb = new StringBuilder();
                for (var i = 0; i < lst.Count; i++)
                {
                    if (i == 0)
                    {
                        foreach (var data in headers)
                        {
                            //Append data with comma(,) separator.
                            sb.Append(data + ',');
                        }
                    }
                    else
                    {
                        sb.Append(lst[i]);
                    }
                    //Append new line character.
                    sb.Append("\r\n");
                }

                #endregion

                #region Download CSV

                return File(Encoding.ASCII.GetBytes(sb.ToString()), "text/csv", "Worksafe BC Inspections.csv");

                #endregion
            }
            else if (model_name.Equals("AssistedLivingInspectionDTO"))
            {
                var lst = TablesContainer.list16.ToList<object>();

                var headers = new string[]
                        {
                            "Location","Date","Inspection/Complaint # (where applicable)", "Inspection Type/Reason",
                               "No Findings", "Assisted Living Regulation", "Community Care and Assisted Living Act",
                            "Section of the Act or Regulation", "Subsection of the Act or Regulation", "Category",
                            "Brief Description of the Finding/Observation", "Action Plan", "Responsibility",
                            "Action Date"
                        };

                lst.Insert(0, headers);

                #region Generate CSV

                StringBuilder sb = new StringBuilder();
                for (var i = 0; i < lst.Count; i++)
                {
                    if (i == 0)
                    {
                        foreach (var data in headers)
                        {
                            //Append data with comma(,) separator.
                            sb.Append(data + ',');
                        }
                    }
                    else
                    {
                        sb.Append(lst[i]);
                    }
                    //Append new line character.
                    sb.Append("\r\n");
                }

                #endregion

                #region Download CSV

                return File(Encoding.ASCII.GetBytes(sb.ToString()), "text/csv", "Assisted Living Inspections.csv");

                #endregion
            }
            else if (model_name.Equals("BC_LTC_Reportable_Incidents_DTO"))
            {
                var lst = TablesContainer.list19.ToList<object>();

                var headers = new string[]
{
                            "Location","Date of Incident","Incident Type",
                            "Brief Description of the Incident",
                            "Brief Description of the Actions Taken","Notifications"
};

                lst.Insert(0, headers);

                #region Generate CSV

                StringBuilder sb = new StringBuilder();
                for (var i = 0; i < lst.Count; i++)
                {
                    if (i == 0)
                    {
                        foreach (var data in headers)
                        {
                            //Append data with comma(,) separator.
                            sb.Append(data + ',');
                        }
                    }
                    else
                    {
                        sb.Append(lst[i]);
                    }
                    //Append new line character.
                    sb.Append("\r\n");
                }

                #endregion

                #region Download CSV

                return File(Encoding.ASCII.GetBytes(sb.ToString()), "text/csv", "Reportable Incidents LTC.csv");

                #endregion
            }
            else if (model_name.Equals("BC_Assisted_Living_Reportable_Incidents_DTO"))
            {
                var lst = TablesContainer.list20.ToList<object>();

                var headers = new string[]
{
                            "Location","Date of Incident","Incident Type",
                            "Brief Description of the Incident",
                            "Brief Description of the Actions Taken","Notifications"
};

                lst.Insert(0, headers);

                #region Generate CSV

                StringBuilder sb = new StringBuilder();
                for (var i = 0; i < lst.Count; i++)
                {
                    if (i == 0)
                    {
                        foreach (var data in headers)
                        {
                            //Append data with comma(,) separator.
                            sb.Append(data + ',');
                        }
                    }
                    else
                    {
                        sb.Append(lst[i]);
                    }
                    //Append new line character.
                    sb.Append("\r\n");
                }

                #endregion

                #region Download CSV

                return File(Encoding.ASCII.GetBytes(sb.ToString()), "text/csv", "Reportable Incidents Assisted Living.csv");

                #endregion
            }
            return null;
        }
        #endregion

        #region All Forms:

        void IsEmptyList()
        {

        }
        public static SelectList GetFormNames()
        {
            List<WorTabs> forms = new List<WorTabs>()
            {
                new WorTabs{Id=1, Name = "1. Critical Incidents"},
                new WorTabs{Id=2,Name = "2. Complaints"},
                new WorTabs { Id=3,Name = "3. Good News" },
                new WorTabs {Id=4, Name = "4. Emergency Prep" },
                new WorTabs {Id=5, Name = "5. Community Risks or Legal" },
                new WorTabs {Id=6, Name = "6. Visits by Other Agencies" },
                new WorTabs {Id=7, Name = "7a. Privacy Breaches" },
                new WorTabs {Id=8, Name = "7b. Privacy Complaints" },
                new WorTabs {Id=9, Name = "8. Education" },
                new WorTabs {Id=10, Name = "9. Labour Relations" },
                new WorTabs {Id=11, Name = "10. Immunization" },
                new WorTabs {Id=12, Name = "11. Outbreak" },
                new WorTabs {Id=13, Name = "12a. WSIB" },
                new WorTabs {Id=14, Name = "12b. Not WSIB" },
            };
            return new SelectList(forms, "Id", "Name");
        }
        #endregion

        #region Get Tables methods:
        /// <summary>
        /// For Summary handler
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        ArrayList FillOutTableById(int id, bool onList = false)
        {
            int regionNumber = 0;
            if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                regionNumber = GetCurrUser().Region;
            ArrayList tableList = new ArrayList();
            if (id != 0)
            {
                switch (id)
                {
                    case 1:
                        if (CurrentLocalUser.Role == Role.SuperUser.ToString())
                            TablesContainer.list1 = db.ReadIncidents().ToList();
                        else if (CurrentLocalUser.Role == Role.User.ToString())
                            TablesContainer.list1 = db.ReadIncidents().Where(i => i.Location == localLocation).ToList();
                        else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                        {
                            if (!onList)
                                if (regionNumber != (int)Region.NoN && regionNumber != 0)
                                {
                                    string[] regions = Init.GetLocByRegion(regionNumber);
                                    var arrayList =
                                        RegionService.GetListByRegion(new Critical_Incidents_DTO(), regionNumber, db, HomeController.SelectRegions);
                                    foreach (var obj in arrayList)
                                        TablesContainer.list1.Add((Critical_Incidents_DTO)obj);
                                }
                        }
                        else // if Admin role
                            TablesContainer.list1 = db.ReadIncidents().ToList();
                        tableList.AddRange(TablesContainer.list1);
                        break;
                    case 2:
                        if (CurrentLocalUser.Role == Role.SuperUser.ToString())
                            TablesContainer.list2 = db.ReadComplaints().ToList();
                        else if (CurrentLocalUser.Role == Role.User.ToString())
                            TablesContainer.list2 = db.ReadComplaints().Where(i => i.Location == localLocation).ToList();
                        else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                        {
                            if (!onList)
                                if (regionNumber != (int)Region.NoN && regionNumber != 0)
                                {
                                    string[] regions = Init.GetLocByRegion(regionNumber);
                                    var arrayList =
                                        RegionService.GetListByRegion(new Complaint_DTO(), regionNumber, db, HomeController.SelectRegions);
                                    foreach (var obj in arrayList)
                                        TablesContainer.list2.Add((Complaint_DTO)obj);
                                }
                        }
                        else
                            TablesContainer.list2 = db.ReadComplaints().ToList();                          
                        tableList.AddRange(TablesContainer.list2);
                        break;
                    case 3:
                        if (CurrentLocalUser.Role == Role.SuperUser.ToString())
                             TablesContainer.list3 = db.ReadNews().ToList();
                        else if (CurrentLocalUser.Role == Role.User.ToString())
                             TablesContainer.list3 = db.ReadNews().Where(i => i.Location == localLocation).ToList();
                        else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                        {
                            if (!onList)
                                if (regionNumber != (int)Region.NoN && regionNumber != 0)
                                {
                                    string[] regions = Init.GetLocByRegion(regionNumber);
                                    var arrayList =
                                        RegionService.GetListByRegion(new Good_News_DTO(), regionNumber, db, HomeController.SelectRegions);
                                    foreach (var obj in arrayList)
                                        TablesContainer.list3.Add((Good_News_DTO)obj);
                                }
                        }
                        else
                            TablesContainer.list3 = db.ReadNews().ToList();
                        tableList.AddRange(TablesContainer.list3);
                        break;
                    case 4:
                        if (CurrentLocalUser.Role == Role.SuperUser.ToString())
                             TablesContainer.list4 = db.ReadEmergency().ToList();
                        else if (CurrentLocalUser.Role == Role.User.ToString())
                             TablesContainer.list4 = db.ReadEmergency().Where(i => i.Location == localLocation).ToList();
                        else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                        {
                            if (!onList)
                                if (regionNumber != (int)Region.NoN && regionNumber != 0)
                                {
                                    string[] regions = Init.GetLocByRegion(regionNumber);
                                    var arrayList =
                                        RegionService.GetListByRegion(new Emergency_Prep_DTO(), regionNumber, db, HomeController.SelectRegions);
                                    foreach (var obj in arrayList)
                                        TablesContainer.list4.Add((Emergency_Prep_DTO)obj);
                                }
                        }
                        else TablesContainer.list4 = db.ReadEmergency().ToList();
                        tableList.AddRange(TablesContainer.list4);
                        break;
                    case 5:
                        if (CurrentLocalUser.Role == Role.SuperUser.ToString())
                           TablesContainer.list5 = db.ReadRisks().ToList();
                        else if (CurrentLocalUser.Role == Role.User.ToString())
                           TablesContainer.list5 = db.ReadRisks().Where(i => i.Location == localLocation).ToList();
                        else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                        {
                            if (!onList)
                                if (regionNumber != (int)Region.NoN && regionNumber != 0)
                                {
                                    string[] regions = Init.GetLocByRegion(regionNumber);
                                    var arrayList =
                                        RegionService.GetListByRegion(new Community_Risks_DTO(), regionNumber, db, HomeController.SelectRegions);
                                    foreach (var obj in arrayList)
                                        TablesContainer.list5.Add((Community_Risks_DTO)obj);
                                }
                        }
                        else
                            TablesContainer.list5 = db.ReadRisks().ToList();
                        tableList.AddRange(TablesContainer.list5);
                        break;
                    #region not usage..
                    case 6: // 
                        List<Visits_Others_DTO> tbl6 = null;
                        if (CurrentLocalUser.Role == Role.SuperUser.ToString())
                            tbl6 = db.ReadOthers().ToList();
                        else if (CurrentLocalUser.Role == Role.User.ToString()) tbl6 = db.ReadOthers().Where(i => i.Location == localLocation).ToList();
                        else
                            tbl6 = db.ReadOthers().ToList();
                        tableList.AddRange(tbl6);
                        break;
                    case 15: // not usage..
                        List<OtherDTO> tbl15 = null;
                        if (CurrentLocalUser.Role == Role.SuperUser.ToString())
                            tbl15 = db.ReadOthersO().ToList();
                        else if (CurrentLocalUser.Role == Role.User.ToString())
                            tbl15 = db.ReadOthersO().Where(i => i.Location == localLocation).ToList();
                        else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                            tbl15 = db.ReadOthersO().ToList();
                        else
                            tbl15 = db.ReadOthersO().ToList();
                        tableList.AddRange(tbl15);
                        break;
                    case 20: // not usage..
                        List<Immunization_DTO> tbl11 = null;
                        if (CurrentLocalUser.Role == Role.SuperUser.ToString())
                            tbl11 = db.ReadImmunizations().ToList();
                        else tbl11 = db.ReadImmunizations().Where(i => i.Location == localLocation).ToList();
                        tableList.AddRange(tbl11);
                        break;
                    #endregion
                    case 16:
                        if (CurrentLocalUser.Role == Role.SuperUser.ToString())
                            TablesContainer.list7 = db.ReadBreaches().ToList();
                        else if (CurrentLocalUser.Role == Role.User.ToString())
                            TablesContainer.list7 = db.ReadBreaches().Where(i => i.Location == localLocation).ToList();
                        else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                        {
                            if (!onList)
                                if (regionNumber != (int)Region.NoN && regionNumber != 0)
                                {
                                    string[] regions = Init.GetLocByRegion(regionNumber);
                                    var arrayList =
                                        RegionService.GetListByRegion(new Privacy_Breaches_DTO(), regionNumber, db, HomeController.SelectRegions);
                                    foreach (var obj in arrayList)
                                        TablesContainer.list7.Add((Privacy_Breaches_DTO)obj);
                                }
                        }
                        else
                           TablesContainer.list7 = db.ReadBreaches().ToList();
                        tableList.AddRange(TablesContainer.list7);
                        break;
                    case 17:
                        if (CurrentLocalUser.Role == Role.SuperUser.ToString())
                            TablesContainer.list8 = db.ReadPComplaints().ToList();
                        else if (CurrentLocalUser.Role == Role.User.ToString())
                           TablesContainer.list8 = db.ReadPComplaints().Where(i => i.Location == localLocation).ToList();
                        else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                        {
                            if (!onList)
                                if (regionNumber != (int)Region.NoN && regionNumber != 0)
                                {
                                    string[] regions = Init.GetLocByRegion(regionNumber);
                                    var arrayList =
                                        RegionService.GetListByRegion(new Privacy_Complaints_DTO(), regionNumber, db, HomeController.SelectRegions);
                                    foreach (var obj in arrayList)
                                        TablesContainer.list8.Add((Privacy_Complaints_DTO)obj);
                                }
                        }
                        else TablesContainer.list8 = db.ReadPComplaints().ToList();
                        tableList.AddRange(TablesContainer.list8);
                        break;
                    case 18: // NoN 
                        List<Education_DTO> tbl18 = null;
                        if (CurrentLocalUser.Role == Role.SuperUser.ToString())
                            tbl18 = db.ReadEducation().ToList();
                        else if (CurrentLocalUser.Role == Role.User.ToString())
                            tbl18 = db.ReadEducation().Where(i => i.Location == localLocation).ToList();
                        else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                            tbl18 = TablesContainer.list9;
                        else
                            tbl18 = db.ReadEducation().ToList();
                        tableList.AddRange(tbl18);
                        break;
                    case 19:
                        List<Labour_Relations_DTO> tbl19 = null;
                        if (CurrentLocalUser.Role == Role.SuperUser.ToString())
                            tbl19 = TablesContainer.list10 = db.ReadRelations().ToList();
                        else if (CurrentLocalUser.Role == Role.User.ToString())
                            tbl19 = TablesContainer.list10 = db.ReadRelations().Where(i => i.Location == localLocation).ToList();
                        else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                            tbl19 = TablesContainer.list10;
                        else tbl19 = TablesContainer.list10 = db.ReadRelations().ToList();
                        tableList.AddRange(tbl19);
                        break;
                    case 21:
                        if (CurrentLocalUser.Role == Role.SuperUser.ToString())
                             TablesContainer.list12 = db.ReadOutbreaks().ToList();
                        else if (CurrentLocalUser.Role == Role.User.ToString())
                             TablesContainer.list12 = db.ReadOutbreaks().Where(i => i.Location == localLocation).ToList();
                        else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                        {
                            if (!onList)
                                if (regionNumber != (int)Region.NoN && regionNumber != 0)
                                {
                                    string[] regions = Init.GetLocByRegion(regionNumber);
                                    var arrayList =
                                        RegionService.GetListByRegion(new Outbreaks_DTO(), regionNumber, db, HomeController.SelectRegions);
                                    foreach (var obj in arrayList)
                                        TablesContainer.list12.Add((Outbreaks_DTO)obj);
                                }
                        }
                        else  TablesContainer.list12 = db.ReadOutbreaks().ToList();
                        tableList.AddRange(TablesContainer.list12);
                        break;
                    case 22:
                        if (CurrentLocalUser.Role == Role.SuperUser.ToString())
                             TablesContainer.list13 = db.ReadWSiBs().ToList();
                        else if (CurrentLocalUser.Role == Role.User.ToString())
                             TablesContainer.list13 = db.ReadWSiBs().Where(i => i.Location == localLocation).ToList();
                        else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                        {
                            if (!onList)
                                if (regionNumber != (int)Region.NoN && regionNumber != 0)
                                {
                                    string[] regions = Init.GetLocByRegion(regionNumber);
                                    var arrayList =
                                        RegionService.GetListByRegion(new WSIB_DTO(), regionNumber, db, HomeController.SelectRegions);
                                    foreach (var obj in arrayList)
                                        TablesContainer.list13.Add((WSIB_DTO)obj);
                                }
                        }
                        else TablesContainer.list13 = db.ReadWSiBs().ToList();
                        tableList.AddRange(TablesContainer.list13);
                        break;
                    case 23:
                        if (CurrentLocalUser.Role == Role.SuperUser.ToString())
                            TablesContainer.list14 = db.ReadNotWSIBs().ToList();
                        else if (CurrentLocalUser.Role == Role.User.ToString())
                            TablesContainer.list14 = db.ReadNotWSIBs().Where(i => i.Location == localLocation).ToList();
                        else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                        {
                            if (!onList)
                                if (regionNumber != (int)Region.NoN && regionNumber != 0)
                                {
                                    string[] regions = Init.GetLocByRegion(regionNumber);
                                    var arrayList =
                                        RegionService.GetListByRegion(new Not_WSIBs_DTO(), regionNumber, db, HomeController.SelectRegions);
                                    foreach (var obj in arrayList)
                                        TablesContainer.list14.Add((Not_WSIBs_DTO)obj);
                                }
                        }
                        else  TablesContainer.list14 = db.ReadNotWSIBs().ToList();
                        tableList.AddRange(TablesContainer.list14);
                        break;
                    case 24:
                        if (CurrentLocalUser.Role == Role.SuperUser.ToString())
                            TablesContainer.list15 = db.ReadLiceInspect().ToList();
                        else if (CurrentLocalUser.Role == Role.User.ToString())
                            TablesContainer.list15 = db.ReadLiceInspect().Where(i => i.CareComName == localLocation).ToList();
                        else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                        {
                            if (!onList)
                                if (regionNumber != (int)Region.NoN && regionNumber != 0)
                                {
                                    string[] regions = Init.GetLocByRegion(regionNumber);
                                    var arrayList =
                                        RegionService.GetListByRegion(new LicensingInspectionDTO(), regionNumber, db, HomeController.SelectRegions);
                                    foreach (var obj in arrayList)
                                        TablesContainer.list15.Add((LicensingInspectionDTO)obj);
                                }
                        }
                        else
                             TablesContainer.list15 = db.ReadLiceInspect().ToList();
                        tableList.AddRange(TablesContainer.list15);
                        break;
                    case 25:
                        if (CurrentLocalUser.Role == Role.SuperUser.ToString())
                            TablesContainer.list16 = db.ReadAssLivInspect().ToList();
                        else if (CurrentLocalUser.Role == Role.User.ToString())
                            TablesContainer.list16 = db.ReadAssLivInspect().Where(i => i.CareComName == localLocation).ToList();
                        else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                        {
                            if (!onList)
                                if (regionNumber != (int)Region.NoN && regionNumber != 0)
                                {
                                    string[] regions = Init.GetLocByRegion(regionNumber);
                                    var arrayList =
                                        RegionService.GetListByRegion(new AssistedLivingInspectionDTO(), regionNumber, db, HomeController.SelectRegions);
                                    foreach (var obj in arrayList)
                                        TablesContainer.list16.Add((AssistedLivingInspectionDTO)obj);
                                }
                        }
                        else TablesContainer.list16 = db.ReadAssLivInspect().ToList();
                        tableList.AddRange(TablesContainer.list16);
                        break;
                    case 26:
                        if (CurrentLocalUser.Role == Role.SuperUser.ToString())
                             TablesContainer.list17 = db.ReadWorkshopBCInspect().ToList();
                        else if (CurrentLocalUser.Role == Role.User.ToString())
                             TablesContainer.list17 = db.ReadWorkshopBCInspect().Where(i => i.CareComName == localLocation).ToList();
                        else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                        {
                            if (!onList)
                                if (regionNumber != (int)Region.NoN && regionNumber != 0)
                                {
                                    string[] regions = Init.GetLocByRegion(regionNumber);
                                    var arrayList =
                                        RegionService.GetListByRegion(new WorkshopBCInspection_DTO(), regionNumber, db, HomeController.SelectRegions);
                                    foreach (var obj in arrayList)
                                        TablesContainer.list17.Add((WorkshopBCInspection_DTO)obj);
                                }
                        }
                        else
                            TablesContainer.list17 = db.ReadWorkshopBCInspect().ToList();
                        tableList.AddRange(TablesContainer.list17);
                        break;
                    case 27:
                        if (CurrentLocalUser.Role == Role.SuperUser.ToString())
                             TablesContainer.list18 = db.ReadQualityReviews().ToList();
                        else if (CurrentLocalUser.Role == Role.User.ToString())
                             TablesContainer.list18 = db.ReadQualityReviews().Where(i => i.CareComName == localLocation).ToList();
                        else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                        {
                            if (!onList)
                                if (regionNumber != (int)Region.NoN && regionNumber != 0)
                                {
                                    string[] regions = Init.GetLocByRegion(regionNumber);
                                    var arrayList =
                                        RegionService.GetListByRegion(new QualityReview_DTO(), regionNumber, db, HomeController.SelectRegions);
                                    foreach (var obj in arrayList)
                                        TablesContainer.list18.Add((QualityReview_DTO)obj);
                                }
                        }
                        else TablesContainer.list18 = db.ReadQualityReviews().ToList();
                        tableList.AddRange(TablesContainer.list18);
                        break;
                    case 28:
                        if (CurrentLocalUser.Role == Role.SuperUser.ToString())
                            TablesContainer.list19 = db.ReadBC_LTC().ToList();
                        else if (CurrentLocalUser.Role == Role.User.ToString())
                            TablesContainer.list19 = db.ReadBC_LTC().Where(i => i.CareCommName == localLocation).ToList();
                        else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                        {
                            if (!onList)
                                if (regionNumber != (int)Region.NoN && regionNumber != 0)
                                {
                                    string[] regions = Init.GetLocByRegion(regionNumber);
                                    var arrayList =
                                        RegionService.GetListByRegion(new BC_LTC_Reportable_Incidents_DTO(), regionNumber, db, HomeController.SelectRegions);
                                    foreach (var obj in arrayList)
                                        TablesContainer.list19.Add((BC_LTC_Reportable_Incidents_DTO)obj);
                                }
                        }
                        else TablesContainer.list19 = db.ReadBC_LTC().ToList();
                        tableList.AddRange(TablesContainer.list19);
                        break;
                    case 29:
                        if (CurrentLocalUser.Role == Role.SuperUser.ToString())
                            TablesContainer.list20 = db.ReadBC_LTCAssisted().ToList();
                        else if (CurrentLocalUser.Role == Role.User.ToString())
                             TablesContainer.list20 = db.ReadBC_LTCAssisted().Where(i => i.NameCareCommu == localLocation).ToList();
                        else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                        {
                            if (!onList)
                                if (regionNumber != (int)Region.NoN && regionNumber != 0)
                                {
                                    string[] regions = Init.GetLocByRegion(regionNumber);
                                    var arrayList =
                                        RegionService.GetListByRegion(new BC_Assisted_Living_Reportable_Incidents_DTO(), regionNumber, db, HomeController.SelectRegions);
                                    foreach (var obj in arrayList)
                                        TablesContainer.list20.Add((BC_Assisted_Living_Reportable_Incidents_DTO)obj);                                 
                                }
                        }
                        else
                            TablesContainer.list20 = db.ReadBC_LTCAssisted().ToList();
                        tableList.AddRange(TablesContainer.list20);
                        break;
                    case 30:
                        List<MOH_Inspection_DTO> tbl30 = null;
                        if (CurrentLocalUser.Role == Role.SuperUser.ToString())
                            tbl30 = db.ReadMOHInspections().ToList();
                        else if (CurrentLocalUser.Role == Role.User.ToString())
                            tbl30 = db.ReadMOHInspections().Where(i => i.Location == localLocation).ToList();
                        else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                            tbl30 = TablesContainer.list21;
                        else
                            tbl30 = db.ReadMOHInspections().ToList();
                        tableList.AddRange(tbl30);
                        break;
                    case 31://InspectionInfoInsrt()
                        List<InspectionInfo_DTO> tbl31 = null;
                        //if (CurrentLocalUser.Role == Role.SuperUser.ToString())
                        //    tbl22 = db.ReadInspectionInfos().ToList();
                        //else if (CurrentLocalUser.Role == Role.User)
                        //    tbl22 = db.ReadInspectionInfos()/*.Where(i => i.lo == localLocation)*/.ToList();
                        //else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                        //    tbl22 = db.ReadInspectionInfos().ToList();
                        //else
                        //    tbl22 = db.ReadInspectionInfos().ToList();
                        tbl31 = new List<InspectionInfo_DTO>(0);
                        tableList.AddRange(tbl31);
                        break;
                }
            }
            return tableList;
        }

        IEnumerable<IModel> GetStaticList(IModel entity)
        {
            var model = entity.GetType().Name;
            switch (model)
            {
                case "Critical_Incidents_DTO":
                    return TablesContainer.list1.ToList();
                case "Complaint_DTO":
                    return TablesContainer.list2.ToList();
                case "Good_News_DTO":
                    return TablesContainer.list3.ToList();
                case "Emergency_Prep_DTO":
                    return TablesContainer.list4.ToList();
                case "Community_Risks_DTO":
                    return TablesContainer.list5.ToList();
                case "OtherDTO":
                    return TablesContainer.list15.ToList();
                case "Privacy_Breaches_DTO":
                    return TablesContainer.list7.ToList();
                case "Privacy_Complaints_DTO":
                    return TablesContainer.list8.ToList();
                case "Education_DTO":
                    return TablesContainer.list9.ToList();
                case "Labour_Relations_DTO":
                    return TablesContainer.list10.ToList();
                case "Immunization_DTO":
                    return TablesContainer.list11.ToList();
                case "Outbreaks_DTO":
                    return TablesContainer.list12.ToList();
                case "WSIB_DTO":
                    return TablesContainer.list13.ToList();
                case "Not_WSIBs_DTO":
                    return TablesContainer.list14.ToList();
                case "LicensingInspectionDTO":
                    return TablesContainer.list15.ToList();
                case "AssistedLivingInspectionDTO":
                    return TablesContainer.list16.ToList();
                case "WorkshopBCInspection_DTO":
                    return TablesContainer.list17.ToList();
                case "QualityReview_DTO":
                    return TablesContainer.list18.ToList();
                case "BC_LTC_Reportable_Incidents_DTO":
                    return TablesContainer.list19.ToList();
                case "BC_Assisted_Living_Reportable_Incidents_DTO":
                    return TablesContainer.list20.ToList();
                case "MOH_Inspection_DTO":
                    return TablesContainer.list21.ToList();
                case "InspectionInfo_DTO":
                    return TablesContainer.list22.ToList();
            }
            return default;
        }

        public ActionResult GoToSelectFormList(string name)
        {
            var arr = name.Split(new char[] { '/' });
            string last = arr.Last();
            if (last != null)
            {
                switch (last)
                {
                    case "1":
                        return RedirectToAction("../Select/Select_Incidents");
                    case "2":
                        return RedirectToAction("../Select/Select_Complaints");
                    case "3":
                        return RedirectToAction("../Select/Select_GoodNews");
                    case "4":
                        return RedirectToAction("../Select/Select_Emergency_Prep");
                    case "5":
                        return RedirectToAction("../Select/Select_Community");
                    case "6":
                        return RedirectToAction("../Select/Select_Visits_Others");
                    case "15":
                        return RedirectToAction("../Select/Select_Others");
                    case "16":
                        return RedirectToAction("../Select/Privacy_Breaches");
                    case "17":
                        return RedirectToAction("../Select/Privacy_Complaints");
                    case "18":
                        return RedirectToAction("../Select/Education_Select");
                    case "19":
                        return RedirectToAction("../Select/Select_Labour");
                    case "20":
                        return RedirectToAction("../Select/Select_Immunization");
                    case "21":
                        return RedirectToAction("../Select/Outbreaks");
                    case "22":
                        return RedirectToAction("../Select/Select_WSIB");
                    case "23":
                        return RedirectToAction("../Select/Select_Not_WSIB");
                    case "24":
                        return RedirectToAction("../Select/Select_LiceInspect");
                    case "25":
                        return RedirectToAction("../Select/Select_AssistLivInspect");
                    case "26":
                        return RedirectToAction("../Select/Select_WorksaveBC");
                    case "27":
                        return RedirectToAction("../Select/Select_QualityReview");
                    case "28":
                        return RedirectToAction("../Select/Select_BC_LTC");
                    case "29":
                        return RedirectToAction("../Select/Select_BC_LTC_Assisted");
                }
            }
            return RedirectToAction("../Home/WOR_Tabs");
        }

        public ActionResult GoToSelectForm(int id)
        {
            if (id != 0)
            {
                switch (id)
                {
                    case 1:
                        return RedirectToAction("../Home/Insert");
                    case 2:
                        return RedirectToAction("../Home/Complaint_Insert");
                    case 3:
                        return RedirectToAction("../Home/GoodNews_Insert");
                    case 4:
                        return RedirectToAction("../Home/Emergency_Prep_Insert");
                    case 5:
                        return RedirectToAction("../Home/Community_Insert");
                    case -1:
                        return RedirectToAction("../Home/Visits_Others");
                    case 15:
                        return RedirectToAction("../Home/OtherInsert");
                    case 16:
                        return RedirectToAction("../Home/Privacy_Breaches");
                    case 17:
                        return RedirectToAction("../Home/Privacy_Complaints");
                    case 18:
                        return RedirectToAction("../Home/Education_Insert");
                    case 19:
                        return RedirectToAction("../Home/Labour_Insert");
                    case 20:
                        return RedirectToAction("../Home/Immunization_Insert");
                    case 21:
                        return RedirectToAction("../Home/Outbreaks");
                    case 22:
                        return RedirectToAction("../Home/WSIB");
                    case 23:
                        return RedirectToAction("../Home/Not_WSIB");
                    case 24:
                        return RedirectToAction("../Home/LicensingInspections");
                    case 25:
                        return RedirectToAction("../Home/AssistedLivsingInspections");
                    case 26:
                        return RedirectToAction("../Home/WorksaveBCInspect");
                    case 27:
                        return RedirectToAction("../Home/QualityReview");
                    case 28:
                        return RedirectToAction("../Home/BC_LTC_Insert");
                    case 29:
                        return RedirectToAction("../Home/BC_LTC_Assisted_Insert");
                    case 30:
                        return RedirectToAction("../Home/BC_LTC_Assisted_Insert");
                    case 31:
                        return RedirectToAction("../Home/InspectionInfoInsrt");
                }
            }
            return RedirectToAction("../Home/WOR_Tabs");
        }

        public static string GetModelNameByNum(int numTbl)
        {
            if (numTbl != 0)
            {
                switch (numTbl)
                {
                    case 1:
                        return "Critical Incidents";
                    case 2:
                        return "Complaints";
                    case 3:
                        return "Good News";
                    case 4:
                        return "Emergency Prep";
                    case 5:
                        return "Community Risks";
                    case 15:
                        return "Other";
                    case 16:
                        return "Privacy Breaches";
                    case 17:
                        return "Privacy Complaints";
                    case 18:
                        return "Education";
                    case 19:
                        return "Labour Relation";
                    case 20:
                        return "Immunization";
                    case 21:
                        return "Outbreaks";
                    case 22:
                        return "WSIB";
                    case 23:
                        return "Internal";
                    case 24:
                        return "Licensing Inspections";
                    case 25:
                        return "Assisted Living Inspections";
                    case 26:
                        return "Worksafe BC Inspections";
                    case 27:
                        return "Quality Reviews/QACR";
                    case 28:
                        return "Reportable Incidents – LTC";
                    case 29:
                        return "Reportable Incidents – Assisted Living";
                    case 31:
                        return "MOH Inspection Info";
                }
            }

            return "NoN";
        }
        #endregion

        #region Insert (Critical Incidents):
        [HttpGet]
        public ActionResult Insert()
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
            object[] objs = new object[] { list, list3, list4, list5 };
            ViewBag.locations = objs;
            return View();
        }

        [HttpPost]
        public ActionResult Insert(Critical_Incidents_DTO entity)
        {
            object[] objs = new object[] { list, list3, list4, list5 };
            ViewBag.locations = objs;
            if (entity.Brief_Description == null && entity.Care_Plan_Updated == null && entity.CIS_Initiated == null && entity.CI_Category_Type == 0 &&
               entity.CI_Form_Number == null && entity.Date == null && entity.File_Complete == null && entity.Follow_Up_Amendments == null &&
               entity.Location == 0 && entity.MOHLTC_Follow_Up == null && entity.MOH_Notified == null && entity.POAS_Notified == null && entity.Police_Notified == null &&
               entity.Quality_Improvement_Actions == null && entity.Risk_Locked == null)
            {
                ViewBag.Empty = "All fields have to be filled.";
                return View();
            }
            else if (entity.Brief_Description == null || entity.Care_Plan_Updated == null || entity.CIS_Initiated == null || entity.CI_Category_Type == 0 ||
              entity.CI_Form_Number == null || entity.Date == null || entity.File_Complete == null || entity.Follow_Up_Amendments == null ||
              entity.Location == 0 || entity.MOHLTC_Follow_Up == null || entity.MOH_Notified == null || entity.POAS_Notified == null || entity.Police_Notified == null ||
              entity.Quality_Improvement_Actions == null || entity.Risk_Locked == null)
            {

                try
                {
                    db.Insert(entity);
                    //msg_infos = ADO_NET_CRUD.Insert_Incident(entity);
                    return RedirectToAction("../Select/Select_Incidents");
                }
                catch (Exception ex) { return Json("An error has occurred. Error details: " + ex.Message); }
                // ViewBag.Empty = "Some fields are empty. Please fill it out and try again!";
                // return View();
            }
            else
            {
                try
                {
                    db.Insert(entity);
                    // msg_infos = ADO_NET_CRUD.Insert_Incident(entity);
                    return RedirectToAction("../Select/Select_Incidents");
                }
                catch (Exception ex) { return HttpNotFound(ex.Message); }
            }
        }
        #endregion

        #region Insert (Labour Insert):
        [HttpGet]
        public ActionResult Labour_Insert()
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
            ViewBag.locations = list;
            return View();
        }

        [HttpPost]
        public ActionResult Labour_Insert(Labour_Relations_DTO entity)
        {
            ViewBag.locations = list;
            if (entity.Accruals == null && entity.Category == null && entity.Date == DateTime.MinValue && entity
                .Details == null && entity.Lessons_Learned == null && entity.Location == 0 && entity.Outcome == null && entity.Status == null &&
                entity.Union == null)
            {
                return View();
            }
            else if (entity.Accruals == null || entity.Category == null || entity.Date == DateTime.MinValue || entity
                          .Details == null || entity.Lessons_Learned == null || entity.Location == 0 || entity.Outcome == null || entity.Status == null ||
                          entity.Union == null)
            {
                db.Insert(entity);
                return RedirectToAction("../Select/Select_Labour");
            }
            else
            {
                db.Insert(entity);
                return RedirectToAction("../Select/Select_Labour");
            }
        }
        #endregion

        #region Insert (Community_Insert):
        [HttpGet]
        public ActionResult Community_Insert()
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
            ViewBag.locations = new object[] { list, list17, list4 };
            return View();
        }

        [HttpPost]
        public ActionResult Community_Insert(Community_Risks_DTO entity)
        {
            ViewBag.locations = new object[] { list, list17, list4 };
            if (entity.Date == DateTime.MinValue && entity.Descriptions == null && entity.Hot_Alert == null &&
                entity.Location == 0 && entity.MOH_Visit == null && entity.Potential_Risk == null && entity.Resolved == null &&
                entity.Risk_Legal_Action == null && entity.Status_Update == null && entity.Type_Of_Risk == null)
            {
                //ViewBag.Empty = "All fields have to be filled.";
                return View();
            }
            else
            {
                db.Insert(entity);
                return RedirectToAction("../Select/Select_Community");
            }
        }
        #endregion

        #region GoodNews Insert:
        [HttpGet]
        public ActionResult GoodNews_Insert()
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
            ViewBag.locations = new object[] { list, list12, list13, list14, list15, list16 };
            return View();
        }

        [HttpPost]
        public ActionResult GoodNews_Insert(Good_News_DTO entity)
        {
            ViewBag.locations = new object[] { list, list12, list13, list14, list15, list16 };
            if (entity.Awards_Details == null && entity.Awards_Received == null && entity.Category == null && entity.Community_Inititives == null &&
                entity.Compliment == null && entity.DateNews == DateTime.MinValue && entity.Department == null && entity.Location == 0 &&
                entity.Description_Complim == null && entity.Growth == false && entity.NameAwards == null && entity.Passion == false &&
                entity.ReceivedFrom == null && entity.Respect == false && entity.Responsibility == false && entity.SourceCompliment == null &&
                entity.Spot_Awards == null && entity.Teamwork == false)
            {
                //ViewBag.Empty = "All fields have to be filled.";
                return View();
            }
            else
            {
                //if(entity.DateNews == DateTime.MinValue) { entity.DateNews = DateTime.Now; }
                localLocation = entity.Location;
                db.Insert(entity);
                return RedirectToAction("../Select/Select_GoodNews");
            }
        }
        #endregion

        #region Visits Agency Insert:
        [HttpGet]
        public ActionResult Agency_Insert()
        {
            ViewBag.locations = new object[] { list, list18, list19, list4 };
            return View();
        }

        [HttpPost]
        public ActionResult Agency_Insert(Visits_Agency_DTO entity)
        {
            ViewBag.locations = new object[] { list, list18, list19, list4 };

            db.Insert(entity);
            return RedirectToAction("../Select/Select_Agencies");
        }
        #endregion

        #region WSiB Insert:
        [HttpGet]
        public ActionResult WSIB()
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
            ViewBag.locations = list;
            return View();
        }

        [HttpPost]
        public ActionResult WSIB(WSIB_DTO entity)
        {
            ViewBag.locations = list;
            if (entity.Location.Equals(0))
                return View();
            else
                db.Insert(entity);
            return RedirectToAction("../Select/Select_WSIB");
        }
        #endregion

        #region Not WSiB Insert:
        [HttpGet]
        public ActionResult Not_WSIB()
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
            ViewBag.locations = list;
            return View();
        }

        [HttpPost]
        public ActionResult Not_WSIB(Not_WSIBs_DTO entity)
        {
            ViewBag.locations = list;
            if (entity.Location.Equals(0))
                return View();
            else
                db.Insert(entity);
            return RedirectToAction("../Select/Select_Not_WSIB");
        }
        #endregion

        #region Care Community Insert:
        [HttpGet]
        public ActionResult Care_Community()
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
            return View();
        }

        [HttpPost]
        public ActionResult Care_Community(Home_DTO entity)
        {
            ViewBag.locations = list;

            db.Insert(entity);
            return RedirectToAction("../Home/Index");
        }
        #endregion

        #region Position Insert:
        [HttpGet]
        public ActionResult Add_Position()
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
            return View();
        }

        [HttpPost]
        public ActionResult Add_Position(Position_DTO entity)
        {
            ViewBag.locations = list;
            if (ModelState.IsValid)
            {
                db.Insert(entity);
                return RedirectToAction("../Home/Index");
            }
            return View();
        }
        #endregion

        #region Vosit Others Insert:
        [HttpGet]
        public ActionResult Visits_Others()
        {
            ViewBag.locations = new object[] { list, list18, list19, list4 };
            return View();
        }

        [HttpPost]
        public ActionResult Visits_Others(Visits_Others_DTO entity)
        {
            ViewBag.locations = new object[] { list, list18, list19, list4 };

            db.Insert(entity);
            return RedirectToAction("../Select/Select_Visits_Others");
        }
        #endregion

        #region Outbreaks Insert:
        [HttpGet]
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
            ViewBag.locations = list;
            return View();
        }

        [HttpPost]
        public ActionResult Outbreaks(Outbreaks_DTO entity)
        {
            ViewBag.locations = list;
            if (entity.Location.Equals(0))
                return View();
            else
                db.Insert(entity);
            return RedirectToAction("../Select/Outbreaks");
        }
        #endregion

        #region Immunization Insert:
        public ActionResult Immunization_Insert()
        {
            ViewBag.locations = list;
            return View();
        }

        [HttpPost]
        public ActionResult Immunization_Insert(Immunization_DTO entity)
        {
            ViewBag.locations = list;
            if (entity.Location.Equals(0))
                return View();
            else
                db.Insert(entity);
            return RedirectToAction("../Select/Select_Immunization");
        }
        #endregion

        #region Privacy Breaches Insert:
        [HttpGet]
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
            ViewBag.locations = list;
            return View();
        }

        [HttpPost]
        public ActionResult Privacy_Breaches(Privacy_Breaches_DTO entity)
        {
            ViewBag.locations = list;
            if (entity.Location.Equals(0) || entity.Date_Breach_Occurred.Equals(DateTime.MinValue))
                return View();
            else
                db.Insert(entity);
            return RedirectToAction("../Select/Privacy_Breaches");
        }
        #endregion

        #region Privacy Complaints Insert:
        [HttpGet]
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
            ViewBag.locations = list;
            return View();
        }

        [HttpPost]
        public ActionResult Privacy_Complaints(Privacy_Complaints_DTO entity)
        {

            ViewBag.locations = list;
            if (entity.Location.Equals(0))
                return View();
            else
                db.Insert(entity);
            return RedirectToAction("../Select/Privacy_Complaints");
        }
        #endregion

        #region Education Insert:
        [HttpGet]
        public ActionResult Education_Insert()
        {
            ViewBag.locations = list;
            return View();
        }

        [HttpPost]
        public ActionResult Education_Insert(Education_DTO entity)
        {
            ViewBag.locations = list;
            if (entity.DateStart.Equals(DateTime.MinValue) || entity.Location.Equals(0))
                return View();
            else
                db.Insert(entity);
            return RedirectToAction("../Select/Education_Select");
        }
        #endregion

        #region Emergency Prep Insert:
        [HttpGet]
        public ActionResult Emergency_Prep_Insert()
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
            ViewBag.locations = new object[] { list, list23, list24, list25 };
            return View();
        }

        [HttpPost]
        public ActionResult Emergency_Prep_Insert(Emergency_Prep_DTO entity)
        {
            ViewBag.locations = new object[] { list, list23, list24, list25 };

            if (entity.Location.Equals(0))
                return View();
            else
                db.Insert(entity);
            return RedirectToAction("../Select/Select_Emergency_Prep");
        }
        #endregion

        #region BC_LTC_Reportable_Incidents
        [HttpGet]
        public ActionResult BC_LTC_Insert()
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
            object[] objs = new object[] { list, list21, list22 };
            ViewBag.locations = objs;
            return View();
        }

        [HttpPost]
        public ActionResult BC_LTC_Insert(BC_LTC_Reportable_Incidents_DTO entity)
        {
            object[] objs = new object[] { list, list21, list22 };
            ViewBag.locations = objs;
            if (ModelState.IsValid)
            {
                db.Insert(entity);
                return RedirectToAction("../Select/Select_BC_LTC");
            }
            else return View();
        }
        #endregion

        #region BC_LTC_Assisted_Incidents
        [HttpGet]
        public ActionResult BC_LTC_Assisted_Insert()
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
            object[] objs = new object[] { list, list21, list29 };
            ViewBag.locations = objs;
            return View();
        }

        [HttpPost]
        public ActionResult BC_LTC_Assisted_Insert(BC_Assisted_Living_Reportable_Incidents_DTO entity)
        {
            object[] objs = new object[] { list, list21, list29 };
            ViewBag.locations = objs;
            if (ModelState.IsValid)
            {
                db.Insert(entity);
                return RedirectToAction("../Select/Select_BC_LTC_Assisted");
            }
            else return View();
        }
        #endregion

        #region Other(Insert):
        [HttpGet]
        public ActionResult OtherInsert()
        {
            ViewBag.locations = list;
            return View();
        }

        [HttpPost]
        public ActionResult OtherInsert(OtherDTO entity)
        {
            ViewBag.locations = list;
            if (ModelState.IsValid)
            {
                db.Insert(entity);
                return RedirectToAction("../Select/Select_Others");
            }
            else return View();
        }
        #endregion

        #region Licensing Inspections:
        [HttpGet]
        public ActionResult LicensingInspections()
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
            { ViewBag.TypeReason = HomeController.list26; }
            ViewBag.locations = list;
            return View();
        }

        [HttpPost]
        public ActionResult LicensingInspections(LicensingInspectionDTO entity)
        {
            ViewBag.locations = list;
            { ViewBag.TypeReason = HomeController.list26; }
            if (entity.CareComName.Equals(0))
                return View();
            else
                db.Insert(entity);
            return RedirectToAction("../Select/Select_LiceInspect");
        }
        #endregion

        #region Assisted Living Inspections:
        [HttpGet]
        public ActionResult AssistedLivsingInspections()
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
            { ViewBag.TypeReason = HomeController.list26; }
            ViewBag.locations = list;
            return View();
        }

        [HttpPost]
        public ActionResult AssistedLivsingInspections(AssistedLivingInspectionDTO entity)
        {
            { ViewBag.TypeReason = HomeController.list26; }
            ViewBag.locations = list;
            if (entity.CareComName.Equals(0))
                return View();
            else
            {
                db.Insert(entity);
            }
            return RedirectToAction("../Select/Select_AssistLivInspect");
        }
        #endregion

        #region Worksafe BC Inspections:
        [HttpGet]
        public ActionResult WorksaveBCInspect()
        {
            CheckAlreadyUser();
            { ViewBag.ScopeInspect = HomeController.list28; }
            ViewBag.locations = list;
            return View();
        }

        [HttpPost]
        public ActionResult WorksaveBCInspect(WorkshopBCInspection_DTO entity)
        {
            { ViewBag.ScopeInspect = HomeController.list28; }
            ViewBag.locations = list;
            if (entity.CareComName.Equals(0))
                return View();
            else
                db.Insert(entity);
            return RedirectToAction("../Select/Select_WorksaveBC");
        }
        #endregion

        #region Quality Reviews/QACR:
        [HttpGet]
        public ActionResult QualityReview()
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
            ViewBag.locations = list;
            return View();
        }

        [HttpPost]
        public ActionResult QualityReview(QualityReview_DTO entity)
        {
            ViewBag.locations = list;
            if (entity.CareComName.Equals(0))
                return View();
            else
                db.Insert(entity);
            return RedirectToAction("../Select/Select_QualityReview");
        }
        #endregion

        #region MOH Inspections [no implement]:
        [HttpGet]
        public ActionResult MOH_Inspections()
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
            ViewBag.locations = list;
            return View();
        }

        [HttpPost]
        public ActionResult MOH_Inspections(MOH_Inspection_DTO entity)
        {
            ViewBag.locations = list;
            if (entity.Location.Equals(0))
                return View();
            else
                db.Insert(entity);
            return RedirectToAction("../Select/Select_MOHInspections");
        }
        #endregion

        #region Inspection Info:
        static int count_form = 1;
        static bool success = false;

        public ActionResult InspectionInfoInsrt(string[] data)
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
            ViewBag.Check = check;
            if (data != default)
            {
                if (data[0] == "11")
                {
                    int selectedNumber1 = -1;
                    object[] objs1 = new object[]
                    {
                        new SelectList(AppSettings.inspectTypes, "Id", "Name"),
                        new SelectList(db.ReadHomes().ToList(), "Id", "Full_Home_Name"),
                        new SelectList(db.ReadNonCompleances(), "Id", "Name")
                    };
                    ViewBag.List = objs1;

                    var _1stcombo1 = new SelectList(db.ReadLTCHARegs(), "Id", "Name", selectedNumber1);
                    ViewBag._1st = _1stcombo1;

                    var _2ndcombo1 = new SelectList(db.ReadSections().Where(c => c.LTCHARegId == selectedNumber1), "Id", "Name");
                    ViewBag._2nd = _2ndcombo1;

                    var _3rtcombo1 = new SelectList(db.ReadSubsections().Where(s => s.SectionId == selectedNumber1), "Id", "Name");
                    ViewBag._3rd = _3rtcombo1;

                    var _4thcombo1 = new SelectList(db.ReadOtherOptions().Where(o => o.SubsectionId == selectedNumber1), "Id", "Name");
                    ViewBag._4th = _4thcombo1;

                    count_form++;
                    ViewBag.CountForm = count_form;

                    // When we call adition form:
                    List<string> names1 = new List<string>();
                    path = Server.MapPath("~/Uploaded_Files");
                    string[] files_names1 = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
                    if (files_names1.Length != 0)
                    {
                        string[] seperated;
                        check = true;
                        ViewBag.Check = check;
                        for (int i = 0; i < files_names1.Length; i++)
                        {
                            seperated = files_names1[i].Split('\\');
                            //names.Add(Path.GetFileName(files_names[i])); 
                            names1.Add(seperated[seperated.Length - 1]);
                            seperated = null;
                        }
                        ViewBag.Files = names1;
                    }
                    else
                    {
                        ViewBag.Check = check;
                        ViewBag.Empty = "Folder doesn't any files...It is Empty! Try to upload any file.";
                    }
                    if (success) { ViewBag.Success = "The record was inserted successfuly!"; }
                    return View();
                }
            }

            List<string> names = new List<string>();
            path = Server.MapPath("~/Uploaded_Files");
            string[] files_names = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
            if (files_names.Length != 0)
            {
                string[] seperated;
                check = true;
                ViewBag.Check = check;
                for (int i = 0; i < files_names.Length; i++)
                {
                    seperated = files_names[i].Split('\\');
                    //names.Add(Path.GetFileName(files_names[i])); 
                    names.Add(seperated[seperated.Length - 1]);
                    seperated = null;
                }

                ViewBag.Files = names;
            }
            else
            {
                ViewBag.Check = check;
                ViewBag.Empty = "This folder doesn't contain any file/s.";
            }

            if (success) { ViewBag.Success = "The record was inserted successfuly!"; }
            int selectedNumber = -1;

            ViewBag.CountForm = count_form;

            object[] objs = new object[]
                    {
                        new SelectList(AppSettings.inspectTypes, "Id", "Name"),
                        new SelectList(db.ReadHomes().ToList(), "Id", "Full_Home_Name"),
                        new SelectList(db.ReadNonCompleances(), "Id", "Name")
                    };
            ViewBag.List = objs;

            var _1stcombo = new SelectList(db.ReadLTCHARegs(), "Id", "Name", selectedNumber);
            ViewBag._1st = _1stcombo;

            var _2ndcombo = new SelectList(db.ReadSections().Where(c => c.LTCHARegId == selectedNumber), "Id", "Name");
            ViewBag._2nd = _2ndcombo;

            var _3rtcombo = new SelectList(db.ReadSubsections().Where(s => s.SectionId == selectedNumber), "Id", "Name");
            ViewBag._3rd = _3rtcombo;

            var _4thcombo = new SelectList(db.ReadOtherOptions().Where(o => o.SubsectionId == selectedNumber), "Id", "Name");
            ViewBag._4th = _4thcombo;

            return View();
        }

        [HttpPost]
        public ActionResult InspectionInfoInsrt(InspectionInfo_DTO model)
        {
            string btnName = Request.Params
                     .Cast<string>()
                     .Where(p => p.StartsWith("btn"))
                     .Select(p => p.Substring("btn".Length))
                     .First();
            if (btnName.Equals("-submit"))
            {
                success = true;
                try
                {
                    if (model != null)
                    {
                        //if (model.ClearedByInspectionNo != null || model.HomeId != 0 || model.InspectNumber != 0.0 ||
                        //    model.Intermediate1 != null || model.Intermediate2 != null || model.Intermediate3 != null || model.Responsibility1 != null 
                        //    || model.Responsibility2 != null || model.Responsibility3 != null ||
                        //    model.TargetDate1 != DateTime.MinValue || model.TargetDate2 != DateTime.MinValue || model.TargetDate3 != DateTime.MinValue)
                        //{
                        //    ViewBag.ErrMsg = "Something went wrong...";
                        //}
                        //else
                        //{
                        model.HomeId = 1;
                        model.OtherOptionAsNeeded = "Option 4";
                        model.TypeId = 1;
                        db.Insert(model);
                        ViewBag.Success = "The Record Was Addd Successfully!";
                        //}
                    }
                }
                catch (System.Data.DataException dax) { ViewBag.ErrMsg = dax.Message; }
                return RedirectToAction("../Home/WOR_Tabs");
            }
            return View();
        }
        #region File manipulations [IO]:
        #region Upload files:
        [HttpPost]
        public ActionResult UplodedInspect()
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;

                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else fname = file.FileName;

                        path = Path.Combine(Server.MapPath($"~/Uploaded_Files/{fname}"));
                        file.SaveAs(path);
                    }

                    return Json("Your file was uploaded successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else return Json("There was no file selected. Please try again.");
        }

        static bool check = false;
        public ActionResult AllFilesInspect()
        {
            List<string> names = new List<string>();
            path = Server.MapPath("~/Uploaded_Files");
            string[] files_names = Directory.GetFiles(path, "*", SearchOption.AllDirectories);

            if (files_names.Length != 0)
            {
                check = true;
                ViewBag.Check = check;
                for (int i = 0; i < files_names.Length; i++)
                    names.Add(Path.GetFileName(files_names[i]));

                ViewBag.Files = files_names;
                return View();
            }
            else
            {
                ViewBag.Check = check;
                ViewBag.Empty = "Folder doesn't any files...It is Empty! Try to upload any file.";
                return View();
            }
        }
        #endregion

        #region Delete file:
        public ActionResult DeleteFileInspect(string item)
        {
            bool flag = false;
            if (item != null)
            {
                flag = true;
                check = false;
                ViewBag.Check = check;
                string path = Path.Combine(Server.MapPath($"~/Uploaded_Files/{item}"));
                System.IO.File.Delete(path);
            }
            if (!flag) return Json("There is nothing to delete...Please upload a file first.");
            else return RedirectToAction("../Home/InspectionInfoInsrt");
        }
        #endregion
        #endregion

        #region Auxiliary methods(get item & for part views):
        [HttpGet]
        public ActionResult GetItem(int id)
        {
            return PartialView(db.ReadSections().Where(p => p.LTCHARegId == id).ToList());
        }

        [HttpGet]
        public ActionResult GetSub1(int id)
        {
            return PartialView(db.ReadSubsections().Where(p => p.SectionId == id).ToList());
        }

        [HttpGet]
        public ActionResult GetSub2(int id)
        {
            return PartialView(db.ReadOtherOptions().Where(p => p.SubsectionId == id).ToList());
        }
        #endregion
        #endregion
    }
}