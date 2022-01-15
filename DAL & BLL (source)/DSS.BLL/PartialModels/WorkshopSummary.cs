namespace DSS.BLL.PartialModels
{
    public class WorkshopSummary
    {
        public string CareComName { get; set; }
        public string InspecteReport { get; set; }
        public string ScopeOfInspectiont { get; set; }
        public string NoOrders { get; set; }
        public string Orders { get; set; }
        public string BriefDescFind { get; set; }
        public string ActionPlan { get; set; }
        public string Responsibility { get; set; }
        public string StatusOfTheOrder { get; set; }
    }

    public class WorkshopSummaryAll
    {
        public int CareComName { get; set; }
        public int InspecteReport { get; set; }
        public int ScopeOfInspectiont { get; set; }
        public int NoOrders { get; set; }
        public int Orders { get; set; }
        public int BriefDescFind { get; set; }
        public int ActionPlan { get; set; }
        public int Responsibility { get; set; }
        public int StatusOfTheOrder { get; set; }
    }
}
