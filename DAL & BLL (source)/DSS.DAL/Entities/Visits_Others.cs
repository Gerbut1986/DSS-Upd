namespace DSS.DAL.Entities
{
    public class Visits_Others
    {
        public int Id { get; set; }
        public System.DateTime Date_of_Visit { get; set; }
        public int Location { get; set; }
        public string Agency { get; set; }
        public string Number_of_Findings { get; set; }
        public string Details_of_Findings { get; set; }
        public string Corrective_Actions { get; set; }
        public string Report_Posted { get; set; }
        public string LHIN_Letter_Received { get; set; }
        public string PH_Letter_Received { get; set; }
    }
}