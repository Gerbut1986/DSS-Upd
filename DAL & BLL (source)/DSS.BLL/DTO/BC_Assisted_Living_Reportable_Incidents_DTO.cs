namespace DSS.BLL.DTO
{
    using System.Linq;
    using System.ComponentModel.DataAnnotations;

    public class BC_Assisted_Living_Reportable_Incidents_DTO : Interfaces.IModel
    {
        string[] locNames;
        public BC_Assisted_Living_Reportable_Incidents_DTO() => locNames = STREAM.GetLocNames().ToArray();
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required! Please fill it in.")]
        public int NameCareCommu { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "This field is required! Please fill it in.")]
        public System.DateTime DateIncident { get; set; }
        public string IncidentType { get; set; }
        public string BriefDescrincident { get; set; }
        public string BriefDescrTaken { get; set; }
        public string Notifications { get; set; }
        public override string ToString() =>
        $"{locNames[NameCareCommu - 1]},{DateIncident},{IncidentType},{BriefDescrincident},{BriefDescrTaken}," +
            $"{Notifications}";
    }
}
