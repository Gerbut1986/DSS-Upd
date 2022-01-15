namespace DTS.Models
{
    using DSS.BLL;
    using System.Linq;
    using DSS.BLL.DTO;
    using DSS.BLL.PartialModels;
    using System.Collections.Generic;

    public class EducationSummaryLogic
    {
        #region Fields:
        public static bool checkRepead = false;
        public static List<string> locList = new List<string>();
        public static EducationSummary model;
        public static List<EducationSummary> foundSummary = new List<EducationSummary>();
        public static List<EducationSummaryAll> allSummary = new List<EducationSummaryAll>();
        public static List<Education_DTO[]> aa = new List<Education_DTO[]>();
        #endregion

        #region Checking Location:
        public static void CheckLocation()
        {
            Counters.Nullify();
            checkRepead = true;
            var tbl = TablesContainer.list9;
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
            foreach (var it in TablesContainer.list9)
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
                        aa[i] = TablesContainer.list9.Where(loc => STREAM.GetLocNameById(loc.Location)
                        == all[j]).ToArray();
        }
        #endregion

        #region Set 'Complaints' Statistic for each Location:
        static void ComplaintsStatistic(string locName, Education_DTO[] arr)
        {
            model = new EducationSummary();
            var ll = arr.ToList();
            if (ll[0] == null) return;
            Counters.ResetPCount();
            model.LocationName = STREAM.ContainsLoc                (locList, locName);

            var att1 = ll.GroupBy(i => i.Jan);
            if (att1 != null)
            {
                foreach (var cc in att1)
                {
                    string key = cc.Key == 0 ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Jan += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p2 += cc.Count();
                }
            }

            var att2 = ll.GroupBy(i => i.Feb);
            if (att2 != null)
            {
                foreach (var cc in att2)
                {
                    string key = cc.Key == 0 ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Feb += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p3 += cc.Count();
                }
            }

            var att3 = ll.GroupBy(i => i.Mar);
            if (att3 != null)
            {
                foreach (var cc in att3)
                {
                    string key = cc.Key == 0 ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Mar += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p4 += cc.Count();
                }
            }

            var att4 = ll.GroupBy(i => i.Apr);
            if (att4 != null)
            {
                foreach (var cc in att4)
                {
                    string key = cc.Key == 0 ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Apr += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p5 += cc.Count();
                }
            }

            var att5 = ll.GroupBy(i => i.May);
            if (att5 != null)
            {
                foreach (var cc in att5)
                {
                    string key = cc.Key == 0 ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.May += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p6 += cc.Count();
                }
            }

            var att6 = ll.GroupBy(i => i.Jun);
            if (att6 != null)
            {
                foreach (var cc in att6)
                {
                    string key = cc.Key == 0 ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Jun += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p7 += cc.Count();
                }
            }

            var att7 = ll.GroupBy(i => i.Jul);
            if (att7 != null)
            {
                foreach (var cc in att7)
                {
                    string key = cc.Key == 0 ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Jul += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p8 += cc.Count();
                }
            }

            var att8 = ll.GroupBy(i => i.Aug);
            if (att8 != null)
            {
                foreach (var cc in att8)
                {
                    string key = cc.Key == 0 ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Aug += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p9 += cc.Count();
                }
            }

            var att9 = ll.GroupBy(i => i.Sep);
            if (att9 != null)
            {
                foreach (var cc in att9)
                {
                    string key = cc.Key == 0 ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Sep += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p10 += cc.Count();
                }
            }

            var att10 = ll.GroupBy(i => i.Oct);
            if (att10 != null)
            {
                foreach (var cc in att10)
                {
                    string key = cc.Key == 0 ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Oct += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p11 += cc.Count();
                }
            }

            var att11 = ll.GroupBy(i => i.Nov);
            if (att11 != null)
            {
                foreach (var cc in att11)
                {
                    string key = cc.Key == 0 ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Nov += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p12 += cc.Count();
                }
            }

            var att12 = ll.GroupBy(i => i.Dec);
            if (att12 != null)
            {
                foreach (var cc in att12)
                {
                    string key = cc.Key == 0 ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Dec += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p13 += cc.Count();
                }
            }

            var att13 = ll.GroupBy(i => i.Total_Numb_Educ);
            if (att13 != null)
            {
                foreach (var cc in att13)
                {
                    string key = cc.Key == 0 ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Total_Numb_Educ += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p13 += cc.Count();
                }
            }

            var att14 = ll.GroupBy(i => i.Total_Numb_Eligible);
            if (att14 != null)
            {
                foreach (var cc in att14)
                {
                    string key = cc.Key == 0 ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Total_Numb_Eligible += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p13 += cc.Count();
                }
            }

            var att15 = ll.GroupBy(i => i.Approx_Per_Educated);
            if (att15 != null)
            {
                foreach (var cc in att15)
                {
                    string key = cc.Key == 0 ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Approx_Per_Educated += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p13 += cc.Count();
                }
            }

            Counters.allp1 += Counters.p1; Counters.allp2 += Counters.p2; Counters.allp3 += Counters.p3;
            Counters.allp4 += Counters.p4; Counters.allp5 += Counters.p5; Counters.allp6 += Counters.p6;
            Counters.allp7 += Counters.p7; Counters.allp8 += Counters.p8; Counters.allp9 += Counters.p9;
            Counters.allp10 += Counters.p10; Counters.allp11 += Counters.p11; Counters.allp12 += Counters.p12;
            Counters.allp13 += Counters.p13; Counters.allp14 += Counters.p14; Counters.allp15 += Counters.p15;
            foundSummary.Add(model);
            model = new EducationSummary();
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
            allSummary.Add(new EducationSummaryAll()
            {
                Jan = Counters.allp2,
                Feb = Counters.allp3,
                Mar = Counters.allp4,
                Apr = Counters.allp5,
                May = Counters.allp6,
                Jun = Counters.allp7,
                Jul = Counters.allp8,
                Aug = Counters.allp9,
                Sep = Counters.allp10,
                Oct = Counters.allp11,
                Dec = Counters.allp12,
                Nov = Counters.allp13,
                Total_Numb_Educ = Counters.allp12,
                Total_Numb_Eligible = Counters.allp13,
                Approx_Per_Educated = Counters.allp12         
            });
            #endregion
        }
        #endregion

        #region Clear all list for new search(for range):
        public static void ClearAllStatic()
        {
            checkRepead = false;
            foundSummary = new List<EducationSummary>();
            allSummary = new List<EducationSummaryAll>();
            locList = new List<string>();
            aa = new List<Education_DTO[]>();
            for (int i = 0; i < 11; i++) aa.Add(new Education_DTO[1]);
            Counters.p1 = Counters.p2 = Counters.p3 = Counters.p4 = Counters.p5 = Counters.p6 = Counters.p7 = Counters.p8 =
            Counters.p9 = Counters.p10 = Counters.p11 = Counters.p12 = Counters.p13 = Counters.p14 = Counters.p15 = 0;
            Counters.allp1 = Counters.allp2 = Counters.allp3 = Counters.allp4 = Counters.allp5 =
            Counters.allp6 = Counters.allp7 = Counters.allp8 = Counters.allp9 = Counters.allp10 = Counters.allp11 =
            Counters.allp12 = Counters.allp13 = Counters.allp14 = Counters.allp15 = 0;
            Counters.Nullify();
        }
        #endregion
    }
}