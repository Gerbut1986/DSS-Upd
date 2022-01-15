namespace DSS.BLL.DTO
{
    using DSS.BLL;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;

    public class Critical_Incidents_DTO : Interfaces.IModel, System.IEquatable<Critical_Incidents_DTO>
    {
        string[] locNames, ciNames;
        public Critical_Incidents_DTO()
        {
            locNames = STREAM.GetLocNames().ToArray();
            ciNames = STREAM.GetCINames().ToArray();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required! Please fill it in.")]
        [DataType(DataType.Date)]
        public System.DateTime Date { get; set; }
        [Required(ErrorMessage = "This field is required! Please fill it in.")]
        public string CI_Form_Number { get; set; }
        [Required(ErrorMessage = "This field is required! Please fill it in.")]
        public int CI_Category_Type { get; set; }
        [Required(ErrorMessage = "This field is required! Please fill it in.")]
        public int Location { get; set; }
        [Required(ErrorMessage = "This field is required! Please fill it in.")]
        public string Brief_Description { get; set; }       
        public string MOH_Notified { get; set; }
        public string Police_Notified { get; set; }
        public string POAS_Notified { get; set; }
        public string Care_Plan_Updated { get; set; }
        public string Quality_Improvement_Actions { get; set; }
        public string MOHLTC_Follow_Up { get; set; }
        public string CIS_Initiated { get; set; }
        public string Follow_Up_Amendments { get; set; }
        public string Risk_Locked { get; set; }
        public string File_Complete { get; set; }

        public bool Equals(Critical_Incidents_DTO other)
        {
            if (this.Location == other.Location) return true;
            else return false;
        }

        public override string ToString()
        {
            return $"{Date},{CI_Form_Number},{ciNames[CI_Category_Type - 1]},{locNames[Location - 1]},{Brief_Description},{MOH_Notified}," +
                        $"{Police_Notified},{POAS_Notified},{Care_Plan_Updated}," +
                        $"{Quality_Improvement_Actions},{MOHLTC_Follow_Up}," +
                        $"{CIS_Initiated},{Follow_Up_Amendments},{Risk_Locked},{File_Complete}";
        }
    }
}
