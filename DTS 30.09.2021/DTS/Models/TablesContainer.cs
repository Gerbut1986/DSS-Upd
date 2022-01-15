namespace DTS.Models
{
    using DSS.BLL.DTO;
    using System.Collections.Generic;
    /// <summary>
    /// List all tables in Database:
    /// </summary>
    public class TablesContainer : Controllers.HomeController
    {
        public static int c1 = 0, c2 = 0, c3 = 0, c4 = 0, c5 = 0, c6 = 0, c7 = 0, c8 = 0, c9 = 0, c10 = 0, c11 = 0, c12 = 0, c13 = 0, c14 = 0, c15 = 0, COUNT;
        public static List<int> count_arr = new List<int>();
        public static List<Critical_Incidents_DTO> list1 { get; set; } = new List<Critical_Incidents_DTO>();
        public static List<Complaint_DTO> list2 { get; set; } = new List<Complaint_DTO>();
        public static List<Good_News_DTO> list3 { get; set; } = new List<Good_News_DTO>();
        public static List<Emergency_Prep_DTO> list4 { get; set; } = new List<Emergency_Prep_DTO>();
        public static List<Community_Risks_DTO> list5 { get; set; } = new List<Community_Risks_DTO>();
        public static List<Visits_Others_DTO> list6 { get; set; } = new List<Visits_Others_DTO>();
        public static List<Privacy_Breaches_DTO> list7 { get; set; } = new List<Privacy_Breaches_DTO>();
        public static List<Privacy_Complaints_DTO> list8 { get; set; } = new List<Privacy_Complaints_DTO>();
        public static List<Education_DTO> list9 { get; set; } = new List<Education_DTO>();
        public static List<Labour_Relations_DTO> list10 { get; set; } = new List<Labour_Relations_DTO>();
        public static List<Immunization_DTO> list11 { get; set; } = new List<Immunization_DTO>();
        public static List<Outbreaks_DTO> list12 { get; set; } = new List<Outbreaks_DTO>();
        public static List<WSIB_DTO> list13 { get; set; } = new List<WSIB_DTO>();
        public static List<Not_WSIBs_DTO> list14 { get; set; } = new List<Not_WSIBs_DTO>();
        public static List<LicensingInspectionDTO> list15 { get; set; } = new List<LicensingInspectionDTO>();// = db.ReadLiceInspect().ToList();
        public static List<AssistedLivingInspectionDTO> list16 { get; set; } = new List<AssistedLivingInspectionDTO>();
        public static List<WorkshopBCInspection_DTO> list17 { get; set; } = new List<WorkshopBCInspection_DTO>();
        public static List<QualityReview_DTO> list18 { get; set; } = new List<QualityReview_DTO>();
        public static List<BC_LTC_Reportable_Incidents_DTO> list19 { get; set; } = new List<BC_LTC_Reportable_Incidents_DTO>();
        public static List<BC_Assisted_Living_Reportable_Incidents_DTO> list20 = new List<BC_Assisted_Living_Reportable_Incidents_DTO>();
        public static List<MOH_Inspection_DTO> list21 { get; set; } = new List<MOH_Inspection_DTO>();
        public static List<InspectionInfo_DTO> list22 { get; set; } = new List<InspectionInfo_DTO>();
        public static object[] all = new object[]
        { list1, list2, list3, list4, list5, list6, list7, list8, list9, list10, list11,
            list12, list13, list14, list15, list16, list17, list18, list19, list20, list21, list22
        };

        public void ResetAllTabls()
        {
            list1 = new List<Critical_Incidents_DTO>();
            list2 = new List<Complaint_DTO>();
            list3 = new List<Good_News_DTO>();
            list4 = new List<Emergency_Prep_DTO>();
            list5 = new List<Community_Risks_DTO>();
            list6 = new List<Visits_Others_DTO>();
            list7 = new List<Privacy_Breaches_DTO>();
            list8 = new List<Privacy_Complaints_DTO>();
            list9 = new List<Education_DTO>();
            list10 = new List<Labour_Relations_DTO>();
            list11 = new List<Immunization_DTO>();
            list12 = new List<Outbreaks_DTO>();
            list13 = new List<WSIB_DTO>();
            list14 = new List<Not_WSIBs_DTO>();
            list15 = new List<LicensingInspectionDTO>();// = db.ReadLiceInspect().ToList();
            list16 = new List<AssistedLivingInspectionDTO>();
            list17 = new List<WorkshopBCInspection_DTO>();
            list18 = new List<QualityReview_DTO>();
            list19 = new List<BC_LTC_Reportable_Incidents_DTO>();
            list20 = new List<BC_Assisted_Living_Reportable_Incidents_DTO>();
            list21 = new List<MOH_Inspection_DTO>();
            list22 = new List<InspectionInfo_DTO>();
        }
    }
}