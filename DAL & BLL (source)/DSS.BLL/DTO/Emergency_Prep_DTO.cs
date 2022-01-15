namespace DSS.BLL.DTO
{
    using System.Linq;
    using System.ComponentModel.DataAnnotations;

    public class Emergency_Prep_DTO : Interfaces.IModel
    {
        string[] locNames;
        public Emergency_Prep_DTO() => locNames = STREAM.GetLocNames().ToArray();
        public int Id { get; set; }
        [Required(ErrorMessage ="This is a required field. Please fill it in.")]
        public int Location { get; set; }
        public string Code { get; set; }
        public string Exercise { get; set; }
        public string Method { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "This field is required! Please fill it in.")]
        public System.DateTime Date { get; set; }
       public override string ToString() => $"{locNames[Location - 1]},{Code},{Exercise},{Method},{Date}";
    }
}