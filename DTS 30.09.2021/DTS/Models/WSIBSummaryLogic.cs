namespace DTS.Models
{
    using DSS.BLL;
    using DSS.BLL.DTO;
    using System.Linq;
    using DSS.BLL.PartialModels;
    using System.Collections.Generic;

    public class WSIBSummaryLogic
    {
        #region Fields:
        public static bool checkRepead = false;
        public static List<string> locList = new List<string>();
        public static WSIBSummary model;
        public static List<WSIBSummary> foundSummary = new List<WSIBSummary>();
        public static List<WSIBSummaryAll> allSummary = new List<WSIBSummaryAll>();
        public static List<WSIB_DTO[]> aa = new List<WSIB_DTO[]>(); 
        #endregion

        #region Checking Location:
        public static void CheckLocation()
        {
            Counters.Nullify();
            checkRepead = true;
            var tbl = TablesContainer.list13;
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
            foreach (var it in TablesContainer.list13)
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
                        aa[i] = TablesContainer.list13.Where(loc => STREAM.GetLocNameById(loc.Location)
                        == all[j]).ToArray();            
        }
        #endregion

        #region Set 'WSIB' Statistic for each Location:
        static void ComplaintsStatistic(string locName, WSIB_DTO[] arr)
        {
            model = new WSIBSummary();
            List<WSIB_DTO> ll = arr.ToList();
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

            var att2 = ll.GroupBy(i => i.Accident_Cause);
            if (att2 != null)
            {
                foreach (var cc in att2)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Accident_Cause += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p2 += cc.Count();
                }
            }

            var att3 = ll.GroupBy(i => i.Lost_Days);
            if (att3 != null)
            {
                foreach (var cc in att3)
                {
                    string key = cc.Key == 0 ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Lost_Days += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p3 += cc.Count();
                }
            }

            var att4 = ll.GroupBy(i => i.Modified_Days_Not_Shadowed);
            if (att4 != null)
            {
                foreach (var cc in att4)
                {
                    string key = cc.Key == 0 ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Modified_Days_Not_Shadowed += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p4 += cc.Count();
                }
            }

            var att5 = ll.GroupBy(i => i.Modified_Days_Shadowed);
            if (att5 != null)
            {
                foreach (var cc in att5)
                {
                    string key = cc.Key == 0 ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Modified_Days_Shadowed += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p5 += cc.Count();
                }
            }

            var att6 = ll.GroupBy(i => i.Form_7);
            if (att6 != null)
            {
                foreach (var cc in att6)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Form_7 += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p6 += cc.Count();
                }
            }

            Counters.allp1 += Counters.p1; Counters.allp2 += Counters.p2; Counters.allp3 += Counters.p3;
            Counters.allp4 += Counters.p4; Counters.allp5 += Counters.p5; Counters.allp6 += Counters.p6;
            foundSummary.Add(model);
            model = new WSIBSummary();
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
            allSummary.Add(new WSIBSummaryAll()
            {
                Employee_Initials = Counters.allp1,
                Accident_Cause = Counters.allp2,
                Lost_Days = Counters.allp3,
                Modified_Days_Not_Shadowed = Counters.allp4,
                Modified_Days_Shadowed = Counters.allp5,
                Form_7 = Counters.allp6
            });
            #endregion
        }
        #endregion

        #region Clear all list for new search(for range):
        public static void ClearAllStatic()
        {
            checkRepead = false;
            foundSummary = new List<WSIBSummary>();
            allSummary = new List<WSIBSummaryAll>();
            locList = new List<string>();
            aa = new List<WSIB_DTO[]>();
            for (int i = 0; i < 11; i++)
                aa.Add(new WSIB_DTO[1]);
            Counters.p1 = Counters.p2 = Counters.p3 = Counters.p4 = Counters.p5 = Counters.p6 = 0;
            Counters.allp1 = Counters.allp2 = Counters.allp3 = Counters.allp4 = Counters.allp5 = Counters.allp6 = 0;
            Counters.Nullify();
        }
        #endregion
    }
}