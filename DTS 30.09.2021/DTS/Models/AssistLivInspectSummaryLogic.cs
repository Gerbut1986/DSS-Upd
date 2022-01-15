namespace DTS.Models
{
    using DSS.BLL;
    using System.Linq;
    using DSS.BLL.DTO;
    using DSS.BLL.PartialModels;
    using System.Collections.Generic;

    public class AssistLivInspectSummaryLogic
    {
        #region Fields:
        public static bool checkRepead = false;
        public static List<string> locList = new List<string>();
        public static AssistLivInspectSummary model;
        public static List<AssistLivInspectSummary> foundSummary = new List<AssistLivInspectSummary>();
        public static List<AssistLivInspectSummaryAll> allSummary = new List<AssistLivInspectSummaryAll>();
        public static List<AssistedLivingInspectionDTO[]> aa = new List<AssistedLivingInspectionDTO[]>();
        #endregion

        #region Checking Location:
        public static void CheckLocation()
        {
            Counters.Nullify();
            checkRepead = true;
            var tbl = TablesContainer.list16;
            for (int i = 0; i < tbl.Count; i++)
                for (int j = 1; j < UnmanageCode.ReadLocFromFile().Length; j++)
                    if (STREAM.GetLocNameById(tbl[i].CareComName).Contains(UnmanageCode.ReadLocFromFile()[j]))
                        Counters.cnt[j - 1]++;
        }
        #endregion

        #region Get locations without repeat:
        public static void GetDistinctList(List<Home_DTO> listCommunity)
        {
            var locDistinct = new HashSet<string>();
            var locId = new List<int>();
            foreach (var it in TablesContainer.list16)
            {
                var cc = listCommunity.Where(i => i.Id == it.CareComName).SingleOrDefault();
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
                        aa[i] = TablesContainer.list16.Where(loc => STREAM.GetLocNameById(loc.CareComName)
                        == all[j]).ToArray();
        }
        #endregion

        #region Set 'Complaints' Statistic for each Location:
        static void ComplaintsStatistic(string locName, AssistedLivingInspectionDTO[] arr)
        {
            model = new AssistLivInspectSummary();
            var ll = arr.ToList();
            if (ll[0] == null) return;
            Counters.ResetPCount();
            model.CareComName = STREAM.ContainsLoc(locList, locName);

            var att1 = ll.GroupBy(i => i.ActionPlan);
            if (att1 != null)
            {
                foreach (var cc in att1)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.ActionPlan += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p1 += cc.Count();
                }
            }

            var att2 = ll.GroupBy(i => i.InspectComplaint);
            if (att2 != null)
            {
                foreach (var cc in att2)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.InspectComplaint += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p2 += cc.Count();
                }
            }

            var att3 = ll.GroupBy(i => i.InspectTypeReason);
            if (att3 != null)
            {
                foreach (var cc in att3)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.InspectTypeReason += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p3 += cc.Count();
                }
            }

            var att4 = ll.GroupBy(i => i.NoFinding);
            if (att4 != null)
            {
                foreach (var cc in att4)
                {
                    model.NoFinding += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p4 += cc.Count();
                }
            }

            var att5 = ll.GroupBy(i => i.AssistLivReg);
            if (att5 != null)
            {
                foreach (var cc in att5)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.AssistLivReg += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p5 += cc.Count();
                }
            }

            var att6 = ll.GroupBy(i => i.AssistLivAct);
            if (att6 != null)
            {
                foreach (var cc in att6)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.AssistLivAct += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p6 += cc.Count();
                }
            }

            var att7 = ll.GroupBy(i => i.ActOrReg);
            if (att7 != null)
            {
                foreach (var cc in att7)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.ActOrReg += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p7 += cc.Count();
                }
            }

            var att8 = ll.GroupBy(i => i.SubActOrReg);
            if (att8 != null)
            {
                foreach (var cc in att8)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.SubActOrReg += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p8 += cc.Count();
                }
            }

            var att9 = ll.GroupBy(i => i.Category);
            if (att9 != null)
            {
                foreach (var cc in att9)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Category += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p9 += cc.Count();
                }
            }

            var att10 = ll.GroupBy(i => i.BriefDescOfFinding);
            if (att10 != null)
            {
                foreach (var cc in att10)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.BriefDescOfFinding += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p10 += cc.Count();
                }
            }

            var att11 = ll.GroupBy(i => i.Responsibility);
            if (att11 != null)
            {
                foreach (var cc in att11)
                {
                    model.Responsibility += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p11 += cc.Count();
                }
            }    

            Counters.allp1 += Counters.p1; Counters.allp2 += Counters.p2; Counters.allp3 += Counters.p3;
            Counters.allp4 += Counters.p4; Counters.allp5 += Counters.p5; Counters.allp6 += Counters.p6;
            Counters.allp7 += Counters.p7; Counters.allp8 += Counters.p8; Counters.allp9 += Counters.p9;
            Counters.allp10 += Counters.p10; Counters.allp11 += Counters.p11; 
            foundSummary.Add(model);
            model = new AssistLivInspectSummary();
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
            allSummary.Add(new AssistLivInspectSummaryAll()
            {
                ActionPlan = Counters.allp1,
                InspectComplaint = Counters.allp2,
                InspectTypeReason = Counters.allp3,
                NoFinding = Counters.allp4,
                AssistLivReg = Counters.allp5,
                AssistLivAct = Counters.allp6,
                ActOrReg = Counters.allp7,
                SubActOrReg = Counters.allp8,
                Category = Counters.allp9,
                BriefDescOfFinding = Counters.allp10,
                Responsibility = Counters.allp11
            });
            #endregion
        }
        #endregion

        #region Clear all list for new search(for range):
        public static void ClearAllStatic()
        {
            checkRepead = false;
            foundSummary = new List<AssistLivInspectSummary>();
            allSummary = new List<AssistLivInspectSummaryAll>();
            locList = new List<string>();
            aa = new List<AssistedLivingInspectionDTO[]>();
            for (int i = 0; i < 11; i++) aa.Add(new AssistedLivingInspectionDTO[1]);
            Counters.Nullify();
            Counters.ResetAllP();
            Counters.ResetPCount();
        }
        #endregion
    }
}