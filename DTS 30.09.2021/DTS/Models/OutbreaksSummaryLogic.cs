namespace DTS.Models
{
    using DSS.BLL;
    using System.Linq;
    using DSS.BLL.DTO;
    using DSS.BLL.PartialModels;
    using System.Collections.Generic;

    public class OutbreaksSummaryLogic
    {
        #region Fields:
        public static bool checkRepead = false;
        public static List<string> locList = new List<string>();
        public static OutbreaksSummary model;
        public static List<OutbreaksSummary> foundSummary = new List<OutbreaksSummary>();
        public static List<OutbreaksSummaryAll> allSummary = new List<OutbreaksSummaryAll>();
        public static List<Outbreaks_DTO[]> aa = new List<Outbreaks_DTO[]>();
        #endregion

        #region Checking Location:
        public static void CheckLocation()
        {
            Counters.Nullify();
            checkRepead = true;
            var tbl = TablesContainer.list12;
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
            foreach (var it in TablesContainer.list12)
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
                {
                    var fromFile = UnmanageCode.ReadLocFromFile()[j];
                    if (locList[i].Contains(fromFile))
                    {
                        locList[i] = locList[i] + " - " + Counters.cnt[j - 1];
                        break;
                    }
                }
        }
        #endregion

        #region Fill out lists aa1, aa2, aa3... aa11 existing locations:
        public static void FillOutLists()
        {
            var all = UnmanageCode.ReadLocFromFile();
            for (var i = 0; i < locList.Count; i++)
                for (int j = 1; j < all.Length; j++)
                    if (locList[i].Contains(all[j]))
                        aa[i] = TablesContainer.list12.Where(loc => STREAM.GetLocNameById(loc.Location)
                        == all[j]).ToArray();
        }
        #endregion

        #region Set 'Complaints' Statistic for each Location:
        static void ComplaintsStatistic(string locName, Outbreaks_DTO[] arr)
        {
            model = new OutbreaksSummary();
            List<Outbreaks_DTO> ll = arr.ToList();
            if (ll[0] == null) return;
            Counters.ResetPCount();
            
            model.LocationName = STREAM.ContainsLoc(locList, locName);

            var att1 = ll.GroupBy(i => i.Type_of_Outbreak);
            if (att1 != null)
            {
                foreach (var cc in att1)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Type_of_Outbreak += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p1 += cc.Count();
                }
            }

            var att2 = ll.GroupBy(i => i.Total_Days_Closed);
            if (att2 != null)
            {
                foreach (var cc in att2)
                {
                    string key = cc.Key == 0 ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Total_Days_Closed += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p2 += cc.Count();
                }
            }

            var att3 = ll.GroupBy(i => i.Total_Residents_Affected);
            if (att3 != null)
            {
                foreach (var cc in att3)
                {
                    string key = cc.Key == 0 ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Total_Residents_Affected += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p3 += cc.Count();
                }
            }

            var att4 = ll.GroupBy(i => i.Total_Staff_Affected);
            if (att4 != null)
            {
                foreach (var cc in att4)
                {
                    string key = cc.Key == 0 ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Total_Staff_Affected += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p4 += cc.Count();
                }
            }

            var att5 = ll.GroupBy(i => i.Strain_Identified);
            if (att5 != null)
            {
                foreach (var cc in att5)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Strain_Identified += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p5 += cc.Count();
                }
            }

            var att6 = ll.GroupBy(i => i.Deaths_Due);
            if (att6 != null)
            {
                foreach (var cc in att6)
                {
                    string key = cc.Key == 0 ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Deaths_Due += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p6 += cc.Count();
                }
            }

            var att7 = ll.GroupBy(i => i.CI_Report_Submitted);
            if (att7 != null)
            {
                foreach (var cc in att7)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.CI_Report_Submitted += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p7 += cc.Count();
                }
            }

            var att8 = ll.GroupBy(i => i.Notify_MOL);
            if (att8 != null)
            {
                foreach (var cc in att8)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Notify_MOL += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p8 += cc.Count();
                }
            }

            var att9 = ll.GroupBy(i => i.Credit_for_Lost_Days);
            if (att9 != null)
            {
                foreach (var cc in att9)
                {
                    string key = cc.Key == 0 ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Credit_for_Lost_Days += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p9 += cc.Count();
                }
            }

            var att10 = ll.GroupBy(i => i.Tracking_Sheet_Completed);
            if (att10 != null)
            {
                foreach (var cc in att10)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Tracking_Sheet_Completed += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p10 += cc.Count();
                }
            }

            var att11 = ll.GroupBy(i => i.Docs_Submitted_Finance);
            if (att11 != null)
            {
                foreach (var cc in att11)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Docs_Submitted_Finance += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p11 += cc.Count();
                }
            }
         
            Counters.allp1 += Counters.p1; Counters.allp2 += Counters.p2; Counters.allp3 += Counters.p3;
            Counters.allp4 += Counters.p4; Counters.allp5 += Counters.p5; Counters.allp6 += Counters.p6;
            Counters.allp7 += Counters.p7; Counters.allp8 += Counters.p8; Counters.allp9 += Counters.p9;
            Counters.allp10 += Counters.p10; Counters.allp11 += Counters.p11;
            foundSummary.Add(model);
            model = new OutbreaksSummary();
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
            allSummary.Add(new OutbreaksSummaryAll()
            {
                Type_of_Outbreak = Counters.allp1,
                Total_Days_Closed = Counters.allp2,
                Total_Residents_Affected = Counters.allp3,
                Total_Staff_Affected = Counters.allp4,
                Strain_Identified = Counters.allp5,
                Deaths_Due = Counters.allp6,
                CI_Report_Submitted = Counters.allp7,
                Notify_MOL = Counters.allp8,
                Credit_for_Lost_Days = Counters.allp9,
                Tracking_Sheet_Completed = Counters.allp10,
                Docs_Submitted_Finance = Counters.allp11
            });
            #endregion
        }
        #endregion

        #region Clear all list for new search(for range):
        public static void ClearAllStatic()
        {
            checkRepead = false;
            foundSummary = new List<OutbreaksSummary>();
            allSummary = new List<OutbreaksSummaryAll>();
            locList = new List<string>();
            aa = new List<Outbreaks_DTO[]>();
            for (int i = 0; i < 11; i++)
                aa.Add(new Outbreaks_DTO[1]);
            Counters.p1 = Counters.p2 = Counters.p3 = Counters.p4 = Counters.p5 = Counters.p6 = Counters.p7 = Counters.p8 =
            Counters.p9 = Counters.p10 = Counters.p11 = 0;
            Counters.allp1 = Counters.allp2 = Counters.allp3 = Counters.allp4 = Counters.allp5 =
            Counters.allp6 = Counters.allp7 = Counters.allp8 = Counters.allp9 = Counters.allp10 = Counters.allp11 = 0;
            Counters.Nullify();
        }
        #endregion
    }
}