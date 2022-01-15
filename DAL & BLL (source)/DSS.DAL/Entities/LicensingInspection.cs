namespace DSS.DAL.Entities
{
    public class LicensingInspection
    {
        public int Id { get; set; }
        public int CareComName { get; set; }
        public System.DateTime Date { get; set; }
        public string InspectComplaint { get; set; }
        public string InspectTypeReason { get; set; }
        public bool NoFinding { get; set; }
        public string Contraventions { get; set; }
        public string CommCareLivAct { get; set; }
        public string ResidentCareRegSec { get; set; }
        public string ResidentCareRegSub { get; set; }
        public string BriefDescription { get; set; }
        public string ActionPlan { get; set; }
        public string Responsibility { get; set; }
        public System.DateTime? ActionDate { get; set; } = null;
    }
}
