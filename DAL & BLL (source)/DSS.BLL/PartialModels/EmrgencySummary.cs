namespace DSS.BLL.PartialModels
{
    public class EmrgencySummary
    {
        public string LocationName { get; set; }
        public string Code { get; set; }
        public string Exercise { get; set; }
        public string Method { get; set; }
        public string Date { get; set; }
    }

    public class EmrgencySummaryAll
    {
        public int Code { get; set; }
        public int Exercise { get; set; }
        public int Method { get; set; }
        public int Date { get; set; }
    }
}
