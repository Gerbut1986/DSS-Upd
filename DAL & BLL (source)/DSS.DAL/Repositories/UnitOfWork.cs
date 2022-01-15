namespace DSS.DAL.Repositories
{
    using DSS.DAL.Entities;
    using Interfaces;
    using EF;

    public class UnitOfWork : IUnitOfWork
    {
        readonly MyContext db; // declaration of the db connection.

        #region All Repos:
        BC_LTCRepIncidRepository BC_LTCRepo;
        BC_AssistedLivingRepository BC_AssisRepo;
        CareCommRepository careRepo;
        CICategoryRepository ciRepo;
        CommRiskRepository riskRepo;
        ComplaintRepository complRepo;
        IncidentRepository incidentRepo;
        DepartmentRepository departRepo;
        EducationRepository educRepo;
        EmergencyRepository emergRepo;
        GoodNewsRepository newsRepo;
        ImmunizationRepository immunRepo;
        LabourRepository labourRepo;
        NotWSiBRepository notWsibRepo;
        OutbreakeRepository outbrRepo;
        PBreachesRepository breachRepo;
        PComplaintsRepository complainRepo;
        PositionRepository posRepo;
        SignInMainRepository signInRepo;
        SWordsRepository sWordRepo;
        UserRepository userRepo;
        VAgencyRepository agencyRepo;
        VOthersRepository otherRepo;
        WSiBRepository wsibRepo;
        OtherRepository othersRepo;
        LicensInspecRepository inspecRepo;
        AssistLivInspecRepository assistLivRepo;
        WorkshopBCInspectRepo workshopRepo;
        QualityReviewRepo qualityRev;
        MOHInspectRepository mohRepo;
        InspectTypeRopository inspTypeRepo;
        InspectInfoRepository inspInfoRepo;
        SectionRepository sectionRepo;
        LTCHARegRepository ltchaRegRepo;
        NonCompleanceRepository nonComplRepo;
        SubsectionRepository subsectionRepo;
        OtherOptionRepository otherOptRepo;
        ZTestRepository zTestRepo;
        LoginSessionRepository logSessRepo;
        #endregion

        public UnitOfWork(string conn) => db = new MyContext(conn);

        // Properties to all repositories (readonly) each of them has opened connection to database:
        public IRepository<Home> Homes
        {
            get
            {
                if (careRepo == null)
                    careRepo = new CareCommRepository(db);
                return careRepo;
            }
        }

        public IRepository<CI_Category_Type> CI_Types
        {
            get
            {
                if (ciRepo == null)
                    ciRepo = new CICategoryRepository(db);
                return ciRepo;
            }
        }

        public IRepository<Community_Risks> CommRisks
        {
            get
            {
                if (riskRepo == null)
                    riskRepo = new CommRiskRepository(db);
                return riskRepo;
            }
        }

        public IRepository<Complaint> Complaints
        {
            get
            {
                if (complRepo == null)
                    complRepo = new ComplaintRepository(db);
                return complRepo;
            }
        }

        public IRepository<Critical_Incidents> Incidents
        {
            get
            {
                if (incidentRepo == null)
                    incidentRepo = new IncidentRepository(db);
                return incidentRepo;
            }
        }

        public IRepository<Department> Departments
        {
            get
            {
                if (departRepo == null)
                    departRepo = new DepartmentRepository(db);
                return departRepo;
            }
        }

        public IRepository<Education> Educations
        {
            get
            {
                if (educRepo == null)
                    educRepo = new EducationRepository(db);
                return educRepo;
            }
        }

        public IRepository<Emergency_Prep> EmerPreps
        {
            get
            {
                if (emergRepo == null)
                    emergRepo = new EmergencyRepository(db);
                return emergRepo;
            }
        }

        public IRepository<Good_News> GoodNews
        {
            get
            {
                if (newsRepo == null)
                    newsRepo = new GoodNewsRepository(db);
                return newsRepo;
            }
        }

        public IRepository<Immunization> Immunizations
        {
            get
            {
                if (immunRepo == null)
                    immunRepo = new ImmunizationRepository(db);
                return immunRepo;
            }
        }

        public IRepository<Labour_Relations> Relations
        {
            get
            {
                if (labourRepo == null)
                    labourRepo = new LabourRepository(db);
                return labourRepo;
            }
        }

        public IRepository<Not_WSIBs> NotWSIBs
        {
            get
            {
                if (notWsibRepo == null)
                    notWsibRepo = new NotWSiBRepository(db);
                return notWsibRepo;
            }
        }

        public IRepository<Outbreaks> Outbrakes
        {
            get
            {
                if (outbrRepo == null)
                    outbrRepo = new OutbreakeRepository(db);
                return outbrRepo;
            }
        }

        public IRepository<Position> Positions
        {
            get
            {
                if (posRepo == null)
                    posRepo = new PositionRepository(db);
                return posRepo;
            }
        }

        public IRepository<Privacy_Breaches> PrivacyBreaches
        {
            get
            {
                if (breachRepo == null)
                    breachRepo = new PBreachesRepository(db);
                return breachRepo;
            }
        }

        public IRepository<Privacy_Complaints> PrivacyComplaints
        {
            get
            {
                if (complainRepo == null)
                    complainRepo = new PComplaintsRepository(db);
                return complainRepo;
            }
        }

        public IRepository<Search_Word> SearchWords
        {
            get
            {
                if (sWordRepo == null)
                    sWordRepo = new SWordsRepository(db);
                return sWordRepo;
            }
        }

        public IRepository<Sign_in_Main> SignInMains
        {
            get
            {
                if (signInRepo == null)
                    signInRepo = new SignInMainRepository(db);
                return signInRepo;
            }
        }

        public IRepository<Users> Users
        {
            get
            {
                if (userRepo == null)
                    userRepo = new UserRepository(db);
                return userRepo;
            }
        }

        public IRepository<Visits_Agency> VisitsAgencies
        {
            get
            {
                if (agencyRepo == null)
                    agencyRepo = new VAgencyRepository(db);
                return agencyRepo;
            }
        }

        public IRepository<Visits_Others> VisitsOthers
        {
            get
            {
                if (otherRepo == null)
                    otherRepo = new VOthersRepository(db);
                return otherRepo;
            }
        }

        public IRepository<WSIB> WSIBs
        {
            get
            {
                if (wsibRepo == null)
                    wsibRepo = new WSiBRepository(db);
                return wsibRepo;
            }
        }

        public IRepository<BC_LTC_Reportable_Incidents> BC_LTCIncident
        {
            get
            {
                if (BC_LTCRepo == null)
                    BC_LTCRepo = new BC_LTCRepIncidRepository(db);
                return BC_LTCRepo;
            }
        }

        public IRepository<BC_Assisted_Living_Reportable_Incidents> BC_Assisted_Living
        {
            get
            {
                if (BC_AssisRepo == null)
                    BC_AssisRepo = new BC_AssistedLivingRepository(db);
                return BC_AssisRepo;
            }
        }

        public IRepository<Other> Others
        {
            get
            {
                if (othersRepo == null)
                    othersRepo = new OtherRepository(db);
                return othersRepo;
            }
        }

        public IRepository<LicensingInspection> LicensInspect
        {
            get
            {
                if (inspecRepo == null)
                    inspecRepo = new LicensInspecRepository(db);
                return inspecRepo;
            }
        }

        public IRepository<AssistedLivingInspection> AssistLivingInspect
        {
            get
            {
                if (assistLivRepo == null)
                    assistLivRepo = new AssistLivInspecRepository(db);
                return assistLivRepo;
            }
        }

        public IRepository<WorkshopBCInspection> WorkshopBCInspection
        {
            get
            {
                if (workshopRepo == null)
                    workshopRepo = new WorkshopBCInspectRepo(db);
                return workshopRepo;
            }
        }

        public IRepository<QualityReview> QualityReviews
        {
            get
            {
                if (qualityRev == null)
                    qualityRev = new QualityReviewRepo(db);
                return qualityRev;
            }
        }

        public IRepository<MOH_Inspection> MOHInspect
        {
            get
            {
                if (mohRepo == null)
                    mohRepo = new MOHInspectRepository(db);
                return mohRepo;
            }
        }

        public IRepository<InspectType> InspectTypes
        {
            get
            {
                if (inspTypeRepo == null)
                    inspTypeRepo = new InspectTypeRopository(db);
                return inspTypeRepo;
            }
        }

        public IRepository<InspectionInfo> InspectionInfos
        {
            get
            {
                if (inspInfoRepo == null)
                    inspInfoRepo = new InspectInfoRepository(db);
                return inspInfoRepo;
            }
        }

        public IRepository<Section> Section
        {
            get
            {
                if (sectionRepo == null)
                    sectionRepo = new SectionRepository(db);
                return sectionRepo;
            }
        }

        public IRepository<LTCHAReg> LTCHARegs
        {
            get
            {
                if (ltchaRegRepo == null)
                    ltchaRegRepo = new LTCHARegRepository(db);
                return ltchaRegRepo;
            }
        }

        public IRepository<NonCompleance> NonCompleances
        {
            get
            {
                if (nonComplRepo == null)
                    nonComplRepo = new NonCompleanceRepository(db);
                return nonComplRepo;
            }
        }

        public IRepository<Subsection> Subsections
        {
            get
            {
                if (subsectionRepo == null)
                    subsectionRepo = new SubsectionRepository(db);
                return subsectionRepo;
            }
        }

        public IRepository<OtherOption> OtherOptions
        {
            get
            {
                if (otherOptRepo == null)
                    otherOptRepo = new OtherOptionRepository(db);
                return otherOptRepo;
            }
        }

        public IRepository<ZTest> ZTest
        {
            get
            {
                if (zTestRepo == null)
                    zTestRepo = new ZTestRepository(db);
                return zTestRepo;
            }
        }

        public IRepository<LoginSession> LoginSessions
        {
            get
            {
                if (logSessRepo == null)
                    logSessRepo = new LoginSessionRepository(db);
                return logSessRepo;
            }
        }

        #region Dispose:
        bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    db.Dispose();

                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }
        #endregion

        public async System.Threading.Tasks.Task<int> SaveAsync() => await db.SaveChangesAsync();
    }
}
