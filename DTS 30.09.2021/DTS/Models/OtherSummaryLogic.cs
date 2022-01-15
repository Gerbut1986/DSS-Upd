//namespace DTS.Models
//{
//    using DSS.BLL;
//    using System.Linq;
//    using DSS.BLL.DTO;
//    using DSS.BLL.PartialModels;
//    using System.Collections.Generic;

//    public class OtherSummaryLogic
//    {
//        #region Fields:
//        public static bool checkRepead = false;
//        public static List<string> locList = new List<string>();
//        public static OtherSummary model;
//        public static List<OtherSummary> foundSummary = new List<OtherSummary>();
//        public static List<OtherSummaryAll> allSummary = new List<OtherSummaryAll>();
//        public static List<OtherDTO[]> aa = new List<OtherDTO[]>();
//        #endregion

//        #region Checking Location:
//        public static void CheckLocation()
//        {
//            Counters.Nullify();
//            checkRepead = true;
//            var tbl = TablesContainer.list15;
//            for (int i = 0; i < tbl.Count; i++)
//                for (int j = 1; j < UnmanageCode.ReadLocFromFile().Length; j++)
//                    if (STREAM.GetLocNameById(tbl[i].Location).Contains(UnmanageCode.ReadLocFromFile()[j]))
//                        Counters.cnt[j - 1]++;
//        }
//        #endregion

//        #region Get locations without repeat:
//        public static void GetDistinctList(List<Home_DTO> listCommunity)
//        {
//            var locDistinct = new HashSet<string>();
//            var locId = new List<int>();
//            foreach (var it in TablesContainer.list15)
//            {
//                var cc = listCommunity.Where(i => i.Id == it.Location).SingleOrDefault();
//                locDistinct.Add(cc.Full_Home_Name);
//                locId.Add(cc.Id);
//            }

//            locList = locDistinct.ToList();
//            locList.Sort(); // Sorted by alphanumeric
//        }
//        #endregion

//        #region Add count location for each exist:
//        public static void AddCntLoc()
//        {
//            for (var i = 0; i < locList.Count; i++)
//                for (int j = 1; j < UnmanageCode.ReadLocFromFile().Length; j++)
//                    if (locList[i].Contains(UnmanageCode.ReadLocFromFile()[j]))
//                        locList[i] = locList[i] + " - " + Counters.cnt[j - 1];
//        }
//        #endregion

//        #region Fill out lists aa1, aa2, aa3... aa11 existing locations:
//        public static void FillOutLists()
//        {
//            var all = UnmanageCode.ReadLocFromFile();
//            for (var i = 0; i < locList.Count; i++)
//                for (int j = 1; j < all.Length; j++)
//                    if (locList[i].Contains(all[j]))
//                        aa[i] = TablesContainer.list15.Where(loc => STREAM.GetLocNameById(loc.Location)
//                        == all[j]).ToArray();
//        }
//        #endregion

//        #region Set 'Complaints' Statistic for each Location:
//        static void ComplaintsStatistic(string locName, OtherDTO[] arr)
//        {
//            model = new OtherSummary();
//            List<OtherDTO> ll = arr.ToList();
//            if (ll[0] == null) return;
//            Counters.ResetPCount();
//            model.Location = STREAM.ContainsLoc(locList, locName);

//            var att1 = ll.GroupBy(i => i.Inspected_By);
//            if (att1 != null)
//            {
//                foreach (var cc in att1)
//                {
//                    string key = cc.Key == null ? "" : cc.Key.ToString();
//                    if (key == "") continue;
//                    else
//                        model.Inspected_By += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p1 += cc.Count();
//                }
//            }

//            var att2 = ll.GroupBy(i => i.Inspection_Number);
//            if (att2 != null)
//            {
//                foreach (var cc in att2)
//                {
//                    string key = cc.Key == null ? "" : cc.Key.ToString();
//                    if (key == "") continue;
//                    else
//                        model.Inspection_Number += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p2 += cc.Count();
//                }
//            }

//            var att3 = ll.GroupBy(i => i.Number_of_Violations);
//            if (att3 != null)
//            {
//                foreach (var cc in att3)
//                {
//                    string key = cc.Key == null ? "" : cc.Key.ToString();
//                    if (key == "") continue;
//                    else
//                        model.Number_of_Violations += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p3 += cc.Count();
//                }
//            }

//            var att4 = ll.GroupBy(i => i.Notes_Comments);
//            if (att4 != null)
//            {
//                foreach (var cc in att4)
//                {
//                    string key = cc.Key == null ? "" : cc.Key.ToString();
//                    if (key == "") continue;
//                    else
//                        model.Notes_Comments += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p4 += cc.Count();
//                }
//            }

//            Counters.allp1 += Counters.p1; Counters.allp2 += Counters.p2; Counters.allp3 += Counters.p3;
//            Counters.allp4 += Counters.p4; Counters.allp5 += Counters.p5;
//            foundSummary.Add(model);
//            model = new OtherSummary();
//        }
//        #endregion

//        #region Call All Statistics:
//        public static void AllStatIncident()
//        {
//            Counters.cnt = STREAM.DelZeros(Counters.cnt);
//            for (int i = 0; i < locList.Count; i++)
//                if (aa[i] != null)
//                {
//                    ComplaintsStatistic(locList[i], aa[i]);
//                }
//            #region Add All Summary quantity on List:
//            allSummary.Add(new OtherSummaryAll()
//            {
//                Inspected_By = Counters.allp2,
//                Inspection_Number = Counters.allp3,
//                Number_of_Violations = Counters.allp4,
//                Notes_Comments = Counters.allp5
//            });
//            #endregion
//        }
//        #endregion

//        #region Clear all list for new search(for range):
//        public static void ClearAllStatic()
//        {
//            checkRepead = false;
//            foundSummary = new List<OtherSummary>();
//            allSummary = new List<OtherSummaryAll>();
//            locList = new List<string>();
//            aa = new List<OtherDTO[]>();
//            for (int i = 0; i < 11; i++)
//                aa.Add(new OtherDTO[1]);
//            Counters.p1 = Counters.p2 = Counters.p3 = Counters.p4 = 0;
//            Counters.allp1 = Counters.allp2 = Counters.allp3 = Counters.allp4 = 0;
//            Counters.Nullify();
//        }
//        #endregion
//    }
//}