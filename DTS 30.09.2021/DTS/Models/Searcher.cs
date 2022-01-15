namespace DTS.Models
{
    using DSS.BLL.DTO;

    public class Searcher
    {
        #region Method witch find model by name:
        public static object FindObjByName(string name)
        {
            switch (name)
            {
                case "Critical_Incidents_DTO":
                    return new Critical_Incidents_DTO();
                case "Complaint_DTO":
                    return new Complaint_DTO();
                case "Good_News_DTO":
                    return new Good_News_DTO();
                case "Emergency_Prep_DTO":
                    return new Emergency_Prep_DTO();
                case "Community_Risks_DTO":
                    return new Community_Risks_DTO();
                case "Visits_Others_DTO":
                    return new Visits_Others_DTO();
                case "Privacy_Breaches_DTO":
                    return new Privacy_Breaches_DTO();
                case "Privacy_Complaints_DTO":
                    return new Privacy_Complaints_DTO();
                case "Education_DTO":
                    return new Education_DTO();
                case "Labour_Relations_DTO":
                    return new Labour_Relations_DTO();
                case "Immunization_DTO":
                    return new Immunization_DTO();
                case "Outbreaks_DTO":
                    return new Outbreaks_DTO();
                case "WSIB_DTO":
                    return new WSIB_DTO();
                case "Not_WSIBs_DTO":
                    return new Not_WSIBs_DTO();
                case "OtherDTO":
                    return new OtherDTO();
                case "LicensingInspectionDTO":
                    return new LicensingInspectionDTO();
                case "AssistedLivingInspectionDTO":
                    return new AssistedLivingInspectionDTO();
                case "WorkshopBCInspection_DTO":
                    return new WorkshopBCInspection_DTO();
                case "QualityReview_DTO":
                    return new QualityReview_DTO();
                case "BC_LTC_Reportable_Incidents_DTO":
                    return new BC_LTC_Reportable_Incidents_DTO();
                case "BC_Assisted_Living_Reportable_Incidents_DTO":
                    return new BC_Assisted_Living_Reportable_Incidents_DTO();
                default:
                    return "NoN ... You may need to add a new Entity!";
            }
        }
        #endregion
    }
}