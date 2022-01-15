namespace DTS.Controllers
{
    using System.Net;
    using DTS.Models;
    using System.Linq;
    using System.Web.Mvc;
    using DSS.BLL.Services;
    using System.Threading.Tasks;

    public class DeleteController : Controller
    {
        readonly ServiceDSS db;
        int localLocation;
        private ApplicationUser CurrentLocalUser { get; set; }

        public ApplicationUser GetCurrUser() => AccountController.AllUsers.FirstOrDefault(n => n.Email.Equals(User.Identity.Name));

        public DeleteController() => db = new ServiceDSS(Init.ConnectionStrAdm);

        [HttpGet]
        public ActionResult Incident_Delete(int? id) //parameter can be a null; will give an error if there is no (?)
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

            var delete = db.ReadIncidents().SingleOrDefault(e => e.Id == id); // get CriticalIncident object by id using lamda expression
            if (delete == null) return RedirectToAction("../Home/WOR_tabs");
            
            try
            {
                var n = db.ReadHomes().SingleOrDefault(i => i.Id == delete.Location);     // get Care_Community(Location) object by id location  CriticalIncident foreign key
                var name2 = AppSettings.ci_types.Where(i => i.Id == delete.CI_Category_Type).SingleOrDefault();  // get CI_Category_Type object by id CI_Category_Type  CriticalIncident foreign key

                var arr = new string[] { n.Full_Home_Name, name2.Name };        // create string array and put two elements - Location.Name & CI_Category_Type.Name into the model
                ViewBag.list = arr;   // add array w/ 2 variables to ViewBag        
            }
            catch { }

            return View(delete);  
        }


        [HttpPost]
        public async Task<ActionResult> Incident_Delete(int id)
        {
            await db.DeleteIncidentsByIdAsync(id);   
            return RedirectToAction("../Select/Select_Incidents");  /// Redirect to the List Select_Incidents
        }

        [HttpGet]
        public ActionResult Labour_Delete(int? id)
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

            var delete = db.ReadRelations().SingleOrDefault(e => e.Id == id);
            if (delete == null) return RedirectToAction("../Home/WOR_tabs");
            var n = db.ReadHomes().Where(i => i.Id == delete.Location).SingleOrDefault();
            ViewBag.list = n.Full_Home_Name;

            return View(delete);
        }


        [HttpPost]
        public async Task<ActionResult> Labour_Delete(int id)
        {
            await db.DeleteLabourByIdAsync(id);
            return RedirectToAction("../Select/Select_Labour");
        }

        [HttpGet]
        public ActionResult Education_Delete(int? id)
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

            var delete = db.ReadEducation().SingleOrDefault(e => e.Id == id);
            if (delete == null) return RedirectToAction("../Home/WOR_tabs");
            var n = db.ReadHomes().Where(i => i.Id == delete.Location).SingleOrDefault();
            ViewBag.list = n.Full_Home_Name;

            return View(delete);
        }

        [HttpPost]
        public async Task<ActionResult> Education_Delete(int id)
        {
            await db.DeleteEducatByIdAsync(id);
            return RedirectToAction("../Select/Education_Select");
        }

        [HttpGet]
        public ActionResult Community_Delete(int? id)
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

            var delete = db.ReadRisks().SingleOrDefault(e => e.Id == id);
            if (delete == null) return RedirectToAction("../Home/WOR_tabs");
            var n = db.ReadHomes().Where(i => i.Id == delete.Location).SingleOrDefault();  
            ViewBag.list = n.Full_Home_Name;

            return View(delete);
        }


        [HttpPost]
        public async Task<ActionResult> Community_Delete(int id)
        {
            await db.DeleteRisksByIdAsync(id);
            return RedirectToAction("../Select/Select_Community");
        }

        [HttpGet]
        public ActionResult User_Delete(int? id)
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

            var delete = db.ReadUsers().SingleOrDefault(e => e.Id == id);
            if (delete == null) return RedirectToAction("../Home/WOR_tabs");

            return View(delete);
        }


        [HttpPost]
        public async Task<ActionResult> User_Delete(int id)
        {
            await db.DeleteUserByIdAsync(id);
            return RedirectToAction("../Select/Select_Users");
        }

        [HttpGet]
        public ActionResult GoodNews_Delete(int? id)
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

            var delete = db.ReadNews().SingleOrDefault(e => e.Id == id);
            if (delete == null) return RedirectToAction("../Home/WOR_tabs");
            var n = db.ReadHomes().Where(i => i.Id == delete.Location).SingleOrDefault();
            ViewBag.list = n.Full_Home_Name;
            if (delete == null) return HttpNotFound();

            return View(delete);
        }


        [HttpPost]
        public async Task<ActionResult> GoodNews_Delete(int id)
        {
            await db.DeleteNewsByIdAsync(id);
            return RedirectToAction("../Select/Select_GoodNews");
        }

        [HttpGet]
        public ActionResult Agency_Delete(int? id)
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

            var delete = db.ReadAgencies().SingleOrDefault(e => e.Id == id);
            if (delete == null) return RedirectToAction("../Home/WOR_tabs");
            if (delete == null) return HttpNotFound();

            return View(delete);
        }


        [HttpPost]
        public ActionResult Agency_Delete(int id)
        {
            db.DeleteAgency(id);
            return RedirectToAction("../Select/Select_Agencies");
        }

        [HttpGet]
        public ActionResult WSIB_Delete(int? id)
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

            var delete = db.ReadWSiBs().SingleOrDefault(e => e.Id == id);
            if (delete == null) return RedirectToAction("../Home/WOR_tabs");
            if (delete == null) return HttpNotFound();

            return View(delete);
        }


        [HttpPost]
        public async Task<ActionResult> WSIB_Delete(int id)
        {
            await db.DeleteWSIBByIdAsync(id);
            return RedirectToAction("../Select/Select_WSIB");
        }

        [HttpGet]
        public ActionResult Visits_Others_Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var delete = db.ReadOthers().SingleOrDefault(e => e.Id == id);
            if (delete == null) return RedirectToAction("../Home/WOR_tabs");
            var n = db.ReadHomes().Where(i => i.Id == delete.Location).SingleOrDefault();
            ViewBag.list = n.Full_Home_Name;
            if (delete == null) return HttpNotFound();

            return View(delete);
        }


        [HttpPost]
        public ActionResult Visits_Others_Delete(int id)
        {
            db.DeleteOther(id);
            return RedirectToAction("../Select/Select_Visits_Others");
        }

        [HttpGet]
        public ActionResult Outbreaks_Delete(int? id)
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
            var delete = db.ReadOutbreaks().SingleOrDefault(e => e.Id == id);
            if (delete == null) return RedirectToAction("../Home/WOR_tabs");
            var n = db.ReadHomes().Where(i => i.Id == delete.Location).SingleOrDefault();
            ViewBag.list = n.Full_Home_Name;
            if (delete == null) return HttpNotFound();

            return View(delete);
        }

        [HttpPost]
        public async Task<ActionResult> Outbreaks_Delete(int id)
        {
            await db.DeleteByIdAsync(id);
            return RedirectToAction("../Select/Outbreaks");
        }

        [HttpGet]
        public ActionResult Privacy_Breaches_Delete(int? id)
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
            var delete = db.ReadBreaches().SingleOrDefault(e => e.Id == id);
            if (delete == null) return RedirectToAction("../Home/WOR_tabs");
            var n = db.ReadHomes().Where(i => i.Id == delete.Location).SingleOrDefault();
            ViewBag.list = n.Full_Home_Name;
            if (delete == null) return HttpNotFound();

            return View(delete);
        }

        [HttpPost]
        public async Task<ActionResult> Privacy_Breaches_Delete(int id)
        {
            await db.DeleteBreachByIdAsync(id);
            return RedirectToAction("../Select/Privacy_Breaches");
        }

        [HttpGet]
        public ActionResult Emergency_Prep_Delete(int? id)
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
            var delete = db.ReadEmergency().SingleOrDefault(e => e.Id == id);
            if (delete == null) return RedirectToAction("../Home/WOR_tabs");
            var n = db.ReadHomes().Where(i => i.Id == delete.Location).SingleOrDefault();
            ViewBag.list = n.Full_Home_Name;
            if (delete == null) return HttpNotFound();

            return View(delete);
        }

        [HttpPost]
        public async Task<ActionResult> Emergency_Prep_Delete(int id)
        {
            await db.DeleteEmergenByIdAsync(id);
            return RedirectToAction("../Select/Select_Emergency_Prep");
        }

        [HttpGet]
        public ActionResult Complaints_Delete(int? id)
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

            var delete = db.ReadComplaints().SingleOrDefault(e => e.Id == id);
            if (delete == null) return RedirectToAction("../Home/WOR_tabs");
            var n = db.ReadHomes().Where(i => i.Id == delete.Location).SingleOrDefault();
            ViewBag.list = n.Full_Home_Name;
            if (delete == null) return HttpNotFound();

            return View(delete);
        }

        [HttpPost]
        public async Task<ActionResult> Complaints_Delete(int id)
        {
            await db.DeleteComplaintsByIdAsync(id);
            return RedirectToAction("../Select/Select_Complaints");
        }

        [HttpGet]
        public ActionResult Privacy_Complaints_Delete(int? id)
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

            var delete = db.ReadPComplaints().SingleOrDefault(e => e.Id == id);
            if (delete == null) return RedirectToAction("../Home/WOR_tabs");
            var n = db.ReadHomes().Where(i => i.Id == delete.Location).SingleOrDefault();
            ViewBag.list = n.Full_Home_Name;
            if (delete == null) return HttpNotFound();

            return View(delete);
        }

        [HttpPost]
        public async Task<ActionResult> Privacy_Complaints_Delete(int id)
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
            await db.DeletePComplainByIdAsync(id);
            return RedirectToAction("../Select/Privacy_Complaints");
        }

        [HttpGet]
        public ActionResult Emergency_Delete(int? id)
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

            var delete = db.ReadEmergency().SingleOrDefault(e => e.Id == id);
            if (delete == null) return RedirectToAction("../Home/WOR_tabs");
            var n = db.ReadHomes().Where(i => i.Id == delete.Location).SingleOrDefault();
            ViewBag.list = n.Full_Home_Name;
            if (delete == null) return HttpNotFound();

            return View(delete);
        }

        [HttpPost]
        public async Task<ActionResult> Emergency_Delete(int id)
        {
            await db.DeleteEmergenByIdAsync(id);
            return RedirectToAction("../Select/Select_Emergency_Prep");
        }

        [HttpGet]
        public ActionResult Immun_Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var delete = db.ReadImmunizations().SingleOrDefault(e => e.Id == id);
            if (delete == null) return RedirectToAction("../Home/WOR_tabs");
            var n = db.ReadHomes().Where(i => i.Id == delete.Location).SingleOrDefault();
            ViewBag.list = n.Full_Home_Name;
            if (delete == null) return HttpNotFound();

            return View(delete);
        }

        [HttpPost]
        public async Task<ActionResult> Immun_Delete(int id)
        {
            await db.DeleteImmunByIdAsync(id);
            return RedirectToAction("../Select/Select_Immunization");
        }

        public ActionResult Not_WSIB_Delete(int? id)
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

            var delete = db.ReadNotWSIBs().SingleOrDefault(e => e.Id == id);
            if (delete == null) return RedirectToAction("../Home/WOR_tabs");
            var n = db.ReadHomes().Where(i => i.Id == delete.Location).SingleOrDefault();
            ViewBag.list = n.Full_Home_Name;
            if (delete == null) return HttpNotFound();

            return View(delete);
        }

        [HttpPost]
        public async Task<ActionResult> Not_WSIB_Delete(int id)
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
            await db.DeleteMotWByIdAsync(id);
            return RedirectToAction("../Select/Select_Not_WSIB");
        }

        public ActionResult Delete_BC_TLC(int? id)
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

            var delete = db.ReadBC_LTC().SingleOrDefault(e => e.Id == id);
            if (delete == null) return RedirectToAction("../Home/WOR_tabs");
            var n = db.ReadHomes().Where(i => i.Id == delete.CareCommName).SingleOrDefault();
            ViewBag.list = n.Full_Home_Name;
            if (delete == null) return HttpNotFound();

            return View(delete);
        }

        [HttpPost]
        public async Task<ActionResult> Delete_BC_TLC(int id)
        {
            await db.DeleteBC_LTCByIdAsync(id);
            return RedirectToAction("../Select/Select_BC_LTC");
        }

        public ActionResult Delete_BC_Assisted(int? id)
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

            var delete = db.ReadBC_LTCAssisted().SingleOrDefault(e => e.Id == id);
            if (delete == null) return RedirectToAction("../Home/WOR_tabs");
            var n = db.ReadHomes().Where(i => i.Id == delete.NameCareCommu).SingleOrDefault();
            ViewBag.list = n.Full_Home_Name;
            if (delete == null) return HttpNotFound();

            return View(delete);
        }

        [HttpPost]
        public async Task<ActionResult> Delete_BC_Assisted(int id)
        {
            await db.DeleteBC_LTCAssistedByIdAsync(id);
            return RedirectToAction("../Select/Select_BC_LTC_Assisted");
        }

        public ActionResult Other_Delete(int? id)
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

            var delete = db.ReadOthersO().SingleOrDefault(e => e.Id == id);
            if (delete == null) return RedirectToAction("../Home/WOR_tabs");
            var n = db.ReadHomes().Where(i => i.Id == delete.Location).SingleOrDefault();
            ViewBag.list = n.Full_Home_Name;
            if (delete == null) return HttpNotFound();

            return View(delete);
        }

        [HttpPost]
        public async Task<ActionResult> Other_Delete(int id)
        {
            await db.DeleteOtherByOIdAsync(id);
            return RedirectToAction("../Select/Select_Others");
        }

        public ActionResult Delete_LiceInspect(int? id)
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

            var delete = db.ReadLiceInspect().SingleOrDefault(e => e.Id == id);
            if (delete == null) return RedirectToAction("../Home/WOR_tabs");
            var n = db.ReadHomes().Where(i => i.Id == delete.CareComName).SingleOrDefault();
            ViewBag.list = n.Full_Home_Name;
            if (delete == null) return HttpNotFound();

            return View(delete);
        }

        [HttpPost]
        public async Task<ActionResult> Delete_LiceInspect(int id)
        {
            await db.DeleteLiceInspByIdAsync(id);
            return RedirectToAction("../Select/Select_LiceInspect");
        }

        public ActionResult Delete_AssistLivingInpect(int? id)
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

            var delete = db.ReadAssLivInspect().SingleOrDefault(e => e.Id == id);
            if (delete == null) return RedirectToAction("../Home/WOR_tabs");
            var n = db.ReadHomes().Where(i => i.Id == delete.CareComName).SingleOrDefault();
            ViewBag.list = n.Full_Home_Name;
            if (delete == null) return HttpNotFound();

            return View(delete);
        }

        [HttpPost]
        public async Task<ActionResult> Delete_AssistLivingInpect(int id)
        {
            await db.DeleteAssLivInspByIdAsync(id);
            return RedirectToAction("../Select/Select_AssistLivInspect");
        }

        public ActionResult Delete_WorksaveBC(int? id)
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

            var delete = db.ReadWorkshopBCInspect().SingleOrDefault(e => e.Id == id);
            if (delete == null) return RedirectToAction("../Home/WOR_tabs");
            var n = db.ReadHomes().Where(i => i.Id == delete.CareComName).SingleOrDefault();
            ViewBag.list = n.Full_Home_Name;
            if (delete == null) return HttpNotFound();

            return View(delete);
        }

        [HttpPost]
        public async Task<ActionResult> Delete_WorksaveBC(int id)
        {
            await db.DeleteWorkshopBCIInspByIdAsync(id);
            return RedirectToAction("../Select/Select_WorksaveBC");
        }

        public ActionResult Delete_QualityReview(int? id)
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

            var delete = db.ReadQualityReviews().SingleOrDefault(e => e.Id == id);
            if (delete == null) return RedirectToAction("../Home/WOR_tabs");
            var n = db.ReadHomes().Where(i => i.Id == delete.CareComName).SingleOrDefault();
            ViewBag.list = n.Full_Home_Name;
            if (delete == null) return HttpNotFound();

            return View(delete);
        }

        [HttpPost]
        public async Task<ActionResult> Delete_QualityReview(int id)
        {
            await db.DeleteQualityReviewAsync(id);
            return RedirectToAction("../Select/Select_QualityReview");
        }
    }
}