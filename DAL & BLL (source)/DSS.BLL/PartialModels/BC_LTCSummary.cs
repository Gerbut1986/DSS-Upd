namespace DSS.BLL.PartialModels
{
    public class BC_LTCSummary
    {
        public string CareComName { get; set; }
        public string IncidentType { get; set; }
        public string BriefDescIncid { get; set; }
        public string BriefDescTaken { get; set; }
        public string Notifications { get; set; }
    }

    public class BC_LTCSummaryAll
    {
        public int CareComName { get; set; }
        public int IncidentType { get; set; }
        public int BriefDescIncid { get; set; }
        public int BriefDescTaken { get; set; }
        public int Notifications { get; set; }
    }
}
