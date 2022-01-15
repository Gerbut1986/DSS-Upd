namespace DTS.Models
{
    using System;
    using DSS.BLL;
    using DSS.BLL.DTO;
    using System.Linq;
    using DSS.BLL.Interfaces;
    using DSS.BLL.PartialModels;
    using System.Collections.Generic;

    public class IncidentSummaryLogic
    {
        #region Fields:
        public static bool checkRepead = false;
        public static List<string> locList = new List<string>();
        public static CriticalIncidentSummary model;
        public static List<CriticalIncidentSummary> foundSummary1 = new List<CriticalIncidentSummary>();
        public static List<IncidentSummaryAll> allSummary1 = new List<IncidentSummaryAll>();
        public static List<Critical_Incidents_DTO[]> aa = new List<Critical_Incidents_DTO[]>();
        #endregion

        #region Old logic..:
        //#region Accounting of all existing Locations:
        //public static void CountLocs(IEnumerable<IModel> list)
        //{
        //    checkRepead = true;
        //    foreach (var cc in list)
        //    {
        //        Type type = cc.GetType();
        //        switch (GetModel(type))
        //        {
        //            case 1:
        //                var cr = (Critical_Incidents_DTO)cc;
        //                CheckLocation(cr.Location);
        //                break;
        //        }
        //    }
        //}
        //#endregion

        //#region Get Model by number:
        //static int GetModel(Type type)
        //{
        //    switch (type.Name)
        //    {
        //        case "Critical_Incidents_DTO": return 1;
        //        case "Complaint_DTO": return 2;
        //        case "Good_News_DTO": return 3;
        //        case "": return 0; // to be continue
        //    }
        //    return 0;
        //}
        //#endregion
        #endregion

        #region Checking Location:
        public static void CheckLocation()
        {
            Counters.Nullify();
            checkRepead = true;
            var tbl = TablesContainer.list1;
            for (int i = 0; i < tbl.Count; i++)
                for (int j = 1; j < UnmanageCode.ReadLocFromFile().Length; j++)
                    if (STREAM.GetLocNameById(tbl[i].Location).Contains(UnmanageCode.ReadLocFromFile()[j]))
                        Counters.cnt[j - 1]++;
        }
        #endregion

        #region Get locations without repeat:
        public static void GetDistinctList(List<Home_DTO> listCommunity)
        {
            CriticalIncidentSummary model = new CriticalIncidentSummary();
            var locDistinct = new HashSet<string>();
            var locId = new List<int>();
            foreach (var it in TablesContainer.list1)
            {
                var cc = listCommunity.Where(i => i.Id == it.Location).SingleOrDefault();
                locDistinct.Add(cc.Full_Home_Name);
                locId.Add(cc.Id);
            }

            locList = locDistinct.ToList();
            locList.Sort(); // Sorted by alphanumeric
        }
        #endregion

        #region Add count location for each exist:
        public static void AddCntLoc()
        {
            for (var i = 0; i < locList.Count; i++)
                for (int j = 1; j < UnmanageCode.ReadLocFromFile().Length; j++)
                    if (locList[i].Contains(UnmanageCode.ReadLocFromFile()[j]))
                        locList[i] = locList[i] + " - " + Counters.cnt[j - 1];
        }
        #endregion

        #region Fill out lists ll1,ll2,ll3...ll11 existing locations:
        public static void FillOutLists()
        {
            var all = UnmanageCode.ReadLocFromFile();
            for (var i = 0; i < locList.Count; i++)
                for (int j = 1; j < all.Length; j++)
                    if (locList[i].Contains(all[j]))
                        aa[i] = TablesContainer.list1.Where(loc => STREAM.GetLocNameById(loc.Location) == all[j]).ToArray();
        }
        #endregion

        #region Set 'Critical Incidents' Statistics for each Location:
        static void IncidentsStatistic(string locName, Critical_Incidents_DTO[] arr)
        {
            model = new CriticalIncidentSummary();
             var  ll = arr.ToList();
            if (ll[0] == null) return;
            Counters.ResetPCount();
            model.LocationName = STREAM.ContainsLoc(locList, locName);
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
            var attr113 = ll.GroupBy(i => i.CI_Category_Type);
            if (attr113 != null)
            {
                string getNamebyId = string.Empty;
                var listCi = STREAM.GetCINames().ToList();
                foreach (var cc in attr113)
                {
                    if (cc.Key != 0)
                    {
                        getNamebyId = listCi[cc.Key];
                        getNamebyId = getNamebyId == null ? "" : getNamebyId;
                        model.CI_Category_Type += $"{getNamebyId}\t - \t{cc.Count()}" + " | "; Counters.p13 += cc.Count();
                    }
                    else continue;
                }
            }
            Counters.allp1 += Counters.p1; Counters.allp2 += Counters.p2; Counters.allp3 += Counters.p3;
            Counters.allp4 += Counters.p4; Counters.allp5 += Counters.p5; Counters.allp6 += Counters.p6;
            Counters.allp7 += Counters.p7; Counters.allp8 += Counters.p8; Counters.allp9 += Counters.p9;
            Counters.allp10 += Counters.p10; Counters.allp11 += Counters.p11; Counters.allp12 += Counters.p12; 
            Counters.allp13 += Counters.p13; // for ci category type

            foundSummary1.Add(model);
            model = new CriticalIncidentSummary();
        }
        #endregion

        #region Call All Statistics:
        public static void AllStatIncident()
        {
            Counters.cnt = STREAM.DelZeros(Counters.cnt);
            for (int i = 0; i < locList.Count; i++)
                if (aa[i] != null)
                {
                    IncidentsStatistic(locList[i], aa[i]);
                }
            #region Add All Summary quantity on List:
            allSummary1.Add(new IncidentSummaryAll
            {
                MOHLTC_Follow_Up = Counters.allp1,
                CIS_Initiated = Counters.allp2,
                MOH_Notified = Counters.allp3,
                POAS_Notified = Counters.allp4,
                Police_Notified = Counters.allp5,
                Quality_Improvement_Actions = Counters.allp6,
                Risk_Locked = Counters.allp7,
                Brief_Description = Counters.allp8,
                Care_Plan_Updated = Counters.allp9,
                CI_Form_Number = Counters.allp10,
                File_Complete = Counters.allp11,
                Follow_Up_Amendments = Counters.allp12,
                CI_Category_Type = Counters.allp13
            });
            #endregion
        }
        #endregion

        #region Clear all list for new search(for range):
        public static void ClearAllStatic()
        {
            checkRepead = false;
            foundSummary1 = new List<CriticalIncidentSummary>();
            allSummary1 = new List<IncidentSummaryAll>();
            locList = new List<string>();
            aa = new List<Critical_Incidents_DTO[]>();
            for (int i = 0; i < UnmanageCode.ReadLocFromFile().Length; i++)
                aa.Add(new Critical_Incidents_DTO[1]);
            Counters.ResetAllP();
            Counters.ResetPCount();
            Counters.Nullify();
        }
        #endregion
    }
}
