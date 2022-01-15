namespace DSS.DAL.Entities
{
    public class BC_Assisted_Living_Reportable_Incidents
    {
        public int Id { get; set; }
        public int NameCareCommu { get; set; }
        public System.DateTime DateIncident { get; set; }
        public string IncidentType { get; set; }
        public string BriefDescrincident { get; set; }
        public string BriefDescrTaken { get; set; }
        public string Notifications { get; set; }
    }
}
