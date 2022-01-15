namespace DTS.Models
{
    using DSS.BLL;
    using DSS.BLL.DTO;
    using System.Linq;
    using DSS.BLL.PartialModels;
    using System.Collections.Generic;

    public class GoodNewsSummaryLogic
    {
        #region Fields:
        public static bool checkRepead = false;
        public static List<string> locList = new List<string>();
        public static GoodNewsSummary model;
        public static List<GoodNewsSummary> foundSummary = new List<GoodNewsSummary>();
        public static List<GoodNewsSummaryAll> allSummary = new List<GoodNewsSummaryAll>();
        public static List<Good_News_DTO[]> aa = new List<Good_News_DTO[]>();
        #endregion

        #region Checking Location:
        public static void CheckLocation()
        {
            Counters.Nullify();
            checkRepead = true;
            var tbl = TablesContainer.list3;
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
            foreach (var it in TablesContainer.list3)
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
                        aa[i] = TablesContainer.list3.Where(loc => STREAM.GetLocNameById(loc.Location)
                        == all[j]).ToArray();
        }
        #endregion

        #region Set 'Good News' Statistic for each Location:
        static void ComplaintsStatistic(string locName, Good_News_DTO[] arr)
        {
            model = new GoodNewsSummary();
            List<Good_News_DTO> ll = arr.ToList();
            if (ll[0] == null) return;
            Counters.ResetPCount();
            model.LocationName = STREAM.ContainsLoc(locList, locName);

            var att1 = ll.GroupBy(i => i.Category);
            if (att1 != null)
            {
                foreach (var cc in att1)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Category += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p1 += cc.Count();
                }
            }

            var att2 = ll.GroupBy(i => i.Department);
            if (att2 != null)
            {
                foreach (var cc in att2)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Department += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p2 += cc.Count();
                }
            }

            var att3 = ll.GroupBy(i => i.SourceCompliment);
            if (att3 != null)
            {
                foreach (var cc in att3)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.SourceCompliment += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p3 += cc.Count();
                }
            }

            var att4 = ll.GroupBy(i => i.ReceivedFrom);
            if (att4 != null)
            {
                foreach (var cc in att4)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.ReceivedFrom += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p4 += cc.Count();
                }
            }

            var att5 = ll.GroupBy(i => i.Description_Complim);
            if (att5 != null)
            {
                foreach (var cc in att5)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Description_Complim += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p5 += cc.Count();
                }
            }

            var att6 = ll.GroupBy(i => i.Respect);
            if (att6 != null)
            {
                foreach (var cc in att6)
                {
                    model.Respect += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p6 += cc.Count();
                }
            }

            var att7 = ll.GroupBy(i => i.Passion);
            if (att7 != null)
            {
                foreach (var cc in att7)
                {
                    model.Passion += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p7 += cc.Count();
                }
            }

            var att8 = ll.GroupBy(i => i.Teamwork);
            if (att8 != null)
            {
                foreach (var cc in att8)
                {
                    model.Teamwork += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p8 += cc.Count();
                }
            }

            var att9 = ll.GroupBy(i => i.Responsibility);
            if (att9 != null)
            {
                foreach (var cc in att9)
                {
                    model.Responsibility += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p9 += cc.Count();
                }
            }

            var att10 = ll.GroupBy(i => i.Growth);
            if (att10 != null)
            {
                foreach (var cc in att10)
                {
                    model.Growth += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p10 += cc.Count();
                }
            }

            var att11 = ll.GroupBy(i => i.Compliment);
            if (att11 != null)
            {
                foreach (var cc in att1)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Compliment += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p11 += cc.Count();
                }
            }

            var att12 = ll.GroupBy(i => i.Spot_Awards);
            if (att12 != null)
            {
                foreach (var cc in att12)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Spot_Awards += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p12 += cc.Count();
                }
            }

            var att13 = ll.GroupBy(i => i.Awards_Details);
            if (att13 != null)
            {
                foreach (var cc in att13)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Awards_Details += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p13 += cc.Count();
                }
            }

            var att14 = ll.GroupBy(i => i.NameAwards);
            if (att14 != null)
            {
                foreach (var cc in att14)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.NameAwards += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p14 += cc.Count();
                }
            }

            var att15 = ll.GroupBy(i => i.Awards_Received);
            if (att15 != null)
            {
                foreach (var cc in att15)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Awards_Received += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p15 += cc.Count();
                }
            }

            var att16 = ll.GroupBy(i => i.Community_Inititives);
            if (att16 != null)
            {
                foreach (var cc in att16)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Community_Inititives += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p16 += cc.Count();
                }
            }

            Counters.allp1 += Counters.p1; Counters.allp2 += Counters.p2; Counters.allp3 += Counters.p3;
            Counters.allp4 += Counters.p4; Counters.allp5 += Counters.p5; Counters.allp6 += Counters.p6;
            Counters.allp7 += Counters.p7; Counters.allp8 += Counters.p8; Counters.allp9 += Counters.p9;
            Counters.allp10 += Counters.p10; Counters.allp11 += Counters.p11; Counters.allp12 += Counters.p12;
            Counters.allp13 += Counters.p13; Counters.allp14 += Counters.p14; Counters.allp15 += Counters.p15;
            Counters.allp16 += Counters.p16;
            foundSummary.Add(model);
            model = new GoodNewsSummary();
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
            allSummary.Add(new GoodNewsSummaryAll
            {
                Category = Counters.allp1,
                Department = Counters.allp2,
                SourceCompliment = Counters.allp3,
                ReceivedFrom = Counters.allp4,
                Description_Complim = Counters.allp5,
                Respect = Counters.allp6,
                Passion = Counters.allp7,
                Teamwork = Counters.allp8,
                Responsibility = Counters.allp9,
                Growth = Counters.allp10,
                Compliment = Counters.allp11,
                Spot_Awards = Counters.allp12,
                Awards_Details = Counters.allp13,
                NameAwards = Counters.allp14,
                Awards_Received = Counters.allp15,
                Community_Inititives = Counters.allp16,
            });
            #endregion
        }
        #endregion

        #region Clear all list for new search(for range):
        public static void ClearAllStatic()
        {
            checkRepead = false;
            foundSummary = new List<GoodNewsSummary>();
            allSummary = new List<GoodNewsSummaryAll>();
            locList = new List<string>();
            aa = new List<Good_News_DTO[]>();
            for (int i = 0; i < 11; i++)
                aa.Add(new Good_News_DTO[1]);
            Counters.p1 = Counters.p2 = Counters.p3 = Counters.p4 = Counters.p5 = Counters.p6 = Counters.p7 = Counters.p8 =
            Counters.p9 = Counters.p10 = Counters.p11 = Counters.p12 = Counters.p13 = Counters.p14 = Counters.p15 =
            Counters.p16 =
            Counters.allp1 = Counters.allp2 = Counters.allp3 = Counters.allp4 = Counters.allp5 =
            Counters.allp6 = Counters.allp7 = Counters.allp8 = Counters.allp9 = Counters.allp10 = Counters.allp11 =
            Counters.allp12 = Counters.allp13 = Counters.allp14 = Counters.allp15 = Counters.allp16 = Counters.allp17 =
            Counters.allp18 = Counters.allp19 = Counters.allp20 = Counters.allp21 = Counters.allp22 = Counters.allp23 =
            Counters.allp24 = Counters.allp25 = Counters.allp26 = 0;
            Counters.Nullify();
        }
        #endregion
    }
}