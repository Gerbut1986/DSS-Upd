namespace DSS.DAL.Entities
{
    public class QualityReview
    {
        public int Id { get; set; }
        public int CareComName { get; set; }
        public System.DateTime Date { get; set; }
        public string Outcomes { get; set; }
        public string BriefDescFind { get; set; }
        public string BriefDescRecommend { get; set; }
        public string Actions { get; set; }
        public string Responsibility { get; set; }
        public System.DateTime? ActionDate { get; set; } = null;
    }
}
