namespace DSS.DAL.Entities
{
    public class BC_LTC_Reportable_Incidents 
    {
        public int Id { get; set; }
        public int CareCommName { get; set; }
        public System.DateTime DateIncident { get; set; }
        public string IncidentType { get; set; }
        public string BriefDescIncid { get; set; }
        public string BriefDescTaken { get; set; }
        public string Notifications { get; set; }
    }
}
