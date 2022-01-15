namespace DSS.BLL.DTO
{
    using System.Linq;
    using System.ComponentModel.DataAnnotations;

    public class Complaint_DTO : Interfaces.IModel
    {
        string[] locNames;
        public Complaint_DTO() => locNames = STREAM.GetLocNames().ToArray();
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required! Please fill it in.")]
        [DataType(DataType.Date)]
        public System.DateTime? DateReceived { get; set; }
        [Required(ErrorMessage = "This field is required! Please fill it in.")]
        public int Location { get; set; }
        [Required(ErrorMessage = "This field is required! Please fill it in.")]
        public string WritenOrVerbal { get; set; }
        [Required(ErrorMessage = "This field is required! Please fill it in.")]
        public string Receive_Directly { get; set; }
        [Required(ErrorMessage = "This field is required! Please fill it in.")]
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
        [Required(ErrorMessage = "This field is required! Please fill it in.")]
        public string MOHLTCNotified { get; set; }
        public string CopyToVP { get; set; }
        public string ResponseSent { get; set; }
        public string ActionToken { get; set; }
        public string Resolved { get; set; }
        public string MinistryVisit { get; set; }
         public override string ToString()
        {
            return $"{DateReceived},{locNames[Location - 1]},{WritenOrVerbal},{Receive_Directly},{FromResident},{ResidentName},{Department},{HomeArea},{BriefDescription}," +
                        $"{IsAdministration},{CareServices},{PalliativeCare},{Dietary},{Housekeeping},{Laundry}," +
                        $"{Maintenance},{Programs},{Admissions},{Physician},{Other},{MOHLTCNotified},{CopyToVP},{ResponseSent}," +
                        $"{ActionToken},{Resolved},{MinistryVisit}";
        }
    }
}