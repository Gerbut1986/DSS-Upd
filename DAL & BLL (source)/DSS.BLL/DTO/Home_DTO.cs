namespace DSS.BLL.DTO
{
    using System.ComponentModel.DataAnnotations;

    public class Home_DTO
    {
        public int Id { get; set; }
        public string Home_ID { get; set; }
        public string Home_Name { get; set; }
        [Required(ErrorMessage = "This field is required! Please fill it in.")]
        public string Full_Home_Name { get; set; }
        public string Care_Type { get; set; }
        public string Current_VPRO { get; set; }
        public string ED_GM { get; set; }
        public string ED_GM_Email_Address { get; set; }
        public string Previous_VPRO { get; set; }
    }
}