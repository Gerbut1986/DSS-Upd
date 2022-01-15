namespace DTS.Controllers
{
    using System.Net;
    using DTS.Models;
    using DSS.BLL.DTO;
    using System.Linq;
    using System.Web.Mvc;
    using DSS.BLL.Services;
    using System.Collections.Generic;

    public class DetailsController : Controller
    {
        ServiceDSS db;
        int localLocation;
        private ApplicationUser CurrentLocalUser { get; set; }

        public ApplicationUser GetCurrUser() => AccountController.AllUsers.FirstOrDefault(n => n.Email.Equals(User.Identity.Name));

        public DetailsController() => db = new ServiceDSS(Init.ConnectionStrAdm);

        public ActionResult Incidents_Details(int? id)
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
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var entity = db.ReadIncidents().SingleOrDefault(rel => rel.Id == id);   // get Critical_Incidents by id
            var name1 = db.ReadHomes().Where(i=>i.Id == entity.Location).SingleOrDefault(); // get Location.Name
            var name2 = db.ReadCICategory().Where(i=>i.Id ==entity.CI_Category_Type).SingleOrDefault(); // get CI_Category_Type.Name
            var arr = new string[] { name1.Full_Home_Name, name2.Name };   // create string array and put two elements - Location.Name & CI_Category_Type.Name both model
            ViewBag.list = arr;                                                                        // add array to ViewBag
            if (entity == null) //if home_id is null - give a not found message or transfer to an appropriate view.
                return HttpNotFound();
            return View(entity);
        }

        public ActionResult Labour_Details(int? id)
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
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var entity = db.ReadRelations().SingleOrDefault(rel => rel.Id == id);
            var name1 = db.ReadHomes().Where(i => i.Id == entity.Location).SingleOrDefault();
            ViewBag.list = name1.Full_Home_Name;
            if (entity == null)
                return HttpNotFound();
            return View(entity);
        }

        public ActionResult Community_Details(int? id)
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
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var entity = db.ReadRisks().SingleOrDefault(rel => rel.Id == id);
            var name1 = db.ReadHomes().Where(i => i.Id == entity.Location).SingleOrDefault();
            ViewBag.list = name1.Full_Home_Name;
            if (entity == null)
                return HttpNotFound();
            return View(entity);
        }

        public ActionResult User_Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var entity = db.ReadUsers().SingleOrDefault(rel => rel.Id == id);
            if (entity == null)
                return HttpNotFound();
            return View(entity);
        }

        public ActionResult GoodNews_Details(int? id)
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
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var entity = db.ReadNews().SingleOrDefault(rel => rel.Id == id);
            var name1 = db.ReadHomes().Where(i => i.Id == entity.Location).SingleOrDefault();
            ViewBag.list = name1.Full_Home_Name;
            if (entity == null)
                return HttpNotFound();
            return View(entity);
        }

        public ActionResult Agency_Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var entity = db.ReadAgencies().SingleOrDefault(rel => rel.Id == id);
            if (entity == null)
                return HttpNotFound();
            return View(entity);
        }

        public ActionResult WSIB_Details(int? id)
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
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WSIB_DTO entity = db.ReadWSiBs().SingleOrDefault(w => w.Id == id);
            if (entity == null)
                return HttpNotFound();
            return View(entity);
        }

        public ActionResult Visits_Others_Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var entity = db.ReadOthers().SingleOrDefault(w => w.Id == id);
            var n = db.ReadHomes().Where(i => i.Id == entity.Location).SingleOrDefault();
            ViewBag.list = n.Full_Home_Name;
            if (entity == null)
                return HttpNotFound();
            return View(entity);
        }

        public ActionResult EducationDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var entity = db.ReadEducation().SingleOrDefault(w => w.Id == id);
            var n = db.ReadHomes().Where(i => i.Id == entity.Location).SingleOrDefault();
            ViewBag.list = n.Full_Home_Name;
            if (entity == null)
                return HttpNotFound();
            return View(entity);
        }

        public ActionResult Outbreaks(int? id)
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
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var entity = db.ReadOutbreaks().SingleOrDefault(w => w.Id == id);
            if (entity == null)
                return HttpNotFound();
            return View(entity);
        }

        public ActionResult Privacy_Breaches(int? id)
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
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var entity = db.ReadOutbreaks().SingleOrDefault(w => w.Id == id);
            if (entity == null)
                return HttpNotFound();
            return View(entity);
        }

        public ActionResult Complaints_Details(int? id)
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
            var entity = db.ReadComplaints().SingleOrDefault(w => w.Id == id);
            var n = db.ReadHomes().Where(i => i.Id == entity.Location).SingleOrDefault();
            ViewBag.list = n.Full_Home_Name;
            if (entity == null)
                return HttpNotFound();
            return View(entity);
        }

        public ActionResult Privacy_Complaints_Details(int? id)
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
            var entity = db.ReadPComplaints().SingleOrDefault(w => w.Id == id);
            var n = db.ReadHomes().Where(i => i.Id == entity.Location).SingleOrDefault();
            ViewBag.list = n.Full_Home_Name;
            if (entity == null)
                return HttpNotFound();
            return View(entity);
        }

        public ActionResult Emergency_Details(int? id)
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
            var entity = db.ReadEmergency().SingleOrDefault(w => w.Id == id);
            var n = db.ReadHomes().Where(i => i.Id == entity.Location).SingleOrDefault();
            ViewBag.list = n.Full_Home_Name;
            if (entity == null)
                return HttpNotFound();
            return View(entity);
        }

        public ActionResult Privacy_Breaches_Details(int? id)
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
            var entity = db.ReadBreaches().SingleOrDefault(w => w.Id == id);
            var n = db.ReadHomes().Where(i => i.Id == entity.Location).SingleOrDefault();
            ViewBag.list = n.Full_Home_Name;
            if (entity == null)
                return HttpNotFound();
            return View(entity);
        }

        public ActionResult Immun_Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var entity = db.ReadImmunizations().SingleOrDefault(w => w.Id == id);
            var n = db.ReadHomes().Where(i => i.Id == entity.Location).SingleOrDefault();
            ViewBag.list = n.Full_Home_Name;
            if (entity == null)
                return HttpNotFound();
            return View(entity);
        }

        [HttpGet]
        public ActionResult Not_WSIB_Details(int? id)
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
            var entity = db.ReadNotWSIBs().SingleOrDefault(w => w.Id == id);
            var n = db.ReadHomes().Where(i => i.Id == entity.Location).SingleOrDefault();
            ViewBag.list = n.Full_Home_Name;
            if (entity == null)
                return HttpNotFound();
            return View(entity);
        }

        [HttpGet]
        public ActionResult Details_BC_TLC(int? id)
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
            var entity = db.ReadBC_LTC().SingleOrDefault(w => w.Id == id);
            var n = db.ReadHomes().Where(i => i.Id == entity.CareCommName).SingleOrDefault();
            ViewBag.list = n.Full_Home_Name;
            if (entity == null)
                return HttpNotFound();
            return View(entity);
        }

        [HttpGet]
        public ActionResult Details_BC_Assisted(int? id)
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
            var entity = db.ReadBC_LTCAssisted().SingleOrDefault(w => w.Id == id);
            var n = db.ReadHomes().Where(i => i.Id == entity.NameCareCommu).SingleOrDefault();
            ViewBag.list = n.Full_Home_Name;
            if (entity == null)
                return HttpNotFound();
            return View(entity);
        }

        [HttpGet]
        public ActionResult Other_Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var entity = db.ReadLiceInspect().SingleOrDefault(w => w.Id == id);
            var n = db.ReadHomes().Where(i => i.Id == entity.CareComName).SingleOrDefault();
            ViewBag.list = n.Full_Home_Name;
            if (entity == null)
                return HttpNotFound();
            return View(entity);
        }

        public ActionResult Details_LiceInspect(int? id)
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
            var entity = db.ReadLiceInspect().SingleOrDefault(w => w.Id == id);
            var n = db.ReadHomes().Where(i => i.Id == entity.CareComName).SingleOrDefault();
            ViewBag.list = n.Full_Home_Name;
            if (entity == null)
                return HttpNotFound();
            return View(entity);
        }

        public ActionResult Details_AssistLivingInpect(int? id)
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
            var entity = db.ReadAssLivInspect().SingleOrDefault(w => w.Id == id);
            var n = db.ReadHomes().Where(i => i.Id == entity.CareComName).SingleOrDefault();
            ViewBag.list = n.Full_Home_Name;
            if (entity == null)
                return HttpNotFound();
            return View(entity);
        }

        public ActionResult Details_WorksaveBC(int? id)
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
            var entity = db.ReadWorkshopBCInspect().SingleOrDefault(w => w.Id == id);
            var n = db.ReadHomes().Where(i => i.Id == entity.CareComName).SingleOrDefault();
            ViewBag.list = n.Full_Home_Name;
            if (entity == null)
                return HttpNotFound();
            return View(entity);
        }

        public ActionResult Details_QualityReview(int? id)
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
            var entity = db.ReadQualityReviews().SingleOrDefault(w => w.Id == id);
            var n = db.ReadHomes().Where(i => i.Id == entity.CareComName).SingleOrDefault();
            ViewBag.list = n.Full_Home_Name;
            if (entity == null)
                return HttpNotFound();
            return View(entity);
        }
    }
}
