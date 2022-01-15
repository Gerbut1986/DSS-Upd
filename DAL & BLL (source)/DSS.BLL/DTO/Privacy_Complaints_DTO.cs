namespace DSS.BLL.DTO
{
    using System.Linq;
    using System.ComponentModel.DataAnnotations;

    public class Privacy_Complaints_DTO : Interfaces.IModel
    {
        string[] locNames;
        public Privacy_Complaints_DTO() => locNames = STREAM.GetLocNames().ToArray();
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required! Please fill it in.")]
        public int Location { get; set; }
        public string Status { get; set; }
        [Required(ErrorMessage = "This field is required! Please fill it in.")]
        [DataType(DataType.Date)]
        public System.DateTime Date_Complain_Received { get; set; }
        public string Complain_Filed_By { get; set; }
        public string Type_of_Complaint { get; set; }
        public string Is_Complaint_Resolved { get; set; }
        public string Description_Outcome { get; set; }
        public override string ToString()
        {
            return $"{locNames[Location - 1]},{Status},{Date_Complain_Received},{Complain_Filed_By}," +
                $"{Type_of_Complaint},{Is_Complaint_Resolved},{Description_Outcome}";
        }
    }
}