namespace DSS.DAL.Entities
{
    public class OtherOption
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SubsectionId { get; set; }
        public Subsection Subsection { get; set; }
    }
}
