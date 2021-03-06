namespace DSS.DAL.Entities
{
    public class Privacy_Breaches
    {
        public int Id { get; set; }
        public int Location { get; set; }
        public string Status { get; set; }
        public System.DateTime Date_Breach_Occurred { get; set; }
        public string Description_Outcome { get; set; }
        public System.DateTime? Date_Breach_Reported { get; set; } = null;
        public string Date_Breach_Reported_By { get; set; }
        public string Type_of_Breach { get; set; }
        public string Type_of_PHI_Involved { get; set; }
        public int Number_of_Individuals_Affected { get; set; }
        public string Risk_Level { get; set; }
    }
}