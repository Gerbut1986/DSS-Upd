namespace DSS.BLL.DTO
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class MOH_Inspection_DTO : Interfaces.IModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required! Please fill it in.")]
        public int Location { get; set; }
        public string Inspection_Number { get; set; }
        [Required(ErrorMessage = "This field is required! Please fill it in.")]
        [DataType(DataType.Date)]
        public DateTime Report_Date { get; set; }
        public string Type_Inspection { get; set; }
        public DateTime Last_Date_Inspection { get; set; }
        public string Home { get; set; }
        public bool No_Findings { get; set; }
    }
}
