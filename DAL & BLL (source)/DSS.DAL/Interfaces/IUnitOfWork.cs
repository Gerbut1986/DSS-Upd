namespace DSS.DAL.Interfaces
{
    using Entities;

    /// <summary>
    ///An interface, which unites all repositories
    /// </summary>
    public interface IUnitOfWork : System.IDisposable
    {
        //{get;} is for read-only access
        IRepository<Home> Homes { get; }
        IRepository<CI_Category_Type> CI_Types { get; }
        IRepository<Community_Risks> CommRisks { get; }
        IRepository<Complaint> Complaints { get; }
        IRepository<Critical_Incidents> Incidents { get; }
        IRepository<Department> Departments { get; }
        IRepository<Education> Educations { get; }
        IRepository<Emergency_Prep> EmerPreps { get; }
        IRepository<Good_News> GoodNews { get; }
        IRepository<Immunization> Immunizations { get; }
        IRepository<Labour_Relations> Relations { get; }
        IRepository<Not_WSIBs> NotWSIBs { get; }
        IRepository<Outbreaks> Outbrakes { get; }
        IRepository<Position> Positions { get; }
        IRepository<Privacy_Breaches> PrivacyBreaches { get; }
        IRepository<Privacy_Complaints> PrivacyComplaints { get; }
        IRepository<Search_Word> SearchWords { get; }
        IRepository<Sign_in_Main> SignInMains { get; }
        IRepository<Users> Users { get; }
        IRepository<Visits_Agency> VisitsAgencies { get; }
        IRepository<Visits_Others> VisitsOthers { get; }
        IRepository<WSIB> WSIBs { get; }
        IRepository<Other> Others { get; }
        IRepository<BC_LTC_Reportable_Incidents> BC_LTCIncident { get; }
        IRepository<BC_Assisted_Living_Reportable_Incidents> BC_Assisted_Living { get; }
        IRepository<LicensingInspection> LicensInspect { get; }
        IRepository<AssistedLivingInspection> AssistLivingInspect { get; }
        IRepository<WorkshopBCInspection> WorkshopBCInspection { get; }
        IRepository<QualityReview> QualityReviews { get; }
        IRepository<MOH_Inspection> MOHInspect { get; }
        IRepository<InspectType> InspectTypes { get; }
        IRepository<InspectionInfo> InspectionInfos { get; }
        IRepository<Section> Section { get; }
        IRepository<LTCHAReg> LTCHARegs { get; }
        IRepository<NonCompleance> NonCompleances { get; }
        IRepository<Subsection> Subsections { get; }
        IRepository<OtherOption> OtherOptions { get; }
        IRepository<ZTest> ZTest { get; }
        IRepository<LoginSession> LoginSessions { get; }
        System.Threading.Tasks.Task<int> SaveAsync();
    }

    
}
