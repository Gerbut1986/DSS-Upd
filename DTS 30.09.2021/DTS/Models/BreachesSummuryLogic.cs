namespace DTS.Models
{
    using DSS.BLL;
    using System.Linq;
    using DSS.BLL.DTO;
    using DSS.BLL.PartialModels;
    using System.Collections.Generic;

    public class BreachesSummuryLogic
    {
        #region Fields:
        public static bool checkRepead = false;
        public static List<string> locList = new List<string>();
        public static PBreachesSummary model;
        public static List<PBreachesSummary> foundSummary = new List<PBreachesSummary>();
        public static List<PBreachesSummaryAll> allSummary = new List<PBreachesSummaryAll>();
        public static List<Privacy_Breaches_DTO[]> aa = new List<Privacy_Breaches_DTO[]>();
        #endregion

        #region Checking Location:
        public static void CheckLocation()
        {
            Counters.Nullify();
            checkRepead = true;
            var tbl = TablesContainer.list7;
            for (int i = 0; i < tbl.Count; i++)
                for (int j = 1; j < UnmanageCode.ReadLocFromFile().Length; j++)
                    if (STREAM.GetLocNameById(tbl[i].Location).Contains(UnmanageCode.ReadLocFromFile()[j]))
                        Counters.cnt[j - 1]++;
        }
        #endregion

        #region Get locations without repeat:
        public static void GetDistinctList(List<Home_DTO> listCommunity)
        {
            var locDistinct = new HashSet<string>();
            var locId = new List<int>();
            foreach (var it in TablesContainer.list7)
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

        #region Fill out lists aa1, aa2, aa3... aa11 existing locations:
        public static void FillOutLists()
        {
            var all = UnmanageCode.ReadLocFromFile();
            for (var i = 0; i < locList.Count; i++)
                for (int j = 1; j < all.Length; j++)
                    if (locList[i].Contains(all[j]))
                        aa[i] = TablesContainer.list7.Where(loc => STREAM.GetLocNameById(loc.Location)
                        == all[j]).ToArray();
        }
        #endregion

        #region Set 'Complaints' Statistic for each Location:
        static void ComplaintsStatistic(string locName, Privacy_Breaches_DTO[] arr)
        {
            model = new PBreachesSummary();
            List<Privacy_Breaches_DTO> ll = arr.ToList();
            if (ll[0] == null) return;
            Counters.ResetPCount();
            model.LocationName = STREAM.ContainsLoc(locList,locName);

            var att1 = ll.GroupBy(i => i.Status);
            if (att1 != null)
            {
                foreach (var cc in att1)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Status += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p1 += cc.Count();
                }
            }

            var att2 = ll.GroupBy(i => i.Description_Outcome);
            if (att2 != null)
            {
                foreach (var cc in att2)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Description_Outcome += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p2 += cc.Count();
                }
            }

            var att3 = ll.GroupBy(i => i.Type_of_Breach);
            if (att3 != null)
            {
                foreach (var cc in att3)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Type_of_Breach += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p3 += cc.Count();
                }
            }

            var att4 = ll.GroupBy(i => i.Type_of_PHI_Involved);
            if (att4 != null)
            {
                foreach (var cc in att4)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Type_of_PHI_Involved += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p4 += cc.Count();
                }
            }

            var att5 = ll.GroupBy(i => i.Number_of_Individuals_Affected);
            if (att5 != null)
            {
                foreach (var cc in att5)
                {
                    string key = cc.Key == 0 ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Number_of_Individuals_Affected += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p5 += cc.Count();
                }
            }

            var att6 = ll.GroupBy(i => i.Risk_Level);
            if (att6 != null)
            {
                foreach (var cc in att6)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Risk_Level += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p6 += cc.Count();
                }
            }           

            Counters.allp1 += Counters.p1; Counters.allp2 += Counters.p2; Counters.allp3 += Counters.p3;
            Counters.allp4 += Counters.p4; Counters.allp5 += Counters.p5; Counters.allp6 += Counters.p6;
            foundSummary.Add(model);
            model = new PBreachesSummary();
        }
        #endregion

        #region Call All Statistics:
        public static void AllStatIncident()
        {
            Counters.cnt = STREAM.DelZeros(Counters.cnt);
            for (int i = 0; i < locList.Count; i++)
                if (aa[i] != null)
                {
                    ComplaintsStatistic(locList[i], aa[i]);
                }
            #region Add All Summary quantity on List:
            allSummary.Add(new PBreachesSummaryAll()
            {
                Status = Counters.allp1,
                Description_Outcome = Counters.allp2,
                Type_of_Breach = Counters.allp3,
                Type_of_PHI_Involved = Counters.allp4,
                Number_of_Individuals_Affected = Counters.allp5,
                Risk_Level = Counters.allp6
            });
            #endregion
        }
        #endregion

        #region Clear all list for new search(for range):
        public static void ClearAllStatic()
        {
            checkRepead = false;
            foundSummary = new List<PBreachesSummary>();
            allSummary = new List<PBreachesSummaryAll>();
            locList = new List<string>();
            aa = new List<Privacy_Breaches_DTO[]>();
            for (int i = 0; i < 11; i++)
                aa.Add(new Privacy_Breaches_DTO[1]);
            Counters.p1 = Counters.p2 = Counters.p3 = Counters.p4 = Counters.p5 = Counters.p6 =
            Counters.allp1 = Counters.allp2 = Counters.allp3 = Counters.allp4 = Counters.allp5 = Counters.allp6 = 0;
            Counters.Nullify();
        }
        #endregion
    }
}