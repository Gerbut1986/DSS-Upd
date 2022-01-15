namespace DTS.Models
{
    using DSS.BLL;
    using System.Linq;
    using DSS.BLL.DTO;
    using DSS.BLL.PartialModels;
    using System.Collections.Generic;

    public class ImmunizationSummaryLogic
    {
        #region Fields:
        public static bool checkRepead = false;
        public static List<string> locList = new List<string>();
        public static ImmunizationSummary model;
        public static List<ImmunizationSummary> foundSummary = new List<ImmunizationSummary>();
        public static List<ImmunizationSummaryAll> allSummary = new List<ImmunizationSummaryAll>();
        public static List<Immunization_DTO[]> aa = new List<Immunization_DTO[]>();
        #endregion

        #region Checking Location:
        public static void CheckLocation()
        {
            Counters.Nullify();
            checkRepead = true;
            var tbl = TablesContainer.list11;
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
            foreach (var it in TablesContainer.list11)
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
                        aa[i] = TablesContainer.list11.Where(loc => STREAM.GetLocNameById(loc.Location)
                        == all[j]).ToArray();
        }
        #endregion

        #region Set 'Complaints' Statistic for each Location:
        static void ComplaintsStatistic(string locName, Immunization_DTO[] arr)
        {
            model = new ImmunizationSummary();
            List<Immunization_DTO> ll = arr.ToList();
            if (ll[0] == null) return;
            Counters.ResetPCount();
            model.LocationName = STREAM.ContainsLoc(locList, locName);

            var att1 = ll.GroupBy(i => i.Numb_Res_Comm);
            if (att1 != null)
            {
                foreach (var cc in att1)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Numb_Res_Comm += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p2 += cc.Count();
                }
            }

            var att2 = ll.GroupBy(i => i.Numb_Res_Immun);
            if (att2 != null)
            {
                foreach (var cc in att2)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Numb_Res_Immun += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p3 += cc.Count();
                }
            }

            var att3 = ll.GroupBy(i => i.Numb_Res_Not_Immun);
            if (att3 != null)
            {
                foreach (var cc in att3)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Numb_Res_Not_Immun += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p4 += cc.Count();
                }
            }

            var att4 = ll.GroupBy(i => i.Per_Res_Immun);
            if (att4 != null)
            {
                foreach (var cc in att4)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Per_Res_Immun += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p5 += cc.Count();
                }
            }

            var att5 = ll.GroupBy(i => i.Per_Res_Not_Immun);
            if (att5 != null)
            {
                foreach (var cc in att5)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Per_Res_Not_Immun += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p6 += cc.Count();
                }
            }

            Counters.allp1 += Counters.p1; Counters.allp2 += Counters.p2; Counters.allp3 += Counters.p3;
            Counters.allp4 += Counters.p4; Counters.allp5 += Counters.p5; 
            foundSummary.Add(model);
            model = new ImmunizationSummary();
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
            allSummary.Add(new ImmunizationSummaryAll()
            {
                Numb_Res_Comm = Counters.allp2,
                Numb_Res_Immun = Counters.allp3,
                Numb_Res_Not_Immun = Counters.allp4,
                Per_Res_Immun = Counters.allp5,
                Per_Res_Not_Immun = Counters.allp6
            });
            #endregion
        }
        #endregion

        #region Clear all list for new search(for range):
        public static void ClearAllStatic()
        {
            checkRepead = false;
            foundSummary = new List<ImmunizationSummary>();
            allSummary = new List<ImmunizationSummaryAll>();
            locList = new List<string>();
            aa = new List<Immunization_DTO[]>();
            for (int i = 0; i < 11; i++)
                aa.Add(new Immunization_DTO[1]);
            Counters.p1 = Counters.p2 = Counters.p3 = Counters.p4 = Counters.p5 = 0;
            Counters.allp1 = Counters.allp2 = Counters.allp3 = Counters.allp4 = Counters.allp5 = 0;
            Counters.Nullify();
        }
        #endregion
    }
}