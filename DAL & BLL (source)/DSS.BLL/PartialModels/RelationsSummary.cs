namespace DSS.BLL.PartialModels
{
    public class RelationsSummary
    {
        public string LocationName { get; set; }
        public string Union { get; set; }
        public string Category { get; set; }
        public string Details { get; set; }
        public string Status { get; set; }
        public string Accruals { get; set; }
        public string Outcome { get; set; }
        public string Lessons_Learned { get; set; }
    }

    public class RelationsSummaryAll
    {
        public int LocationName { get; set; }
        public int Union { get; set; }
        public int Category { get; set; }
        public int Details { get; set; }
        public int Status { get; set; }
        public int Accruals { get; set; }
        public int Outcome { get; set; }
        public int Lessons_Learned { get; set; }
    }
}
