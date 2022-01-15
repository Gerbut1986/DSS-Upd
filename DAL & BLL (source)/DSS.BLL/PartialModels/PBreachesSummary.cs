namespace DSS.BLL.PartialModels
{
    public class PBreachesSummary
    {
        public string LocationName { get; set; }
        public string Status { get; set; }
        public string Description_Outcome { get; set; }
        public string Date_Breach_Reported_By { get; set; }
        public string Type_of_Breach { get; set; }
        public string Type_of_PHI_Involved { get; set; }
        public string Number_of_Individuals_Affected { get; set; }
        public string Risk_Level { get; set; }
    }

    public class PBreachesSummaryAll
    {
        public int LocationName { get; set; }
        public int Status { get; set; }
        public int Description_Outcome { get; set; }
        public int Date_Breach_Reported_By { get; set; }
        public int Type_of_Breach { get; set; }
        public int Type_of_PHI_Involved { get; set; }
        public int Number_of_Individuals_Affected { get; set; }
        public int Risk_Level { get; set; }
    }
}
