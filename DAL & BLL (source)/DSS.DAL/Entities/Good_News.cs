namespace DSS.DAL.Entities
{
    public class Good_News
    {
        public int Id { get; set; }
        public int Location { get; set; }
        public System.DateTime DateNews { get; set; }
        public string Category { get; set; }
        public string Department { get; set; }
        public string SourceCompliment { get; set; }
        public string ReceivedFrom { get; set; }
        public string Description_Complim { get; set; }
        public bool Respect { get; set; }
        public bool Passion { get; set; }
        public bool Teamwork { get; set; }
        public bool Responsibility { get; set; }
        public bool Growth { get; set; }
        public string Compliment { get; set; }
        public string Spot_Awards { get; set; }
        public string Awards_Details { get; set; }
        public string NameAwards { get; set; }
        public string Awards_Received { get; set; }
        public string Community_Inititives { get; set; }
    }
}