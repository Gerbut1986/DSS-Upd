namespace DTS.Controllers
{
    using System.Net;
    using DTS.Models;
    using DSS.BLL.DTO;
    using System.Linq;
    using System.Web.Mvc;
    using DSS.BLL.Services;
    using System.Collections.Generic;

    public class EditController : Controller
    {
        readonly ServiceDSS db;
        private int localLocation;
        private ApplicationUser CurrentLocalUser { get; set; }

        //this is a constructor implemented via lambda expression
        public EditController() => db = new ServiceDSS(Init.ConnectionStrAdm);

        public ApplicationUser GetCurrUser() => AccountController.AllUsers.FirstOrDefault(n => n.Email.Equals(User.Identity.Name));

        [HttpGet]
        public ActionResult Edit_Incidents(int? id)
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
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var incident = db.ReadIncidents().SingleOrDefault(i => i.Id == id);           // get Critical_Incidents object by id
                                                                                         // using thwo list (Location & CI_Category_Type) from a static fields of HomeController class':
            object[] objs = new object[]
            {
                HomeController.list,
                HomeController.list3,
                HomeController.list4,
                HomeController.list5
            };
            ViewBag.locations = objs;        // add object of array to ViewBag
            if (incident == null) return HttpNotFound();
            //ViewBag.id = id;     // add 
            return View(incident);
        }

        [HttpPost]
        public ActionResult Edit_Incidents(Critical_Incidents_DTO entity)   // pass an updated Critical_Incidents object as a parameter
        {
            if (ModelState.IsValid)
            {
                db.Update(entity);    // Set property updated object to 
                return RedirectToAction("../Select/Select_Incidents"); // redirect to view List
            }
            return View(entity);   // else if our model state false or something went wrong
        }

        [HttpGet]
        public ActionResult EditEducation(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var founded = db.ReadEducation().SingleOrDefault(i => i.Id == id);
            ViewBag.locations = HomeController.list;
            if (founded == null) return HttpNotFound();
            return View(founded);
        }

        [HttpPost]
        public ActionResult EditEducation(Education_DTO entity)
        {
            if (ModelState.IsValid)
            {
                entity.DateStart = System.DateTime.Now;
                db.Update(entity);
                return RedirectToAction("../Select/Education_Select");
            }

            return View(entity);
        }

        [HttpGet]
        public ActionResult Edit_Labour(int? id)
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
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var labour = db.ReadRelations().SingleOrDefault(i => i.Id == id);
            ViewBag.locations = HomeController.list;

            if (labour == null) return HttpNotFound();

            return View(labour);
        }

        [HttpPost]
        public ActionResult Edit_Labour(Labour_Relations_DTO entity)
        {
            if (ModelState.IsValid)
            {
                db.Update(entity);
                return RedirectToAction("../Select/Select_Labour");
            }

            return View(entity);
        }

        public ActionResult Edit_Community(int? id)
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
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var edit = db.ReadRisks().SingleOrDefault(i => i.Id == id);
            ViewBag.locations = new object[] { HomeController.list, HomeController.list17, HomeController.list4 };

            if (edit == null) return HttpNotFound();

            return View(edit);
        }

        [HttpPost]
        public ActionResult Edit_Community(Community_Risks_DTO entity)
        {
            if (ModelState.IsValid)
            {
                db.Update(entity);
                return RedirectToAction("../Select/Select_Community");
            }
            return View(entity);
        }

        [HttpGet]
        public ActionResult Edit_User(int? id)
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
            var communities = db.ReadHomes().ToList();
            SelectList list = new SelectList(communities, "Id", "Name");

            var positions = db.ReadPositions().ToList();
            SelectList list2 = new SelectList(positions, "Id", "Name");
            List<object> both = new List<object> { list, list2 };
            ViewBag.listing = both;

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var risk = db.ReadUsers().SingleOrDefault(e => e.Id == id);
            if (risk == null) return HttpNotFound();

            return View(risk);
        }

        [HttpPost]
        public ActionResult Edit_User(Users_DTO entity)
        {
            if (entity.Date != System.DateTime.MinValue)
            {
                db.Update(entity);
                return RedirectToAction("../Select/Select_Users");
            }
            return View(entity);
        }

        [HttpGet]
        public ActionResult Edit_GoodNews(int? id)
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
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var founded = db.ReadNews().SingleOrDefault(i => i.Id == id);
            ViewBag.locations = new object[]
            { HomeController.list, HomeController.list12, HomeController.list13, HomeController.list14, HomeController.list15, HomeController.list16 };
            if (founded == null) return HttpNotFound();
            return View(founded);
        }


        [HttpPost]
        public ActionResult Edit_GoodNews(Good_News_DTO entity)
        {
            if (ModelState.IsValid)
            {
                db.Update(entity);
                return RedirectToAction("../Select/Select_GoodNews");
            }
            return View(entity);
        }


        [HttpGet]
        public ActionResult Edit_Agency(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var risk = db.ReadAgencies().SingleOrDefault(e => e.Id == id);
            if (risk == null) return HttpNotFound();

            return View(risk);
        }

        [HttpPost]
        public ActionResult Edit_Agency(Visits_Agency_DTO entity)
        {
            if (ModelState.IsValid)
            {
                db.Update(entity);
                return RedirectToAction("../Select/Select_Agencies");
            }

            return View(entity);
        }

        [HttpGet]
        public ActionResult Edit_WSIB(int? id)
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
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var wsib = db.ReadWSiBs().SingleOrDefault(e => e.Id == id);
            ViewBag.locations = HomeController.list;
            if (wsib == null) return HttpNotFound();

            return View(wsib);
        }

        [HttpPost]
        public ActionResult Edit_WSIB(WSIB_DTO entity)
        {
            if (ModelState.IsValid)
            {
                db.Update(entity);
                return RedirectToAction("../Select/Select_WSIB");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit_Not_WSIB(int? id)
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
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var wsib = db.ReadNotWSIBs().SingleOrDefault(e => e.Id == id);
            ViewBag.locations = HomeController.list;
            if (wsib == null) return HttpNotFound();

            return View(wsib);
        }

        [HttpPost]
        public ActionResult Edit_Not_WSIB(Not_WSIBs_DTO entity)
        {
            if (ModelState.IsValid)
            {
                db.Update(entity);
                return RedirectToAction("../Select/Select_Not_WSIB");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit_Visits_Others(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var visit = db.ReadOthers().SingleOrDefault(i => i.Id == id);
            ViewBag.locations = new object[] { HomeController.list, HomeController.list18, HomeController.list19, HomeController.list4 };
            if (visit == null) return HttpNotFound();

            return View(visit);
        }

        //[HttpPost]
        //public ActionResult Edit_Visits_Others(Visits_Others_DTO entity)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Update(entity);
        //        return RedirectToAction("../Select/Select_Visits_Others");
        //    }
        //    return View();
        //}

        [HttpGet]
        public ActionResult Edit_Outbreaks(int? id)
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
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.locations = HomeController.list;
            var outbreaks = db.ReadOutbreaks().SingleOrDefault(o => o.Id == id);
            if (outbreaks == null) return HttpNotFound();

            return View(outbreaks);
        }

        [HttpPost]
        public ActionResult Edit_Outbreaks(Outbreaks_DTO entity)
        {
            if (ModelState.IsValid)
            {
                db.Update(entity);
                return RedirectToAction("../Select/Outbreaks");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit_Privacy_Complaints(int? id)
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
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var founded = db.ReadPComplaints().SingleOrDefault(i => i.Id == id);
            ViewBag.locations = HomeController.list;
            if (founded == null) return HttpNotFound();
            return View(founded);
        }

        [HttpPost]
        public ActionResult Edit_Privacy_Complaints(Privacy_Complaints_DTO entity)
        {
            if (ModelState.IsValid)
            {
                db.Update(entity);
                return RedirectToAction("../Select/Privacy_Complaints");
            }
            return View(entity);
        }

        [HttpGet]
        public ActionResult Edit_Complaints(int? id)
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
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var founded = db.ReadComplaints().SingleOrDefault(i => i.Id == id);
            ViewBag.locations = new object[]
                {
                HomeController.list,
                HomeController.list6,
                HomeController.list7,
                HomeController.list8,
                HomeController.list9,
                HomeController.list10,
                HomeController.list11
                };
            if (founded == null) return HttpNotFound();
            return View(founded);
        }

        [HttpPost]
        public ActionResult Edit_Complaints(Complaint_DTO entity)
        {
            if (ModelState.IsValid)
            {
                db.Update(entity);
                return RedirectToAction("../Select/Select_Complaints");
            }
            return View(entity);
        }

        [HttpGet]
        public ActionResult Edit_Privacy_Breaches(int? id)
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
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.locations = HomeController.list;
            var founded = db.ReadBreaches().SingleOrDefault(i => i.Id == id);
            if (founded == null) return HttpNotFound();
            return View(founded);
        }

        [HttpPost]
        public ActionResult Edit_Privacy_Breaches(Privacy_Breaches_DTO entity)
        {
            if (ModelState.IsValid)
            {
                db.Update(entity);
                return RedirectToAction("../Select/Privacy_Breaches");
            }
            return View(entity);
        }

        [HttpGet]
        public ActionResult Edit_Emergency(int? id)
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
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.locations = HomeController.list;
            var founded = db.ReadEmergency().SingleOrDefault(i => i.Id == id);
            if (founded == null) return HttpNotFound();
            return View(founded);
        }

        [HttpPost]
        public ActionResult Edit_Emergency(Emergency_Prep_DTO entity)
        {
            if (ModelState.IsValid)
            {
                db.Update(entity);
                return RedirectToAction("../Select/Select_Emergency_Prep");
            }
            return View(entity);
        }

        [HttpGet]
        public ActionResult Edit_Immun(int? id)
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
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.locations = HomeController.list;
            var founded = db.ReadImmunizations().SingleOrDefault(i => i.Id == id);
            if (founded == null) return HttpNotFound();
            return View(founded);
        }

        [HttpPost]
        public ActionResult Edit_Immun(Immunization_DTO entity)
        {
            if (ModelState.IsValid)
            {
                db.Update(entity);
                return RedirectToAction("../Select/Select_Immunization");
            }
            return View(entity);
        }

        [HttpGet]
        public ActionResult Edit_BC_TLC(int? id)
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
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            object[] objs = new object[] { HomeController.list, HomeController.list21, HomeController.list22 };
            ViewBag.locations = objs;
            var founded = db.ReadBC_LTC().SingleOrDefault(i => i.Id == id);
            if (founded == null) return HttpNotFound();
            return View(founded);
        }

        [HttpPost]
        public ActionResult Edit_BC_TLC(BC_LTC_Reportable_Incidents_DTO entity)
        {            
            if (ModelState.IsValid)
            {
                db.Update(entity);
                return RedirectToAction("../Select/Select_BC_LTC");
            }
            return View(entity);
        }

        [HttpGet]
        public ActionResult Edit_BC_Assisted(int? id)
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
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            object[] objs = new object[] { HomeController.list, HomeController.list21, HomeController.list29 };
            ViewBag.locations = objs;
            var founded = db.ReadBC_LTCAssisted().SingleOrDefault(i => i.Id == id);
            if (founded == null) return HttpNotFound();
            return View(founded);
        }

        [HttpPost]
        public ActionResult Edit_BC_Assisted(BC_Assisted_Living_Reportable_Incidents_DTO entity)
        {
            if (ModelState.IsValid)
            {
                db.Update(entity);
                return RedirectToAction("../Select/Select_BC_LTC_Assisted");
            }
            return View(entity);
        }

        [HttpGet]
        public ActionResult Edit_Other(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.locations = HomeController.list;
            var founded = db.ReadOthersO().SingleOrDefault(i => i.Id == id);
            if (founded == null) return HttpNotFound();
            return View(founded);
        }

        [HttpPost]
        public ActionResult Edit_Other(OtherDTO entity)
        {
            if (ModelState.IsValid)
            {
                db.Update(entity);
                return RedirectToAction("../Select/Select_Others");
            }
            return View(entity);
        }

        #region Licensing Inspections 
        public ActionResult Edit_LiceInspect(int? id)
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
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            { ViewBag.TypeReason = HomeController.list26; }
            ViewBag.locations = HomeController.list;
            var founded = db.ReadLiceInspect().SingleOrDefault(i => i.Id == id);
            if (founded == null) return HttpNotFound();
            return View(founded);
        }

        [HttpPost]
        public ActionResult Edit_LiceInspect(LicensingInspectionDTO entity)
        {
            if (entity.CareComName.Equals(0))
                return View();
            else
                db.Update(entity);
                return RedirectToAction("../Select/Select_LiceInspect");
        }
        #endregion

        #region Assisted Living Inspections
        public ActionResult Edit_AssistLivingInpect(int? id)
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
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            { ViewBag.TypeReason = HomeController.list26; }
            ViewBag.locations = HomeController.list;
            var founded = db.ReadAssLivInspect().SingleOrDefault(i => i.Id == id);
            if (founded == null) return HttpNotFound();
            return View(founded);
        }

        [HttpPost]
        public ActionResult Edit_AssistLivingInpect(AssistedLivingInspectionDTO entity)
        {
            if (ModelState.IsValid)
            {
                db.Update(entity);
                return RedirectToAction("../Select/Select_AssistLivInspect");
            }
            return View(entity);
        }
        #endregion

        #region Worksafe BC Inspections:
        public ActionResult Edit_WorksaveBC(int? id)
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
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.locations = HomeController.list;
            { ViewBag.ScopeInspect = HomeController.list28; }
            var founded = db.ReadWorkshopBCInspect().SingleOrDefault(i => i.Id == id);
            if (founded == null) return HttpNotFound();
            return View(founded);
        }

        [HttpPost]
        public ActionResult Edit_WorksaveBC(WorkshopBCInspection_DTO entity)
        {
            if (ModelState.IsValid)
            {
                db.Update(entity);
                return RedirectToAction("../Select/Select_WorksaveBC");
            }
            return View(entity);
        }
        #endregion

        #region Quality Reviews:
        public ActionResult Edit_QualityReview(int? id)
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
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.locations = HomeController.list;
            var founded = db.ReadQualityReviews().SingleOrDefault(i => i.Id == id);
            if (founded == null) return HttpNotFound();
            return View(founded);
        }

        [HttpPost]
        public ActionResult Edit_QualityReview(QualityReview_DTO entity)
        {
            if (ModelState.IsValid)
            {
                db.Update(entity);
                return RedirectToAction("../Select/Select_QualityReview");
            }
            return View(entity);
        }
        #endregion
    }
}