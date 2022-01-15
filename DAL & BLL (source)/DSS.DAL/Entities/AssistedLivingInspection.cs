namespace DSS.DAL.Entities
{
    public class AssistedLivingInspection
    {
        public int Id { get; set; }
        public int CareComName { get; set; }
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
        public System.DateTime? ActionDate { get; set; } = null;
    }
}
