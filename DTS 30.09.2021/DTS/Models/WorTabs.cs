namespace DTS.Models
{
    using System;
    using System.Web.Mvc;
    using System.ComponentModel.DataAnnotations;

    public class WorTabs
    {
        public int Id { get; set; }
        public string SearchBy { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage = "This field is required! Please fill it in.")]
        [DataType(DataType.Date)]
        public DateTime Start { get; set; }
        [Required(ErrorMessage = "This field is required! Please fill it in.")]
        [DataType(DataType.Date)]
        public DateTime End { get; set; }
        public SelectList ListForms { get; set; }
        public string Filter { get; set; }
        // For Radio:
        public string WithRadio { get; set; }
        public string WithoutRadio { get; set; }
        public string FilterRadio { get; set; }

        // For All Forms:
        public string Param1 { get; set; }
    }
}