namespace DTS.Models
{
    using DSS.BLL;
    using DSS.BLL.DTO;
    using System.Linq;
    using DSS.BLL.PartialModels;
    using System.Collections.Generic;

    public class NotWSIBSummaryLogic
    {
        #region Fields:
        public static bool checkRepead = false;
        public static List<string> locList = new List<string>();
        public static NotWSIBSummary model;
        public static List<NotWSIBSummary> foundSummary = new List<NotWSIBSummary>();
        public static List<NotWSIBSummaryAll> allSummary = new List<NotWSIBSummaryAll>();
        public static List<Not_WSIBs_DTO[]> aa = new List<Not_WSIBs_DTO[]>();
        #endregion

        #region Checking Location:
        public static void CheckLocation()
        {
            Counters.Nullify();
            checkRepead = true;
            var tbl = TablesContainer.list14;
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
            foreach (var it in TablesContainer.list14)
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
                        aa[i] = TablesContainer.list14.Where(loc => STREAM.GetLocNameById(loc.Location)
                        == all[j]).ToArray();
        }
        #endregion

        #region Set 'Complaints' Statistic for each Location:
        static void ComplaintsStatistic(string locName, Not_WSIBs_DTO[] arr)
        {
            model = new NotWSIBSummary();
            var ll = arr.ToList();
            if (ll[0] == null) return;
            Counters.ResetPCount();
            model.LocationName = STREAM.ContainsLoc(locList, locName);

            var att1 = ll.GroupBy(i => i.Employee_Initials);
            if (att1 != null)
            {
                foreach (var cc in att1)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Employee_Initials += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p1 += cc.Count();
                }
            }

            var att2 = ll.GroupBy(i => i.Position);
            if (att2 != null)
            {
                foreach (var cc in att2)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Position += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p2 += cc.Count();
                }
            }

            var att3 = ll.GroupBy(i => i.Time_of_Incident);
            if (att3 != null)
            {
                foreach (var cc in att3)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Time_of_Incident += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p3 += cc.Count();
                }
            }

            var att4 = ll.GroupBy(i => i.Shift);
            if (att4 != null)
            {
                foreach (var cc in att4)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Shift += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p4 += cc.Count();
                }
            }

            var att5 = ll.GroupBy(i => i.Home_Area);
            if (att5 != null)
            {
                foreach (var cc in att5)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Home_Area += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p5 += cc.Count();
                }
            }

            var att6 = ll.GroupBy(i => i.Injury_Related);
            if (att6 != null)
            {
                foreach (var cc in att6)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Injury_Related += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p6 += cc.Count();
                }
            }

            var att7 = ll.GroupBy(i => i.Type_of_Injury);
            if (att6 != null)
            {
                foreach (var cc in att6)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Type_of_Injury += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p6 += cc.Count();
                }
            }

            var att8 = ll.GroupBy(i => i.Details_of_Incident);
            if (att6 != null)
            {
                foreach (var cc in att6)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Details_of_Incident += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p6 += cc.Count();
                }
            }
            Counters.allp1 += Counters.p1; Counters.allp2 += Counters.p2; Counters.allp3 += Counters.p3;
            Counters.allp4 += Counters.p4; Counters.allp5 += Counters.p5; Counters.allp6 += Counters.p6;
            foundSummary.Add(model);
            model = new NotWSIBSummary();
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
            allSummary.Add(new NotWSIBSummaryAll()
            {
                Employee_Initials = Counters.allp1,
                Position = Counters.allp2,
                Time_of_Incident = Counters.allp3,
                Shift = Counters.allp4,
                Home_Area = Counters.allp5,
                Injury_Related = Counters.allp6,
                Type_of_Injury = Counters.allp5,
                Details_of_Incident = Counters.allp5
            });
            #endregion
        }
        #endregion

        #region Clear all list for new search(for range):
        public static void ClearAllStatic()
        {
            checkRepead = false;
            foundSummary = new List<NotWSIBSummary>();
            allSummary = new List<NotWSIBSummaryAll>();
            locList = new List<string>();
            aa = new List<Not_WSIBs_DTO[]>();
            for (int i = 0; i < 11; i++) aa.Add(new Not_WSIBs_DTO[1]);
            Counters.p1 = Counters.p2 = Counters.p3 = Counters.p4 = Counters.p5 = Counters.p6 = 
            Counters.allp1 = Counters.allp2 = Counters.allp3 = Counters.allp4 = Counters.allp5 = Counters.allp6 = 0;
            Counters.Nullify();
        }
        #endregion
    }
}