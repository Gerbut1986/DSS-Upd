namespace DSS.BLL.DTO
{
    using System.Linq;
    using System.ComponentModel.DataAnnotations;

    public class Privacy_Breaches_DTO : Interfaces.IModel
    {
        string[] locNames;
        public Privacy_Breaches_DTO() => locNames = STREAM.GetLocNames().ToArray();
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required! Please fill it in.")]   
        [DataType(DataType.Text)]
        public int Location { get; set; }
        public string Status { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "This field is required! Please fill it in.")]
        public System.DateTime Date_Breach_Occurred { get; set; }
        [DataType(DataType.Date)]
        public System.DateTime? Date_Breach_Reported { get; set; } = null;
        public string Date_Breach_Reported_By { get; set; } 
        public string Type_of_Breach { get; set; }
        public string Type_of_PHI_Involved { get; set; }
        public int Number_of_Individuals_Affected { get; set; }
        public string Description_Outcome { get; set; }
        public string Risk_Level { get; set; }
        public override string ToString()
        {
            return $"{locNames[Location - 1]},{Status},{Date_Breach_Occurred},{Date_Breach_Reported},{Date_Breach_Reported_By},{Type_of_Breach},{Type_of_PHI_Involved},{Number_of_Individuals_Affected}" +
                $",{Description_Outcome},{Risk_Level}";
        }
    }
}