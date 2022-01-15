namespace DSS.BLL.PartialModels
{
    public class OtherSummary
    {
        public string Location { get; set; }
        public string Inspected_By { get; set; }
        public string Inspection_Number { get; set; }
        public string Number_of_Violations { get; set; }
        public string Notes_Comments { get; set; }
    }

    public class OtherSummaryAll
    {
        public int Location { get; set; }
        public int Inspected_By { get; set; }
        public int Inspection_Number { get; set; }
        public int Number_of_Violations { get; set; }
        public int Notes_Comments { get; set; }
    }
}
