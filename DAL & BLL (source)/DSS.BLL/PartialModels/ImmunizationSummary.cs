namespace DSS.BLL.PartialModels
{
    public class ImmunizationSummary
    {
        public string LocationName { get; set; }
        public string Numb_Res_Comm { get; set; }
        public string Numb_Res_Immun { get; set; }
        public string Numb_Res_Not_Immun { get; set; }
        public string Per_Res_Immun { get; set; }
        public string Per_Res_Not_Immun { get; set; }
    }

    public class ImmunizationSummaryAll
    {
        public int LocationName { get; set; }
        public int Numb_Res_Comm { get; set; }
        public int Numb_Res_Immun { get; set; }
        public int Numb_Res_Not_Immun { get; set; }
        public int Per_Res_Immun { get; set; }
        public int Per_Res_Not_Immun { get; set; }
    }
}
