namespace DSS.BLL.DTO
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;

    public class WSIB_DTO : Interfaces.IModel
    {
        string[] locNames;
        public WSIB_DTO() => locNames = STREAM.GetLocNames().ToArray();
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required. Please fill it in. ")]
        public int Location { get; set; }
        [Required(ErrorMessage = "This field is required. Please fill it in. ")]
        [DataType(DataType.Date)]
        public DateTime Date_Accident { get; set; }
        public string Employee_Initials { get; set; }
        public string Accident_Cause { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Date_Duties { get; set; } = null;
        [DataType(DataType.Date)]
        public DateTime? Date_Regular { get; set; } = null;
        public int Lost_Days { get; set; }
        public int Modified_Days_Not_Shadowed { get; set; }
        public int Modified_Days_Shadowed { get; set; }
        public string Form_7 { get; set; }
        public override string ToString()
        {
            return $"{locNames[Location - 1]},{Date_Accident},{Employee_Initials},{Accident_Cause},{Date_Duties},{Date_Regular},{Lost_Days}," +
                $"{Modified_Days_Not_Shadowed}," +
                $"{Modified_Days_Shadowed},{Form_7}";
        }
    }
}