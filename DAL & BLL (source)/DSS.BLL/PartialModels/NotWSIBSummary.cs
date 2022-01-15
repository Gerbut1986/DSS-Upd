namespace DSS.BLL.PartialModels
{
    public class NotWSIBSummary
    {
        public string LocationName { get; set; }
        public string Employee_Initials { get; set; }
        public string Position { get; set; }
        public string Time_of_Incident { get; set; }
        public string Shift { get; set; }
        public string Home_Area { get; set; }
        public string Injury_Related { get; set; }
        public string Type_of_Injury { get; set; }
        public string Details_of_Incident { get; set; }
    }

    public class NotWSIBSummaryAll
    {
        public int Employee_Initials { get; set; }
        public int Position { get; set; }
        public int Time_of_Incident { get; set; }
        public int Shift { get; set; }
        public int Home_Area { get; set; }
        public int Injury_Related { get; set; }
        public int Type_of_Injury { get; set; }
        public int Details_of_Incident { get; set; }
    }
}
