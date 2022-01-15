namespace DTS
{
    using System;
    using Models;
    using System.Linq;
    using DSS.BLL.DTO;
    using System.Web.Mvc;
    using DTS.Controllers;
    using DSS.BLL.Services;
    using System.Collections;
    using DTS.Models.RegionLogic;
    using System.Collections.Generic;

    public class AppSettings
    {
        #region static Fields:
        public static ServiceDSS Db;
        public static IEnumerable<Home_DTO> homes { get; protected set; }
        public static IEnumerable<Position_DTO> positions { get; protected set; }
        public static IEnumerable<CI_Category_Type_DTO> ci_types { get; protected set; }
        public static IEnumerable<InspectType_DTO> inspectTypes { get; protected set; }
        #endregion
        static AppSettings()
        {
        }

        public static async System.Threading.Tasks.Task FillLocListAsync(ServiceDSS db)
        {
            db = new ServiceDSS(Init.ConnectionStrAdm);
            homes = await db.ReadHomesAsync();

            ci_types = await db.ReadCICategoryAsync();
            positions = await db.ReadPositionsAsync();
            inspectTypes = await db.ReadInspectTypesAsync();
        }

        public static void Locs_CI_Pos_Init(ServiceDSS db)
        {
            //db = new ServiceDSS(Init.ConnectionStrAdm);
            homes = db.ReadHomes();

            ci_types = db.ReadCICategory();
            positions = db.ReadPositions();
            inspectTypes = db.ReadInspectTypes();
        }

        #region Initialization of Lists(SelectList's):
        public static void ListsInit(out ServiceDSS db, DSS.BLL.STREAM str)
        {

            //HomeController.IsInit = true;
            #region Initialize of stringLists:
            HomeController.SelectYesNo = new string[] { "Yes", "No" };
            HomeController.visit = new string[] { "Visit", "Phone Call" };
            HomeController.written = new string[] { "Verbal", "Written" };
            HomeController.direct = new string[] { "Direct", "Corporate", "Both" };
            HomeController.resident = new string[] { "Resident", "Family", "Visitor", "Staff", "Other" };
            HomeController.resolved = new string[] { "Yes", "No", "Ongoing" };
            HomeController.ministry = new string[] { "Yes", "No" };
            HomeController.categoryGoodNews = new string[] { "Good News", "Compliments" };
            HomeController.departmentGoodNews = new string[] { "Nursing", "Nursing Admin", "Admin", "Programs", "Food Service", "Maintainence", "Housekeeping", "Laundry", "Other" };
            HomeController.sourceGoodNews = new string[] { "Let's Connect", "Card", "Email", "Letter", "Verbal", "Other" };
            HomeController.receiveFrom = new string[] { "Resident", "Family", "Supplier", "SSO", "Manager", "Leadership", "Tour", "Other" };
            HomeController.picture = new string[] { "Yes", "No" };
            HomeController.visitAgency = new string[] { "MOH", "Fire", "TSSA", "Public Health", "PCQO", "QR Health Authority", "CNS", "Public Health - MHO", "Public Health - EHO", "Other" };
            HomeController.visitnumbers = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
            HomeController.department2 = new string[] {"All","Nursing","Housekeeping","Laundry","Maintenance","Dietary","Recriation","Administration","Individual(s)",
                                            "Physio", "Hairdresser", "Physician","Foot care", "Dental", "Other", "Yes", "No"};
            HomeController.risk_list = new string[] { "Adverse Event", "Environmental", "Infortmation Technology", "Insurance", "Legal", "Near Miss", "Serious Adverse Event",
                     "Vendor", "Workplace Harrasement", "Other", "Health Information Reques" };
            HomeController.bc_ttc1 = new string[]
            {
                "Aggression between persons in care", "Aggressive or Unusual behaviour", "Attempted Suicide", "Choking", "Disease outbreak or occurrence",
                "Emergency restraint", "Emotional Abuse", "Fall", "Financial Abuse", "Food Poisoning", "Medication Error", "Missing or Wandering",
                "Motor Vehicle Injury", "Neglect", "Physical Abuse", "Poisoning", "Service Delivery Problem", "Sexual Abuse", "Unexpected Illness",
                "Other injury", "Expected Death", "Unexpected Death",
            };
            HomeController.bc_ltc2 = new string[] { "Physician/NP", "Contact person or Representative", "RCMP/Police", "Coroner – for unexpected deaths" };
            // For BC LTC Assisted:
            HomeController.bc_ltc_assist = new string[] { "Family", "Coroner" };
            // For EmergencyPrep drodown's:
            HomeController.exercises = new string[] { "Day", "Evening", "Night" };
            HomeController.codes = new string[] { "Red", "Black", "White", "Blue", "Green", "Orange", "Yellow", "Brown", "Gray" };
            HomeController.methods = new string[] { "Table Top", "Actual" };
            // For 'Licensing Inspections' tbl:
            HomeController.inspectType = new string[] { "Complaint", "Reportable Incident", "Routine", "Follow-up" };
            HomeController.contraventions = new string[] { "Residential Care Regulations", "Community Care and Assisted Living Act" };
            HomeController.scopeOfInspects = new string[] { "Outstanding", "Complied", "Closed ", "Rescinded" };
            #endregion

            db = new ServiceDSS(Init.ConnectionStrAdm);
            Db = db;
            //new UnmanageCode(db);
            //str = new DSS.BLL.STREAM(db);
            //System.Threading.Tasks.Task.FromResult(FillLocListAsync(db));
            Locs_CI_Pos_Init(db);
            HomeController.AllLocations = UnmanageCode.ReadLocFromFile();
            HomeController.communities = homes.ToList();
            HomeController.list = new SelectList(HomeController.communities, "Id", "Full_Home_Name");

            HomeController.positions = db.ReadPositions().ToList();
            //positions.ToList();
            HomeController.list2 = new SelectList(HomeController.positions, "Id", "Name");

            HomeController.categories = db.ReadCICategory().ToList();
            //ci_types.ToList();
            HomeController.list3 = new SelectList(HomeController.categories, "Id", "Name");

            HomeController.list4 = new SelectList(HomeController.SelectYesNo);  // For some attributes table Clritical Incident
            HomeController.list5 = new SelectList(HomeController.visit);        // for one attribute MOHLTC_Follow_Up the same table

            // for the Complaints table:
            HomeController.list6 = new SelectList(HomeController.written);
            HomeController.list7 = new SelectList(HomeController.direct);
            HomeController.list8 = new SelectList(HomeController.resident);
            HomeController.list9 = new SelectList(HomeController.departmentGoodNews);
            HomeController.list10 = new SelectList(HomeController.resolved);
            HomeController.list11 = new SelectList(HomeController.ministry);

            // for GoodNews table:
            HomeController.list12 = new SelectList(HomeController.categoryGoodNews);
            HomeController.list13 = new SelectList(HomeController.department2);
            HomeController.list14 = new SelectList(HomeController.sourceGoodNews);
            HomeController.list15 = new SelectList(HomeController.receiveFrom);
            HomeController.list16 = new SelectList(HomeController.picture);

            // for Community Risks:
            HomeController.list17 = new SelectList(HomeController.risk_list);

            // for Visit Agency:
            HomeController.list18 = new SelectList(HomeController.visitAgency);
            HomeController.list19 = new SelectList(HomeController.visitnumbers);

            // for Departments:
            HomeController.departments = db.ReadDepartments().ToList();
            HomeController.list20 = new SelectList(HomeController.departments, "Id", "Name");

            // for BC_TLC:
            HomeController.list21 = new SelectList(HomeController.bc_ttc1);
            HomeController.list22 = new SelectList(HomeController.bc_ltc2);

            // for EmergencyPrep:
            HomeController.list23 = new SelectList(HomeController.codes);
            HomeController.list24 = new SelectList(HomeController.exercises);
            HomeController.list25 = new SelectList(HomeController.methods);

            // for BC Licensing Inspections:
            HomeController.list26 = new SelectList(HomeController.inspectType);
            HomeController.list27 = new SelectList(HomeController.contraventions);

            // for Worksave BC tbl:
            HomeController.list28 = new SelectList(HomeController.scopeOfInspects);

            // for BC_TLC Assisted:
            HomeController.list29 = new SelectList(HomeController.bc_ltc_assist);
        }
        #endregion

        #region Clear All Memberes for Summary Logic(for Logout):
        public string ClearAllSummary(bool check)
        {
            if (check)
            {
                IncidentSummaryLogic.ClearAllStatic();
                ComplaintSummaryLogic.ClearAllStatic();
                GoodNewsSummaryLogic.ClearAllStatic();
                BreachesSummuryLogic.ClearAllStatic();
                CommRiskSummaryLogic.ClearAllStatic();
                EducationSummaryLogic.ClearAllStatic();
                EmergencySummaryLogic.ClearAllStatic();
                ImmunizationSummaryLogic.ClearAllStatic();
                LabourRelationSummaryLogic.ClearAllStatic();
                NotWSIBSummaryLogic.ClearAllStatic();
                OutbreaksSummaryLogic.ClearAllStatic();
                PComplaintSummaryLogic.ClearAllStatic();
                WSIBSummaryLogic.ClearAllStatic();
                LiceInspecSummaryLogic.ClearAllStatic();
                AssistLivInspectSummaryLogic.ClearAllStatic();
                WorksaveBCSummaryLogic.ClearAllStatic();
                QltyRevSummaryLogic.ClearAllStatic();
                BC_LTCSummaryLogic.ClearAllStatic();
                BC_AssistSummaryLogic.ClearAllStatic();
                return "All Summary Logic counters & members has Clear!";
            }
            else return Logger.Logger.Write("This method should pass bool param (true) to clear it..");
        }
        #endregion

        #region Showing List:
        public static ArrayList ShowingList
            (string tblId = "", string btnName = "", Range withOrOut = Range.NoN, ServiceDSS db = null, int userLocation = 0, string user_role = ""
            , int regionNumber = 0, DateTime start = default, DateTime end = default)
        {
            string[] regions = default;
            //var currentUser =
            Role role = GetRoleEnum(user_role);
            ArrayList retList = new ArrayList();

            switch (tblId)
            {
                #region Critical Incidents #1:
                case "1":
                    if (role == Role.User) // if we entered as user role
                    {
                        if (withOrOut == Range.With)
                            TablesContainer.list1 = (from c in db.ReadIncidents() where c.Date >= start && c.Date <= end select c)
                                .Where(l => l.Location == userLocation).ToList();
                        else
                            TablesContainer.list1 = db.ReadIncidents().Where(l => l.Location == userLocation).ToList();
                    }
                    else if (role == Role.RegionalUser) // if we entered as SuperUser role
                        IsRegionalUser(new Critical_Incidents_DTO(), regionNumber,withOrOut, start, end);
                    else // for SuperUser's role
                    {
                        if (withOrOut == Range.With)
                            TablesContainer.list1 = (from c in db.ReadIncidents() where c.Date >= start && c.Date <= end select c).ToList();
                        else TablesContainer.list1 = db.ReadIncidents().ToList();
                    }
                    if (TablesContainer.list1.Count == 0) return null;
                    TablesContainer.list1 = TablesContainer.list1.OrderBy(x => x.Date).ToList();
                    if (btnName.Equals("-upSort"))
                        TablesContainer.list1 = TablesContainer.list1.OrderBy(x => x.Date).ToList();
                    else if (btnName.Equals("-downSort"))
                        TablesContainer.list1 = TablesContainer.list1.OrderByDescending(x => x.Date).ToList();
                    retList.Add(TablesContainer.list1);
                    break;
                #endregion
                #region Complaints #2:
                case "2":
                    if (role == Role.User)
                        if (withOrOut == Range.With)
                            TablesContainer.list2 = (from ent in db.ReadComplaints() where ent.DateReceived >= start && ent.DateReceived <= end select ent).Where(l => l.Location == userLocation).ToList();
                        else TablesContainer.list2 = db.ReadComplaints().Where(l => l.Location == userLocation).ToList();
                    else if (role == Role.RegionalUser)
                        IsRegionalUser(new Complaint_DTO(), regionNumber, withOrOut, start, end);
                    else  // Administrator
                    {
                        if (withOrOut == Range.With)
                            TablesContainer.list2 = (from ent in db.ReadComplaints() where ent.DateReceived >= start && ent.DateReceived <= end select ent).ToList();
                        else TablesContainer.list2 = db.ReadComplaints().ToList();
                    }
                    if (TablesContainer.list2.Count == 0) return null;
                    TablesContainer.list2 = TablesContainer.list2.OrderBy(x => x.DateReceived).ToList();
                    if (btnName.Equals("-upSort"))
                        TablesContainer.list2 = TablesContainer.list2.OrderBy(x => x.DateReceived).ToList();
                    else if (btnName.Equals("-downSort"))
                        TablesContainer.list2 = TablesContainer.list2.OrderByDescending(x => x.DateReceived).ToList();
                    retList.Add(TablesContainer.list2);
                    break;
                #endregion
                #region Good News #3:
                case "3":
                    if (role == Role.User)
                    {
                        if (withOrOut == Range.With)
                            TablesContainer.list3 = (from c in db.ReadNews() where c.DateNews >= start && c.DateNews <= end select c).Where(l => l.Location == userLocation).ToList();
                        else TablesContainer.list3 = db.ReadNews().Where(l => l.Location == userLocation).ToList();
                    }
                    else if (role == Role.RegionalUser) // if we entered as SuperUser role
                        IsRegionalUser(new Good_News_DTO(), regionNumber, withOrOut, start, end);
                    else //admin
                    {
                        if (withOrOut == Range.With)
                            TablesContainer.list3 = (from c in db.ReadNews() where c.DateNews >= start && c.DateNews <= end select c).ToList();
                        else TablesContainer.list3 = db.ReadNews().ToList();
                    }
                    if (TablesContainer.list3.Count == 0) return null;
                    TablesContainer.list3 = TablesContainer.list3.OrderBy(x => x.DateNews).ToList();
                    if (btnName.Equals("-upSort"))
                        TablesContainer.list3 = TablesContainer.list3.OrderBy(x => x.DateNews).ToList();
                    else if (btnName.Equals("-downSort"))
                        TablesContainer.list3 = TablesContainer.list3.OrderByDescending(x => x.DateNews).ToList();
                    retList.Add(TablesContainer.list3);
                    break;
                #endregion
                #region Emergency prep #4:
                case "4":
                    if (role == Role.User)
                        if (withOrOut == Range.With)
                            TablesContainer.list4 = (from c in db.ReadEmergency() where c.Date >= start && c.Date <= end select c).Where(l => l.Location == userLocation).ToList();
                        else TablesContainer.list4 = db.ReadEmergency().Where(l => l.Location == userLocation).ToList();
                    else if (role == Role.RegionalUser)
                        IsRegionalUser(new Emergency_Prep_DTO(), regionNumber, withOrOut, start, end);
                    else
                        if (withOrOut == Range.With)
                            TablesContainer.list4 = (from c in db.ReadEmergency() where c.Date >= start && c.Date <= end select c).ToList();
                        else TablesContainer.list4 = db.ReadEmergency().ToList();                  
                    if (TablesContainer.list4.Count == 0) return null;
                    TablesContainer.list4 = TablesContainer.list4.OrderBy(x => x.Date).ToList();
                    if (btnName.Equals("-upSort"))
                        TablesContainer.list4 = TablesContainer.list4.OrderBy(x => x.Date).ToList();
                    else if (btnName.Equals("-downSort"))
                        TablesContainer.list4 = TablesContainer.list4.OrderByDescending(x => x.Date).ToList();
                    retList.Add(TablesContainer.list4);
                    break;
                #endregion
                #region Community Risks #5:
                case "5":
                    if (role == Role.User)
                        if (withOrOut == Range.With)
                            TablesContainer.list5 = (from c in db.ReadRisks() where c.Date >= start && c.Date <= end select c).Where(l => l.Location == userLocation).ToList();
                        else TablesContainer.list5 = db.ReadRisks().Where(l => l.Location == userLocation).ToList();
                    else if (role == Role.RegionalUser)
                        IsRegionalUser(new Community_Risks_DTO(), regionNumber, withOrOut, start, end);
                    else // admin
                        if (withOrOut == Range.With)
                            TablesContainer.list5 = (from c in db.ReadRisks() where c.Date >= start && c.Date <= end select c).ToList();
                        else TablesContainer.list5 = db.ReadRisks().ToList();
                    if (TablesContainer.list5.Count == 0) return null;
                    TablesContainer.list5 = TablesContainer.list5.OrderBy(x => x.Date).ToList();
                    if (btnName.Equals("-upSort"))
                        TablesContainer.list5 = TablesContainer.list5.OrderBy(x => x.Date).ToList();
                    else if (btnName.Equals("-downSort"))
                        TablesContainer.list5 = TablesContainer.list5.OrderByDescending(x => x.Date).ToList();
                    retList.Add(TablesContainer.list5);
                    break;
                #endregion
                #region Privacy Breaches #16:
                case "16":
                    if (role == Role.User)
                    {
                        if (withOrOut == Range.With)
                            TablesContainer.list7 = (from c in db.ReadBreaches() where c.Date_Breach_Occurred >= start && c.Date_Breach_Occurred <= end select c).Where(l => l.Location == userLocation).ToList();
                        else TablesContainer.list7 = db.ReadBreaches().Where(l => l.Location == userLocation).ToList();
                    }
                    else if (role == Role.RegionalUser) // if we entered as SuperUser role
                        IsRegionalUser(new Privacy_Breaches_DTO(), regionNumber, withOrOut, start, end);
                    else //admin
                    {
                        if (withOrOut == Range.With)
                            TablesContainer.list7 = (from c in db.ReadBreaches() where c.Date_Breach_Occurred >= start && c.Date_Breach_Occurred <= end select c).ToList();
                        else TablesContainer.list7 = db.ReadBreaches().ToList();
                    }
                    if (TablesContainer.list7.Count == 0) return null;
                    TablesContainer.list7 = TablesContainer.list7.OrderBy(x => x.Date_Breach_Occurred).ToList();
                    if (btnName.Equals("-upSort"))
                        TablesContainer.list7 = TablesContainer.list7.OrderBy(x => x.Date_Breach_Occurred).ToList();
                    else if (btnName.Equals("-downSort"))
                        TablesContainer.list7 = TablesContainer.list7.OrderByDescending(x => x.Date_Breach_Occurred).ToList();
                    retList.Add(TablesContainer.list7);
                    break;
                #endregion
                #region Privacy Complaint #17:
                case "17":
                    if (role == Role.User)
                    {
                        if (withOrOut == Range.With)
                            TablesContainer.list8 = (from c in db.ReadPComplaints() where c.Date_Complain_Received >= start && c.Date_Complain_Received <= end select c).Where(l => l.Location == userLocation).ToList();
                        else TablesContainer.list8 = db.ReadPComplaints().Where(l => l.Location == userLocation).ToList();
                    }
                    else if (role == Role.RegionalUser) // if we entered as SuperUser role
                        IsRegionalUser(new Privacy_Complaints_DTO(), regionNumber, withOrOut, start, end);
                    else // admiin
                    {
                        if (withOrOut == Range.With)
                            TablesContainer.list8 = (from c in db.ReadPComplaints() where c.Date_Complain_Received >= start && c.Date_Complain_Received <= end select c).ToList();
                        else TablesContainer.list8 = db.ReadPComplaints().ToList();
                    }
                    if (TablesContainer.list8.Count == 0) return null;
                    TablesContainer.list8 = TablesContainer.list8.OrderBy(x => x.Date_Complain_Received).ToList();
                    if (btnName.Equals("-upSort"))
                        TablesContainer.list8 = TablesContainer.list8.OrderBy(x => x.Date_Complain_Received).ToList();
                    else if (btnName.Equals("-downSort"))
                        TablesContainer.list8 = TablesContainer.list8.OrderByDescending(x => x.Date_Complain_Received).ToList();
                    retList.Add(TablesContainer.list8);
                    break;
                #endregion
                #region Educations #18 (This form is NOT usage):
                case "18":
                    if (role == Role.User)
                    {
                        if (withOrOut == Range.With)
                            TablesContainer.list9 = (from c in db.ReadEducation() where c.DateStart >= start && c.DateStart <= end select c).Where(l => l.Location == userLocation).ToList();
                        else TablesContainer.list9 = db.ReadEducation().Where(l => l.Location == userLocation).ToList();
                    }
                    else // if we entered as user role
                    {
                        if (withOrOut == Range.With)
                            TablesContainer.list9 = (from c in db.ReadEducation() where c.DateStart >= start && c.DateStart <= end select c).ToList();
                        else TablesContainer.list9 = db.ReadEducation().ToList();
                    }
                    TablesContainer.list9 = TablesContainer.list9.OrderBy(x => x.DateStart).ToList();
                    if (btnName.Equals("-upSort"))
                        TablesContainer.list9 = TablesContainer.list9.OrderBy(x => x.DateStart).ToList();
                    else if (btnName.Equals("-downSort"))
                        TablesContainer.list9 = TablesContainer.list9.OrderByDescending(x => x.DateStart).ToList();
                    retList.Add(TablesContainer.list9);
                    break;
                #endregion
                #region Labour Relations #19:
                case "19":
                    if (role == Role.User)
                    {
                        if (withOrOut == Range.With)
                            TablesContainer.list10 = (from c in db.ReadRelations() where c.Date >= start && c.Date <= end select c).Where(l => l.Location == userLocation).ToList();
                        else TablesContainer.list10 = db.ReadRelations().Where(l => l.Location == userLocation).ToList();
                    }
                    else if (role == Role.RegionalUser) // if we entered as SuperUser role
                        IsRegionalUser(new Critical_Incidents_DTO(), regionNumber);
                    else // if we entered as user role
                    {
                        if (withOrOut == Range.With)
                            TablesContainer.list10 = (from c in db.ReadRelations() where c.Date >= start && c.Date <= end select c).ToList();
                        else TablesContainer.list10 = db.ReadRelations().ToList();
                    }
                    TablesContainer.list10 = TablesContainer.list10.OrderBy(x => x.Date).ToList();
                    if (btnName.Equals("-upSort"))
                        TablesContainer.list10 = TablesContainer.list10.OrderBy(x => x.Date).ToList();
                    else if (btnName.Equals("-downSort"))
                        TablesContainer.list10 = TablesContainer.list10.OrderByDescending(x => x.Date).ToList();
                    retList.Add(TablesContainer.list10);
                    break;
                #endregion
                #region Immunizations #20 (This form is NOT usage):
                case "20":
                    if (role == Role.User)
                        TablesContainer.list11 = db.ReadImmunizations().Where(l => l.Location == userLocation).ToList();
                    else TablesContainer.list11 = db.ReadImmunizations().ToList();
                    retList.Add(TablesContainer.list11);
                    break;
                #endregion
                #region Outbrakes #21:
                case "21":
                    if (role == Role.User)
                    {
                        if (withOrOut == Range.With)
                            TablesContainer.list12 = (from c in db.ReadOutbreaks() where c.Date_Declared >= start && c.Date_Declared <= end select c).Where(l => l.Location == userLocation).ToList();
                        else TablesContainer.list12 = db.ReadOutbreaks().Where(l => l.Location == userLocation).ToList();
                    }
                    else if (role == Role.RegionalUser) // if we entered as SuperUser role
                        IsRegionalUser(new Outbreaks_DTO(), regionNumber, withOrOut, start, end);
                    else // if we entered as user role
                    {
                        if (withOrOut == Range.With)
                            TablesContainer.list12 = (from c in db.ReadOutbreaks() where c.Date_Declared >= start && c.Date_Declared <= end select c).ToList();
                        else TablesContainer.list12 = db.ReadOutbreaks().ToList();
                    }
                    if (TablesContainer.list12.Count == 0) return null;
                    TablesContainer.list12 = TablesContainer.list12.OrderBy(x => x.Date_Declared).ToList();
                    if (btnName.Equals("-upSort"))
                        TablesContainer.list12 = TablesContainer.list12.OrderBy(x => x.Date_Declared).ToList();
                    else if (btnName.Equals("-downSort"))
                        TablesContainer.list12 = TablesContainer.list12.OrderByDescending(x => x.Date_Declared).ToList();
                    retList.Add(TablesContainer.list12);
                    break;
                #endregion
                #region WSIB #22:
                case "22":
                    if (role == Role.User)
                    {
                        if (withOrOut == Range.With)
                            TablesContainer.list13 = (from c in db.ReadWSiBs() where c.Date_Accident >= start && c.Date_Accident <= end select c).Where(l => l.Location == userLocation).ToList();
                        else TablesContainer.list13 = db.ReadWSiBs().Where(l => l.Location == userLocation).ToList();
                    }
                    else if (role == Role.RegionalUser) // if we entered as SuperUser role
                        IsRegionalUser(new WSIB_DTO(), regionNumber, withOrOut, start, end);
                    else // if we entered as user role
                    {
                        if (withOrOut == Range.With)
                            TablesContainer.list13 = (from c in db.ReadWSiBs() where c.Date_Accident >= start && c.Date_Accident <= end select c).ToList();
                        else TablesContainer.list13 = db.ReadWSiBs().ToList();
                    }
                    if (TablesContainer.list13.Count == 0) return null;
                    TablesContainer.list13 = TablesContainer.list13.OrderBy(x => x.Date_Accident).ToList();
                    if (btnName.Equals("-upSort"))
                        TablesContainer.list13 = TablesContainer.list13.OrderBy(x => x.Date_Accident).ToList();
                    else if (btnName.Equals("-downSort"))
                        TablesContainer.list13 = TablesContainer.list13.OrderByDescending(x => x.Date_Accident).ToList();
                    retList.Add(TablesContainer.list13);
                    break;
                #endregion
                #region Not WSIB #23:
                case "23":
                    if (role == Role.User)
                    {
                        if (withOrOut == Range.With)
                            TablesContainer.list14 = (from c in db.ReadNotWSIBs() where c.Date_of_Incident >= start && c.Date_of_Incident <= end select c).Where(l => l.Location == userLocation).ToList();
                        else TablesContainer.list14 = db.ReadNotWSIBs().Where(l => l.Location == userLocation).ToList();
                    }
                    else if (role == Role.RegionalUser) // if we entered as SuperUser role
                        IsRegionalUser(new Not_WSIBs_DTO(), regionNumber, withOrOut, start, end);
                    else //admin
                    {
                        if (withOrOut == Range.With)
                            TablesContainer.list14 = (from c in db.ReadNotWSIBs() where c.Date_of_Incident >= start && c.Date_of_Incident <= end select c).ToList();
                        else TablesContainer.list14 = db.ReadNotWSIBs().ToList();
                    }
                    if (TablesContainer.list14.Count == 0) return null;
                    TablesContainer.list14 = TablesContainer.list14.OrderBy(x => x.Date_of_Incident).ToList();
                    if (btnName.Equals("-upSort"))
                        TablesContainer.list14 = TablesContainer.list14.OrderBy(x => x.Date_of_Incident).ToList();
                    else if (btnName.Equals("-downSort"))
                        TablesContainer.list14 = TablesContainer.list14.OrderByDescending(x => x.Date_of_Incident).ToList();
                    retList.Add(TablesContainer.list14);
                    break;
                #endregion
                #region Licening Inspections #24:
                case "24":
                    if (role == Role.User)
                    {
                        if (withOrOut == Range.With)
                            TablesContainer.list15 = (from c in db.ReadLiceInspect() where c.Date >= start && c.Date <= end select c)
                                .Where(l => l.CareComName == userLocation).ToList();
                        else TablesContainer.list15 = db.ReadLiceInspect().Where(l => l.CareComName == userLocation).ToList();
                    }
                    else if (role == Role.RegionalUser) // if we entered as SuperUser role
                        IsRegionalUser(new LicensingInspectionDTO(), regionNumber, withOrOut, start, end);
                    else // if we entered as user role
                    {
                        if (withOrOut == Range.With)
                            TablesContainer.list15 = (from c in db.ReadLiceInspect() where c.Date >= start && c.Date <= end select c).ToList();
                        else TablesContainer.list15 = db.ReadLiceInspect().ToList();
                    }
                    if (TablesContainer.list15.Count == 0) return null;
                    TablesContainer.list15 = TablesContainer.list15.OrderBy(x => x.Date).ToList();
                    if (btnName.Equals("-upSort"))
                        TablesContainer.list15 = TablesContainer.list15.OrderBy(x => x.Date).ToList();
                    else if (btnName.Equals("-downSort"))
                        TablesContainer.list15 = TablesContainer.list15.OrderByDescending(x => x.Date).ToList();
                    retList.Add(TablesContainer.list15);
                    break;
                #endregion
                #region Assisted LivingInspections #25:
                case "25":
                    if (role == Role.User)
                    {
                        if (withOrOut == Range.With)
                            TablesContainer.list16 = (from c in db.ReadAssLivInspect() where c.Date >= start && c.Date <= end select c)
                                .Where(l => l.CareComName == userLocation).ToList();
                        else TablesContainer.list16 = db.ReadAssLivInspect().Where(l => l.CareComName == userLocation).ToList();
                    }
                    else if (role == Role.RegionalUser) // if we entered as SuperUser role
                        IsRegionalUser(new AssistedLivingInspectionDTO(), regionNumber, withOrOut, start, end);
                    else // admin
                    {
                        if (withOrOut == Range.With)
                            TablesContainer.list16 = (from c in db.ReadAssLivInspect() where c.Date >= start && c.Date <= end select c).ToList();
                        else TablesContainer.list16 = db.ReadAssLivInspect().ToList();
                    }
                    if (TablesContainer.list16.Count == 0) return null;
                    TablesContainer.list16 = TablesContainer.list16.OrderBy(x => x.Date).ToList();
                    if (btnName.Equals("-upSort"))
                        TablesContainer.list16 = TablesContainer.list16.OrderBy(x => x.Date).ToList();
                    else if (btnName.Equals("-downSort"))
                        TablesContainer.list16 = TablesContainer.list16.OrderByDescending(x => x.Date).ToList();
                    retList.Add(TablesContainer.list16);
                    break;
                #endregion
                #region Worksafe BC Inpections #26:
                case "26":
                    if (role == Role.User)
                    {
                        if (withOrOut == Range.With)
                            TablesContainer.list17 = (from c in db.ReadWorkshopBCInspect() where c.Date >= start && c.Date <= end select c)
                                .Where(l => l.CareComName == userLocation).ToList();
                        else TablesContainer.list17 = db.ReadWorkshopBCInspect().Where(l => l.CareComName == userLocation).ToList();
                    }
                    else if (role == Role.RegionalUser) // if we entered as SuperUser role
                        IsRegionalUser(new WorkshopBCInspection_DTO(), regionNumber, withOrOut, start, end);
                    else // admin
                    {
                        if (withOrOut == Range.With)
                            TablesContainer.list17 = (from c in db.ReadWorkshopBCInspect() where c.Date >= start && c.Date <= end select c).ToList();
                        else TablesContainer.list17 = db.ReadWorkshopBCInspect().ToList();
                    }
                    if (TablesContainer.list17.Count == 0) return null;
                    TablesContainer.list17 = TablesContainer.list17.OrderBy(x => x.Date).ToList();
                    if (btnName.Equals("-upSort"))
                        TablesContainer.list17 = TablesContainer.list17.OrderBy(x => x.Date).ToList();
                    else if (btnName.Equals("-downSort"))
                        TablesContainer.list17 = TablesContainer.list17.OrderByDescending(x => x.Date).ToList();
                    retList.Add(TablesContainer.list17);
                    break;
                #endregion
                #region Quality Review #27:
                case "27":
                    if (role == Role.User)
                    {
                        if (withOrOut == Range.With)
                            TablesContainer.list18 = (from c in db.ReadQualityReviews() where c.Date >= start && c.Date <= end select c)
                                .Where(l => l.CareComName == userLocation).ToList();
                        else TablesContainer.list18 = db.ReadQualityReviews().Where(l => l.CareComName == userLocation).ToList();
                    }
                    else if (role == Role.RegionalUser) // if we entered as SuperUser role
                        IsRegionalUser(new QualityReview_DTO(), regionNumber, withOrOut, start, end);
                    else // admin
                    {
                        if (withOrOut == Range.With)
                            TablesContainer.list18 = (from c in db.ReadQualityReviews() where c.Date >= start && c.Date <= end select c).ToList();
                        else TablesContainer.list18 = db.ReadQualityReviews().ToList();
                    }
                    if (TablesContainer.list18.Count == 0) return null;
                    TablesContainer.list18 = TablesContainer.list18.OrderBy(x => x.Date).ToList();
                    if (btnName.Equals("-upSort"))
                        TablesContainer.list18 = TablesContainer.list18.OrderBy(x => x.Date).ToList();
                    else if (btnName.Equals("-downSort"))
                        TablesContainer.list18 = TablesContainer.list18.OrderByDescending(x => x.Date).ToList();
                    retList.Add(TablesContainer.list18);
                    break;
                #endregion
                #region BC LTC #28:
                case "28":
                    if (role == Role.User)
                    {
                        if (withOrOut == Range.With)
                            TablesContainer.list19 = (from c in db.ReadBC_LTC() where c.DateIncident >= start && c.DateIncident <= end select c)
                                .Where(l => l.CareCommName == userLocation).ToList();
                        else TablesContainer.list19 = db.ReadBC_LTC().Where(l => l.CareCommName == userLocation).ToList();
                    }
                    else if (role == Role.RegionalUser) // if we entered as SuperUser role
                        IsRegionalUser(new BC_LTC_Reportable_Incidents_DTO(), regionNumber, withOrOut, start, end);
                    else // admin
                    {
                        if (withOrOut == Range.With)
                            TablesContainer.list19 = (from c in db.ReadBC_LTC() where c.DateIncident >= start && c.DateIncident <= end select c).ToList();
                        else TablesContainer.list19 = db.ReadBC_LTC().ToList();
                    }
                    if (TablesContainer.list19.Count == 0) return null;
                    TablesContainer.list19 = TablesContainer.list19.OrderBy(x => x.DateIncident).ToList();
                    if (btnName.Equals("-upSort"))
                        TablesContainer.list19 = TablesContainer.list19.OrderBy(x => x.DateIncident).ToList();
                    else if (btnName.Equals("-downSort"))
                        TablesContainer.list19 = TablesContainer.list19.OrderByDescending(x => x.DateIncident).ToList();
                    retList.Add(TablesContainer.list19);
                    break;
                #endregion
                #region BC LTC Assisted #29:
                case "29":
                    if (role == Role.User)
                    {
                        if (withOrOut == Range.With)
                            TablesContainer.list20 = (from c in db.ReadBC_LTCAssisted() where c.DateIncident >= start && c.DateIncident <= end select c)
                                .Where(l => l.NameCareCommu == userLocation).ToList();
                        else TablesContainer.list20 = db.ReadBC_LTCAssisted().Where(l => l.NameCareCommu == userLocation).ToList();
                    }
                    else if (role == Role.RegionalUser) // if we entered as SuperUser role
                        IsRegionalUser(new BC_Assisted_Living_Reportable_Incidents_DTO(), regionNumber, withOrOut, start, end);
                    else // admin
                    {
                        if (withOrOut == Range.With)
                            TablesContainer.list20 = (from c in db.ReadBC_LTCAssisted() where c.DateIncident >= start && c.DateIncident <= end select c).ToList();
                        else TablesContainer.list20 = db.ReadBC_LTCAssisted().ToList();
                    }
                    if (TablesContainer.list20.Count == 0) return null;
                    TablesContainer.list20 = TablesContainer.list20.OrderBy(x => x.DateIncident).ToList();
                    if (btnName.Equals("-upSort"))
                        TablesContainer.list20 = TablesContainer.list20.OrderBy(x => x.DateIncident).ToList();
                    else if (btnName.Equals("-downSort"))
                        TablesContainer.list20 = TablesContainer.list20.OrderByDescending(x => x.DateIncident).ToList();
                    retList.Add(TablesContainer.list20);
                    break;
                #endregion
                #region Inspection Infos #30:
                case "30":
                    if (role == Role.User)
                    {
                        if (withOrOut == Range.With)
                            TablesContainer.list22 = (from c in db.ReadInspectionInfos() where c.LastDate >= start && c.LastDate <= end select c)
                                .Where(l => l.HomeId == userLocation).ToList();
                        else TablesContainer.list22 = db.ReadInspectionInfos().Where(l => l.HomeId == userLocation).ToList();
                    }
                    else if (role == Role.RegionalUser) // if we entered as SuperUser role
                    {
                        if (regionNumber != (int)Region.NoN && regionNumber != 0)
                        {
                            regions = Init.GetLocByRegion(regionNumber);
                            if (withOrOut == Range.With)
                            {
                                var arrayList =
                                       RegionService.GetListByRegion(new InspectionInfo_DTO(), regionNumber, Db, HomeController.SelectRegions);
                                foreach (var obj in arrayList)
                                    TablesContainer.list22.Add((InspectionInfo_DTO)obj);
                                TablesContainer.list22 = TablesContainer.list22.Where(c => c.LastDate >= start && c.LastDate <= end).ToList();
                            }
                            else // Without date:
                            {
                                var arrayList =
                                    RegionService.GetListByRegion(new InspectionInfo_DTO(), regionNumber, Db, HomeController.SelectRegions);
                                foreach (var obj in arrayList)
                                    TablesContainer.list22.Add((InspectionInfo_DTO)obj);
                            }
                        }
                    }
                    else // admin
                    {
                        if (withOrOut == Range.With)
                            TablesContainer.list22 = (from c in db.ReadInspectionInfos() where c.LastDate >= start && c.LastDate <= end select c).ToList();
                        else TablesContainer.list22 = db.ReadInspectionInfos().ToList();
                    }
                    if (TablesContainer.list22.Count == 0) return null;
                    TablesContainer.list22 = TablesContainer.list22.OrderBy(x => x.LastDate).ToList();
                    if (btnName.Equals("-upSort"))
                        TablesContainer.list22 = TablesContainer.list22.OrderBy(x => x.LastDate).ToList();
                    else if (btnName.Equals("-downSort"))
                        TablesContainer.list22 = TablesContainer.list22.OrderByDescending(x => x.LastDate).ToList();
                    retList.Add(TablesContainer.list22);
                    break;
                    #endregion
            }
            return retList;
        }
        #endregion

        #region Fill Out ViewBags for Wor_Tabs [Get]:
        public static object[] FillVievBags(int tblNum, ServiceDSS db, int userLocation, ApplicationUser CurrentLocalUser)
        {
            List<object> retType = new List<object>();
            switch (tblNum)
            {
                case 1:
                    retType.Add(TablesContainer.list1.Count);
                    retType.Add(IncidentSummaryLogic.foundSummary1);
                    retType.Add("Critical_Incidents");
                    if (ComplaintSummaryLogic.locList.Count != 0) HomeController.isEmpty = true;
                    retType.Add(HomeController.isEmpty);
                    retType.Add(IncidentSummaryLogic.locList);
                    retType.Add(HomeController.isAdmin);
                    retType.Add(IncidentSummaryLogic.allSummary1);
                    retType.Add(HomeController.checkView);
                    retType.Add(HomeController.b);
                    if (CurrentLocalUser.Role == Role.User.ToString())
                    {
                        var found = homes.Where(e => e.Id == userLocation).FirstOrDefault();
                        retType.Add(found.Full_Home_Name);
                    }
                    else
                    {
                        retType.Add("All");
                    }
                    break;
                case 2:
                    retType.Add(TablesContainer.list2.Count);
                    retType.Add(ComplaintSummaryLogic.foundSummary);
                    retType.Add("Complaints");
                    retType.Add(HomeController.isEmpty);
                    retType.Add(ComplaintSummaryLogic.locList);
                    retType.Add(HomeController.isAdmin);
                    retType.Add(ComplaintSummaryLogic.allSummary);
                    retType.Add(HomeController.checkView);
                    retType.Add(HomeController.b);
                    if (CurrentLocalUser.Role == Role.User.ToString())
                    {
                        var found = homes.Where(e => e.Id == userLocation).SingleOrDefault();
                        retType.Add(found.Full_Home_Name);
                    }
                    else
                    {
                        retType.Add("All");
                    }
                    break;
                case 3:
                    retType.Add(TablesContainer.list3.Count);
                    retType.Add(GoodNewsSummaryLogic.foundSummary);
                    retType.Add("Good_News");
                    retType.Add(HomeController.isEmpty);
                    retType.Add(GoodNewsSummaryLogic.locList);
                    retType.Add(HomeController.isAdmin);
                    retType.Add(GoodNewsSummaryLogic.allSummary);
                    retType.Add(HomeController.checkView);
                    retType.Add(HomeController.b);
                    if (CurrentLocalUser.Role == Role.User.ToString())
                    {
                        var found = homes.Where(e => e.Id == userLocation).SingleOrDefault();
                        retType.Add(found.Full_Home_Name);
                    }
                    else
                    {
                        retType.Add("All");
                    }
                    break;
                case 4:
                    retType.Add(TablesContainer.list4.Count);
                    retType.Add(EmergencySummaryLogic.foundSummary);
                    retType.Add("Emergency_Prep");
                    retType.Add(HomeController.isEmpty);
                    retType.Add(EmergencySummaryLogic.locList);
                    retType.Add(HomeController.isAdmin);
                    retType.Add(EmergencySummaryLogic.allSummary);
                    retType.Add(HomeController.checkView);
                    retType.Add(HomeController.b);
                    if (CurrentLocalUser.Role == Role.User.ToString())
                    {
                        var found = homes.Where(e => e.Id == userLocation).SingleOrDefault();
                        retType.Add(found.Full_Home_Name);
                    }
                    else
                    {
                        retType.Add("All");
                    }
                    break;
                case 5:
                    retType.Add(TablesContainer.list5.Count);
                    retType.Add(CommRiskSummaryLogic.foundSummary);
                    retType.Add("Community_Risks");
                    retType.Add(HomeController.isEmpty);
                    retType.Add(CommRiskSummaryLogic.locList);
                    retType.Add(HomeController.isAdmin);
                    retType.Add(CommRiskSummaryLogic.allSummary);
                    retType.Add(HomeController.checkView);
                    retType.Add(HomeController.b);
                    if (CurrentLocalUser.Role == Role.User.ToString())
                    {
                        var found = homes.Where(e => e.Id == userLocation).SingleOrDefault();
                        retType.Add(found.Full_Home_Name);
                    }
                    else
                    {
                        retType.Add("All");
                    }
                    break;
                case 16:
                    retType.Add(TablesContainer.list7.Count);
                    retType.Add(BreachesSummuryLogic.foundSummary);
                    retType.Add("Privacy_Breaches");
                    retType.Add(HomeController.isEmpty);
                    retType.Add(BreachesSummuryLogic.locList);
                    retType.Add(HomeController.isAdmin);
                    retType.Add(BreachesSummuryLogic.allSummary);
                    retType.Add(HomeController.checkView);
                    retType.Add(HomeController.b);
                    if (CurrentLocalUser.Role == Role.User.ToString())
                    {
                        var found = homes.Where(e => e.Id == userLocation).SingleOrDefault();
                        retType.Add(found.Full_Home_Name);
                    }
                    else
                    {
                        retType.Add("All");
                    }
                    break;
                case 17:
                    retType.Add(TablesContainer.list8.Count);
                    retType.Add(PComplaintSummaryLogic.foundSummary);
                    retType.Add("Privacy_Complaints");
                    retType.Add(HomeController.isEmpty);
                    retType.Add(PComplaintSummaryLogic.locList);
                    retType.Add(HomeController.isAdmin);
                    retType.Add(PComplaintSummaryLogic.allSummary);
                    retType.Add(HomeController.checkView);
                    retType.Add(HomeController.b);
                    if (CurrentLocalUser.Role == Role.User.ToString())
                    {
                        var found = homes.Where(e => e.Id == userLocation).SingleOrDefault();
                        retType.Add(found.Full_Home_Name);
                    }
                    else
                    {
                        retType.Add("All");
                    }
                    break;
                case 19:
                    retType.Add(TablesContainer.list10.Count);
                    retType.Add(LabourRelationSummaryLogic.foundSummary);
                    retType.Add("Labour_Relations");
                    retType.Add(HomeController.isEmpty);
                    retType.Add(LabourRelationSummaryLogic.locList);
                    retType.Add(HomeController.isAdmin);
                    retType.Add(LabourRelationSummaryLogic.allSummary);
                    retType.Add(HomeController.checkView);
                    retType.Add(HomeController.b);
                    if (CurrentLocalUser.Role == Role.User.ToString())
                    {
                        var found = homes.Where(e => e.Id == userLocation).SingleOrDefault();
                        retType.Add(found.Full_Home_Name);
                    }
                    else
                    {
                        retType.Add("All");
                    }
                    break;
                case 21:
                    retType.Add(TablesContainer.list12.Count);
                    retType.Add(OutbreaksSummaryLogic.foundSummary);
                    retType.Add("Outbreaks");
                    retType.Add(HomeController.isEmpty);
                    retType.Add(OutbreaksSummaryLogic.locList);
                    retType.Add(HomeController.isAdmin);
                    retType.Add(OutbreaksSummaryLogic.allSummary);
                    retType.Add(HomeController.checkView);
                    retType.Add(HomeController.b);
                    if (CurrentLocalUser.Role == Role.SuperUser.ToString() || CurrentLocalUser.Role == Role.Admin.ToString()) retType.Add("All");
                    else if (CurrentLocalUser.Role == Role.SuperUser.ToString())
                    {
                        var found = homes.Where(e => e.Id == userLocation).SingleOrDefault();
                        retType.Add(found.Full_Home_Name);
                    }
                    else
                    {
                        retType.Add("All");
                    }
                    break;
                case 22:
                    retType.Add(TablesContainer.list13.Count);
                    retType.Add(WSIBSummaryLogic.foundSummary);
                    retType.Add("WSIB");
                    retType.Add(HomeController.isEmpty);
                    retType.Add(WSIBSummaryLogic.locList);
                    retType.Add(HomeController.isAdmin);
                    retType.Add(WSIBSummaryLogic.allSummary);
                    retType.Add(HomeController.checkView);
                    retType.Add(HomeController.b);
                    if (CurrentLocalUser.Role == Role.SuperUser.ToString() || CurrentLocalUser.Role == Role.Admin.ToString())
                    {
                        retType.Add("All");
                    }
                    else if (CurrentLocalUser.Role == Role.User.ToString())
                    {
                        var found = homes.Where(e => e.Id == userLocation).SingleOrDefault();
                        retType.Add(found.Full_Home_Name);
                    }
                    else retType.Add("All");
                    break;
                case 23:
                    retType.Add(TablesContainer.list14.Count);
                    retType.Add(NotWSIBSummaryLogic.foundSummary);
                    retType.Add("Not_WSIB");
                    retType.Add(HomeController.isEmpty);
                    retType.Add(NotWSIBSummaryLogic.locList);
                    retType.Add(HomeController.isAdmin);
                    retType.Add(NotWSIBSummaryLogic.allSummary);
                    retType.Add(HomeController.checkView);
                    retType.Add(HomeController.b);
                    if (CurrentLocalUser.Role == Role.SuperUser.ToString() || CurrentLocalUser.Role == Role.Admin.ToString()) retType.Add("All");
                    else if (CurrentLocalUser.Role == Role.User.ToString())
                    {
                        var found = homes.Where(e => e.Id == userLocation).SingleOrDefault();
                        retType.Add(found.Full_Home_Name);
                    }
                    else retType.Add("All");
                    break;
                case 24:
                    retType.Add(TablesContainer.list15.Count);
                    retType.Add(LiceInspecSummaryLogic.foundSummary);
                    retType.Add("LiceningInspections");
                    retType.Add(HomeController.isEmpty);
                    retType.Add(LiceInspecSummaryLogic.locList);
                    retType.Add(HomeController.isAdmin);
                    retType.Add(LiceInspecSummaryLogic.allSummary);
                    retType.Add(HomeController.checkView);
                    retType.Add(HomeController.b);
                    if (CurrentLocalUser.Role == Role.SuperUser.ToString() || CurrentLocalUser.Role == Role.Admin.ToString())
                    {
                        retType.Add("All");
                    }
                    else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                    {
                        retType.Add("All");
                    }
                    else if (CurrentLocalUser.Role == Role.User.ToString())
                    {
                        var found = homes.Where(e => e.Id == userLocation).SingleOrDefault();
                        retType.Add(found.Full_Home_Name);
                    }
                    break;
                case 25:
                    retType.Add(TablesContainer.list16.Count);
                    retType.Add(AssistLivInspectSummaryLogic.foundSummary);
                    retType.Add("AssistLivInspect");
                    retType.Add(HomeController.isEmpty);
                    retType.Add(AssistLivInspectSummaryLogic.locList);
                    retType.Add(HomeController.isAdmin);
                    retType.Add(AssistLivInspectSummaryLogic.allSummary);
                    retType.Add(HomeController.checkView);
                    retType.Add(HomeController.b);
                    if (CurrentLocalUser.Role == Role.SuperUser.ToString() || CurrentLocalUser.Role == Role.Admin.ToString())
                    {
                        retType.Add("All");
                    }
                    else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                    {
                        retType.Add("All");
                    }
                    else if (CurrentLocalUser.Role == Role.User.ToString())
                    {
                        var found = homes.Where(e => e.Id == userLocation).SingleOrDefault();
                        retType.Add(found.Full_Home_Name);
                    }
                    break;
                case 26:
                    retType.Add(TablesContainer.list17.Count);
                    retType.Add(WorksaveBCSummaryLogic.foundSummary);
                    retType.Add("WorksaveBC");
                    retType.Add(HomeController.isEmpty);
                    retType.Add(WorksaveBCSummaryLogic.locList);
                    retType.Add(HomeController.isAdmin);
                    retType.Add(WorksaveBCSummaryLogic.allSummary);
                    retType.Add(HomeController.checkView);
                    retType.Add(HomeController.b);
                    if (CurrentLocalUser.Role == Role.SuperUser.ToString() || CurrentLocalUser.Role == Role.Admin.ToString())
                    {
                        retType.Add("All");
                    }
                    else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                    {
                        retType.Add("All");
                    }
                    else if (CurrentLocalUser.Role == Role.User.ToString())
                    {
                        var found = homes.Where(e => e.Id == userLocation).SingleOrDefault();
                        retType.Add(found.Full_Home_Name);
                    }
                    break;
                case 27:
                    retType.Add(TablesContainer.list18.Count);
                    retType.Add(QltyRevSummaryLogic.foundSummary);
                    retType.Add("QualityReviews");
                    retType.Add(HomeController.isEmpty);
                    retType.Add(QltyRevSummaryLogic.locList);
                    retType.Add(HomeController.isAdmin);
                    retType.Add(QltyRevSummaryLogic.allSummary);
                    retType.Add(HomeController.checkView);
                    retType.Add(HomeController.b);
                    if (CurrentLocalUser.Role == Role.SuperUser.ToString() || CurrentLocalUser.Role == Role.Admin.ToString())
                    {
                        retType.Add("All");
                    }
                    else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                    {
                        retType.Add("All");
                    }
                    else if (CurrentLocalUser.Role == Role.User.ToString())
                    {
                        var found = homes.Where(e => e.Id == userLocation).SingleOrDefault();
                        retType.Add(found.Full_Home_Name);
                    }
                    break;
                case 28:
                    retType.Add(TablesContainer.list19.Count);
                    retType.Add(BC_LTCSummaryLogic.foundSummary);
                    retType.Add("BC_LTCReport");
                    retType.Add(HomeController.isEmpty);
                    retType.Add(BC_LTCSummaryLogic.locList);
                    retType.Add(HomeController.isAdmin);
                    retType.Add(BC_LTCSummaryLogic.allSummary);
                    retType.Add(HomeController.checkView);
                    retType.Add(HomeController.b);
                    if (CurrentLocalUser.Role == Role.SuperUser.ToString() || CurrentLocalUser.Role == Role.Admin.ToString())
                    {
                        retType.Add("All");
                    }
                    else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                    {
                        retType.Add("All");
                    }
                    else if (CurrentLocalUser.Role == Role.User.ToString())
                    {
                        var found = homes.Where(e => e.Id == userLocation).SingleOrDefault();
                        retType.Add(found.Full_Home_Name);
                    }
                    break;
                case 29:
                    retType.Add(TablesContainer.list20.Count);
                    retType.Add(BC_AssistSummaryLogic.foundSummary);
                    retType.Add("BC_AssisLivRep");
                    retType.Add(HomeController.isEmpty);
                    retType.Add(BC_AssistSummaryLogic.locList);
                    retType.Add(HomeController.isAdmin);
                    retType.Add(BC_AssistSummaryLogic.allSummary);
                    retType.Add(HomeController.checkView);
                    retType.Add(HomeController.b);
                    if (CurrentLocalUser.Role == Role.SuperUser.ToString() || CurrentLocalUser.Role == Role.Admin.ToString())
                    {
                        retType.Add("All");
                    }
                    else if (CurrentLocalUser.Role == Role.RegionalUser.ToString())
                    {
                        retType.Add("All");
                    }
                    else if (CurrentLocalUser.Role == Role.User.ToString())
                    {
                        var found = homes.Where(e => e.Id == userLocation).SingleOrDefault();
                        retType.Add(found.Full_Home_Name);
                    }
                    break;
                    // .. continue for the following tbls.
            }
            return retType.ToArray();
        }
        #endregion

        #region Method to convert type string to enum:
        public static Role GetRoleEnum(string role)
        {
            switch (role)
            {
                case "User": return Role.User;
                case "SuperUser": return Role.SuperUser;
                case "RegionalUser": return Role.RegionalUser;
                case "Admin": return Role.Admin;
                default: return Role.Unknown;
            }
        }

        public static string GetRoleString(string role)
        {
            switch (role)
            {
                case "SuperUser": return "Super User";
                case "RegionalUser": return "Regional User";
                case "User": return "User";
                case "Admin": return "Administrator";
                default: return "Unknown";
            }
        }
        #endregion

        #region Is for Regional User functionality by user's role:
        public static void IsRegionalUser(DSS.BLL.Interfaces.IModel tblName, int regionNumber, Range withOrOut = Range.NoN,
            DateTime start = default, DateTime end = default)
        {
            new TablesContainer().ResetAllTabls();
            string[] regions = default;
            switch (tblName.GetType().Name)
            {
                case nameof(Critical_Incidents_DTO):
                    if (regionNumber != (int)Region.NoN)
                    {
                        regions = Init.GetLocByRegion(regionNumber);
                        if (withOrOut == Range.With) // With date:
                        {
                            var arrayList =
                                   RegionService.GetListByRegion(tblName, regionNumber, Db, HomeController.SelectRegions);
                            foreach (var obj in arrayList)
                                TablesContainer.list1.Add((Critical_Incidents_DTO)obj);
                            TablesContainer.list1 = TablesContainer.list1.Where(c => c.Date >= start && c.Date <= end).ToList();
                        }
                        else                        // Without date:
                        {
                            var arrayList =
                                RegionService.GetListByRegion(tblName, regionNumber, Db,
                                HomeController.SelectRegions);
                            foreach (var obj in arrayList)
                                TablesContainer.list1.Add((Critical_Incidents_DTO)obj);
                        }
                    }
                    break;
                case nameof(Complaint_DTO):
                    if (regionNumber != (int)Region.NoN && regionNumber != 0)
                    {
                        regions = Init.GetLocByRegion(regionNumber);
                        if (withOrOut == Range.With)
                        {
                            var arrayList =
                                   RegionService.GetListByRegion(new Complaint_DTO(), regionNumber, Db, HomeController.SelectRegions);
                            foreach (var obj in arrayList)
                                TablesContainer.list2.Add((Complaint_DTO)obj);
                            TablesContainer.list2 = TablesContainer.list2.Where(c => c.DateReceived >= start && c.DateReceived <= end).ToList();
                        }
                        else // Without date:
                        {
                            var arrayList =
                                RegionService.GetListByRegion(new Complaint_DTO(), regionNumber, Db,
                                HomeController.SelectRegions);
                            foreach (var obj in arrayList)
                                TablesContainer.list2.Add((Complaint_DTO)obj);
                        }
                    }
                    break;
                case nameof(Good_News_DTO):
                    if (regionNumber != (int)Region.NoN && regionNumber != 0)
                    {
                        regions = Init.GetLocByRegion(regionNumber);
                        if (withOrOut == Range.With)
                        {
                            var arrayList =
                                   RegionService.GetListByRegion(new Good_News_DTO(), regionNumber, Db, HomeController.SelectRegions);
                            foreach (var obj in arrayList)
                                TablesContainer.list3.Add((Good_News_DTO)obj);
                            TablesContainer.list3 = TablesContainer.list3.Where(c => c.DateNews >= start && c.DateNews <= end).ToList();
                        }
                        else // Without date:
                        {
                            var arrayList =
                                RegionService.GetListByRegion(new Good_News_DTO(), regionNumber, Db,
                                HomeController.SelectRegions);
                            foreach (var obj in arrayList)
                                TablesContainer.list3.Add((Good_News_DTO)obj);
                        }
                    }
                    break;
                case nameof(Community_Risks_DTO):
                    if (regionNumber != (int)Region.NoN && regionNumber != 0)
                    {
                        regions = Init.GetLocByRegion(regionNumber);
                        if (withOrOut == Range.With)
                        {
                            var arrayList =
                                   RegionService.GetListByRegion(new Community_Risks_DTO(), regionNumber, Db, HomeController.SelectRegions);
                            foreach (var obj in arrayList)
                                TablesContainer.list5.Add((Community_Risks_DTO)obj);
                            TablesContainer.list5 = TablesContainer.list5.Where(c => c.Date >= start && c.Date <= end).ToList();
                        }
                        else // Without date:
                        {
                            var arrayList =
                                RegionService.GetListByRegion(new Community_Risks_DTO(), regionNumber, Db,
                                HomeController.SelectRegions);
                            foreach (var obj in arrayList)
                                TablesContainer.list5.Add((Community_Risks_DTO)obj);
                        }
                    }
                    break;
                case nameof(Emergency_Prep_DTO):
                    if (regionNumber != (int)Region.NoN && regionNumber != 0)
                    {
                        regions = Init.GetLocByRegion(regionNumber);
                        if (withOrOut == Range.With)
                        {
                            var arrayList =
                                   RegionService.GetListByRegion(new Emergency_Prep_DTO(), regionNumber, Db, HomeController.SelectRegions);
                            foreach (var obj in arrayList)
                                TablesContainer.list4.Add((Emergency_Prep_DTO)obj);
                            TablesContainer.list4 = TablesContainer.list4.Where(c => c.Date >= start && c.Date <= end).ToList();
                        }
                        else // Without date:
                        {
                            var arrayList =
                                RegionService.GetListByRegion(new Emergency_Prep_DTO(), regionNumber, Db,
                                HomeController.SelectRegions);
                            foreach (var obj in arrayList)
                                TablesContainer.list4.Add((Emergency_Prep_DTO)obj);
                        }
                    }
                    break;
                case nameof(WSIB_DTO):
                    if (regionNumber != (int)Region.NoN && regionNumber != 0)
                    {
                        regions = Init.GetLocByRegion(regionNumber);
                        if (withOrOut == Range.With)
                        {
                            var arrayList =
                                   RegionService.GetListByRegion(new WSIB_DTO(), regionNumber, Db, HomeController.SelectRegions);
                            foreach (var obj in arrayList)
                                TablesContainer.list13.Add((WSIB_DTO)obj);
                            TablesContainer.list13 = TablesContainer.list13.Where(c => c.Date_Accident >= start && c.Date_Accident <= end)
                                .ToList();
                        }
                        else // Without date:
                        {
                            var arrayList =
                                RegionService.GetListByRegion(new WSIB_DTO(), regionNumber, Db,
                                HomeController.SelectRegions);
                            foreach (var obj in arrayList)
                                TablesContainer.list13.Add((WSIB_DTO)obj);
                        }
                    }
                    break;
                case nameof(Not_WSIBs_DTO):
                    if (regionNumber != (int)Region.NoN && regionNumber != 0)
                    {
                        regions = Init.GetLocByRegion(regionNumber);
                        if (withOrOut == Range.With)
                        {
                            var arrayList =
                                   RegionService.GetListByRegion(new Not_WSIBs_DTO(), regionNumber, Db, HomeController.SelectRegions);
                            foreach (var obj in arrayList)
                                TablesContainer.list14.Add((Not_WSIBs_DTO)obj);
                            TablesContainer.list14 = TablesContainer.list14.Where(c => c.Date_of_Incident >= start && c.Date_of_Incident <= end).ToList();
                        }
                        else // Without date:
                        {
                            var arrayList =
                                RegionService.GetListByRegion(new Not_WSIBs_DTO(), regionNumber, Db,
                                HomeController.SelectRegions);
                            foreach (var obj in arrayList)
                                TablesContainer.list14.Add((Not_WSIBs_DTO)obj);
                        }
                    }
                    break;
                case nameof(Outbreaks_DTO):
                    if (regionNumber != (int)Region.NoN && regionNumber != 0)
                    {
                        regions = Init.GetLocByRegion(regionNumber);
                        if (withOrOut == Range.With)
                        {
                            var arrayList =
                                   RegionService.GetListByRegion(new Outbreaks_DTO(), regionNumber, Db, HomeController.SelectRegions);
                            foreach (var obj in arrayList)
                                TablesContainer.list12.Add((Outbreaks_DTO)obj);
                            TablesContainer.list12 =
                                TablesContainer.list12.Where(c => c.Date_Declared >= start && c.Date_Declared <= end).ToList();
                        }
                        else // Without date:
                        {
                            var arrayList =
                                RegionService.GetListByRegion(new Outbreaks_DTO(), regionNumber, Db,
                                HomeController.SelectRegions);
                            foreach (var obj in arrayList)
                                TablesContainer.list12.Add((Outbreaks_DTO)obj);
                        }
                    }
                    break;
                case nameof(Privacy_Breaches_DTO):
                    if (regionNumber != (int)Region.NoN && regionNumber != 0)
                    {
                        regions = Init.GetLocByRegion(regionNumber);
                        if (withOrOut == Range.With)
                        {
                            var arrayList =
                                   RegionService.GetListByRegion(new Privacy_Breaches_DTO(), regionNumber, Db, HomeController.SelectRegions);
                            foreach (var obj in arrayList)
                                TablesContainer.list7.Add((Privacy_Breaches_DTO)obj);
                            TablesContainer.list7 =
                                TablesContainer.list7.Where(c => c.Date_Breach_Occurred >= start && c.Date_Breach_Occurred <= end).ToList();
                        }
                        else // Without date:
                        {
                            var arrayList =
                                RegionService.GetListByRegion(new Privacy_Breaches_DTO(), regionNumber, Db,
                                HomeController.SelectRegions);
                            foreach (var obj in arrayList)
                                TablesContainer.list7.Add((Privacy_Breaches_DTO)obj);
                        }
                    }
                    break;
                case nameof(Privacy_Complaints_DTO):
                    if (regionNumber != (int)Region.NoN && regionNumber != 0)
                    {
                        regions = Init.GetLocByRegion(regionNumber);
                        if (withOrOut == Range.With)
                        {
                            var arrayList =
                                   RegionService.GetListByRegion(new Privacy_Complaints_DTO(), regionNumber, Db, HomeController.SelectRegions);
                            foreach (var obj in arrayList)
                                TablesContainer.list8.Add((Privacy_Complaints_DTO)obj);
                            TablesContainer.list8
                                = TablesContainer.list8.Where(c => c.Date_Complain_Received >= start && c.Date_Complain_Received <= end).ToList();
                        }
                        else // Without date:
                        {
                            var arrayList =
                                RegionService.GetListByRegion(new Privacy_Complaints_DTO(), regionNumber, Db,
                                HomeController.SelectRegions);
                            foreach (var obj in arrayList)
                                TablesContainer.list8.Add((Privacy_Complaints_DTO)obj);
                        }
                    }
                    break;
                case nameof(BC_LTC_Reportable_Incidents_DTO):
                    if (regionNumber != (int)Region.NoN && regionNumber != 0)
                    {
                        regions = Init.GetLocByRegion(regionNumber);
                        if (withOrOut == Range.With)
                        {
                            var arrayList =
                                   RegionService.GetListByRegion(new BC_LTC_Reportable_Incidents_DTO(), regionNumber, Db, HomeController.SelectRegions);
                            foreach (var obj in arrayList)
                                TablesContainer.list19.Add((BC_LTC_Reportable_Incidents_DTO)obj);
                            TablesContainer.list19 = TablesContainer.list19.Where(c => c.DateIncident >= start && c.DateIncident <= end).ToList();
                        }
                        else // Without date:
                        {
                            var arrayList =
                                RegionService.GetListByRegion(new BC_LTC_Reportable_Incidents_DTO(), regionNumber, Db,
                                HomeController.SelectRegions);
                            foreach (var obj in arrayList)
                                TablesContainer.list19.Add((BC_LTC_Reportable_Incidents_DTO)obj);
                        }
                    }
                    break;
                case nameof(BC_Assisted_Living_Reportable_Incidents_DTO):
                    if (regionNumber != (int)Region.NoN && regionNumber != 0)
                    {
                        regions = Init.GetLocByRegion(regionNumber);
                        if (withOrOut == Range.With)
                        {
                            var arrayList =
                                   RegionService.GetListByRegion(new BC_Assisted_Living_Reportable_Incidents_DTO(), regionNumber, Db, HomeController.SelectRegions);
                            foreach (var obj in arrayList)
                                TablesContainer.list20.Add((BC_Assisted_Living_Reportable_Incidents_DTO)obj);
                            TablesContainer.list20 = TablesContainer.list20.Where(c => c.DateIncident >= start && c.DateIncident <= end).ToList();
                        }
                        else // Without date:
                        {
                            var arrayList =
                                RegionService.GetListByRegion(new BC_Assisted_Living_Reportable_Incidents_DTO(), regionNumber, Db,
                                HomeController.SelectRegions);
                            foreach (var obj in arrayList)
                                TablesContainer.list20.Add((BC_Assisted_Living_Reportable_Incidents_DTO)obj);
                        }
                    }
                    break;
                case nameof(LicensingInspectionDTO):
                    if (regionNumber != (int)Region.NoN && regionNumber != 0)
                    {
                        regions = Init.GetLocByRegion(regionNumber);
                        if (withOrOut == Range.With)
                        {
                            var arrayList =
                                   RegionService.GetListByRegion(new LicensingInspectionDTO(), regionNumber, Db, HomeController.SelectRegions);
                            foreach (var obj in arrayList)
                                TablesContainer.list15.Add((LicensingInspectionDTO)obj);
                            TablesContainer.list15 = TablesContainer.list15.Where(c => c.Date >= start && c.Date <= end).ToList();
                        }
                        else // Without date:
                        {
                            var arrayList =
                                RegionService.GetListByRegion(new LicensingInspectionDTO(), regionNumber, Db,
                                HomeController.SelectRegions);
                            foreach (var obj in arrayList)
                                TablesContainer.list15.Add((LicensingInspectionDTO)obj);
                        }
                    }
                    break;
                case nameof(WorkshopBCInspection_DTO):
                    if (regionNumber != (int)Region.NoN && regionNumber != 0)
                    {
                        regions = Init.GetLocByRegion(regionNumber);
                        if (withOrOut == Range.With)
                        {
                            var arrayList =
                                   RegionService.GetListByRegion(new WorkshopBCInspection_DTO(), regionNumber, Db, HomeController.SelectRegions);
                            foreach (var obj in arrayList)
                                TablesContainer.list17.Add((WorkshopBCInspection_DTO)obj);
                            TablesContainer.list17 = TablesContainer.list17.Where(c => c.Date >= start && c.Date <= end).ToList();
                        }
                        else // Without date:
                        {
                            var arrayList =
                                RegionService.GetListByRegion(new WorkshopBCInspection_DTO(), regionNumber, Db,
                                HomeController.SelectRegions);
                            foreach (var obj in arrayList)
                                TablesContainer.list17.Add((WorkshopBCInspection_DTO)obj);
                        }
                    }
                    break;
                case nameof(AssistedLivingInspectionDTO):
                    if (regionNumber != (int)Region.NoN && regionNumber != 0)
                    {
                        regions = Init.GetLocByRegion(regionNumber);
                        if (withOrOut == Range.With)
                        {
                            var arrayList =
                                   RegionService.GetListByRegion(new AssistedLivingInspectionDTO(), regionNumber, Db, HomeController.SelectRegions);
                            foreach (var obj in arrayList)
                                TablesContainer.list16.Add((AssistedLivingInspectionDTO)obj);
                            TablesContainer.list16 = TablesContainer.list16.Where(c => c.Date >= start && c.Date <= end).ToList();
                        }
                        else // Without date:
                        {
                            var arrayList =
                                RegionService.GetListByRegion(new AssistedLivingInspectionDTO(), regionNumber, Db,
                                HomeController.SelectRegions);
                            foreach (var obj in arrayList)
                                TablesContainer.list16.Add((AssistedLivingInspectionDTO)obj);
                        }
                    }
                    break;
                case nameof(QualityReview_DTO):
                    if (regionNumber != (int)Region.NoN && regionNumber != 0)
                    {
                        regions = Init.GetLocByRegion(regionNumber);
                        if (withOrOut == Range.With)
                        {
                            var arrayList =
                                   RegionService.GetListByRegion(new QualityReview_DTO(), regionNumber, Db, HomeController.SelectRegions);
                            foreach (var obj in arrayList)
                                TablesContainer.list18.Add((QualityReview_DTO)obj);
                            TablesContainer.list18 = TablesContainer.list18.Where(c => c.Date >= start && c.Date <= end).ToList();
                        }
                        else // Without date:
                        {
                            var arrayList =
                                RegionService.GetListByRegion(new QualityReview_DTO(), regionNumber, Db,
                                HomeController.SelectRegions);
                            foreach (var obj in arrayList)
                                TablesContainer.list18.Add((QualityReview_DTO)obj);
                        }
                    }
                    break;
            }
        }
        #endregion
    }
}