namespace DSS.DAL.Entities
{
    using System.Collections.Generic;

    public class Section
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LTCHARegId { get; set; }
        public LTCHAReg LTCHAReg { get; set; }
        public ICollection<Subsection> Subsections { get; set; }
    }
}
