namespace DSS.BLL.DTO
{
    using System.Linq;
    using System.ComponentModel.DataAnnotations;

    public class WorkshopBCInspection_DTO : Interfaces.IModel
    {
        readonly string[] locNames;
        public WorkshopBCInspection_DTO() => locNames = STREAM.GetLocNames().ToArray();

        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required! Please fill it in.")]
        public int CareComName { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "This field is required! Please fill it in.")]
        public System.DateTime Date { get; set; }
        public string InspecteReport { get; set; }
        public string ScopeOfInspectiont { get; set; }
        public bool NoOrders { get; set; }
        public string Orders { get; set; }
        public string BriefDescFind { get; set; }
        public string ActionPlan { get; set; }
        public string Responsibility { get; set; }
        [DataType(DataType.Date)]
        public System.DateTime? ActionDate { get; set; } = null;
        public string StatusOfTheOrder { get; set; }
        public override string ToString() =>
           $"{locNames[CareComName - 1]},{Date},{InspecteReport},{ScopeOfInspectiont}," +
            $"{NoOrders},{Orders},{BriefDescFind},{ActionPlan},{Responsibility},{ActionDate},{StatusOfTheOrder}";
    }
}
