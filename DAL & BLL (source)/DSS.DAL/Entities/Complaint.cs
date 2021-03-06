namespace DSS.DAL.Entities
{
    public class Complaint
    {
        public int Id { get; set; }
        public System.DateTime? DateReceived { get; set; }
        public int Location { get; set; }
        public string WritenOrVerbal { get; set; }
        public string Receive_Directly { get; set; }
        public string FromResident { get; set; }
        public string ResidentName { get; set; }
        public string Department { get; set; }
        public string HomeArea { get; set; }
        public string BriefDescription { get; set; }
        public bool IsAdministration { get; set; }
        public bool CareServices { get; set; }
        public bool PalliativeCare { get; set; }
        public bool Dietary { get; set; }
        public bool Housekeeping { get; set; }
        public bool Laundry { get; set; }
        public bool Maintenance { get; set; }
        public bool Programs { get; set; }
        public bool Admissions { get; set; }
        public bool Physician { get; set; }
        public bool Other { get; set; }
        public string MOHLTCNotified { get; set; }
        public string CopyToVP { get; set; }
        public string ResponseSent { get; set; }
        public string ActionToken { get; set; }
        public string Resolved { get; set; }
        public string MinistryVisit { get; set; }
    }
}