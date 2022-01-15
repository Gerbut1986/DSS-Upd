namespace DTS.Controllers
{
    using DSS.BLL;
    using System.IO;
    using DTS.Models;
    using DSS.BLL.DTO;
    using System.Data;
    using System.Linq;
    using System.Web.Mvc;
    using DSS.BLL.Services;
    using iTextSharp.text.pdf;
    using iTextSharp.text.pdf.parser;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AdminLTEController : Controller
    {
        private ApplicationUser CurrentLocalUser { get; set; }
        string text = ""; // the variable contains all contents of the pdf file
        static SelectList selList, selFilenames;
        List<Search_Word_DTO> listWords;
        List<Search_Word_DTO> fnames;
        static string path;
        ServiceDSS db;

        public AdminLTEController()
        {
            db = new ServiceDSS(Init.ConnectionStrAdm);
            listWords = db.ReadWords().ToList();
            selList = new SelectList(listWords, "Id", "Word");
        }

        public ActionResult GridBootstrapTest()
        {
            CurrentLocalUser = GetCurrUser();
            //AccountController.AllUsers.FirstOrDefault(n => n.Email.Equals(User.Identity.Name)).Region
            WorTabs tabs = null;
            { ViewBag.Names = STREAM.GetLocNames().ToArray(); }
            if (AccountController.AllUsers.FirstOrDefault(n => n.Email.Equals(User.Identity.Name)).Care_Community == 0)
            {
                { ViewBag.List = TablesContainer.list1; }
                { ViewBag.List = TablesContainer.list2; }
                { ViewBag.List = TablesContainer.list3; }
            }
            else
            {
                { ViewBag.List = TablesContainer.list1 = db.ReadIncidents().Where(l => l.Location ==AccountController.AllUsers.FirstOrDefault(n => n.Email.Equals(User.Identity.Name)).Care_Community).ToList(); }
                { ViewBag.List = TablesContainer.list2 = db.ReadComplaints().Where(l => l.Location ==AccountController.AllUsers.FirstOrDefault(n => n.Email.Equals(User.Identity.Name)).Care_Community).ToList(); }
                { ViewBag.List = TablesContainer.list3 = db.ReadNews().Where(l => l.Location ==AccountController.AllUsers.FirstOrDefault(n => n.Email.Equals(User.Identity.Name)).Care_Community).ToList(); }
            }
            if (HomeController.b)
            {
                switch (HomeController.num_tbl)
                {
                    case 1:
                        var arr1 = TablesContainer.list1.Count;

                        {
                            ViewBag.Count = arr1;
                        }

                        {
                            ViewBag.GN_Found = HomeController.strN;
                        }

                        {
                            ViewBag.ObjName = "Critical Incidents";
                        }
                        ViewBag.Check = HomeController.checkView;
                        tabs = new WorTabs();
                        tabs.ListForms = HomeController.GetFormNames();

                        return View(tabs);
                    case 2:
                        var arr2 = TablesContainer.list2.Count;

                        {
                            ViewBag.Count = arr2;
                        }

                        {
                            ViewBag.GN_Found = HomeController.strN;
                        }

                        {
                            ViewBag.ObjName = "Complaints";
                        }
                        ViewBag.Check = HomeController.checkView;
                        tabs = new WorTabs();
                        tabs.ListForms = HomeController.GetFormNames();

                        return View(tabs);
                    case 3:
                        var arr = TablesContainer.list3.Count;

                        {
                            ViewBag.Count = arr;
                        }

                        {
                            ViewBag.GN_Found = HomeController.strN;
                        }

                        {
                            ViewBag.ObjName = "Good News";
                        }
                        ViewBag.Check = HomeController.checkView;
                        tabs = new WorTabs();
                        tabs.ListForms = HomeController.GetFormNames();

                        return View(tabs);
                }
            }

            tabs = new WorTabs();
            tabs.ListForms = HomeController.GetFormNames();
            return View(tabs);
        }

        public ActionResult Index()
        {
            return View(); //review later for refactoring
        }

        public ActionResult _View()
        {
            return View(); //review later for refactoring
        }

        [HttpGet]
        public ActionResult SearchByWord() //renders the search page and a search button
        {
            CurrentLocalUser = GetCurrUser();
            List<string> names = new List<string>();
            string path = Server.MapPath("~/Uploaded_Files");
            string[] files_names = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
            List<Search_Word_DTO> fnames = new List<Search_Word_DTO>();
            for (int i = 0; i < files_names.Length; i++)
            {
                var o = new Search_Word_DTO();
                o.FileName = files_names[i];
                fnames.Add(o);
                names.Add(System.IO.Path.GetFileName(files_names[i]));
            }
            selFilenames = new SelectList(fnames, "FileName", "FileName");
            if (files_names != null || files_names.Length != 0)
            {
                object[] obgs = new object[] { selFilenames, selList };
                ViewBag.DropDown = obgs;
            }
            ViewBag.Check = true; //bool variable is true to render the page
            return View();
        }

        [HttpPost]
        public ActionResult SearchByWord(Search_Word_DTO obj) // a method to click a button and enter word/s
        {
            CurrentLocalUser = GetCurrUser();
            object[] obgs = new object[] { selFilenames, selList };
            ViewBag.DropDown = obgs;
            bool check = false;
            string word = obj.CustomersWord; //the variable takes in the searched word/s

            if((obj.FileName != null) && obj.Word == null && obj.CustomersWord == null)
            {
                path = obj.FileName;
                return RedirectToAction("../AdminLTE/Pdf_Viewer");
            }

            if (word == null) // if a drop-down list is not selected
            {
                if(obj.Word != null)
                {
                    var found = db.ReadWords().Where(i => i.Id == int.Parse(obj.Word)).SingleOrDefault();
                    string selWord = found.Word;
                    if (obj.FileName != null)
                    {
                        text = GetPDFText(obj.FileName);
                        int count = (text.Length - text.Replace(selWord, "").Length) / selWord.Length;
                        if (count == 0) ViewBag.FoundText = "The search has resulted in no word/s mataches.";
                        ViewBag.FoundText = $"The search found word/s: '{selWord}' and it is present in the text {count} time/s.";
                    }
                    else // if file name is not selected
                    {
                        ViewBag.FoundText = $"Please select a file from a menu!";
                    }
                }
                else // if custumer word & drop-down word are not selected
                {
                    if (word == null) ViewBag.Check = check;
                    ViewBag.EmptyText = "Please input any word/s into the textbox and try again!";
                    return View();
                }           
            }
            else // if we want to search by input of word
            {             
                text = GetPDFText(); //transfers all pdf contents to text from GetPDFText method
                int count = (text.Length - text.Replace(word, "").Length) / word.Length;
                if (count == 0) ViewBag.FoundText = "The search has resulted in no word/s mataches.";
                ViewBag.FoundText = $"The search found word/s: '{word}' and it is present in the text {count} time/s."; 
            }

            return View();
        }

        string IgnoreUpperChar(string word)
        {
            return null; //permanently
        }

        #region Pdf viewer (logic):
        /// <summary>
        /// View shown pdf content
        /// </summary>
        /// <returns> Action </returns>
        public ActionResult Pdf_Viewer() //shows the whole pdf file on one page
        {
            text = GetPDFText(path); //text grabs all file contents via GetPDFText method
            ViewBag.Text = text; //renders the file contents on the page
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"> The path pdf file </param>
        /// <returns> text of the pdf file </returns>
        string GetPDFText(string path = "~/Uploaded_Files/Altamont Inspection.pdf") //gets the contents of the uploaded pdf file via a path
        {
            if (path.Equals("~/Uploaded_Files/Altamont Inspection.pdf"))
            {
                string absolPath = System.IO.Path.Combine(Server.MapPath(path)); //the absolute path is received
                return PDFText(absolPath); //calls the main PDFText method
            }
            else
            {
                return PDFText(path);
            }
        }
    
        /// <summary>
        /// Using PdfReader extention
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string PDFText(string path) //the main method that retrieves the word we are searching for
        {
            PdfReader reader = new PdfReader(path);   // PdfReader is a service class from iText library
            string text = string.Empty;
            for (int page = 1; page <= reader.NumberOfPages; page++)
            {
                text = text +"\n\n" + PdfTextExtractor.GetTextFromPage(reader, page);
            }
            reader.Close();
            return text;
        }

        //public static string ReadPdfFile(object Filename, DataTable ReadLibray)
        //{
        //    PdfReader reader2 = new PdfReader((string)Filename);
        //    string strText = string.Empty;

        //    for (int page = 1; page <= reader2.NumberOfPages; page++)
        //    {
        //        ITextExtractionStrategy its = new iTextSharp.text.pdf.parser.SimpleTextExtractionStrategy();
        //        PdfReader reader = new PdfReader((string)Filename);
        //        String s = PdfTextExtractor.GetTextFromPage(reader, page, its);

        //        s = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(s)));
        //        strText = strText + s;
        //        reader.Close();
        //    }
        //    return strText;
        //}
        #endregion

        public ApplicationUser GetCurrUser() => AccountController.AllUsers.FirstOrDefault(n => n.Email.Equals(User.Identity.Name));

        public ActionResult EMERGENCY_CODE_TESTING()
        {
            CurrentLocalUser = GetCurrUser();
            List<List<EmergencyCode>> resultColorsAttr = new List<List<EmergencyCode>>();
            List<EmergencyCode> onView = new List<EmergencyCode>();
            ServiceDSS db = new ServiceDSS(Init.ConnectionStrAdm);
            STREAM stream = new STREAM(db);
            IEnumerable<EmergencyCode> sorted = default;

            if (CurrentLocalUser.Role == Role.User.ToString())
            {
                ViewBag.Role = "User";
                ViewBag.Home = STREAM.GetLocNameById(AccountController.AllUsers.FirstOrDefault(n => n.Email.Equals(User.Identity.Name)).Care_Community);
                onView = Execute(ref resultColorsAttr, CurrentLocalUser.Role.ToString(),
                    AccountController.AllUsers.FirstOrDefault(n => n.Email.Equals(User.Identity.Name)).Care_Community);
                sorted = CodeColorsSort(onView, true);
            }
            else if(CurrentLocalUser.Role == Role.RegionalUser.ToString())
            {
                int region =
                Models.RegionLogic.RegionService.GetRegion(AccountController.AllUsers.FirstOrDefault(n => n.Email.Equals(User.Identity.Name)));   
                var regPath = System.AppDomain.CurrentDomain.BaseDirectory + "Regions/Region" + region + ".txt";
                string[] rgns = System.IO.File.ReadAllLines(regPath);
                { ViewBag.ArrLocs = rgns; }
                { ViewBag.Role = "RegionalUser"; }
                ViewBag.Home = $"Regional User (Region - {region})";
                System.Collections.ArrayList listResult =
                Models.RegionLogic.RegionService.GetListByRegion(new Emergency_Prep_DTO(),region, db, HomeController.SelectRegions);
                GetDistinctListByAdmin(listResult);
                onView.Clear();
                for (int i = 0; i < locList.Count; i++)
                {
                    int locNumber = STREAM.GetIdLocByName(locList[i]);
                    onView.AddRange(Execute(ref resultColorsAttr, "User", locNumber, true)); // add2nd param 'User' to obtain each functionality from it on admin
                    onView.Add(null);
                    resultColorsAttr.Clear();
                }
                { ViewBag.Locs = locList; }
                { ViewBag.PartList = onView; }
                ViewBag.Month = GetMonth();
                return View(onView);
                ///           
            }
            else if (CurrentLocalUser.Role == Role.SuperUser.ToString())
            {
                ViewBag.Role = $"SuperUser";
                GetDistinctListByAdmin();
                onView.Clear();
                for (int i = 0; i < locList.Count; i++)
                {
                    int locNumber = STREAM.GetIdLocByName(locList[i]);
                    onView.AddRange(Execute(ref resultColorsAttr, "User", locNumber)); // add2nd param 'User' to obtain each functionality from it on admin
                    onView.Add(null);
                    resultColorsAttr.Clear();
                }
                { ViewBag.Locs = locList; }
                { ViewBag.PartList = onView; }
                ViewBag.Month = GetMonth();
                return View(onView);
            }
            else  // Admin
            {
                ViewBag.Home = $"Admin";
                GetDistinctListByAdmin();
                onView.Clear();
                for(int i = 0; i < locList.Count; i++)
                {
                    int locNumber = STREAM.GetIdLocByName(locList[i]);
                    onView.AddRange(Execute(ref resultColorsAttr, "User", locNumber)); // add2nd param 'User' to obtain each functionality from it on admin
                    onView.Add(null);
                    resultColorsAttr.Clear();
                }
                { ViewBag.Locs = locList; }
                { ViewBag.PartList = onView; }
                ViewBag.Month = GetMonth();
                return View(onView);
            }

            { ViewBag.Locs = locList; }

            { ViewBag.PartList = onView; }

            ViewBag.Month = GetMonth();
                   
            return View(sorted);
        }

        List<EmergencyCode> Execute(ref List<List<EmergencyCode>> resultColorsAttr, string role, int location, bool isRegional = false)
        {
            List<EmergencyCode> onView = new List<EmergencyCode>();
            List<Emergency_Prep_DTO> emergPreps = default;
            if (!isRegional)
            {
                emergPreps = role == "Admin" || role == "SuperUser" ? db.ReadEmergency().ToList()
                    : role == "User" ? db.ReadEmergency().Where(loc => loc.Location == location).ToList() : null;
            }
            else // if Regional
            {
                int region =
                   Models.RegionLogic.RegionService.GetRegion(AccountController.AllUsers.FirstOrDefault(n => n.Email.Equals(User.Identity.Name)));
                emergPreps = new List<Emergency_Prep_DTO>();
                emergPreps.AddRange(db.ReadEmergency().Where(l => l.Location == location));
                //System.Collections.ArrayList listResult =
                //    Models.RegionLogic.RegionService.GetListByRegion(new Emergency_Prep_DTO(), region, db, HomeController.SelectRegions);
                //for (int i = 0; i < listResult.Count; i++)
                //{
                //    //emergPreps[i] = new Emergency_Prep_DTO();
                //    emergPreps.Add((Emergency_Prep_DTO)listResult[i]);
                //}

            }

            var groupBy = emergPreps.GroupBy(c => c.Code);
            foreach (var g in groupBy)
            {
                var ret = GroupByCodeAttr(g.Key.ToString(), emergPreps);
                if (ret.Count != 0)
                    resultColorsAttr.Add(ret);
            }
                        
            if (resultColorsAttr.Count > 0)
            {
                foreach (List<EmergencyCode> lic in resultColorsAttr)
                {
                    foreach (EmergencyCode ec in lic)
                    {
                        onView.Add(new EmergencyCode { Location = ec.Location, Code = ec.Code, Exercise = ec.Exercise, Date = ec.Date });
                    }
                }
            }
            return onView;
        }

        static List<string> locList = new List<string>();
        void GetDistinctListByAdmin(System.Collections.ArrayList forRegions = default)
        {
            List<Home_DTO> listCommunity = db.ReadHomes().ToList();
            List<Emergency_Prep_DTO> list = default;
            if(forRegions == default)
                list = db.ReadEmergency().ToList();
            else
            {
                list = new List<Emergency_Prep_DTO>();
                for (int i = 0; i < forRegions.Count; i++)
                    list.Add((Emergency_Prep_DTO)forRegions[i]);
            }
            var locDistinct = new HashSet<string>();
            var locId = new List<int>();
            foreach (var it in list)
            {
                var cc = listCommunity.Where(i => i.Id == it.Location).SingleOrDefault();
                locDistinct.Add(cc.Full_Home_Name);
                locId.Add(cc.Id);
            }

            locList = locDistinct.ToList();
            locList.Sort(); // Sorted by alphanumeric
        }

        IEnumerable<EmergencyCode> CodeColorsSort(IEnumerable<EmergencyCode> list, bool isUser)
        {
            var retList = new List<EmergencyCode>();
            var colors = GetColor();
                foreach (var color in colors)
                {
                    var col = SearchByCode(color, list);
                    retList.AddRange(col);
                }
            return retList;
        }

        string[] GetColor()
        {
            return new string[]
            {
                "Red",
                "Black",
                "White",
                "Blue",
                "Green",
                "Orange",
                "Yellow",
                "Brown",
                "Gray"
            };
        }

        IEnumerable<EmergencyCode> SearchByCode(string color, IEnumerable<EmergencyCode> list) =>
            list.Where(c => c.Code == color).ToArray();

        IEnumerable<string> GetMonth()
        {
            return new List<string>
            {
                "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug",
                 "Sep", "Oct", "Nov","Dec"
            };
        }
        
        List<EmergencyCode> GroupByCodeAttr(string codeAttr, IEnumerable<Emergency_Prep_DTO> list)
        {
            switch (codeAttr)
            {
                case "Red":
                    var resList1 = WhtCodeColor(codeAttr, list);
                    return resList1;
                case "Black":
                    var resList2 = WhtCodeColor(codeAttr, list);
                    return resList2;
                case "White":
                    var resList3 = WhtCodeColor(codeAttr, list);
                    return resList3;
                case "Blue":
                    var resList4 = WhtCodeColor(codeAttr, list);
                    return resList4;
                case "Green":
                    var resList5 = WhtCodeColor(codeAttr, list);
                    return resList5;
                case "Orange":
                    var resList6 = WhtCodeColor(codeAttr, list);
                    return resList6;
                case "Yellow":
                    var resList7 = WhtCodeColor(codeAttr, list);
                    return resList7;
                case "Brown":
                    var resList8 = WhtCodeColor(codeAttr, list);
                    return resList8;
                case "Gray":
                    var resList9 = WhtCodeColor(codeAttr, list);
                    return resList9;
                default:return null;
            }
        }

        List<EmergencyCode> WhtCodeColor(string codeAttr, IEnumerable<Emergency_Prep_DTO> list)
        {
            var retList = new List<EmergencyCode>();

            var day = list.Where(code => code.Code == codeAttr).Where(ex => ex.Exercise.Equals("Day")).ToList();
            if (day != null || day.Count != 0)
            {
                foreach (var i in day)
                {
                    EmergencyCode e = new EmergencyCode
                    {
                        Location = STREAM.GetLocNameById(i.Location),
                        Code = i.Code,
                        Exercise = i.Exercise,
                        Date = i.Date
                    };
                    retList.Add(e);
                }
            }
            var evening = list.Where(code => code.Code == codeAttr).Where(ex => ex.Exercise.Equals("Evening")).ToList();
            if (evening != null || evening.Count != 0)
            {
                foreach (var i in evening)
                {
                    EmergencyCode e = new EmergencyCode
                    {
                        Location = STREAM.GetLocNameById(i.Location),
                        Code = i.Code,
                        Exercise = i.Exercise,
                        Date = i.Date
                    };
                    retList.Add(e);
                }
            }
            var night = list.Where(code => code.Code == codeAttr).Where(ex => ex.Exercise.Equals("Night")).ToList();
            if (night != null || night.Count != 0)
            {
                foreach (var i in night)
                {
                    EmergencyCode e = new EmergencyCode
                    {
                        Location = STREAM.GetLocNameById(i.Location),
                        Code = i.Code,
                        Exercise = i.Exercise,
                        Date = i.Date
                    };
                    retList.Add(e);
                }
            }
            return retList;
        }

        public class EmergencyCode
        {
            public string Location { get; set; }
            public string Code { get; set; }
            public string Exercise { get; set; }
            public System.DateTime Date { get; set; } 
        }
    }
}