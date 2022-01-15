namespace DSS.BLL.DTO
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;

    //this is a model of a table w/ added logic that will be seen by client
    public class ZTest_DTO
    {
        ///public ZTest_DTO() => locNames = STREAM.GetLocNames().ToArray(); no need b/c no locations are retreived in this form
        public int Id { get; set; }

        public string Name { get; set; }

        [Required(ErrorMessage = "This field is required! Please try to fill it in")]
        
        public DateTime? Date_Creation { get; set; }         
        
        //to record data into Excel when an Excel file is downloaded
        public override string ToString()
        {
            return $"{Id},{Name},{Date_Creation}";
        }
    }
}
