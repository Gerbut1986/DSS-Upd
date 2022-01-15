namespace DSS.BLL.DTO
{
    using System.Linq;
    using System.ComponentModel.DataAnnotations;

    public class Immunization_DTO : Interfaces.IModel
    {
        string[] locNames;
        public Immunization_DTO() => locNames = STREAM.GetLocNames().ToArray();
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required! Please fill it in!")]
        public int Location { get; set; }
        public string Numb_Res_Comm { get; set; }
        public string Numb_Res_Immun { get; set; }
        public string Numb_Res_Not_Immun { get; set; }
        public string Per_Res_Immun { get; set; }
        public string Per_Res_Not_Immun { get; set; }
        public override string ToString() => $"{locNames[Location - 1]},{Numb_Res_Comm},{Numb_Res_Immun},{Numb_Res_Not_Immun},{Per_Res_Not_Immun}";
    }
}