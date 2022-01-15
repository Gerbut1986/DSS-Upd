namespace DSS.DAL.Entities
{
    public class Other
    {
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public int Location { get; set; }
        public string Inspected_By{ get; set; }
        public string Inspection_Number { get; set; }
        public string Number_of_Violations { get; set; }
        public string Notes_Comments { get; set; }
    }
}
