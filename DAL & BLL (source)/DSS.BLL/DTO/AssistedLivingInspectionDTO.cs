namespace DSS.BLL.DTO
{
    using System.Linq;
    using System.ComponentModel.DataAnnotations;

    public class AssistedLivingInspectionDTO : Interfaces.IModel
    {
        readonly string[] locNames;
        public AssistedLivingInspectionDTO() => locNames = STREAM.GetLocNames().ToArray();

        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required! Please fill it in.")]
        public int CareComName { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "This field is required! Please fill it in.")]
        public System.DateTime Date { get; set; }
        public string InspectComplaint { get; set; }
        public string InspectTypeReason { get; set; }
        public bool NoFinding { get; set; }
        public string AssistLivReg { get; set; }
        public string AssistLivAct { get; set; }
        public string ActOrReg { get; set; }
        public string SubActOrReg { get; set; }
        public string Category { get; set; }
        public string BriefDescOfFinding { get; set; }
        public string ActionPlan { get; set; }
        public string Responsibility { get; set; }
        [DataType(DataType.Date)]
        public System.DateTime? ActionDate { get; set; } = null;
        public override string ToString() =>
            $"{locNames[CareComName - 1]},{Date},{InspectComplaint},{InspectTypeReason},{NoFinding},{AssistLivReg}," +
            $"{AssistLivAct},{ActOrReg},{SubActOrReg},{Category},{BriefDescOfFinding},{ActionPlan},{Responsibility},{ActionDate}";  
    }
}
