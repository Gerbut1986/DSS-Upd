namespace DSS.BLL.PartialModels
{
    public class PComplaintsSummary
    {
        public string LocationName { get; set; }
        public string Status { get; set; }
        public string Complain_Filed_By { get; set; }
        public string Type_of_Complaint { get; set; }
        public string Is_Complaint_Resolved { get; set; }
        public string Description_Outcome { get; set; }
    }

    public class PComplaintsSummaryAll
    {
        public string LocationName { get; set; }
        public int Status { get; set; }
        public int Complain_Filed_By { get; set; }
        public int Type_of_Complaint { get; set; }
        public int Is_Complaint_Resolved { get; set; }
        public int Description_Outcome { get; set; }
    }
}
