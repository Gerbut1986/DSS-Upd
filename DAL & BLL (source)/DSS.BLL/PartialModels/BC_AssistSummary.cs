namespace DSS.BLL.PartialModels
{
    public class BC_AssistSummary
    {
        public string CareComName { get; set; }
        public string IncidentType { get; set; }
        public string BriefDescrincident { get; set; }
        public string BriefDescrTaken { get; set; }
        public string Notifications { get; set; }
    }

    public class BC_AssistSummaryAll
    {
        public int CareComName { get; set; }
        public int IncidentType { get; set; }
        public int BriefDescrincident { get; set; }
        public int BriefDescrTaken { get; set; }
        public int Notifications { get; set; }
    }
}
