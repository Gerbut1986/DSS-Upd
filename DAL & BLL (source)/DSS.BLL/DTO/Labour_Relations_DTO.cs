namespace DSS.BLL.DTO
{
    using System.Linq;
    using System.ComponentModel.DataAnnotations;

    public class Labour_Relations_DTO : Interfaces.IModel
    {
        string[] locNames;
        public Labour_Relations_DTO() => locNames = STREAM.GetLocNames().ToArray();

        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required! Please fill it in.")]
        public int Location { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "This field is required! Please fill it in.")]
        public System.DateTime Date { get; set; }
        public string Union { get; set; }
        public string Category { get; set; }
        public string Details { get; set; }
        public string Status { get; set; }
        public string Accruals { get; set; }
        public string Outcome { get; set; }
        public string Lessons_Learned { get; set; }
        public override string ToString()
        {
            return $"{locNames[Location - 1]},{Date},{Union},{Category},{Details}," +
                        $"{Status},{Accruals},{Outcome},{Lessons_Learned}";
        }
    }
}