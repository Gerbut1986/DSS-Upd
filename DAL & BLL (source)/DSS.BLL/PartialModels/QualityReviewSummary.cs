namespace DSS.BLL.PartialModels
{
    public class QualityReviewSummary
    {
        public string CareComName { get; set; }
        public string Outcomes { get; set; }
        public string BriefDescFind { get; set; }
        public string BriefDescRecommend { get; set; }
        public string Actions { get; set; }
        public string Responsibility { get; set; }
    }

    public class QualityReviewSummaryAll
    {
        public int CareComName { get; set; }
        public int Outcomes { get; set; }
        public int BriefDescFind { get; set; }
        public int BriefDescRecommend { get; set; }
        public int Actions { get; set; }
        public int Responsibility { get; set; }
    }
}
