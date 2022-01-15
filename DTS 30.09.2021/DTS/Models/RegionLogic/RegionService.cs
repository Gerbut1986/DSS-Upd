namespace DTS.Models.RegionLogic
{
    using System.Linq;
    using DTS.Controllers;
    using DSS.BLL.Services;
    using DSS.BLL.Interfaces;
    using System.Collections;

    public class RegionService
    {
        public static int GetRegion(ApplicationUser user) 
        {
            if (user == null) throw new System.NullReferenceException("A parameter is NULL or a passed user doesn't exist.");
            return AccountController.AllUsers.FirstOrDefault(n => n.Email.Equals(user.Email)).Region;
        }

        public static ArrayList GetListByRegion(IModel model, int reg_numb, ServiceDSS Db, string[] allRegions)
        {
            var list = new ArrayList();
            switch (model.GetType().Name)
            {
                case "Critical_Incidents_DTO":
                    list.AddRange(SearchIncident.RegionByLocId(reg_numb, Db, allRegions));
                    break;
                case "Complaint_DTO":
                    list.AddRange(SearchComplaint.RegionByLocId(reg_numb, Db, allRegions));
                    break;
                case "Good_News_DTO":
                    list.AddRange(SearchGoodNews.RegionByLocId(reg_numb, Db, allRegions));
                    break;
                case "Emergency_Prep_DTO":
                    list.AddRange(SearchEmergency.RegionByLocId(reg_numb, Db, allRegions));
                    break;
                case "Community_Risks_DTO":
                    list.AddRange(SearchRisks.RegionByLocId(reg_numb, Db, allRegions));
                    break;
                case "Privacy_Breaches_DTO":
                    list.AddRange(SearchPBreach.RegionByLocId(reg_numb, Db, allRegions));
                    break;
                case "Privacy_Complaints_DTO":
                    list.AddRange(SearchPComplaint.RegionByLocId(reg_numb, Db, allRegions));
                    break;
                case "Outbreaks_DTO":
                    list.AddRange(SearchOutbreaks.RegionByLocId(reg_numb, Db, allRegions));
                    break;
                case "WSIB_DTO":
                    list.AddRange(SearchWSIB.RegionByLocId(reg_numb, Db, allRegions));
                    break;
                case "Not_WSIBs_DTO":
                    list.AddRange(SearchNotWSIB.RegionByLocId(reg_numb, Db, allRegions));
                    break;
                case "LicensingInspectionDTO":
                    list.AddRange(SearchLicInspect.RegionByLocId(reg_numb, Db, allRegions));
                    break;
                case "AssistedLivingInspectionDTO":
                    list.AddRange(SearchAssisLivInspect.RegionByLocId(reg_numb, Db, allRegions));
                    break;
                case "WorkshopBCInspection_DTO":
                    list.AddRange(SearchWorkssafe.RegionByLocId(reg_numb, Db, allRegions));
                    break;
                case "QualityReview_DTO":
                    list.AddRange(SearchQualReview.RegionByLocId(reg_numb, Db, allRegions));
                    break;
                case "BC_LTC_Reportable_Incidents_DTO":
                    list.AddRange(SearchBC_LTC.RegionByLocId(reg_numb, Db, allRegions));
                    break;
                case "BC_Assisted_Living_Reportable_Incidents_DTO":
                    list.AddRange(SearchBC_Assisted.RegionByLocId(reg_numb, Db, allRegions));
                    break;
                default:return null;
            }
            return list;
        }
    }
}