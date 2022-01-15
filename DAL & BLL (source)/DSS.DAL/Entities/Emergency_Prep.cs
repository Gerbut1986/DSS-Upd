namespace DSS.DAL.Entities
{
    public class Emergency_Prep
    {
        public int Id { get; set; }
        public int Location { get; set; }
        public string Code { get; set; }
        public string Exercise { get; set; }
        public string Method { get; set; }
        public System.DateTime Date { get; set; }
    }
}