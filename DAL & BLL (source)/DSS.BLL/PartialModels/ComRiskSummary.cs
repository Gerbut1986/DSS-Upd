namespace DSS.BLL.PartialModels
{
    public class ComRiskSummary
    {
        public string LocationName { get; set; }
        public string Type_Of_Risk { get; set; }
        public string Descriptions { get; set; }
        public string Potential_Risk { get; set; }
        public string MOH_Visit { get; set; }
        public string Risk_Legal_Action { get; set; }
        public string Hot_Alert { get; set; }
        public string Status_Update { get; set; }
        public string Resolved { get; set; }
    }

    public class ComRiskSummaryAll
    {
        public int LocationName { get; set; }
        public int Type_Of_Risk { get; set; }
        public int Descriptions { get; set; }
        public int Potential_Risk { get; set; }
        public int MOH_Visit { get; set; }
        public int Risk_Legal_Action { get; set; }
        public int Hot_Alert { get; set; }
        public int Status_Update { get; set; }
        public int Resolved { get; set; }
    }
}
