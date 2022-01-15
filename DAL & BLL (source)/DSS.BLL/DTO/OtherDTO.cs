namespace DSS.BLL.DTO
{
    using System.ComponentModel.DataAnnotations;

    public class OtherDTO : Interfaces.IModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required! Please try to fill it in")]
        [DataType(DataType.Date)]
        public System.DateTime Date { get; set; }
        [Required(ErrorMessage = "This field is required! Please try to fill it in")]
        public int Location { get; set; }
        public string Inspected_By { get; set; }
        public string Inspection_Number { get; set; }
        public string Number_of_Violations { get; set; }
        public string Notes_Comments { get; set; }
    }
}
