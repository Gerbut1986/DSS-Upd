namespace DSS.DAL.Entities
{
    public class Labour_Relations
    {
        public int Id { get; set; }
        public int Location { get; set; }
        public System.DateTime Date { get; set; }
        public string Union { get; set; }
        public string Category { get; set; }
        public string Details { get; set; }
        public string Status { get; set; }
        public string Accruals { get; set; }
        public string Outcome { get; set; }
        public string Lessons_Learned { get; set; }
    }
}
