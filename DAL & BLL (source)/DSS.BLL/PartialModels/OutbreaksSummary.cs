namespace DSS.BLL.PartialModels
{
    public class OutbreaksSummary
    {        
        public string LocationName { get; set; }
        public string Type_of_Outbreak { get; set; }
        public string Total_Days_Closed { get; set; }
        public string Total_Residents_Affected { get; set; }
        public string Total_Staff_Affected { get; set; }
        public string Strain_Identified { get; set; }
        public string Deaths_Due { get; set; }
        public string CI_Report_Submitted { get; set; }
        public string Notify_MOL { get; set; }
        public string Credit_for_Lost_Days { get; set; }
        public string Tracking_Sheet_Completed { get; set; }
        public string Docs_Submitted_Finance { get; set; }
    }

    public class OutbreaksSummaryAll
    {
        public int Type_of_Outbreak { get; set; }
        public int Total_Days_Closed { get; set; }
        public int Total_Residents_Affected { get; set; }
        public int Total_Staff_Affected { get; set; }
        public int Strain_Identified { get; set; }
        public int Deaths_Due { get; set; }
        public int CI_Report_Submitted { get; set; }
        public int Notify_MOL { get; set; }
        public int Credit_for_Lost_Days { get; set; }
        public int Tracking_Sheet_Completed { get; set; }
        public int Docs_Submitted_Finance { get; set; }
    }
}
