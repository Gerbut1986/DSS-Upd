namespace DSS.DAL.Entities
{
    using System.Collections.Generic;

    public class Subsection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SectionId { get; set; }
        public Section Section { get; set; }
        public ICollection<OtherOption> OtherOptions { get; set; }
    }
}
