namespace DSS.BLL.DTO
{
    using System.Linq;
    using System.ComponentModel.DataAnnotations;

    public class LicensingInspectionDTO : Interfaces.IModel
    {
        readonly string[] locNames;
        public LicensingInspectionDTO() => locNames = STREAM.GetLocNames().ToArray();
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required! Please fill it in.")]
        public int CareComName { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "This field is required! Please fill it in.")]
        public System.DateTime Date { get; set; }
        public string InspectComplaint { get; set; }
        public string InspectTypeReason { get; set; }
        public bool NoFinding { get; set; }
        public string Contraventions { get; set; }
        public string CommCareLivAct { get; set; }
        public string ResidentCareRegSec { get; set; }
        public string ResidentCareRegSub { get; set; }
        public string BriefDescription { get; set; }
        public string ActionPlan { get; set; }
        public string Responsibility { get; set; }
        [DataType(DataType.Date)]
        //[Required(AllowEmptyStrings = true)]
        public System.DateTime? ActionDate { get; set; } = null;

        public override string ToString()
        {
            return $"{locNames[CareComName - 1]},{Date},{InspectComplaint},{InspectTypeReason},{NoFinding}," +
                $"{Contraventions},{CommCareLivAct},{ResidentCareRegSec}," +
                $"{ResidentCareRegSub},{BriefDescription},{ActionPlan},{Responsibility},{ActionDate}";
        }
    }
}
