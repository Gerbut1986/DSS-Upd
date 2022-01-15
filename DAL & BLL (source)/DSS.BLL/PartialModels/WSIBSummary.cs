namespace DSS.BLL.PartialModels
{
    public class WSIBSummary
    {
        public string LocationName { get; set; }
        public string Employee_Initials { get; set; }
        public string Accident_Cause { get; set; }
        public string Lost_Days { get; set; }
        public string Modified_Days_Not_Shadowed { get; set; }
        public string Modified_Days_Shadowed { get; set; }
        public string Form_7 { get; set; }
    }

    public class WSIBSummaryAll
    {
        public int LocationName { get; set; }
        public int Employee_Initials { get; set; }
        public int Accident_Cause { get; set; }
        public int Lost_Days { get; set; }
        public int Modified_Days_Not_Shadowed { get; set; }
        public int Modified_Days_Shadowed { get; set; }
        public int Form_7 { get; set; }
    }
}
