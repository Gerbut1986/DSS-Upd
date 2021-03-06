namespace DSS.DAL.Entities
{
    using System;

    public class WSIB
    {
        public int Id { get; set; }
        public int Location { get; set; }
        public DateTime Date_Accident { get; set; }
        public string Employee_Initials { get; set; }
        public string Accident_Cause { get; set; }
        public DateTime? Date_Duties { get; set; } = null;
        public DateTime? Date_Regular { get; set; } = null;
        public int Lost_Days { get; set; }
        public int Modified_Days_Not_Shadowed { get; set; }
        public int Modified_Days_Shadowed { get; set; }
        public string Form_7 { get; set; }
    }
}