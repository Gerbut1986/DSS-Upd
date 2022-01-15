namespace DSS.DAL.EF
{
    using DSS.DAL.Entities;
    using System.Data.Entity;

    // codefirst approach - entity framework(DbContext is a part of EntityFramework library)
    public class MyContext : DbContext
    {
        //constructor w/ a base class parameter (makes it mandatory to pass a connection string) inherited from the base class
        public MyContext(string connection) : base(connection)
        { 
        }

        // DbSet is a generic collection that represents each table in DSS database
        // Property name has to match the table name
        public virtual DbSet<Critical_Incidents> Critical_Incidents { get; set; }
        public virtual DbSet<Home> Homes { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Labour_Relations> Relations { get; set; }
        public virtual DbSet<Community_Risks> Community_Risks { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Good_News> Good_News { get; set; }
        public virtual DbSet<Sign_in_Main> Sign_in_Mains { get; set; }
        public virtual DbSet<Visits_Agency> Visits_Agencies { get; set; }
        public virtual DbSet<WSIB> WSIBs { get; set; }
        public virtual DbSet<Not_WSIBs> Not_WSIBs { get; set; }
        public virtual DbSet<Visits_Others> Visits_Others { get; set; }
        public virtual DbSet<Outbreaks> Outbreaks { get; set; }
        public virtual DbSet<Complaint> Complaints { get; set; }
        public virtual DbSet<CI_Category_Type> CI_Category_Types { get; set; }
        public virtual DbSet<Immunization> Immunizations { get; set; }
        public virtual DbSet<Privacy_Breaches> Privacy_Breaches { get; set; }
        public virtual DbSet<Privacy_Complaints> Privacy_Complaints { get; set; }
        public virtual DbSet<Education> Educations { get; set; }
        public virtual DbSet<Emergency_Prep> Emergency_Prep { get; set; }
        public virtual DbSet<Search_Word> Search_Words { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Other> Others { get; set; }
        public virtual DbSet<BC_LTC_Reportable_Incidents> BC_LTC_Reportable_Incidents { get; set; }
        public virtual DbSet<BC_Assisted_Living_Reportable_Incidents> BC_LTC_Assisted_Incidents { get; set; }
        public virtual DbSet<LicensingInspection> LicensingInspections { get; set; }
        public virtual DbSet<AssistedLivingInspection> AssistedLivingInspections { get; set; }
        public virtual DbSet<WorkshopBCInspection> WorkshopBCInspections { get; set; }
        public virtual DbSet<QualityReview> QualityReviews { get; set; }
        public virtual DbSet<MOH_Inspection> MOH_Inspections { get; set; }
        public virtual DbSet<InspectionInfo> InspectionInfos { get; set; }
        public virtual DbSet<InspectType> InspectTypes { get; set; }
        public virtual DbSet<NonCompleance> NonCompleances { get; set; }
        public virtual DbSet<LTCHAReg> LTCHARegs { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<Subsection> Subsections { get; set; }
        public virtual DbSet<OtherOption> OtherOptions { get; set; }
        public virtual DbSet<ZTest> ZTest { get; set; }
        public virtual DbSet<LoginSession> LoginSessions { get; set; }
    }
}
