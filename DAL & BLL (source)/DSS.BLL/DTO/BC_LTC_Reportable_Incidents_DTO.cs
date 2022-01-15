namespace DSS.BLL.DTO
{
    using System.Linq;
    using System.ComponentModel.DataAnnotations;

    public class BC_LTC_Reportable_Incidents_DTO : Interfaces.IModel
    {
        string[] locNames;
        public BC_LTC_Reportable_Incidents_DTO() => locNames = STREAM.GetLocNames().ToArray();
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required! Please fill it in.")]
        public int CareCommName { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "This field is required! Please fill it in.")]
        public System.DateTime DateIncident { get; set; }
        public string IncidentType { get; set; }
        public string BriefDescIncid { get; set; }
        public string BriefDescTaken { get; set; }
        public string Notifications { get; set; }
        public override string ToString() =>
   $"{locNames[CareCommName - 1]},{DateIncident},{IncidentType},{BriefDescIncid},{BriefDescTaken}," +
       $"{Notifications}";
    }
}
