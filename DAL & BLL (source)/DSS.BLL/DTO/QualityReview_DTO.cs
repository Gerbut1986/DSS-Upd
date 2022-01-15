namespace DSS.BLL.DTO
{
    using System.Linq;
    using System.ComponentModel.DataAnnotations;

    public class QualityReview_DTO : Interfaces.IModel
    {
        readonly string[] locNames;
        public QualityReview_DTO() => locNames = STREAM.GetLocNames().ToArray();

        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required! Please fill it in.")]
        public int CareComName { get; set; }
        [Required(ErrorMessage = "This field is required! Please fill it in.")]
        [DataType(DataType.Date)]
        public System.DateTime Date { get; set; }
        public string Outcomes { get; set; }
        public string BriefDescFind { get; set; }
        public string BriefDescRecommend { get; set; }
        public string Actions { get; set; }
        public string Responsibility  { get; set; }
        [DataType(DataType.Date)]
        public System.DateTime? ActionDate { get; set; } = null;
        public override string ToString() =>
         $"{locNames[CareComName - 1]},{Date},{Outcomes},{BriefDescFind},{BriefDescRecommend},{Actions},{Responsibility}," +
            $"{ActionDate}";
    }
}
