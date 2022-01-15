namespace DTS.Models
{
    using System.Linq;
    using DSS.BLL.DTO;
    using DSS.BLL.PartialModels;
    using System.Collections.Generic;

    public class StatisticSummary
    {
        public static List<CriticalIncidentSummary> foundSummary1 = new List<CriticalIncidentSummary>();
        static CriticalIncidentSummary model = default;
        public static List<string> locList = new List<string>();

        public static void SetAttrIncident(string locName, List<Critical_Incidents_DTO> ll, int counters)
        {
            model = new CriticalIncidentSummary();
            Counters.ResetPCount();
            model.LocationName = locList.Find(i => i == locName + " - " + counters);
            var attr1 = ll.GroupBy(i => i.MOHLTC_Follow_Up);
            if (attr1 != null)
            {
                foreach (var cc in attr1)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.MOHLTC_Follow_Up += $"{key}\t - \t{cc.Count()}" + " | ";
                    Counters.p1 += cc.Count();
                }
            }

            var attr2 = ll.GroupBy(i => i.CIS_Initiated);
            if (attr2 != null)
            {
                foreach (var cc in attr2)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.CIS_Initiated += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p2 += cc.Count();
                }
            }

            var attr3 = ll.GroupBy(i => i.MOH_Notified);
            if (attr3 != null)
            {
                foreach (var cc in attr3)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.MOH_Notified += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p3 += cc.Count();
                }
            }

            var attr4 = ll.GroupBy(i => i.POAS_Notified);
            if (attr4 != null)
            {
                foreach (var d in attr4)
                {
                    string key = d.Key == null ? "" : d.Key.ToString();
                    if (key == "") continue;
                    else
                        model.POAS_Notified += $"{key}\t - \t{d.Count()}" + " | "; Counters.p4 += d.Count();
                }
            }

            var attr5 = ll.GroupBy(i => i.Police_Notified);
            if (attr5 != null)
            {
                foreach (var cc in attr5)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Police_Notified += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p5 += cc.Count();
                }
            }


            var attr6 = ll.GroupBy(i => i.Quality_Improvement_Actions);
            if (attr6 != null)
            {
                foreach (var cc in attr6)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Quality_Improvement_Actions += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p6 += cc.Count();
                }
            }

            var attr7 = ll.GroupBy(i => i.Risk_Locked);
            if (attr7 != null)
            {
                foreach (var cc in attr7)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Risk_Locked += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p7 += cc.Count();
                }
            }

            var attr8 = ll.GroupBy(i => i.Brief_Description);
            if (attr8 != null)
            {
                foreach (var cc in attr8)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Brief_Description += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p8 += cc.Count();
                }
            }

            var attr9 = ll.GroupBy(i => i.Care_Plan_Updated);
            if (attr9 != null)
            {
                foreach (var e in attr9)
                {
                    string key = e.Key == null ? "" : e.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Care_Plan_Updated += $"{key}\t - \t{e.Count()}" + " | "; Counters.p9 += e.Count();
                }
            }

            var attr10 = ll.GroupBy(i => i.CI_Form_Number);
            if (attr10 != null)
            {
                int count = 0;
                foreach (var cc in attr10)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        count += cc.Count();
                }
                model.CI_Form_Number = $"All\t - \t{count}"; Counters.p10 += count;
            }

            var attr11 = ll.GroupBy(i => i.File_Complete);
            if (attr11 != null)
            {
                foreach (var cc in attr11)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.File_Complete += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p11 += cc.Count();
                }
            }

            var attr112 = ll.GroupBy(i => i.Follow_Up_Amendments);
            if (attr112 != null)
            {
                foreach (var cc in attr112)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Follow_Up_Amendments += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p12 += cc.Count();
                }
            }
            Counters.allp1 += Counters.p1; Counters.allp2 += Counters.p2; Counters.allp3 += Counters.p3;
            Counters.allp4 += Counters.p4; Counters.allp5 += Counters.p5; Counters.allp6 += Counters.p6;
            Counters.allp7 += Counters.p7; Counters.allp8 += Counters.p8; Counters.allp19 += Counters.p9;
            Counters.allp10 += Counters.p10; Counters.allp11 += Counters.p11; Counters.allp12 += Counters.p12;

            foundSummary1.Add(model);
            model = new CriticalIncidentSummary();
        }
    }
}