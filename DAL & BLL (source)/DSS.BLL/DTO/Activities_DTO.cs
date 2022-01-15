namespace DSS.BLL.DTO
{
    using System;
    using System.Linq;
    using DSS.BLL.Helpers;
    using static System.String;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Activities_DTO : IComparable
    {
        [NotMapped]
        private string _activityDescription;
        [Key]
        public int ActivityID { get; set; }
        [NotMapped]
        public int Level { get; set; }
        [NotMapped]
        public string DisplayAsCategory => Concat("\xA0".Multiply(Level * 5), ActivityDescription);
        [Required]
        [Display(Name = "Parent activity Id")]
        public int ParentActivityID { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 4)]
        [Display(Name = "Activity description")]
        public string ActivityDescription
        {
            get { return _activityDescription; }
            set { _activityDescription = value.Trim(); }
        }
        [Required]
        [Display(Name = "Activity start time")]
        public DateTime StartDateTime { get; set; }
        [Required]
        [Display(Name = "Activity end time")]
        public DateTime EndDateTime { get; set; }
        [NotMapped]
        public List<Activities_DTO> ChildActivities { get; set; }
        public Activities_DTO()
        {
            ChildActivities = new List<Activities_DTO>();
        }
        public static Activities_DTO Find(Activities_DTO activity, int activityId)
        {
            if (activity == null) return null;
            return activity.ActivityID == activityId ? activity : activity.ChildActivities.Select(child => Find(child, activityId)).FirstOrDefault(found => found != null);
        }
        public int CompareTo(object compare)
        {
            var next = compare as Activities_DTO;
            return Compare(ActivityDescription, next.ActivityDescription, StringComparison.Ordinal);
        }
    }
}