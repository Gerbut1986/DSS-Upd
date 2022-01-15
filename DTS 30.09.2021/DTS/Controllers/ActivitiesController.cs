namespace DTS.Controllers
{
    using DSS.BLL.DTO;
    using System.Web.Mvc;
    using System.Collections.Generic;

    public class ActivitiesController : Controller
    {
        static string locSelected;
        private List<Activities_DTO> _ActivitiesList = new List<Activities_DTO>();

        [HttpPost]
        public ActionResult SelectLoc(object val)
        {
            locSelected = val.ToString();
            return RedirectToAction("WOR_Tabs", "Home");
        }

        [HttpGet]
        public ActionResult Add()
        {
            var activityId = int.Parse(RouteData.Values["id"].ToString());
            string parentActivityDescription;

            //using (var conn = new ServiceDSS(Init.ConnectionStr))
            //{
            //    //var parentActivity = conn.Activities_DTO.Find(activityId);
            //    parentActivityDescription = parentActivity?.ActivityDescription ?? "No parent activity";
            //}

            ViewBag.ParentActivity = activityId;
           // ViewBag.ParentActivityDescription = parentActivityDescription;

            return View();
        }

        [HttpPost]
        public ActionResult Add(Activities_DTO Activities_DTO)
        {
            try
            {
                var activity = new Activities_DTO()
                {
                    ParentActivityID = Activities_DTO.ParentActivityID,
                    ActivityDescription = Activities_DTO.ActivityDescription,
                    StartDateTime = Activities_DTO.StartDateTime,
                    EndDateTime = Activities_DTO.EndDateTime
                };

                //using (var Activities_DTODb = new MyContext())
                //{
                //    var parentActivity = Activities_DTODb.Activities_DTO.Find(activity.ParentActivityID);
                //    if (parentActivity == null && Activities_DTO.ActivityID != 0)
                //    {
                //        return RedirectToAction("Index");
                //    }
                //    Activities_DTODb.Activities_DTO.Add(activity);
                //    Activities_DTODb.SaveChanges();
                //}
                return RedirectToAction("WOR_Tabs", "Home");
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError("ParentActivityId", ex.Message);
                return View("Add", Activities_DTO);
            }
        }
    }
}