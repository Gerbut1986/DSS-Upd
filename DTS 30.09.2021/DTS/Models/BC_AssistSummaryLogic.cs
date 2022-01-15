namespace DTS.Models
{
    using DSS.BLL;
    using System.Linq;
    using DSS.BLL.DTO;
    using DSS.BLL.PartialModels;
    using System.Collections.Generic;

    public class BC_AssistSummaryLogic
    {
        #region Fields:
        public static bool checkRepead = false;
        public static List<string> locList = new List<string>();
        public static BC_AssistSummary model;
        public static List<BC_AssistSummary> foundSummary = new List<BC_AssistSummary>();
        public static List<BC_AssistSummaryAll> allSummary = new List<BC_AssistSummaryAll>();
        public static List<BC_Assisted_Living_Reportable_Incidents_DTO[]> aa = new List<BC_Assisted_Living_Reportable_Incidents_DTO[]>();
        #endregion

        #region Checking Location:
        public static void CheckLocation()
        {
            Counters.Nullify();
            checkRepead = true;
            var tbl = TablesContainer.list20;
            for (int i = 0; i < tbl.Count; i++)
                for (int j = 1; j < UnmanageCode.ReadLocFromFile().Length; j++)
                    if (STREAM.GetLocNameById(tbl[i].NameCareCommu).Contains(UnmanageCode.ReadLocFromFile()[j]))
                        Counters.cnt[j - 1]++;
        }
        #endregion

        #region Get locations without repeat:
        public static void GetDistinctList(List<Home_DTO> listCommunity)
        {
            var locDistinct = new HashSet<string>();
            var locId = new List<int>();
            foreach (var it in TablesContainer.list20)
            {
                var cc = listCommunity.Where(i => i.Id == it.NameCareCommu).SingleOrDefault();
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
                        aa[i] = TablesContainer.list20.Where(loc => STREAM.GetLocNameById(loc.NameCareCommu)
                        == all[j]).ToArray();
        }
        #endregion

        #region Set 'Complaints' Statistic for each Location:
        static void ComplaintsStatistic(string locName, BC_Assisted_Living_Reportable_Incidents_DTO[] arr)
        {
            model = new BC_AssistSummary();
            var ll = arr.ToList();
            if (ll[0] == null) return;
            Counters.ResetPCount();
            model.CareComName = STREAM.ContainsLoc(locList, locName);

            var att1 = ll.GroupBy(i => i.IncidentType);
            if (att1 != null)
            {
                foreach (var cc in att1)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.IncidentType += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p1 += cc.Count();
                }
            }

            var att2 = ll.GroupBy(i => i.BriefDescrincident);
            if (att2 != null)
            {
                foreach (var cc in att2)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.BriefDescrincident += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p2 += cc.Count();
                }
            }

            var att3 = ll.GroupBy(i => i.BriefDescrTaken);
            if (att3 != null)
            {
                foreach (var cc in att3)
                {
                    model.BriefDescrTaken += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p3 += cc.Count();
                }
            }

            var att4 = ll.GroupBy(i => i.Notifications);
            if (att4 != null)
            {
                foreach (var cc in att4)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Notifications += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p4 += cc.Count();
                }
            }

            Counters.allp1 += Counters.p1; Counters.allp2 += Counters.p2; Counters.allp3 += Counters.p3;
            Counters.allp4 += Counters.p4;
            foundSummary.Add(model);
            model = new BC_AssistSummary();
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
            allSummary.Add(new BC_AssistSummaryAll()
            {
                IncidentType = Counters.allp1,
                BriefDescrincident = Counters.allp2,
                BriefDescrTaken = Counters.allp3,
                Notifications = Counters.allp4
            });
            #endregion
        }
        #endregion

        #region Clear all list for new search(for range):
        public static void ClearAllStatic()
        {
            checkRepead = false;
            foundSummary = new List<BC_AssistSummary>();
            allSummary = new List<BC_AssistSummaryAll>();
            locList = new List<string>();
            aa = new List<BC_Assisted_Living_Reportable_Incidents_DTO[]>();
            for (int i = 0; i < 11; i++) aa.Add(new BC_Assisted_Living_Reportable_Incidents_DTO[1]);
            Counters.p1 = Counters.p2 = Counters.p3 = Counters.p4 = 0;
            Counters.ResetAllP();
            Counters.Nullify();
        }
        #endregion
    }
}