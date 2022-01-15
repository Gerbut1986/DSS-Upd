namespace DSS.DAL.Entities
{

    public class InspectionInfo
        {
            public int Id { get; set; }
            public double InspectNumber { get; set; }
            public System.DateTime ReportDate { get; set; }
            public System.DateTime LastDate { get; set; }
            public bool NoFindings { get; set; }
            public int HomeId { get; set; }
            public Home Home { get; set; }
            public int TypeId { get; set; }
            public string NonCompliance { get; set; }
            public string LTCHAReg { get; set; }
            public string Section { get; set; }
            public string Subsection { get; set; }
            public string OtherOptionAsNeeded { get; set; }
            public bool DirectorReferral { get; set; }
            public string ClearedByInspectionNo { get; set; }
            public InspectType InspectType { get; set; }
            public string Intermediate1 { get; set; }
            public string Intermediate2 { get; set; }
            public string Intermediate3 { get; set; }
            public string Responsibility1 { get; set; }
            public string Responsibility2 { get; set; }
            public string Responsibility3 { get; set; }
            public System.DateTime TargetDate1 { get; set; }
            public System.DateTime TargetDate2 { get; set; }
            public System.DateTime TargetDate3 { get; set; }
        }
    }
