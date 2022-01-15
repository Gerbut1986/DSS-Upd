namespace DTS.Models
{
    using DSS.BLL.DTO;
    using System.Web.Mvc;

    public class EditViewModel
    {
        public Activities_DTO Activity { get; set; }
        public SelectList Categories { get; set; }
        public int SelectedParentActivityId { get; set; }
    }
}