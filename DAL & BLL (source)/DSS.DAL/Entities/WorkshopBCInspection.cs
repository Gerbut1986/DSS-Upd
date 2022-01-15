namespace DSS.DAL.Entities
{
    public class WorkshopBCInspection
    {
        public int Id { get; set; }
        public int CareComName { get; set; }
        public System.DateTime Date { get; set; }
        public string InspecteReport { get; set; }
        public string ScopeOfInspectiont { get; set; }
        public bool NoOrders { get; set; }
        public string Orders { get; set; }
        public string BriefDescFind { get; set; }
        public string ActionPlan { get; set; }
        public string Responsibility { get; set; }
        public System.DateTime? ActionDate { get; set; } = null;
        public string StatusOfTheOrder { get; set; }
    }
}
