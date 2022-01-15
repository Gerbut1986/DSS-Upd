namespace DSS.DAL.Entities
{
    using System;

    public class MOH_Inspection
    {
        public int Id { get; set; }
        public int Location { get; set; }
        public string Inspection_Number { get; set; }
        public DateTime Report_Date { get; set; }
        public string Type_Inspection { get; set; }
        public DateTime Last_Date_Inspection { get; set; }
        public string Home { get; set; }
        public bool No_Findings { get; set; }
    }
}
