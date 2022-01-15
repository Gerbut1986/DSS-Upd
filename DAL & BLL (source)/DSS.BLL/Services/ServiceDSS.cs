namespace DSS.BLL.Services
{
    using System;
    using Logger;
    using BLL.DTO;
    using Interfaces;
    using System.Linq;
    using DAL.Entities;
    using DSS.DAL.Interfaces;
    using DSS.DAL.Repositories;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public class ServiceDSS : IServiceDSS
    {
        readonly IUnitOfWork Db;

        #region Constructor:
        public ServiceDSS(string strConn)
        {
            Db = new UnitOfWork(strConn);
        }
        #endregion

        #region Care Community:
        public void Insert(Home_DTO dto)
        {
            try
            {
                Home model = new Home
                {
                    Home_ID = dto.Home_Name,
                    Home_Name = dto.Home_Name,
                    Full_Home_Name = dto.Full_Home_Name,
                    Care_Type = dto.Care_Type,
                    Current_VPRO = dto.Current_VPRO,
                    ED_GM = dto.ED_GM,
                    ED_GM_Email_Address = dto.ED_GM_Email_Address,
                    Previous_VPRO = dto.Previous_VPRO
                };
                Db.Homes.Create(model);
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public async Task<IEnumerable<Home_DTO>> ReadHomesAsync()
        {
            var l = await Db.Homes.GetAllAsync();
            var list = l.ToList();
            var listDTO = new List<Home_DTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new Home_DTO());
                listDTO[i].Id = list[i].Id;
                listDTO[i].Home_Name = list[i].Home_Name;
                listDTO[i].Full_Home_Name = list[i].Full_Home_Name;
                listDTO[i].Care_Type = list[i].Care_Type;
                listDTO[i].Current_VPRO = list[i].Current_VPRO;
                listDTO[i].ED_GM = list[i].ED_GM;
                listDTO[i].ED_GM_Email_Address = list[i].ED_GM_Email_Address;
                listDTO[i].Previous_VPRO = list[i].Previous_VPRO;
            }
            return listDTO;
        }

        public IEnumerable<Home_DTO> ReadHomes()
        {
            var list = Db.Homes.GetAll().ToList();
            var listDTO = new List<Home_DTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new Home_DTO());
                listDTO[i].Id = list[i].Id;
                listDTO[i].Home_Name = list[i].Home_Name;
                listDTO[i].Full_Home_Name = list[i].Full_Home_Name;
                listDTO[i].Care_Type = list[i].Care_Type;
                listDTO[i].Current_VPRO = list[i].Current_VPRO;
                listDTO[i].ED_GM = list[i].ED_GM;
                listDTO[i].ED_GM_Email_Address = list[i].ED_GM_Email_Address;
                listDTO[i].Previous_VPRO = list[i].Previous_VPRO;
            }
            return listDTO;
        }

        public async Task DeleteHomeByIdAsync(int id)
        {
            await Db.Homes.Delete(id);
        }

        public void DeleteCommunity(int id)
        {
            try
            {
                Db.Homes.Delete(id);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public void Update(Home_DTO dto)
        {
            try
            {
                var model = new Home
                {
                    Id = dto.Id,
                    Home_ID = dto.Home_Name,
                    Home_Name = dto.Home_Name,
                    Full_Home_Name = dto.Full_Home_Name,
                    Care_Type = dto.Care_Type,
                    Current_VPRO = dto.Current_VPRO,
                    ED_GM = dto.ED_GM,
                    ED_GM_Email_Address = dto.ED_GM_Email_Address,
                    Previous_VPRO = dto.Previous_VPRO
                };
                Db.Homes.Update(model);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }
        #endregion

        #region CI Category Type:
        public void Insert(CI_Category_Type_DTO dto)
        {
            try
            {
                CI_Category_Type model = new CI_Category_Type
                {
                    Name = dto.Name,
                };
                Db.CI_Types.Create(model);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public async Task<IEnumerable<CI_Category_Type_DTO>> ReadCICategoryAsync()
        {
            var l = await Db.CI_Types.GetAllAsync();
            var list = l.ToList();
            var listDTO = new List<CI_Category_Type_DTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new CI_Category_Type_DTO());
                listDTO[i].Id = list[i].Id;
                listDTO[i].Name = list[i].Name;
            }
            return listDTO;
        }

        public IEnumerable<CI_Category_Type_DTO> ReadCICategory()
        {
            var list = Db.CI_Types.GetAll().ToList();
            var listDTO = new List<CI_Category_Type_DTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new CI_Category_Type_DTO());
                listDTO[i].Id = list[i].Id;
                listDTO[i].Name = list[i].Name;
            }
            return listDTO;
        }

        public void DeleteCICategory(int id)
        {
            try
            {
                Db.CI_Types.Delete(id);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public void Update(CI_Category_Type_DTO dto)
        {
            try
            {
                var model = new CI_Category_Type
                {
                    Id = dto.Id,
                    Name = dto.Name
                };
                Db.CI_Types.Update(model);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }
        #endregion

        #region Community Risks:
        public void Insert(Community_Risks_DTO dto)
        {
            try
            {
                Community_Risks model = new Community_Risks
                {
                    Date = dto.Date,
                    Descriptions = dto.Descriptions,
                    Hot_Alert = dto.Hot_Alert,
                    Location = dto.Location,
                    MOH_Visit = dto.Potential_Risk,
                    Potential_Risk = dto.Potential_Risk,
                    Resolved = dto.Resolved,
                    Risk_Legal_Action = dto.Risk_Legal_Action,
                    Status_Update = dto.Status_Update,
                    Type_Of_Risk = dto.Type_Of_Risk
                };
                Db.CommRisks.Create(model);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public IEnumerable<Community_Risks_DTO> ReadRisks()
        {
            var list = Db.CommRisks.GetAll().ToList();
            var listDTO = new List<Community_Risks_DTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new Community_Risks_DTO());
                listDTO[i].Id = list[i].Id;
                listDTO[i].Date = list[i].Date;
                listDTO[i].Descriptions = list[i].Descriptions;
                listDTO[i].Hot_Alert = list[i].Hot_Alert;
                listDTO[i].Location = list[i].Location;
                listDTO[i].MOH_Visit = list[i].Potential_Risk;
                listDTO[i].Potential_Risk = list[i].Potential_Risk;
                listDTO[i].Resolved = list[i].Resolved;
                listDTO[i].Risk_Legal_Action = list[i].Risk_Legal_Action;
                listDTO[i].Status_Update = list[i].Status_Update;
                listDTO[i].Type_Of_Risk = list[i].Type_Of_Risk;
            }
            return listDTO;
        }

        public async Task DeleteRisksByIdAsync(int id)
        {
            await Db.CommRisks.Delete(id);
        }

        public void DeleteRisk(int id)
        {
            try
            {
                Db.CommRisks.Delete(id);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public void Update(Community_Risks_DTO dto)
        {
            try
            {
                var model = new Community_Risks
                {
                    Id = dto.Id,
                    Date = dto.Date,
                    Descriptions = dto.Descriptions,
                    Hot_Alert = dto.Hot_Alert,
                    Location = dto.Location,
                    MOH_Visit = dto.Potential_Risk,
                    Potential_Risk = dto.Potential_Risk,
                    Resolved = dto.Resolved,
                    Risk_Legal_Action = dto.Risk_Legal_Action,
                    Status_Update = dto.Status_Update,
                    Type_Of_Risk = dto.Type_Of_Risk
                };
                Db.CommRisks.Update(model);
                Db.SaveAsync();
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }
        #endregion

        #region Complaints:
        public void Insert(Complaint_DTO dto)
        {
            try
            {
                Complaint model = new Complaint
                {
                    ActionToken = dto.ActionToken,
                    Location = dto.Location,
                    BriefDescription = dto.BriefDescription,
                    CareServices = dto.CareServices,
                    CopyToVP = dto.CopyToVP,
                    DateReceived = dto.DateReceived,
                    Department = dto.Department,
                    HomeArea = dto.HomeArea,
                    Dietary = dto.Dietary,
                    FromResident = dto.FromResident,
                    Housekeeping = dto.Housekeeping,
                    IsAdministration = dto.IsAdministration,
                    Laundry = dto.Laundry,
                    Maintenance = dto.Maintenance,
                    MinistryVisit = dto.MinistryVisit,
                    MOHLTCNotified = dto.MOHLTCNotified,
                    Other = dto.Other,
                    PalliativeCare = dto.PalliativeCare,
                    Physician = dto.Physician,
                    Programs = dto.Programs,
                    Admissions = dto.Admissions,
                    Receive_Directly = dto.Receive_Directly,
                    ResidentName = dto.ResidentName,
                    Resolved = dto.Resolved,
                    ResponseSent = dto.ResponseSent,
                    WritenOrVerbal = dto.WritenOrVerbal
                };
                Db.Complaints.Create(model);
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public IEnumerable<Complaint_DTO> ReadComplaints()
        {
            var list = Db.Complaints.GetAll().ToList();
            var listDTO = new List<Complaint_DTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new Complaint_DTO());
                listDTO[i].Id = list[i].Id;
                listDTO[i].ActionToken = list[i].ActionToken;
                listDTO[i].Location = list[i].Location;
                listDTO[i].BriefDescription = list[i].BriefDescription;
                listDTO[i].CareServices = list[i].CareServices;
                listDTO[i].CopyToVP = list[i].CopyToVP;
                listDTO[i].DateReceived = list[i].DateReceived;
                listDTO[i].Department = list[i].Department;
                listDTO[i].HomeArea = list[i].HomeArea;
                listDTO[i].Dietary = list[i].Dietary;
                listDTO[i].FromResident = list[i].FromResident;
                listDTO[i].Housekeeping = list[i].Housekeeping;
                listDTO[i].IsAdministration = list[i].IsAdministration;
                listDTO[i].Laundry = list[i].Laundry;
                listDTO[i].Maintenance = list[i].Maintenance;
                listDTO[i].MinistryVisit = list[i].MinistryVisit;
                listDTO[i].MOHLTCNotified = list[i].MOHLTCNotified;
                listDTO[i].Other = list[i].Other;
                listDTO[i].PalliativeCare = list[i].PalliativeCare;
                listDTO[i].Physician = list[i].Physician;
                listDTO[i].Programs = list[i].Programs;
                listDTO[i].Admissions = list[i].Admissions;
                listDTO[i].Receive_Directly = list[i].Receive_Directly;
                listDTO[i].ResidentName = list[i].ResidentName;
                listDTO[i].Resolved = list[i].Resolved;
                listDTO[i].ResponseSent = list[i].ResponseSent;
                listDTO[i].WritenOrVerbal = list[i].WritenOrVerbal;
            }
            return listDTO;
        }

        public async Task DeleteComplaintsByIdAsync(int id)
        {
            await Db.Complaints.Delete(id);
        }

        public void DeleteComplaint(int id)
        {
            try
            {
                Db.Complaints.Delete(id);
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public void Update(Complaint_DTO dto)
        {
            try
            {
                var model = new Complaint
                {
                    Id = dto.Id,
                    ActionToken = dto.ActionToken,
                    Location = dto.Location,
                    BriefDescription = dto.BriefDescription,
                    CareServices = dto.CareServices,
                    CopyToVP = dto.CopyToVP,
                    DateReceived = dto.DateReceived,
                    Department = dto.Department,
                    HomeArea = dto.HomeArea,
                    Dietary = dto.Dietary,
                    FromResident = dto.FromResident,
                    Housekeeping = dto.Housekeeping,
                    IsAdministration = dto.IsAdministration,
                    Laundry = dto.Laundry,
                    Maintenance = dto.Maintenance,
                    MinistryVisit = dto.MinistryVisit,
                    MOHLTCNotified = dto.MOHLTCNotified,
                    Other = dto.Other,
                    PalliativeCare = dto.PalliativeCare,
                    Physician = dto.Physician,
                    Programs = dto.Programs,
                    Admissions = dto.Admissions,
                    Receive_Directly = dto.Receive_Directly,
                    ResidentName = dto.ResidentName,
                    Resolved = dto.Resolved,
                    ResponseSent = dto.ResponseSent,
                    WritenOrVerbal = dto.WritenOrVerbal
                };
                Db.Complaints.Update(model);
                Db.SaveAsync();
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }
        #endregion

        #region Critical Incidents:
        public void Insert(Critical_Incidents_DTO dto)
        {
            try
            {
                Critical_Incidents model = new Critical_Incidents
                {
                    Date = dto.Date,
                    Brief_Description = dto.Brief_Description,
                    Care_Plan_Updated = dto.Care_Plan_Updated,
                    Location = dto.Location,
                    CIS_Initiated = dto.CIS_Initiated,
                    CI_Category_Type = dto.CI_Category_Type,
                    CI_Form_Number = dto.CI_Form_Number,
                    File_Complete = dto.File_Complete,
                    Follow_Up_Amendments = dto.Follow_Up_Amendments,
                    MOHLTC_Follow_Up = dto.MOHLTC_Follow_Up,
                    MOH_Notified = dto.MOH_Notified,
                    POAS_Notified = dto.POAS_Notified,
                    Police_Notified = dto.Police_Notified,
                    Quality_Improvement_Actions = dto.Quality_Improvement_Actions,
                    Risk_Locked = dto.Risk_Locked
                };
                Db.Incidents.Create(model);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public async Task<List<Critical_Incidents_DTO>> ReadIncidentsAsync()
        {
            var list = await Db.Incidents.GetAllAsync();
            var listDTO = new List<Critical_Incidents_DTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new Critical_Incidents_DTO());
                listDTO[i].Id = list[i].id;
                listDTO[i].Date = list[i].Date;
                listDTO[i].Brief_Description = list[i].Brief_Description;
                listDTO[i].Care_Plan_Updated = list[i].Care_Plan_Updated;
                listDTO[i].Location = list[i].Location;
                listDTO[i].CIS_Initiated = list[i].CIS_Initiated;
                listDTO[i].CI_Category_Type = list[i].CI_Category_Type;
                listDTO[i].CI_Form_Number = list[i].CI_Form_Number;
                listDTO[i].File_Complete = list[i].File_Complete;
                listDTO[i].Follow_Up_Amendments = list[i].Follow_Up_Amendments;
                listDTO[i].MOHLTC_Follow_Up = list[i].MOHLTC_Follow_Up;
                listDTO[i].MOH_Notified = list[i].MOH_Notified;
                listDTO[i].POAS_Notified = list[i].POAS_Notified;
                listDTO[i].Police_Notified = list[i].Police_Notified;
                listDTO[i].Quality_Improvement_Actions = list[i].Quality_Improvement_Actions;
                listDTO[i].Risk_Locked = list[i].Risk_Locked;
            }
            return listDTO;
        }

        public IEnumerable<Critical_Incidents_DTO> ReadIncidents()
        {
            var list = Db.Incidents.GetAll().ToList();
            var listDTO = new List<Critical_Incidents_DTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new Critical_Incidents_DTO());
                listDTO[i].Id = list[i].id;
                listDTO[i].Date = list[i].Date;
                listDTO[i].Brief_Description = list[i].Brief_Description;
                listDTO[i].Care_Plan_Updated = list[i].Care_Plan_Updated;
                listDTO[i].Location = list[i].Location;
                listDTO[i].CIS_Initiated = list[i].CIS_Initiated;
                listDTO[i].CI_Category_Type = list[i].CI_Category_Type;
                listDTO[i].CI_Form_Number = list[i].CI_Form_Number;
                listDTO[i].File_Complete = list[i].File_Complete;
                listDTO[i].Follow_Up_Amendments = list[i].Follow_Up_Amendments;
                listDTO[i].MOHLTC_Follow_Up = list[i].MOHLTC_Follow_Up;
                listDTO[i].MOH_Notified = list[i].MOH_Notified;
                listDTO[i].POAS_Notified = list[i].POAS_Notified;
                listDTO[i].Police_Notified = list[i].Police_Notified;
                listDTO[i].Quality_Improvement_Actions = list[i].Quality_Improvement_Actions;
                listDTO[i].Risk_Locked = list[i].Risk_Locked;
            }
            return listDTO;
        }

        public async Task DeleteIncidentsByIdAsync(int id)
        {
            await Db.Incidents.Delete(id);
        }

        public void DeleteIncident(int id)
        {
            try
            {
                Db.Incidents.Delete(id);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public async Task Update(int id)
        {
            await Db.Incidents.UpdateAsync(id);
        }

        public void Update(Critical_Incidents_DTO dto)
        {
            try
            {
                var model = new Critical_Incidents
                {
                    id = dto.Id,
                    Date = dto.Date,
                    Brief_Description = dto.Brief_Description,
                    Care_Plan_Updated = dto.Care_Plan_Updated,
                    Location = dto.Location,
                    CIS_Initiated = dto.CIS_Initiated,
                    CI_Category_Type = dto.CI_Category_Type,
                    CI_Form_Number = dto.CI_Form_Number,
                    File_Complete = dto.File_Complete,
                    Follow_Up_Amendments = dto.Follow_Up_Amendments,
                    MOHLTC_Follow_Up = dto.MOHLTC_Follow_Up,
                    MOH_Notified = dto.MOH_Notified,
                    POAS_Notified = dto.POAS_Notified,
                    Police_Notified = dto.Police_Notified,
                    Quality_Improvement_Actions = dto.Quality_Improvement_Actions,
                    Risk_Locked = dto.Risk_Locked
                };
                Db.Incidents.Update(model);
                Db.SaveAsync();
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }
        #endregion

        #region Departments:
        public void Insert(Department_DTO dto)
        {
            try
            {
                Department model = new Department
                {
                    Name = dto.Name
                };
                Db.Departments.Create(model);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public IEnumerable<Department_DTO> ReadDepartments()
        {
            var list = Db.Departments.GetAll().ToList();
            var listDTO = new List<Department_DTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new Department_DTO());
                listDTO[i].Id = list[i].Id;
                listDTO[i].Name = list[i].Name;
            }
            return listDTO;
        }

        public async Task DeleteDepartByIdAsync(int id)
        {
            await Db.Departments.Delete(id);
        }

        public void DeleteDepartment(int id)
        {
            try
            {
                Db.Departments.Delete(id);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public void Update(Department_DTO dto)
        {
            try
            {
                var model = new Department
                {
                    Id = dto.Id,
                    Name = dto.Name
                };
                Db.Departments.Update(model);
                Db.SaveAsync();
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }
        #endregion

        #region Education:
        public void Insert(Education_DTO dto)
        {
            try
            {
                Education model = new Education
                {
                    Approx_Per_Educated = dto.Approx_Per_Educated,
                    Apr = dto.Apr,
                    Location = dto.Location,
                    Aug = dto.Aug,
                    DateStart = dto.DateStart,
                    Dec = dto.Dec,
                    Feb = dto.Feb,
                    Jan = dto.Jan,
                    Jul = dto.Jul,
                    Jun = dto.Jun,
                    Mar = dto.Mar,
                    May = dto.May,
                    Nov = dto.Nov,
                    Oct = dto.Oct,
                    Sep = dto.Sep,
                    Session_Name = dto.Session_Name,
                    Total_Numb_Educ = dto.Total_Numb_Educ,
                    Total_Numb_Eligible = dto.Total_Numb_Eligible
                };
                Db.Educations.Create(model);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public IEnumerable<Education_DTO> ReadEducation()
        {
            var list = Db.Educations.GetAll().ToList();
            var listDTO = new List<Education_DTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new Education_DTO());
                listDTO[i].Id = list[i].Id;
                listDTO[i].Approx_Per_Educated = list[i].Approx_Per_Educated;
                listDTO[i].Apr = list[i].Apr;
                listDTO[i].Location = list[i].Location;
                listDTO[i].Aug = list[i].Aug;
                listDTO[i].DateStart = list[i].DateStart;
                listDTO[i].Dec = list[i].Dec;
                listDTO[i].Feb = list[i].Feb;
                listDTO[i].Jan = list[i].Jan;
                listDTO[i].Jul = list[i].Jul;
                listDTO[i].Jun = list[i].Jun;
                listDTO[i].Mar = list[i].Mar;
                listDTO[i].May = list[i].May;
                listDTO[i].Nov = list[i].Nov;
                listDTO[i].Oct = list[i].Oct;
                listDTO[i].Sep = list[i].Sep;
                listDTO[i].Session_Name = list[i].Session_Name;
                listDTO[i].Total_Numb_Educ = list[i].Total_Numb_Educ;
                listDTO[i].Total_Numb_Eligible = list[i].Total_Numb_Eligible;
            }
            return listDTO;
        }

        public async Task DeleteEducatByIdAsync(int id)
        {
            await Db.Educations.Delete(id);
        }

        public void DeleteEducation(int id)
        {
            try
            {
                Db.Educations.Delete(id);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public void Update(Education_DTO dto)
        {
            try
            {
                var model = new Education
                {
                    Id = dto.Id,
                    Approx_Per_Educated = dto.Approx_Per_Educated,
                    Apr = dto.Apr,
                    Location = dto.Location,
                    Aug = dto.Aug,
                    DateStart = dto.DateStart,
                    Dec = dto.Dec,
                    Feb = dto.Feb,
                    Jan = dto.Jan,
                    Jul = dto.Jul,
                    Jun = dto.Jun,
                    Mar = dto.Mar,
                    May = dto.May,
                    Nov = dto.Nov,
                    Oct = dto.Oct,
                    Sep = dto.Sep,
                    Session_Name = dto.Session_Name,
                    Total_Numb_Educ = dto.Total_Numb_Educ,
                    Total_Numb_Eligible = dto.Total_Numb_Eligible
                };
                Db.Educations.Update(model);
                Db.SaveAsync();
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }
        #endregion

        #region Emergency Prep:
        public void Insert(Emergency_Prep_DTO dto)
        {
            try
            {
                Emergency_Prep model = new Emergency_Prep
                {
                    Code = dto.Code,
                    Exercise = dto.Exercise,
                    Date = dto.Date,
                    Method = dto.Method,
                    Location = dto.Location
                };
                Db.EmerPreps.Create(model);
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public IEnumerable<Emergency_Prep_DTO> ReadEmergency()
        {
            var list = Db.EmerPreps.GetAll().ToList();
            var listDTO = new List<Emergency_Prep_DTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new Emergency_Prep_DTO());
                listDTO[i].Id = list[i].Id;
                listDTO[i].Location = list[i].Location;
                listDTO[i].Code = list[i].Code;
                listDTO[i].Date = list[i].Date;
                listDTO[i].Exercise = list[i].Exercise;
                listDTO[i].Method = list[i].Method;
            }
            return listDTO;
        }

        public async Task DeleteEmergenByIdAsync(int id)
        {
            await Db.EmerPreps.Delete(id);
        }

        public void DeleteEmergency(int id)
        {
            try
            {
                Db.EmerPreps.Delete(id);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public void Update(Emergency_Prep_DTO dto)
        {
            try
            {
                var model = new Emergency_Prep
                {
                    Id = dto.Id,
                    Code = dto.Code,
                    Exercise = dto.Exercise,
                    Date = dto.Date,
                    Method = dto.Method,
                    Location = dto.Location
                };
                Db.EmerPreps.Update(model);
                Db.SaveAsync();
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }
        #endregion

        #region Good News:
        public void Insert(Good_News_DTO dto)
        {
            try
            {
                Good_News model = new Good_News
                {
                    Location = dto.Location,
                    Awards_Details = dto.Awards_Details,
                    Category = dto.Category,
                    Awards_Received = dto.Awards_Received,
                    Community_Inititives = dto.Community_Inititives,
                    Compliment = dto.Compliment,
                    DateNews = dto.DateNews,
                    Department = dto.Department,
                    Description_Complim = dto.Description_Complim,
                    Growth = dto.Growth,
                    NameAwards = dto.NameAwards,
                    Passion = dto.Passion,
                    ReceivedFrom = dto.ReceivedFrom,
                    Respect = dto.Respect,
                    Responsibility = dto.Responsibility,
                    SourceCompliment = dto.SourceCompliment,
                    Spot_Awards = dto.Spot_Awards,
                    Teamwork = dto.Teamwork
                };
                Db.GoodNews.Create(model);
                Db.SaveAsync();
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public IEnumerable<Good_News_DTO> ReadNews()
        {
            var list = Db.GoodNews.GetAll().ToList();
            var listDTO = new List<Good_News_DTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new Good_News_DTO());
                listDTO[i].Id = list[i].Id;
                listDTO[i].Location = list[i].Location;
                listDTO[i].Awards_Details = list[i].Awards_Details;
                listDTO[i].Category = list[i].Category;
                listDTO[i].Awards_Received = list[i].Awards_Received;
                listDTO[i].Community_Inititives = list[i].Community_Inititives;
                listDTO[i].Compliment = list[i].Compliment;
                listDTO[i].DateNews = list[i].DateNews;
                listDTO[i].Department = list[i].Department;
                listDTO[i].Description_Complim = list[i].Description_Complim;
                listDTO[i].Growth = list[i].Growth;
                listDTO[i].NameAwards = list[i].NameAwards;
                listDTO[i].Passion = list[i].Passion;
                listDTO[i].ReceivedFrom = list[i].ReceivedFrom;
                listDTO[i].Respect = list[i].Respect;
                listDTO[i].Responsibility = list[i].Responsibility;
                listDTO[i].SourceCompliment = list[i].SourceCompliment;
                listDTO[i].Spot_Awards = list[i].Spot_Awards;
                listDTO[i].Teamwork = list[i].Teamwork;
            }
            return listDTO;
        }

        public async Task DeleteNewsByIdAsync(int id)
        {
            await Db.GoodNews.Delete(id);
        }

        public void DeleteNews(int id)
        {
            try
            {
                Db.GoodNews.Delete(id);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public void Update(Good_News_DTO dto)
        {
            try
            {
                var model = new Good_News
                {
                    Id = dto.Id,
                    Location = dto.Location,
                    Awards_Details = dto.Awards_Details,
                    Category = dto.Category,
                    Awards_Received = dto.Awards_Received,
                    Community_Inititives = dto.Community_Inititives,
                    Compliment = dto.Compliment,
                    DateNews = dto.DateNews,
                    Department = dto.Department,
                    Description_Complim = dto.Description_Complim,
                    Growth = dto.Growth,
                    NameAwards = dto.NameAwards,
                    Passion = dto.Passion,
                    ReceivedFrom = dto.ReceivedFrom,
                    Respect = dto.Respect,
                    Responsibility = dto.Responsibility,
                    SourceCompliment = dto.SourceCompliment,
                    Spot_Awards = dto.Spot_Awards,
                    Teamwork = dto.Teamwork
                };
                Db.GoodNews.Update(model);
                Db.SaveAsync();
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }
        #endregion

        #region Immunization:
        public void Insert(Immunization_DTO dto)
        {
            try
            {
                Immunization model = new Immunization
                {
                    Location = dto.Location,
                    Numb_Res_Comm = dto.Numb_Res_Comm,
                    Numb_Res_Immun = dto.Numb_Res_Immun,
                    Numb_Res_Not_Immun = dto.Numb_Res_Not_Immun,
                    Per_Res_Immun = dto.Per_Res_Immun,
                    Per_Res_Not_Immun = dto.Per_Res_Not_Immun
                };
                Db.Immunizations.Create(model);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public IEnumerable<Immunization_DTO> ReadImmunizations()
        {
            var list = Db.Immunizations.GetAll().ToList();
            var listDTO = new List<Immunization_DTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new Immunization_DTO());
                listDTO[i].Id = list[i].Id;
                listDTO[i].Location = list[i].Location;
                listDTO[i].Numb_Res_Comm = list[i].Numb_Res_Comm;
                listDTO[i].Numb_Res_Immun = list[i].Numb_Res_Immun;
                listDTO[i].Numb_Res_Not_Immun = list[i].Numb_Res_Not_Immun;
                listDTO[i].Per_Res_Immun = list[i].Per_Res_Immun;
                listDTO[i].Per_Res_Not_Immun = list[i].Per_Res_Not_Immun;
            }
            return listDTO;
        }

        public async Task DeleteImmunByIdAsync(int id)
        {
            await Db.Immunizations.Delete(id);
        }

        public void DeleteImmunization(int id)
        {
            try
            {
                Db.Immunizations.Delete(id);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public void Update(Immunization_DTO dto)
        {
            try
            {
                var model = new Immunization
                {
                    Id = dto.Id,
                    Location = dto.Location,
                    Numb_Res_Comm = dto.Numb_Res_Comm,
                    Numb_Res_Immun = dto.Numb_Res_Immun,
                    Numb_Res_Not_Immun = dto.Numb_Res_Not_Immun,
                    Per_Res_Immun = dto.Per_Res_Immun,
                    Per_Res_Not_Immun = dto.Per_Res_Not_Immun
                };
                Db.Immunizations.Update(model);
                Db.SaveAsync();
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }
        #endregion

        #region Labour Relations:
        public void Insert(Labour_Relations_DTO dto)
        {
            try
            {
                Labour_Relations model = new Labour_Relations
                {
                    Location = dto.Location,
                    Accruals = dto.Accruals,
                    Category = dto.Category,
                    Date = dto.Date,
                    Details = dto.Details,
                    Lessons_Learned = dto.Lessons_Learned,
                    Outcome = dto.Outcome,
                    Status = dto.Status,
                    Union = dto.Union
                };
                Db.Relations.Create(model);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public IEnumerable<Labour_Relations_DTO> ReadRelations()
        {
            var list = Db.Relations.GetAll().ToList();
            var listDTO = new List<Labour_Relations_DTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new Labour_Relations_DTO());
                listDTO[i].Id = list[i].Id;
                listDTO[i].Location = list[i].Location;
                listDTO[i].Accruals = list[i].Accruals;
                listDTO[i].Category = list[i].Category;
                listDTO[i].Date = list[i].Date;
                listDTO[i].Details = list[i].Details;
                listDTO[i].Lessons_Learned = list[i].Lessons_Learned;
                listDTO[i].Outcome = list[i].Outcome;
                listDTO[i].Status = list[i].Status;
                listDTO[i].Union = list[i].Union;
            }
            return listDTO;
        }

        public async Task DeleteLabourByIdAsync(int id)
        {
            await Db.Relations.Delete(id);
        }

        public void DeleteRelation(int id)
        {
            try
            {
                Db.Relations.Delete(id);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public void Update(Labour_Relations_DTO dto)
        {
            try
            {
                var model = new Labour_Relations
                {
                    Id = dto.Id,
                    Location = dto.Location,
                    Accruals = dto.Accruals,
                    Category = dto.Category,
                    Date = dto.Date,
                    Details = dto.Details,
                    Lessons_Learned = dto.Lessons_Learned,
                    Outcome = dto.Outcome,
                    Status = dto.Status,
                    Union = dto.Union
                };
                Db.Relations.Update(model);
                Db.SaveAsync();
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }
        #endregion

        #region Not WSiB:
        public void Insert(Not_WSIBs_DTO dto)
        {
            try
            {
                Not_WSIBs model = new Not_WSIBs
                {
                    Location = dto.Location,
                    Date_of_Incident = dto.Date_of_Incident,
                    Details_of_Incident = dto.Details_of_Incident,
                    Employee_Initials = dto.Employee_Initials,
                    Home_Area = dto.Home_Area,
                    Injury_Related = dto.Injury_Related,
                    Position = dto.Position,
                    Shift = dto.Shift,
                    Time_of_Incident = dto.Time_of_Incident,
                    Type_of_Injury = dto.Type_of_Injury
                };
                Db.NotWSIBs.Create(model);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public IEnumerable<Not_WSIBs_DTO> ReadNotWSIBs()
        {
            var list = Db.NotWSIBs.GetAll().ToList();
            var listDTO = new List<Not_WSIBs_DTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new Not_WSIBs_DTO());
                listDTO[i].Id = list[i].Id;
                listDTO[i].Location = list[i].Location;
                listDTO[i].Date_of_Incident = list[i].Date_of_Incident;
                listDTO[i].Details_of_Incident = list[i].Details_of_Incident;
                listDTO[i].Employee_Initials = list[i].Employee_Initials;
                listDTO[i].Home_Area = list[i].Home_Area;
                listDTO[i].Injury_Related = list[i].Injury_Related;
                listDTO[i].Position = list[i].Position;
                listDTO[i].Shift = list[i].Shift;
                listDTO[i].Time_of_Incident = list[i].Time_of_Incident;
                listDTO[i].Type_of_Injury = list[i].Type_of_Injury;
            }
            return listDTO;
        }

        public async Task DeleteMotWByIdAsync(int id)
        {
            await Db.NotWSIBs.Delete(id);
        }

        public void DeleteNotWSIB(int id)
        {
            try
            {
                Db.NotWSIBs.Delete(id);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public void Update(Not_WSIBs_DTO dto)
        {
            try
            {
                var model = new Not_WSIBs
                {
                    Id = dto.Id,
                    Location = dto.Location,
                    Date_of_Incident = dto.Date_of_Incident,
                    Details_of_Incident = dto.Details_of_Incident,
                    Employee_Initials = dto.Employee_Initials,
                    Home_Area = dto.Home_Area,
                    Injury_Related = dto.Injury_Related,
                    Position = dto.Position,
                    Shift = dto.Shift,
                    Time_of_Incident = dto.Time_of_Incident,
                    Type_of_Injury = dto.Type_of_Injury
                };
                Db.NotWSIBs.Update(model);
                Db.SaveAsync();
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }
        #endregion

        #region Outbrakes:
        public void Insert(Outbreaks_DTO dto)
        {
            try
            {
                Outbreaks model = new Outbreaks
                {
                    Location = dto.Location,
                    CI_Report_Submitted = dto.CI_Report_Submitted,
                    Credit_for_Lost_Days = dto.Credit_for_Lost_Days,
                    Date_Concluded = dto.Date_Concluded,
                    Date_Declared = dto.Date_Declared,
                    Deaths_Due = dto.Deaths_Due,
                    Docs_Submitted_Finance = dto.Docs_Submitted_Finance,
                    Notify_MOL = dto.Notify_MOL,
                    Strain_Identified = dto.Strain_Identified,
                    Total_Days_Closed = dto.Total_Days_Closed,
                    Total_Residents_Affected = dto.Total_Residents_Affected,
                    Total_Staff_Affected = dto.Total_Staff_Affected,
                    Tracking_Sheet_Completed = dto.Tracking_Sheet_Completed,
                    Type_of_Outbreak = dto.Type_of_Outbreak
                };
                Db.Outbrakes.Create(model);
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public IEnumerable<Outbreaks_DTO> ReadOutbreaks()
        {
            var list = Db.Outbrakes.GetAll().ToList();
            var listDTO = new List<Outbreaks_DTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new Outbreaks_DTO());
                listDTO[i].Id = list[i].Id;
                listDTO[i].Location = list[i].Location;
                listDTO[i].CI_Report_Submitted = list[i].CI_Report_Submitted;
                listDTO[i].Credit_for_Lost_Days = list[i].Credit_for_Lost_Days;
                listDTO[i].Date_Concluded = list[i].Date_Concluded;
                listDTO[i].Date_Declared = list[i].Date_Declared;
                listDTO[i].Deaths_Due = list[i].Deaths_Due;
                listDTO[i].Docs_Submitted_Finance = list[i].Docs_Submitted_Finance;
                listDTO[i].Notify_MOL = list[i].Notify_MOL;
                listDTO[i].Strain_Identified = list[i].Strain_Identified;
                listDTO[i].Total_Days_Closed = list[i].Total_Days_Closed;
                listDTO[i].Total_Residents_Affected = list[i].Total_Residents_Affected;
                listDTO[i].Total_Staff_Affected = list[i].Total_Staff_Affected;
                listDTO[i].Tracking_Sheet_Completed = list[i].Tracking_Sheet_Completed;
                listDTO[i].Type_of_Outbreak = list[i].Type_of_Outbreak;
            }
            return listDTO;
        }

        public async Task DeleteByIdAsync(int id)
        {
            await Db.Outbrakes.Delete(id);
        }

        public void DeleteOutbreak(int id)
        {
            try
            {
                Db.Outbrakes.Delete(id);
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public void Update(Outbreaks_DTO dto)
        {
            try
            {
                var model = new Outbreaks
                {
                    Id = dto.Id,
                    Location = dto.Location,
                    CI_Report_Submitted = dto.CI_Report_Submitted,
                    Credit_for_Lost_Days = dto.Credit_for_Lost_Days,
                    Date_Concluded = dto.Date_Concluded,
                    Date_Declared = dto.Date_Declared,
                    Deaths_Due = dto.Deaths_Due,
                    Docs_Submitted_Finance = dto.Docs_Submitted_Finance,
                    Notify_MOL = dto.Notify_MOL,
                    Strain_Identified = dto.Strain_Identified,
                    Total_Days_Closed = dto.Total_Days_Closed,
                    Total_Residents_Affected = dto.Total_Residents_Affected,
                    Total_Staff_Affected = dto.Total_Staff_Affected,
                    Tracking_Sheet_Completed = dto.Tracking_Sheet_Completed,
                    Type_of_Outbreak = dto.Type_of_Outbreak
                };
                Db.Outbrakes.Update(model);
                Db.SaveAsync();
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }
        #endregion

        #region Position:
        public void Insert(Position_DTO dto)
        {
            try
            {
                Position model = new Position
                {
                    Name = dto.Name
                };
                Db.Positions.Create(model);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public async Task<IEnumerable<Position_DTO>> ReadPositionsAsync()
        {
            var l = await Db.Positions.GetAllAsync();
            var list = l.ToList();
            var listDTO = new List<Position_DTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new Position_DTO());
                listDTO[i].Id = list[i].Id;
                listDTO[i].Name = list[i].Name;
            }
            return listDTO;
        }

        public IEnumerable<Position_DTO> ReadPositions()
        {
            var list = Db.Positions.GetAll().ToList();
            var listDTO = new List<Position_DTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new Position_DTO());
                listDTO[i].Id = list[i].Id;
                listDTO[i].Name = list[i].Name;
            }
            return listDTO;
        }

        public void DeletePosition(int id)
        {
            try
            {
                Db.Positions.Delete(id);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public void Update(Position_DTO dto)
        {
            try
            {
                var model = new Position
                {
                    Id = dto.Id,
                    Name = dto.Name,
                };
                Db.Positions.Update(model);
                Db.SaveAsync();
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }
        #endregion

        #region Privacy Breaches:
        public void Insert(Privacy_Breaches_DTO dto)
        {
            try
            {
                Privacy_Breaches model = new Privacy_Breaches
                {
                    Location = dto.Location,
                    Date_Breach_Occurred = dto.Date_Breach_Occurred,
                    Date_Breach_Reported = dto.Date_Breach_Reported,
                    Date_Breach_Reported_By = dto.Date_Breach_Reported_By,
                    Description_Outcome = dto.Description_Outcome,
                    Number_of_Individuals_Affected = dto.Number_of_Individuals_Affected,
                    Risk_Level = dto.Risk_Level,
                    Status = dto.Status,
                    Type_of_Breach = dto.Type_of_Breach,
                    Type_of_PHI_Involved = dto.Type_of_PHI_Involved
                };
                Db.PrivacyBreaches.Create(model);
                Db.SaveAsync();
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public IEnumerable<Privacy_Breaches_DTO> ReadBreaches()
        {
            var list = Db.PrivacyBreaches.GetAll().ToList();
            var listDTO = new List<Privacy_Breaches_DTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new Privacy_Breaches_DTO());
                listDTO[i].Id = list[i].Id;
                listDTO[i].Location = list[i].Location;
                listDTO[i].Date_Breach_Occurred = list[i].Date_Breach_Occurred;
                listDTO[i].Date_Breach_Reported = list[i].Date_Breach_Reported;
                listDTO[i].Date_Breach_Reported_By = list[i].Date_Breach_Reported_By;
                listDTO[i].Description_Outcome = list[i].Description_Outcome;
                listDTO[i].Number_of_Individuals_Affected = list[i].Number_of_Individuals_Affected;
                listDTO[i].Risk_Level = list[i].Risk_Level;
                listDTO[i].Status = list[i].Status;
                listDTO[i].Type_of_Breach = list[i].Type_of_Breach;
                listDTO[i].Type_of_PHI_Involved = list[i].Type_of_PHI_Involved;
            }
            return listDTO;
        }

        public async Task DeleteBreachByIdAsync(int id)
        {
            await Db.PrivacyBreaches.Delete(id);
        }

        public void DeleteBreach(int id)
        {
            try
            {
                Db.PrivacyBreaches.Delete(id);
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public void Update(Privacy_Breaches_DTO dto)
        {
            try
            {
                var model = new Privacy_Breaches
                {
                    Id = dto.Id,
                    Location = dto.Location,
                    Date_Breach_Occurred = dto.Date_Breach_Occurred,
                    Date_Breach_Reported = dto.Date_Breach_Reported,
                    Date_Breach_Reported_By = dto.Date_Breach_Reported_By,
                    Description_Outcome = dto.Description_Outcome,
                    Number_of_Individuals_Affected = dto.Number_of_Individuals_Affected,
                    Risk_Level = dto.Risk_Level,
                    Status = dto.Status,
                    Type_of_Breach = dto.Type_of_Breach,
                    Type_of_PHI_Involved = dto.Type_of_PHI_Involved
                };
                Db.PrivacyBreaches.Update(model);
                Db.SaveAsync();
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }
        #endregion

        #region Privacy Complaints:
        public void Insert(Privacy_Complaints_DTO dto)
        {
            try
            {
                Privacy_Complaints model = new Privacy_Complaints
                {
                    Location = dto.Location,
                    Complain_Filed_By = dto.Complain_Filed_By,
                    Is_Complaint_Resolved = dto.Is_Complaint_Resolved,
                    Date_Complain_Received = dto.Date_Complain_Received,
                    Description_Outcome = dto.Description_Outcome,
                    Type_of_Complaint = dto.Type_of_Complaint,
                    Status = dto.Status
                };
                Db.PrivacyComplaints.Create(model);
                Db.SaveAsync();
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public IEnumerable<Privacy_Complaints_DTO> ReadPComplaints()
        {
            var list = Db.PrivacyComplaints.GetAll().ToList();
            var listDTO = new List<Privacy_Complaints_DTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new Privacy_Complaints_DTO());
                listDTO[i].Id = list[i].id;
                listDTO[i].Location = list[i].Location;
                listDTO[i].Complain_Filed_By = list[i].Complain_Filed_By;
                listDTO[i].Is_Complaint_Resolved = list[i].Is_Complaint_Resolved;
                listDTO[i].Date_Complain_Received = list[i].Date_Complain_Received;
                listDTO[i].Description_Outcome = list[i].Description_Outcome;
                listDTO[i].Type_of_Complaint = list[i].Type_of_Complaint;
                listDTO[i].Status = list[i].Status;
            }
            return listDTO;
        }

        public async Task DeletePComplainByIdAsync(int id)
        {
            await Db.PrivacyComplaints.Delete(id);
        }

        public void DeletePComplaint(int id)
        {
            try
            {
                Db.PrivacyComplaints.Delete(id);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public void Update(Privacy_Complaints_DTO dto)
        {
            try
            {
                var model = new Privacy_Complaints
                {
                    id = dto.Id,
                    Location = dto.Location,
                    Complain_Filed_By = dto.Complain_Filed_By,
                    Is_Complaint_Resolved = dto.Is_Complaint_Resolved,
                    Date_Complain_Received = dto.Date_Complain_Received,
                    Description_Outcome = dto.Description_Outcome,
                    Type_of_Complaint = dto.Type_of_Complaint,
                    Status = dto.Status
                };
                Db.PrivacyComplaints.Update(model);
                Db.SaveAsync();
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }
        #endregion

        #region Search Words:
        public void Insert(Search_Word_DTO dto)
        {
            try
            {
                Search_Word model = new Search_Word
                {
                    CustomersWord = dto.CustomersWord,
                    FileName = dto.FileName,
                    Word = dto.Word
                };
                Db.SearchWords.Create(model);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public IEnumerable<Search_Word_DTO> ReadWords()
        {
            var list = Db.SearchWords.GetAll().ToList();
            var listDTO = new List<Search_Word_DTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new Search_Word_DTO());
                listDTO[i].Id = list[i].Id;
                listDTO[i].CustomersWord = list[i].CustomersWord;
                listDTO[i].FileName = list[i].FileName;
                listDTO[i].Word = list[i].Word;
            }
            return listDTO;
        }

        public async Task DeleteWordsByIdAsync(int id)
        {
            await Db.SearchWords.Delete(id);
        }

        public void DeleteWord(int id)
        {
            try
            {
                Db.SearchWords.Delete(id);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public void Update(Search_Word_DTO dto)
        {
            try
            {
                var model = new Search_Word
                {
                    Id = dto.Id,
                    CustomersWord = dto.CustomersWord,
                    FileName = dto.FileName,
                    Word = dto.Word
                };
                Db.SearchWords.Update(model);
                Db.SaveAsync();
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }
        #endregion

        #region Sign In Main:
        public void Insert(Sign_in_Main_DTO dto)
        {
            try
            {
                Sign_in_Main model = new Sign_in_Main
                {
                    Care_Community_Centre = dto.Care_Community_Centre,
                    Current_Date = dto.Current_Date,
                    Date_Entred = dto.Date_Entred,
                    Position = dto.Position,
                    User_Name = dto.User_Name,
                    Week = dto.Week
                };
                Db.SignInMains.Create(model);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public IEnumerable<Sign_in_Main_DTO> ReadSignIn()
        {
            var list = Db.SignInMains.GetAll().ToList();
            var listDTO = new List<Sign_in_Main_DTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new Sign_in_Main_DTO());
                listDTO[i].Id = list[i].Id;
                listDTO[i].Care_Community_Centre = list[i].Care_Community_Centre;
                listDTO[i].Current_Date = list[i].Current_Date;
                listDTO[i].Date_Entred = list[i].Date_Entred;
                listDTO[i].Position = list[i].Position;
                listDTO[i].User_Name = list[i].User_Name;
                listDTO[i].Week = list[i].Week;
            }
            return listDTO;
        }

        public async Task DeleteSignInMainByIdAsync(int id)
        {
            await Db.SignInMains.Delete(id);
        }

        public void DeletesignIn(int id)
        {
            try
            {
                Db.SignInMains.Delete(id);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public void Update(Sign_in_Main_DTO dto)
        {
            try
            {
                var model = new Sign_in_Main
                {
                    Id = dto.Id,
                    Care_Community_Centre = dto.Care_Community_Centre,
                    Current_Date = dto.Current_Date,
                    Date_Entred = dto.Date_Entred,
                    Position = dto.Position,
                    User_Name = dto.User_Name,
                    Week = dto.Week
                };
                Db.SignInMains.Update(model);
                Db.SaveAsync();
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }
        #endregion

        #region Users:
        public void Insert(Users_DTO dto)
        {
            try
            {
                Users model = new Users
                {
                    Care_Community = dto.Care_Community,
                    Date = dto.Date,
                    Date_Register = dto.Date_Register,
                    First_Name = dto.First_Name,
                    Last_Name = dto.Last_Name,
                    Login = dto.Login,
                    Password = dto.Password,
                    Position = dto.Position,
                    Role = dto.Role,
                    User_Name = dto.User_Name,
                    Week = dto.Week,
                    Region = dto.Region,
                    Email = dto.Email,
                    ConfirmedEmail = dto.ConfirmedEmail,
                    ConfirmPassword = dto.ConfirmPassword
                };
                var res = Db.Users.Create(model);
                Db.SaveAsync();
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public IEnumerable<Users_DTO> ReadUsers()
        {
            var list = Db.Users.GetAll().ToList();
            var listDTO = new List<Users_DTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new Users_DTO());
                listDTO[i].Id = list[i].Id;
                listDTO[i].Care_Community = list[i].Care_Community;
                listDTO[i].Date = list[i].Date;
                listDTO[i].Date_Register = list[i].Date_Register;
                listDTO[i].First_Name = list[i].First_Name;
                listDTO[i].Last_Name = list[i].Last_Name;
                listDTO[i].Login = list[i].Login;
                listDTO[i].Password = list[i].Password;
                listDTO[i].Position = list[i].Position;
                listDTO[i].Role = list[i].Role;
                listDTO[i].User_Name = list[i].User_Name;
                listDTO[i].Week = list[i].Week;
                listDTO[i].Email = list[i].Email;
                listDTO[i].ConfirmedEmail = list[i].ConfirmedEmail;
                listDTO[i].Date_Register = list[i].Date_Register;
                listDTO[i].Region = list[i].Region;
            }
            return listDTO;
        }

        public async Task DeleteUserByIdAsync(int id)
        {
            await Db.Users.Delete(id);
        }

        public void DeleteUser(int id)
        {
            try
            {
                Db.Users.Delete(id);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public void Update(Users_DTO dto)
        {
            try
            {
                var model = new Users
                {
                    Id = dto.Id,
                    Care_Community = dto.Care_Community,
                    Date = dto.Date,
                    Date_Register = dto.Date_Register,
                    First_Name = dto.First_Name,
                    Last_Name = dto.Last_Name,
                    Login = dto.Login,
                    Password = dto.Password,
                    Position = dto.Position,
                    Role = dto.Role,
                    User_Name = dto.User_Name,
                    Week = dto.Week,
                    Region = dto.Region,
                    Email = dto.Email,
                    ConfirmedEmail = dto.ConfirmedEmail,
                    ConfirmPassword = dto.ConfirmPassword
                };
                Db.Users.Update(model);
                Db.SaveAsync();
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }
        #endregion

        #region Visits Agency:
        public void Insert(Visits_Agency_DTO dto)
        {
            try
            {
                Visits_Agency model = new Visits_Agency
                {
                    Agency = dto.Agency,
                    Corrective_Actions = dto.Corrective_Actions,
                    Date_of_Visit = dto.Date_of_Visit,
                    Location = dto.Location,
                    Findings_Details = dto.Findings_Details,
                    Report_Posted = dto.Report_Posted,
                    Findings_number = dto.Findings_number
                };
                Db.VisitsAgencies.Create(model);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public IEnumerable<Visits_Agency_DTO> ReadAgencies()
        {
            var list = Db.VisitsAgencies.GetAll().ToList();
            var listDTO = new List<Visits_Agency_DTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new Visits_Agency_DTO());
                listDTO[i].Id = list[i].Id;
                listDTO[i].Agency = list[i].Agency;
                listDTO[i].Corrective_Actions = list[i].Corrective_Actions;
                listDTO[i].Date_of_Visit = list[i].Date_of_Visit;
                listDTO[i].Location = list[i].Location;
                listDTO[i].Findings_Details = list[i].Findings_Details;
                listDTO[i].Report_Posted = list[i].Report_Posted;
                listDTO[i].Findings_number = list[i].Findings_number;
            }
            return listDTO;
        }

        public void DeleteAgency(int id)
        {
            try
            {
                Db.VisitsAgencies.Delete(id);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public void Update(Visits_Agency_DTO dto)
        {
            try
            {
                var model = new Visits_Agency
                {
                    Id = dto.Id,
                    Agency = dto.Agency,
                    Corrective_Actions = dto.Corrective_Actions,
                    Date_of_Visit = dto.Date_of_Visit,
                    Location = dto.Location,
                    Findings_Details = dto.Findings_Details,
                    Report_Posted = dto.Report_Posted,
                    Findings_number = dto.Findings_number
                };
                Db.VisitsAgencies.Update(model);
                Db.SaveAsync();
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }
        #endregion

        #region Visits Others:
        public void Insert(Visits_Others_DTO dto)
        {
            try
            {
                Visits_Others model = new Visits_Others
                {
                    Agency = dto.Agency,
                    Corrective_Actions = dto.Corrective_Actions,
                    Date_of_Visit = dto.Date_of_Visit,
                    Location = dto.Location,
                    Details_of_Findings = dto.Details_of_Findings,
                    Report_Posted = dto.Report_Posted,
                    LHIN_Letter_Received = dto.LHIN_Letter_Received,
                    Number_of_Findings = dto.Report_Posted,
                    PH_Letter_Received = dto.PH_Letter_Received
                };
                Db.VisitsOthers.Create(model);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public IEnumerable<Visits_Others_DTO> ReadOthers()
        {
            var list = Db.VisitsOthers.GetAll().ToList();
            var listDTO = new List<Visits_Others_DTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new Visits_Others_DTO());
                listDTO[i].Id = list[i].Id;
                listDTO[i].Agency = list[i].Agency;
                listDTO[i].Corrective_Actions = list[i].Corrective_Actions;
                listDTO[i].Date_of_Visit = list[i].Date_of_Visit;
                listDTO[i].Location = list[i].Location;
                listDTO[i].Details_of_Findings = list[i].Details_of_Findings;
                listDTO[i].Report_Posted = list[i].Report_Posted;
                listDTO[i].LHIN_Letter_Received = list[i].LHIN_Letter_Received;
                listDTO[i].Number_of_Findings = list[i].Report_Posted;
                listDTO[i].PH_Letter_Received = list[i].PH_Letter_Received;
            }
            return listDTO;
        }

        public void DeleteOther(int id)
        {
            try
            {
                Db.VisitsOthers.Delete(id);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public void Update(Visits_Others_DTO dto)
        {
            try
            {
                var model = new Visits_Others
                {
                    Id = dto.Id,
                    Agency = dto.Agency,
                    Corrective_Actions = dto.Corrective_Actions,
                    Date_of_Visit = dto.Date_of_Visit,
                    Location = dto.Location,
                    Details_of_Findings = dto.Details_of_Findings,
                    Report_Posted = dto.Report_Posted,
                    LHIN_Letter_Received = dto.LHIN_Letter_Received,
                    Number_of_Findings = dto.Report_Posted,
                    PH_Letter_Received = dto.PH_Letter_Received
                };
                Db.VisitsOthers.Update(model);
                Db.SaveAsync();
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }
        #endregion

        #region WSIB:
        public void Insert(WSIB_DTO dto)
        {
            try
            {
                WSIB model = new WSIB
                {
                    Accident_Cause = dto.Accident_Cause,
                    Date_Accident = dto.Date_Accident,
                    Date_Duties = dto.Date_Duties,
                    Location = dto.Location,
                    Employee_Initials = dto.Employee_Initials,
                    Date_Regular = dto.Date_Regular,
                    Form_7 = dto.Form_7,
                    Lost_Days = dto.Lost_Days,
                    Modified_Days_Not_Shadowed = dto.Modified_Days_Not_Shadowed,
                    Modified_Days_Shadowed = dto.Modified_Days_Shadowed
                };
                Db.WSIBs.Create(model);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public IEnumerable<WSIB_DTO> ReadWSiBs()
        {
            var list = Db.WSIBs.GetAll().ToList();
            var listDTO = new List<WSIB_DTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new WSIB_DTO());
                listDTO[i].Id = list[i].Id;
                listDTO[i].Accident_Cause = list[i].Accident_Cause;
                listDTO[i].Date_Accident = list[i].Date_Accident;
                listDTO[i].Date_Duties = list[i].Date_Duties;
                listDTO[i].Location = list[i].Location;
                listDTO[i].Employee_Initials = list[i].Employee_Initials;
                listDTO[i].Date_Regular = list[i].Date_Regular;
                listDTO[i].Form_7 = list[i].Form_7;
                listDTO[i].Lost_Days = list[i].Lost_Days;
                listDTO[i].Modified_Days_Not_Shadowed = list[i].Modified_Days_Not_Shadowed;
                listDTO[i].Modified_Days_Shadowed = list[i].Modified_Days_Shadowed;
            }
            return listDTO;
        }

        public async Task DeleteWSIBByIdAsync(int id)
        {
            await Db.WSIBs.Delete(id);
        }

        public void DeleteWSiB(int id)
        {
            try
            {
                Db.WSIBs.Delete(id);
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public void Update(WSIB_DTO dto)
        {
            try
            {
                var model = new WSIB
                {
                    Id = dto.Id,
                    Accident_Cause = dto.Accident_Cause,
                    Date_Accident = dto.Date_Accident,
                    Date_Duties = dto.Date_Duties,
                    Location = dto.Location,
                    Employee_Initials = dto.Employee_Initials,
                    Date_Regular = dto.Date_Regular,
                    Form_7 = dto.Form_7,
                    Lost_Days = dto.Lost_Days,
                    Modified_Days_Not_Shadowed = dto.Modified_Days_Not_Shadowed,
                    Modified_Days_Shadowed = dto.Modified_Days_Shadowed
                };
                Db.WSIBs.Update(model);
                Db.SaveAsync();
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }
        #endregion

        #region Activities:
        //public void Insert(Activities_DTO model)
        //{
        //    try
        //    {
        //        act model = new WSIB
        //        {
        //            Accident_Cause = dto.Accident_Cause,
        //            Date_Accident = dto.Date_Accident,
        //            Date_Duties = dto.Date_Duties,
        //            Location = dto.Location,
        //            Employee_Initials = dto.Employee_Initials,
        //            Date_Regular = dto.Date_Regular,
        //            Form_7 = dto.Form_7,
        //            Lost_Days = dto.Lost_Days,
        //            Modified_Days_Not_Shadowed = dto.Modified_Days_Not_Shadowed,
        //            Modified_Days_Shadowed = dto.Modified_Days_Shadowed
        //        };
        //        Db.WSIBs.Create(model);
        //        
        //    }
        //    catch (Exception ex) { Logger.Write(ex.Message); }
        //}

        //public IEnumerable<Activities_DTO> ReadActivities()
        //{
        //    var list = Db.WSIBs.GetAll().ToList();
        //    var listDTO = new List<WSIB_DTO>();
        //    for (int i = 0; i < list.Count; i++)
        //    {
        //        listDTO.Add(new WSIB_DTO());
        //        listDTO[i].Id = list[i].Id;
        //        listDTO[i].Accident_Cause = list[i].Accident_Cause;
        //        listDTO[i].Date_Accident = list[i].Date_Accident;
        //        listDTO[i].Date_Duties = list[i].Date_Duties;
        //        listDTO[i].Location = list[i].Location;
        //        listDTO[i].Employee_Initials = list[i].Employee_Initials;
        //        listDTO[i].Date_Regular = list[i].Date_Regular;
        //        listDTO[i].Form_7 = list[i].Form_7;
        //        listDTO[i].Lost_Days = list[i].Lost_Days;
        //        listDTO[i].Modified_Days_Not_Shadowed = list[i].Modified_Days_Not_Shadowed;
        //        listDTO[i].Modified_Days_Shadowed = list[i].Modified_Days_Shadowed;
        //    }
        //    return listDTO;
        //}

        //public void Update(Activities_DTO model)
        //{
        //    try
        //    {
        //        var model = new WSIB
        //        {
        //            Id = dto.Id,
        //            Accident_Cause = dto.Accident_Cause,
        //            Date_Accident = dto.Date_Accident,
        //            Date_Duties = dto.Date_Duties,
        //            Location = dto.Location,
        //            Employee_Initials = dto.Employee_Initials,
        //            Date_Regular = dto.Date_Regular,
        //            Form_7 = dto.Form_7,
        //            Lost_Days = dto.Lost_Days,
        //            Modified_Days_Not_Shadowed = dto.Modified_Days_Not_Shadowed,
        //            Modified_Days_Shadowed = dto.Modified_Days_Shadowed
        //        };
        //        Db.WSIBs.Update(model);
        //    }
        //    catch (Exception ex) { Logger.Write(ex.Message); }
        //}

        //public void DeleteActivity(int id)
        //{
        //    try
        //    {
        //        Db.WSIBs.Delete(id);
        //    }
        //    catch (Exception ex) { Logger.Write(ex.Message); }
        //}
        #endregion

        #region BC_LTC_Reportable_Incidents:
        public void Insert(BC_LTC_Reportable_Incidents_DTO dto)
        {
            try
            {
                BC_LTC_Reportable_Incidents model = new BC_LTC_Reportable_Incidents
                {
                    Id = dto.Id,
                    BriefDescIncid = dto.BriefDescIncid,
                    BriefDescTaken = dto.BriefDescTaken,
                    CareCommName = dto.CareCommName,
                    DateIncident = dto.DateIncident,
                    IncidentType = dto.IncidentType,
                    Notifications = dto.Notifications
                };
                Db.BC_LTCIncident.Create(model);
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public async Task<IEnumerable<BC_LTC_Reportable_Incidents_DTO>> ReadBC_LTCAsync()
        {
            var l = await Db.BC_LTCIncident.GetAllAsync();
            var list = l.ToList();
            var listDTO = new List<BC_LTC_Reportable_Incidents_DTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new BC_LTC_Reportable_Incidents_DTO());
                listDTO[i].Id = list[i].Id;
                listDTO[i].BriefDescIncid = list[i].BriefDescIncid;
                listDTO[i].BriefDescTaken = list[i].BriefDescTaken;
                listDTO[i].CareCommName = list[i].CareCommName;
                listDTO[i].DateIncident = list[i].DateIncident;
                listDTO[i].IncidentType = list[i].IncidentType;
                listDTO[i].Notifications = list[i].Notifications;
            }
            return listDTO;
        }

        public IEnumerable<BC_LTC_Reportable_Incidents_DTO> ReadBC_LTC()
        {
            var list = Db.BC_LTCIncident.GetAll().ToList();
            var listDTO = new List<BC_LTC_Reportable_Incidents_DTO>();
            for (int i = 0; i < list.Count; i++)
            {

                listDTO.Add(new BC_LTC_Reportable_Incidents_DTO());
                listDTO[i].Id = list[i].Id;
                listDTO[i].BriefDescIncid = list[i].BriefDescIncid;
                listDTO[i].BriefDescTaken = list[i].BriefDescTaken;
                listDTO[i].CareCommName = list[i].CareCommName;
                listDTO[i].DateIncident = list[i].DateIncident;
                listDTO[i].IncidentType = list[i].IncidentType;
                listDTO[i].Notifications = list[i].Notifications;
            }
            return listDTO;
        }

        public async Task DeleteBC_LTCByIdAsync(int id)
        {
            await Db.BC_LTCIncident.Delete(id);
        }

        public void DeleteBC_LTC(int id)
        {
            try
            {
                Db.BC_LTCIncident.Delete(id);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public void Update(BC_LTC_Reportable_Incidents_DTO dto)
        {
            try
            {
                var model = new BC_LTC_Reportable_Incidents
                {
                    Id = dto.Id,
                    BriefDescIncid = dto.BriefDescIncid,
                    BriefDescTaken = dto.BriefDescTaken,
                    CareCommName = dto.CareCommName,
                    DateIncident = dto.DateIncident,
                    IncidentType = dto.IncidentType,
                    Notifications = dto.Notifications
                };
                Db.BC_LTCIncident.Update(model);
                //Db.BC_LTCIncident.UpdateAsync(model.Id);
                var res = Db.SaveAsync();
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }
        #endregion

        #region BC_Assisted_Living_Reportable_Incidents:
        public void Insert(BC_Assisted_Living_Reportable_Incidents_DTO dto)
        {
            try
            {
                BC_Assisted_Living_Reportable_Incidents model = new BC_Assisted_Living_Reportable_Incidents
                {
                    Id = dto.Id,
                    NameCareCommu = dto.NameCareCommu,
                    BriefDescrTaken = dto.BriefDescrTaken,
                    BriefDescrincident = dto.BriefDescrincident,
                    DateIncident = dto.DateIncident,
                    IncidentType = dto.IncidentType,
                    Notifications = dto.Notifications
                };
                Db.BC_Assisted_Living.Create(model);
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public async Task<IEnumerable<BC_Assisted_Living_Reportable_Incidents_DTO>> ReadBC_LTC_AssistedAsync()
        {
            var l = await Db.BC_Assisted_Living.GetAllAsync();
            var list = l.ToList();
            var listDTO = new List<BC_Assisted_Living_Reportable_Incidents_DTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new BC_Assisted_Living_Reportable_Incidents_DTO());
                listDTO[i].Id = list[i].Id;
                listDTO[i].BriefDescrincident = list[i].BriefDescrincident;
                listDTO[i].BriefDescrTaken = list[i].BriefDescrTaken;
                listDTO[i].NameCareCommu = list[i].NameCareCommu;
                listDTO[i].DateIncident = list[i].DateIncident;
                listDTO[i].IncidentType = list[i].IncidentType;
                listDTO[i].Notifications = list[i].Notifications;
            }
            return listDTO;
        }

        public IEnumerable<BC_Assisted_Living_Reportable_Incidents_DTO> ReadBC_LTCAssisted()
        {
            var list = Db.BC_Assisted_Living.GetAll().ToList();
            var listDTO = new List<BC_Assisted_Living_Reportable_Incidents_DTO>();
            for (int i = 0; i < list.Count; i++)
            {

                listDTO.Add(new BC_Assisted_Living_Reportable_Incidents_DTO());
                listDTO[i].Id = list[i].Id;
                listDTO[i].BriefDescrincident = list[i].BriefDescrincident;
                listDTO[i].BriefDescrTaken = list[i].BriefDescrTaken;
                listDTO[i].NameCareCommu = list[i].NameCareCommu;
                listDTO[i].DateIncident = list[i].DateIncident;
                listDTO[i].IncidentType = list[i].IncidentType;
                listDTO[i].Notifications = list[i].Notifications;
            }
            return listDTO;
        }

        public async Task DeleteBC_LTCAssistedByIdAsync(int id)
        {
            await Db.BC_Assisted_Living.Delete(id);
        }

        public void DeleteBC_LTCAssisted(int id)
        {
            try
            {
                Db.BC_Assisted_Living.Delete(id);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public void Update(BC_Assisted_Living_Reportable_Incidents_DTO dto)
        {
            try
            {
                var model = new BC_Assisted_Living_Reportable_Incidents
                {
                    Id = dto.Id,
                    NameCareCommu = dto.NameCareCommu,
                    BriefDescrTaken = dto.BriefDescrTaken,
                    BriefDescrincident = dto.BriefDescrincident,
                    DateIncident = dto.DateIncident,
                    IncidentType = dto.IncidentType,
                    Notifications = dto.Notifications
                };
                Db.BC_Assisted_Living.Update(model);
                Db.SaveAsync();

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }
        #endregion

        #region Other:
        public void Insert(OtherDTO dto)
        {
            try
            {
                Other model = new Other
                {
                    Location = dto.Location,
                    Date = dto.Date,
                    Inspected_By = dto.Inspected_By,
                    Inspection_Number = dto.Inspection_Number,
                    Notes_Comments = dto.Notes_Comments,
                    Number_of_Violations = dto.Number_of_Violations
                };
                Db.Others.Create(model);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public IEnumerable<OtherDTO> ReadOthersO()
        {
            var list = Db.Others.GetAll().ToList();
            var listDTO = new List<OtherDTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new OtherDTO());
                listDTO[i].Id = list[i].Id;
                listDTO[i].Location = list[i].Location;
                listDTO[i].Date = list[i].Date;
                listDTO[i].Inspected_By = list[i].Inspected_By;
                listDTO[i].Inspection_Number = list[i].Inspection_Number;
                listDTO[i].Notes_Comments = list[i].Notes_Comments;
                listDTO[i].Number_of_Violations = list[i].Number_of_Violations;
            }
            return listDTO;
        }

        public async Task DeleteOtherByOIdAsync(int id)
        {
            await Db.Others.Delete(id);
        }

        public void DeleteOtherO(int id)
        {
            try
            {
                Db.Others.Delete(id);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public void Update(OtherDTO dto)
        {
            try
            {
                var model = new Other
                {
                    Id = dto.Id,
                    Location = dto.Location,
                    Date = dto.Date,
                    Inspected_By = dto.Inspected_By,
                    Inspection_Number = dto.Inspection_Number,
                    Notes_Comments = dto.Notes_Comments,
                    Number_of_Violations = dto.Number_of_Violations
                };
                Db.Others.Update(model);
                Db.SaveAsync();
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }
        #endregion

        #region Licensing Inspections:
        public void Insert(LicensingInspectionDTO dto)
        {
            try
            {
                var model = new LicensingInspection
                {
                    BriefDescription = dto.BriefDescription,
                    CareComName = dto.CareComName,
                    Contraventions = dto.Contraventions,
                    Date = dto.Date,
                    InspectComplaint = dto.InspectComplaint,
                    InspectTypeReason = dto.InspectTypeReason,
                    NoFinding = dto.NoFinding,
                    ResidentCareRegSec = dto.ResidentCareRegSec,
                    ResidentCareRegSub = dto.ResidentCareRegSub,
                    ActionDate = dto.ActionDate,
                    ActionPlan = dto.ActionPlan,
                    Responsibility = dto.Responsibility,
                    CommCareLivAct = dto.CommCareLivAct
                };
                Db.LicensInspect.Create(model);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public IEnumerable<LicensingInspectionDTO> ReadLiceInspect()
        {
            var list = Db.LicensInspect.GetAll().ToList();
            var listDTO = new List<LicensingInspectionDTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new LicensingInspectionDTO());
                listDTO[i].Id = list[i].Id;
                listDTO[i].BriefDescription = list[i].BriefDescription;
                listDTO[i].CareComName = list[i].CareComName;
                listDTO[i].Contraventions = list[i].Contraventions;
                listDTO[i].Date = list[i].Date;
                listDTO[i].InspectComplaint = list[i].InspectComplaint;
                listDTO[i].InspectTypeReason = list[i].InspectTypeReason;
                listDTO[i].NoFinding = list[i].NoFinding;
                listDTO[i].ResidentCareRegSec = list[i].ResidentCareRegSec;
                listDTO[i].ResidentCareRegSub = list[i].ResidentCareRegSub;
                listDTO[i].Responsibility = list[i].Responsibility;
                listDTO[i].ActionDate = list[i].ActionDate;
                listDTO[i].ActionPlan = list[i].ActionPlan;
                listDTO[i].CommCareLivAct = list[i].CommCareLivAct;
            }
            return listDTO;
        }

        public async Task DeleteLiceInspByIdAsync(int id)
        {
            await Db.LicensInspect.Delete(id);
        }

        public void DeleteLiceInspect(int id)
        {
            try
            {
                Db.LicensInspect.Delete(id);
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public void Update(LicensingInspectionDTO dto)
        {
            try
            {
                var model = new LicensingInspection
                {
                    Id = dto.Id,
                    BriefDescription = dto.BriefDescription,
                    CareComName = dto.CareComName,
                    Contraventions = dto.Contraventions,
                    Date = dto.Date,
                    InspectComplaint = dto.InspectComplaint,
                    InspectTypeReason = dto.InspectTypeReason,
                    NoFinding = dto.NoFinding,
                    ResidentCareRegSec = dto.ResidentCareRegSec,
                    ResidentCareRegSub = dto.ResidentCareRegSub,
                    ActionDate = dto.ActionDate,
                    ActionPlan = dto.ActionPlan,
                    Responsibility = dto.Responsibility,
                    CommCareLivAct = dto.CommCareLivAct
                };
                Db.LicensInspect.Update(model);
                Db.SaveAsync();
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }
        #endregion

        #region AssistedLivingInspection:
        public void Insert(AssistedLivingInspectionDTO dto)
        {
            try
            {
                var model = new AssistedLivingInspection
                {
                    CareComName = dto.CareComName,
                    ActionPlan = dto.ActionPlan,
                    Date = dto.Date,
                    InspectComplaint = dto.InspectComplaint,
                    InspectTypeReason = dto.InspectTypeReason,
                    NoFinding = dto.NoFinding,
                    AssistLivReg = dto.AssistLivReg,
                    AssistLivAct = dto.AssistLivAct,
                    ActOrReg = dto.ActOrReg,
                    SubActOrReg = dto.SubActOrReg,
                    Category = dto.Category,
                    BriefDescOfFinding = dto.BriefDescOfFinding,
                    ActionDate = dto.ActionDate,
                    Responsibility = dto.Responsibility
                };
                Db.AssistLivingInspect.Create(model);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public IEnumerable<AssistedLivingInspectionDTO> ReadAssLivInspect()
        {
            var list = Db.AssistLivingInspect.GetAll().ToList();
            var listDTO = new List<AssistedLivingInspectionDTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new AssistedLivingInspectionDTO());
                listDTO[i].Id = list[i].Id;
                listDTO[i].CareComName = list[i].CareComName;
                listDTO[i].AssistLivReg = list[i].AssistLivReg;
                listDTO[i].AssistLivAct = list[i].AssistLivAct;
                listDTO[i].ActOrReg = list[i].ActOrReg;
                listDTO[i].SubActOrReg = list[i].SubActOrReg;
                listDTO[i].Category = list[i].Category;
                listDTO[i].BriefDescOfFinding = list[i].BriefDescOfFinding;
                listDTO[i].Date = list[i].Date;
                listDTO[i].InspectComplaint = list[i].InspectComplaint;
                listDTO[i].InspectTypeReason = list[i].InspectTypeReason;
                listDTO[i].NoFinding = list[i].NoFinding;
                listDTO[i].ActionPlan = list[i].ActionPlan;
                listDTO[i].Responsibility = list[i].Responsibility;
                listDTO[i].ActionDate = list[i].ActionDate;
            }
            return listDTO;
        }

        public void Update(AssistedLivingInspectionDTO dto)
        {
            try
            {
                var model = new AssistedLivingInspection
                {
                    Id = dto.Id,
                    CareComName = dto.CareComName,
                    ActionPlan = dto.ActionPlan,
                    Date = dto.Date,
                    InspectComplaint = dto.InspectComplaint,
                    InspectTypeReason = dto.InspectTypeReason,
                    NoFinding = dto.NoFinding,
                    AssistLivReg = dto.AssistLivReg,
                    AssistLivAct = dto.AssistLivAct,
                    ActOrReg = dto.ActOrReg,
                    SubActOrReg = dto.SubActOrReg,
                    Category = dto.Category,
                    BriefDescOfFinding = dto.BriefDescOfFinding,
                    ActionDate = dto.ActionDate,
                    Responsibility = dto.Responsibility
                };

                Db.AssistLivingInspect.Update(model);
                Db.SaveAsync();
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public async Task DeleteAssLivInspByIdAsync(int id)
        {
            await Db.AssistLivingInspect.Delete(id);
        }
        #endregion

        #region  WorkshopDCInspections:
        public void Insert(WorkshopBCInspection_DTO dto)
        {
            try
            {
                var model = new WorkshopBCInspection
                {
                    CareComName = dto.CareComName,
                    ActionPlan = dto.ActionPlan,
                    Date = dto.Date,
                    InspecteReport = dto.InspecteReport,
                    NoOrders = dto.NoOrders,
                    Orders = dto.Orders,
                    BriefDescFind = dto.BriefDescFind,
                    ActionDate = dto.ActionDate,
                    Responsibility = dto.Responsibility,
                    ScopeOfInspectiont = dto.ScopeOfInspectiont,
                    StatusOfTheOrder= dto.StatusOfTheOrder
                };
                Db.WorkshopBCInspection.Create(model);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public IEnumerable<WorkshopBCInspection_DTO> ReadWorkshopBCInspect()
        {
            var list = Db.WorkshopBCInspection.GetAll().ToList();
            var listDTO = new List<WorkshopBCInspection_DTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new WorkshopBCInspection_DTO());
                listDTO[i].Id = list[i].Id;
                listDTO[i].CareComName = list[i].CareComName;
                listDTO[i].ActionPlan = list[i].ActionPlan;
                listDTO[i].Date = list[i].Date;
                listDTO[i].InspecteReport = list[i].InspecteReport;
                listDTO[i].ScopeOfInspectiont = list[i].ScopeOfInspectiont;
                listDTO[i].NoOrders = list[i].NoOrders;
                listDTO[i].StatusOfTheOrder = list[i].StatusOfTheOrder;
                listDTO[i].Orders = list[i].Orders;
                listDTO[i].Responsibility = list[i].Responsibility;
                listDTO[i].ActionDate = list[i].ActionDate;
                listDTO[i].BriefDescFind = list[i].Orders;
            }
            return listDTO;
        }

        public void Update(WorkshopBCInspection_DTO dto)
        {
            try
            {
                var model = new WorkshopBCInspection
                {
                    Id = dto.Id,
                    CareComName = dto.CareComName,
                    ActionPlan = dto.ActionPlan,
                    Date = dto.Date,
                    InspecteReport = dto.InspecteReport,
                    NoOrders = dto.NoOrders,
                    Orders = dto.Orders,
                    BriefDescFind = dto.BriefDescFind,
                    ActionDate = dto.ActionDate,
                    Responsibility = dto.Responsibility,
                    ScopeOfInspectiont = dto.ScopeOfInspectiont,
                    StatusOfTheOrder = dto.StatusOfTheOrder

                };
                Db.WorkshopBCInspection.Update(model);
                Db.SaveAsync();
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public async Task DeleteWorkshopBCIInspByIdAsync(int id)
        {
           await Db.WorkshopBCInspection.Delete(id);
        }
        #endregion

        #region  QualityReviews:
        public void Insert(QualityReview_DTO dto)
        {
            try
            {
                var model = new QualityReview
                {
                    CareComName = dto.CareComName,
                    Actions = dto.Actions,
                    Date = dto.Date,
                    Outcomes = dto.Outcomes,
                    ActionDate = dto.ActionDate,
                    BriefDescFind = dto.BriefDescFind,
                    BriefDescRecommend = dto.BriefDescRecommend,
                    Responsibility = dto.BriefDescRecommend
                };
                Db.QualityReviews.Create(model);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public IEnumerable<QualityReview_DTO> ReadQualityReviews()
        {
            var list = Db.QualityReviews.GetAll().ToList();
            var listDTO = new List<QualityReview_DTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new QualityReview_DTO());
                listDTO[i].Id = list[i].Id;
                listDTO[i].CareComName = list[i].CareComName;
                listDTO[i].Actions = list[i].Actions;
                listDTO[i].Date = list[i].Date;
                listDTO[i].Outcomes = list[i].Outcomes;
                listDTO[i].ActionDate = list[i].ActionDate;
                listDTO[i].BriefDescFind = list[i].BriefDescFind;
                listDTO[i].BriefDescRecommend = list[i].BriefDescRecommend;
                listDTO[i].Responsibility = list[i].Responsibility;
            }
            return listDTO;
        }

        public void Update(QualityReview_DTO dto)
        {
            try
            {
                var model = new QualityReview
                {
                    Id = dto.Id,
                    CareComName = dto.CareComName,
                    Actions = dto.Actions,
                    Date = dto.Date,
                    Outcomes = dto.Outcomes,
                    ActionDate = dto.ActionDate,
                    BriefDescFind = dto.BriefDescFind,
                    BriefDescRecommend = dto.BriefDescRecommend,
                    Responsibility = dto.BriefDescRecommend
                };

                Db.QualityReviews.Update(model);
                Db.SaveAsync();
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public async Task DeleteQualityReviewAsync(int id)
        {
           await Db.QualityReviews.Delete(id);
        }
        #endregion

        #region MOH Ispections:
        public void Insert(MOH_Inspection_DTO dto)
        {
            try
            {
                var model = new MOH_Inspection
                {
                    Home = dto.Home,
                    Inspection_Number = dto.Inspection_Number,
                    Last_Date_Inspection = dto.Last_Date_Inspection,
                    Location = dto.Location,
                    No_Findings = dto.No_Findings,
                    Report_Date = dto.Report_Date,
                    Type_Inspection = dto.Type_Inspection
                };
                Db.MOHInspect.Create(model);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public IEnumerable<MOH_Inspection_DTO> ReadMOHInspections()
        {
            var list = Db.MOHInspect.GetAll().ToList();
            var listDTO = new List<MOH_Inspection_DTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new MOH_Inspection_DTO());
                listDTO[i].Id = list[i].Id;
                listDTO[i].Inspection_Number = list[i].Inspection_Number;
                listDTO[i].Last_Date_Inspection = list[i].Last_Date_Inspection;
                listDTO[i].Home = list[i].Home;
                listDTO[i].Location = list[i].Location;
                listDTO[i].No_Findings = list[i].No_Findings;
                listDTO[i].Report_Date = list[i].Report_Date;
                listDTO[i].Type_Inspection = list[i].Type_Inspection;
            }
            return listDTO;
        }

        public void Update(MOH_Inspection_DTO dto)
        {
            try
            {
                var model = new MOH_Inspection
                {
                    Id = dto.Id,
                    Home = dto.Home,
                    Inspection_Number = dto.Inspection_Number,
                    Last_Date_Inspection = dto.Last_Date_Inspection,
                    Location = dto.Location,
                    No_Findings = dto.No_Findings,
                    Report_Date = dto.Report_Date,
                    Type_Inspection = dto.Type_Inspection
                };

                Db.MOHInspect.Update(model);
                Db.SaveAsync();
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public async Task DeleteMOHInspectAsync(int id)
        {
            await Db.MOHInspect.Delete(id);
        }
        #endregion

        #region Ispections Info:
        public void Insert(InspectionInfo_DTO dto)
        {
            try
            {
                var model = new InspectionInfo
                {
                    ClearedByInspectionNo = dto.ClearedByInspectionNo,
                    DirectorReferral = dto.DirectorReferral,
                    HomeId = dto.HomeId,
                    InspectNumber = dto.InspectNumber,
                    Intermediate1 = dto.Intermediate1,
                    Intermediate2 = dto.Intermediate2,
                    Intermediate3 = dto.Intermediate3,
                    Responsibility1 = dto.Responsibility1,
                    Responsibility2 = dto.Responsibility2,
                    Responsibility3 = dto.Responsibility3,
                    TargetDate1 = dto.TargetDate1,
                    TargetDate2 = dto.TargetDate2,
                    TargetDate3 = dto.TargetDate3,
                    TypeId = dto.TypeId,
                    LastDate = dto.LastDate,
                    LTCHAReg = dto.LTCHAReg,
                    NoFindings = dto.NoFindings,
                    NonCompliance = dto.NonCompliance,
                    OtherOptionAsNeeded = dto.OtherOptionAsNeeded,
                    ReportDate = dto.ReportDate,
                    Section = dto.Section,
                    Subsection = dto.Subsection
                };
                Db.InspectionInfos.Create(model);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public IEnumerable<InspectionInfo_DTO> ReadInspectionInfos()
        {
            var list = Db.InspectionInfos.GetAll().ToList();
            var listDTO = new List<InspectionInfo_DTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new InspectionInfo_DTO());
                listDTO[i].Id = list[i].Id;
                listDTO[i].ClearedByInspectionNo = list[i].ClearedByInspectionNo;
                listDTO[i].DirectorReferral = list[i].DirectorReferral;
                listDTO[i].HomeId = list[i].HomeId;
                listDTO[i].InspectNumber = list[i].InspectNumber;
                listDTO[i].Intermediate1 = list[i].Intermediate1;
                listDTO[i].Intermediate2 = list[i].Intermediate2;
                listDTO[i].Intermediate3 = list[i].Intermediate3;
                listDTO[i].Responsibility1 = list[i].Responsibility1;
                listDTO[i].Responsibility2 = list[i].Responsibility2;
                listDTO[i].Responsibility3 = list[i].Responsibility3;
                listDTO[i].TargetDate1 = list[i].TargetDate1;
                listDTO[i].TargetDate2 = list[i].TargetDate2;
                listDTO[i].TargetDate3 = list[i].TargetDate3;
                listDTO[i].TypeId = list[i].TypeId;
                listDTO[i].LastDate = list[i].LastDate;
                listDTO[i].LTCHAReg = list[i].LTCHAReg;
                listDTO[i].NonCompliance = list[i].NonCompliance;
                listDTO[i].OtherOptionAsNeeded = list[i].OtherOptionAsNeeded;
                listDTO[i].ReportDate = list[i].ReportDate;
                listDTO[i].NoFindings = list[i].NoFindings;
                listDTO[i].Section = list[i].Section;
                listDTO[i].Subsection = list[i].Subsection;
            }
            return listDTO;
        }

        public void Update(InspectionInfo_DTO dto)
        {
            try
            {
                var model = new InspectionInfo
                {
                    Id = dto.Id,
                    ClearedByInspectionNo = dto.ClearedByInspectionNo,
                    DirectorReferral = dto.DirectorReferral,
                    HomeId = dto.HomeId,
                    InspectNumber = dto.InspectNumber,
                    Intermediate1 = dto.Intermediate1,
                    Intermediate2 = dto.Intermediate2,
                    Intermediate3 = dto.Intermediate3,
                    Responsibility1 = dto.Responsibility1,
                    Responsibility2 = dto.Responsibility2,
                    Responsibility3 = dto.Responsibility3,
                    TargetDate1 = dto.TargetDate1,
                    TargetDate2 = dto.TargetDate2,
                    TargetDate3 = dto.TargetDate3,
                    TypeId = dto.TypeId,
                    LastDate = dto.LastDate,
                    LTCHAReg = dto.LTCHAReg,
                    NoFindings = dto.NoFindings,
                    NonCompliance = dto.NonCompliance,
                    OtherOptionAsNeeded = dto.OtherOptionAsNeeded,
                    ReportDate = dto.ReportDate,
                    Section = dto.Section,
                    Subsection = dto.Subsection
                };

                Db.InspectionInfos.Update(model);
                Db.SaveAsync();
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public async Task DeleteInspectionInfoAsync(int id)
        {
            await Db.InspectionInfos.Delete(id);
        }
        #endregion

        #region Ispections Type:
        public void Insert(InspectType_DTO dto)
        {
            try
            {
                var model = new InspectType
                {
                    Name = dto.Name
                };
                Db.InspectTypes.Create(model);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public IEnumerable<InspectType_DTO> ReadInspectTypes()
        {
            var list = Db.InspectTypes.GetAll().ToList();
            var listDTO = new List<InspectType_DTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new InspectType_DTO());
                listDTO[i].Id = list[i].Id;
                listDTO[i].Name = list[i].Name;               
            }
            return listDTO;
        }

        public async Task<IEnumerable<InspectType_DTO>> ReadInspectTypesAsync()
        {
            var l = await Db.InspectTypes.GetAllAsync();
            var list = l.ToList();
            var listDTO = new List<InspectType_DTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new InspectType_DTO());
                listDTO[i].Id = list[i].Id;
                listDTO[i].Name = list[i].Name;
            }
            return listDTO;
        }

        public void Update(InspectType_DTO dto)
        {
            try
            {
                var model = new InspectType
                {
                    Id = dto.Id,
                    Name = dto.Name
                };

                Db.InspectTypes.Update(model);
                Db.SaveAsync();
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public async Task DeleteInspectTypeAsync(int id)
        {
            await Db.InspectTypes.Delete(id);
        }
        #endregion

        #region Section:
        public void Insert(Section_DTO dto)
        {
            try
            {
                var model = new Section
                {
                    Name = dto.Name,
                    LTCHARegId = dto.LTCHARegId
                };
                Db.Section.Create(model);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public IEnumerable<Section_DTO> ReadSections()
        {
            var list = Db.Section.GetAll().ToList();
            var listDTO = new List<Section_DTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new Section_DTO());
                listDTO[i].Id = list[i].Id;
                listDTO[i].Name = list[i].Name;
                listDTO[i].LTCHARegId = list[i].LTCHARegId;
            }
            return listDTO;
        }

        public void Update(Section_DTO dto)
        {
            try
            {
                var model = new Section
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    LTCHARegId = dto.LTCHARegId
                };

                Db.Section.Update(model);
                Db.SaveAsync();
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public async Task DeleteSectionAsync(int id)
        {
            await Db.InspectTypes.Delete(id);
        }
        #endregion

        #region LTCHAReg:
        public void Insert(LTCHAReg_DTO dto)
        {
            try
            {
                var model = new LTCHAReg
                {
                    Name = dto.Name
                };
                Db.LTCHARegs.Create(model);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public IEnumerable<LTCHAReg_DTO> ReadLTCHARegs()
        {
            var list = Db.LTCHARegs.GetAll().ToList();
            var listDTO = new List<LTCHAReg_DTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new LTCHAReg_DTO());
                listDTO[i].Id = list[i].Id;
                listDTO[i].Name = list[i].Name;
            }
            return listDTO;
        }

        public void Update(LTCHAReg_DTO dto)
        {
            try
            {
                var model = new LTCHAReg
                {
                    Id = dto.Id,
                    Name = dto.Name
                };

                Db.LTCHARegs.Update(model);
                Db.SaveAsync();
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public async Task DeleteLTCHARegAsync(int id)
        {
            await Db.LTCHARegs.Delete(id);
        }
        #endregion

        #region Non Complence:
        public void Insert(NonCompleance_DTO dto)
        {
            try
            {
                var model = new NonCompleance
                {
                    Name = dto.Name
                };
                Db.NonCompleances.Create(model);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public IEnumerable<NonCompleance_DTO> ReadNonCompleances()
        {
            var list = Db.NonCompleances.GetAll().ToList();
            var listDTO = new List<NonCompleance_DTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new NonCompleance_DTO());
                listDTO[i].Id = list[i].Id;
                listDTO[i].Name = list[i].Name;
            }
            return listDTO;
        }

        public void Update(NonCompleance_DTO dto)
        {
            try
            {
                var model = new NonCompleance
                {
                    Id = dto.Id,
                    Name = dto.Name
                };

                Db.NonCompleances.Update(model);
                Db.SaveAsync();
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public async Task DeleteNonCompleanceAsync(int id)
        {
            await Db.LTCHARegs.Delete(id);
        }
        #endregion

        #region Subsection:
        public void Insert(Subsection_DTO dto)
        {
            try
            {
                var model = new Subsection
                {
                    Name = dto.Name,
                    SectionId = dto.SectionId
                };
                Db.Subsections.Create(model);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public IEnumerable<Subsection_DTO> ReadSubsections()
        {
            var list = Db.Subsections.GetAll().ToList();
            var listDTO = new List<Subsection_DTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new Subsection_DTO());
                listDTO[i].Id = list[i].Id;
                listDTO[i].Name = list[i].Name;
                listDTO[i].SectionId = list[i].SectionId;
            }
            return listDTO;
        }

        public void Update(Subsection_DTO dto)
        {
            try
            {
                var model = new Subsection
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    SectionId = dto.SectionId
                };

                Db.Subsections.Update(model);
                Db.SaveAsync();
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public async Task DeleteSubsectionAsync(int id)
        {
            await Db.Subsections.Delete(id);
        }
        #endregion

        #region Other Option:
        public void Insert(OtherOption_DTO dto)
        {
            try
            {
                var model = new OtherOption
                {
                    Name = dto.Name,
                    SubsectionId = dto.SubsectionId
                };
                Db.OtherOptions.Create(model);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public IEnumerable<OtherOption_DTO> ReadOtherOptions()
        {
            var list = Db.OtherOptions.GetAll().ToList();
            var listDTO = new List<OtherOption_DTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new OtherOption_DTO());
                listDTO[i].Id = list[i].Id;
                listDTO[i].Name = list[i].Name;
                listDTO[i].SubsectionId = list[i].SubsectionId;
            }
            return listDTO;
        }

        public void Update(OtherOption_DTO dto)
        {
            try
            {
                var model = new OtherOption
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    SubsectionId = dto.SubsectionId
                };

                Db.OtherOptions.Update(model);
                Db.SaveAsync();
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public async Task DeleteSOtherOptionAsync(int id)
        {
            await Db.OtherOptions.Delete(id);
        }
        #endregion

        #region ZTest:
        public void Insert(ZTest_DTO dto)
        {
            try
            {
                var model = new ZTest
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Date_Creation = dto.Date_Creation,     
                };
                Db.ZTest.Create(model);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public IEnumerable<ZTest_DTO> ReadZTest()
        {
            var list = Db.ZTest.GetAll().ToList();
            var listDTO = new List<ZTest_DTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new ZTest_DTO());
                listDTO[i].Id = list[i].Id;
                listDTO[i].Name = list[i].Name;
                listDTO[i].Date_Creation = list[i].Date_Creation;
                
            }
            return listDTO;
        }

        
        public async Task DeleteZTestByIdAsync(int id)
        {
            await Db.LicensInspect.Delete(id);
        }

        public void DeleteZTest(int id)
        {
            try
            {
                Db.LicensInspect.Delete(id);
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public void Update(ZTest_DTO dto)
        {
            try
            {
                var model = new ZTest
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Date_Creation = dto.Date_Creation,                
                };
                Db.ZTest.Update(model);
                Db.SaveAsync();
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }
        #endregion

        #region Login Sessions:
        public void Insert(LoginSession_DTO dto)
        {
            try
            {
                var model = new LoginSession
                {
                    UserName = dto.UserName,
                    Email = dto.Email,
                    DateOfEntry = dto.DateOfEntry,
                    Location = dto.Location,
                    SessionId = dto.SessionId
                };
                Db.LoginSessions.Create(model);

            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public IEnumerable<LoginSession_DTO> ReadLogSessions()
        {
            var list = Db.LoginSessions.GetAll().ToList();
            var listDTO = new List<LoginSession_DTO>();
            for (int i = 0; i < list.Count; i++)
            {
                listDTO.Add(new LoginSession_DTO());
                listDTO[i].Id = list[i].Id;
                listDTO[i].UserName = list[i].UserName;
                listDTO[i].Email = list[i].Email;
                listDTO[i].DateOfEntry = list[i].DateOfEntry;
                listDTO[i].Location = list[i].Location;
                listDTO[i].SessionId = list[i].SessionId;
            }
            return listDTO;
        }


        public async Task DeleteLogSessionByIdAsync(int id)
        {
            await Db.LoginSessions.Delete(id);
        }

        public void DeleteLogSession(int id)
        {
            try
            {
                Db.LoginSessions.Delete(id);
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        public void Update(LoginSession_DTO dto)
        {
            try
            {
                var model = new LoginSession
                {
                    Id = dto.Id,
                    UserName = dto.UserName,
                    Email = dto.Email,
                    DateOfEntry = dto.DateOfEntry,
                    Location = dto.Location,
                    SessionId = dto.SessionId
                };
                Db.LoginSessions.Update(model);
                Db.SaveAsync();
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }
        #endregion
    }
}
