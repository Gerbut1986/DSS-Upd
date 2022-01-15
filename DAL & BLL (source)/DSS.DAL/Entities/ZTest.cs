using System;

namespace DSS.DAL.Entities
{
    //Entities contain models for all tables
    //Each property corresponds to the named column in the same named table
    public class ZTest
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //? exists so that we can use datetime as a nullible type
        public DateTime? Date_Creation { get; set; }       
        
    }
}
