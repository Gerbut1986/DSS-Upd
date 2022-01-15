namespace DSS.BLL.Interfaces
{
    using DTO;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IServiceDSS
    {
        #region Care Community:
        void Insert(Home_DTO model);
        IEnumerable<Home_DTO> ReadHomes();
        void Update(Home_DTO model);
        void DeleteCommunity(int id);
        #endregion

        #region CI Category Type:
        void Insert(CI_Category_Type_DTO model);
        IEnumerable<CI_Category_Type_DTO> ReadCICategory();
        void Update(CI_Category_Type_DTO model);
        void DeleteCICategory(int id);
        #endregion

        #region Community Risks:
        void Insert(Community_Risks_DTO model);
        IEnumerable<Community_Risks_DTO> ReadRisks();
        void Update(Community_Risks_DTO model);
        void DeleteRisk(int id);
        #endregion

        #region Complaints:
        void Insert(Complaint_DTO model);
        IEnumerable<Complaint_DTO> ReadComplaints();
        void Update(Complaint_DTO model);
        void DeleteComplaint(int id);
        #endregion

        #region Critical Incidents:
        void Insert(Critical_Incidents_DTO model);
        IEnumerable<Critical_Incidents_DTO> ReadIncidents();
        void Update(Critical_Incidents_DTO model);
        void DeleteIncident(int id);
        #endregion

        #region Department:
        void Insert(Department_DTO model);
        IEnumerable<Department_DTO> ReadDepartments();
        void Update(Department_DTO model);
        void DeleteDepartment(int id);
        #endregion

        #region Education:
        void Insert(Education_DTO model);
        IEnumerable<Education_DTO> ReadEducation();
        void Update(Education_DTO model);
        void DeleteEducation(int id);
        #endregion

        #region Emergency Prep:
        void Insert(Emergency_Prep_DTO model);
        IEnumerable<Emergency_Prep_DTO> ReadEmergency();
        void Update(Emergency_Prep_DTO model);
        void DeleteEmergency(int id);
        #endregion

        #region Good News:
        void Insert(Good_News_DTO model);
        IEnumerable<Good_News_DTO> ReadNews();
        void Update(Good_News_DTO model);
        void DeleteNews(int id);
        #endregion

        #region Immunization:
        void Insert(Immunization_DTO model);
        IEnumerable<Immunization_DTO> ReadImmunizations();
        void Update(Immunization_DTO model);
        void DeleteImmunization(int id);
        #endregion

        #region Labour Relations:
        void Insert(Labour_Relations_DTO model);
        IEnumerable<Labour_Relations_DTO> ReadRelations();
        void Update(Labour_Relations_DTO model);
        void DeleteRelation(int id);
        #endregion

        #region Not WSIB:
        void Insert(Not_WSIBs_DTO model);
        IEnumerable<Not_WSIBs_DTO> ReadNotWSIBs();
        void Update(Not_WSIBs_DTO model);
        void DeleteNotWSIB(int id);
        #endregion

        #region Outbreaks:
        void Insert(Outbreaks_DTO model);
        IEnumerable<Outbreaks_DTO> ReadOutbreaks();
        void Update(Outbreaks_DTO model);
        void DeleteOutbreak(int id);
        #endregion

        #region Position:
        void Insert(Position_DTO model);
        IEnumerable<Position_DTO> ReadPositions();
        void Update(Position_DTO model);
        void DeletePosition(int id);
        #endregion

        #region Privacy Breaches:
        void Insert(Privacy_Breaches_DTO model);
        IEnumerable<Privacy_Breaches_DTO> ReadBreaches();
        void Update(Privacy_Breaches_DTO model);
        void DeleteBreach(int id);
        #endregion

        #region Privacy Complaints:
        void Insert(Privacy_Complaints_DTO model);
        IEnumerable<Privacy_Complaints_DTO> ReadPComplaints();
        void Update(Privacy_Complaints_DTO model);
        void DeletePComplaint(int id);
        #endregion

        #region Search Word:
        void Insert(Search_Word_DTO model);
        IEnumerable<Search_Word_DTO> ReadWords();
        void Update(Search_Word_DTO model);
        void DeleteWord(int id);
        #endregion

        #region Sign In Main:
        void Insert(Sign_in_Main_DTO model);
        IEnumerable<Sign_in_Main_DTO> ReadSignIn();
        void Update(Sign_in_Main_DTO model);
        void DeletesignIn(int id);
        #endregion

        #region Users:
        void Insert(Users_DTO model);
        IEnumerable<Users_DTO> ReadUsers();
        void Update(Users_DTO model);
        void DeleteUser(int id);
        #endregion

        #region Visits Agency:
        void Insert(Visits_Agency_DTO model);
        IEnumerable<Visits_Agency_DTO> ReadAgencies();
        void Update(Visits_Agency_DTO model);
        void DeleteAgency(int id);
        #endregion

        #region Visits Others:
        void Insert(Visits_Others_DTO model);
        IEnumerable<Visits_Others_DTO> ReadOthers();
        void Update(Visits_Others_DTO model);
        void DeleteOther(int id);
        #endregion

        #region WSIB:
        void Insert(WSIB_DTO model);
        IEnumerable<WSIB_DTO> ReadWSiBs();
        void Update(WSIB_DTO model);
        void DeleteWSiB(int id);
        #endregion

        #region LicensingInspections:
        void Insert(LicensingInspectionDTO model);
        IEnumerable<LicensingInspectionDTO> ReadLiceInspect();
        void Update(LicensingInspectionDTO model);
        Task DeleteLiceInspByIdAsync(int id);
        #endregion

        #region AssistedLivingInspections:
        void Insert(AssistedLivingInspectionDTO model);
        IEnumerable<AssistedLivingInspectionDTO> ReadAssLivInspect();
        void Update(AssistedLivingInspectionDTO model);
        Task DeleteAssLivInspByIdAsync(int id);
        #endregion

        #region WorkshopBCInspect:
        void Insert(WorkshopBCInspection_DTO model);
        IEnumerable<WorkshopBCInspection_DTO> ReadWorkshopBCInspect();
        void Update(WorkshopBCInspection_DTO model);
        Task DeleteWorkshopBCIInspByIdAsync(int id);
        #endregion

        #region QualityReviews:
        void Insert(QualityReview_DTO model);
        IEnumerable<QualityReview_DTO> ReadQualityReviews();
        void Update(QualityReview_DTO model);
        Task DeleteQualityReviewAsync(int id);
        #endregion

        #region For Other (New one):
        Task DeleteOtherByOIdAsync(int id);

        IEnumerable<OtherDTO> ReadOthersO();
        #endregion

        #region Activities(NoN implementation):
        //void Insert(Activities_DTO model);
        //IEnumerable<Activities_DTO> ReadActivities();
        //void Update(Activities_DTO model);
        //void DeleteActivity(int id);
        #endregion

        #region ZTest:
        void Insert(ZTest_DTO model);
        IEnumerable<ZTest_DTO> ReadZTest();
        void Update(ZTest_DTO model);
        Task DeleteZTestByIdAsync(int id);
        #endregion

        #region ZTest:
        void Insert(LoginSession_DTO model);
        IEnumerable<LoginSession_DTO> ReadLogSessions();
        Task DeleteLogSessionByIdAsync(int id);
        void Update(LoginSession_DTO dto);
        #endregion
    }
}
