namespace DSS.DAL.Entities
{
    public class Visits_Agency
    {
        public int Id { get; set; }
        public System.DateTime Date_of_Visit { get; set; }
        public int Location { get; set; }
        public string Agency { get; set; }
        public string Findings_number { get; set; }
        public string Findings_Details { get; set; }
        public string Corrective_Actions { get; set; }
        public string Report_Posted { get; set; }
    }
}