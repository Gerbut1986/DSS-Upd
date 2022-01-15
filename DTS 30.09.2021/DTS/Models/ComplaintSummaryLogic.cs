namespace DTS.Models
{
    using DSS.BLL;
    using DSS.BLL.DTO;
    using System.Linq;
    using DSS.BLL.PartialModels;
    using System.Collections.Generic;

    public class ComplaintSummaryLogic
    {
        #region Fields:
        public static bool checkRepead = false;
        public static List<string> locList = new List<string>();
        public static ComplaintsSummary model;
        public static List<ComplaintsSummary> foundSummary = new List<ComplaintsSummary>();
        public static List<ComplaintsSummaryAll> allSummary = new List<ComplaintsSummaryAll>();
        public static List<Complaint_DTO[]> aa = new List<Complaint_DTO[]>();
        #endregion

        #region Checking Location:
        public static void CheckLocation()
        {
            Counters.Nullify();
            checkRepead = true;
            var tbl = TablesContainer.list2;
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
            foreach (var it in TablesContainer.list2)
            {
                var cc = listCommunity.Where(i => i.Id == it.Location).FirstOrDefault();
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
                        aa[i] = TablesContainer.list2.Where(loc => STREAM.GetLocNameById(loc.Location) == all[j]).ToArray();
        }
        #endregion

        #region Set 'Complaints' Statistic for each Location:
        static void ComplaintsStatistic(string locName, Complaint_DTO[] arr)
        {
            model = new ComplaintsSummary();
            List<Complaint_DTO> ll = arr.ToList();
            if (ll[0] == null) return;
            Counters.ResetPCount();
            model.LocationName = STREAM.ContainsLoc(locList, locName);

            var att1 = ll.GroupBy(i => i.WritenOrVerbal);
            if (att1 != null)
            {
                foreach (var cc in att1)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.WritenOrVerbal += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p1 += cc.Count();
                }
            }

            var att2 = ll.GroupBy(i => i.Receive_Directly);
            if (att2 != null)
            {
                foreach (var cc in att2)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Receive_Directly += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p2 += cc.Count();
                }
            }

            var att3 = ll.GroupBy(i => i.FromResident);
            if (att3 != null)
            {
                foreach (var cc in att3)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.FromResident += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p3 += cc.Count();
                }
            }

            var att4 = ll.GroupBy(i => i.ResidentName);
            if (att4 != null)
            {
                foreach (var cc in att4)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.ResidentName += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p4 += cc.Count();
                }
            }

            var att5 = ll.GroupBy(i => i.Department);
            if (att5 != null)
            {
                foreach (var cc in att5)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Department += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p5 += cc.Count();
                }
            }

            var att6 = ll.GroupBy(i => i.HomeArea);
            if (att6 != null)
            {
                foreach (var cc in att6)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.HomeArea += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p6 += cc.Count();
                }
            }

            var att7 = ll.GroupBy(i => i.BriefDescription);
            if (att7 != null)
            {
                foreach (var cc in att7)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.BriefDescription += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p7 += cc.Count();
                }
            }

            var att8 = ll.GroupBy(i => i.IsAdministration);
            if (att8 != null)
            {
                foreach (var cc in att8)
                {
                    model.IsAdministration += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p8 += cc.Count();
                }
            }

            var att9 = ll.GroupBy(i => i.CareServices);
            if (att9 != null)
            {
                foreach (var cc in att9)
                {
                    model.CareServices += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p9 += cc.Count();
                }
            }

            var att10 = ll.GroupBy(i => i.PalliativeCare);
            if (att10 != null)
            {
                foreach (var cc in att10)
                {
                    model.PalliativeCare += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p10 += cc.Count();
                }
            }

            var att11 = ll.GroupBy(i => i.Dietary);
            if (att11 != null)
            {
                foreach (var cc in att11)
                {
                    model.Dietary += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p11 += cc.Count();
                }
            }

            var att12 = ll.GroupBy(i => i.Housekeeping);
            if (att12 != null)
            {
                foreach (var cc in att12)
                {
                    model.Housekeeping += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p12 += cc.Count();
                }
            }

            var att13 = ll.GroupBy(i => i.Laundry);
            if (att13 != null)
            {
                foreach (var cc in att13)
                {
                    model.Laundry += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p13 += cc.Count();
                }
            }

            var att14 = ll.GroupBy(i => i.Maintenance);
            if (att14 != null)
            {
                foreach (var cc in att14)
                {
                    model.Maintenance += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p14 += cc.Count();
                }
            }

            var att15 = ll.GroupBy(i => i.Programs);
            if (att15 != null)
            {
                foreach (var cc in att15)
                {
                    model.Programs += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p15 += cc.Count();
                }
            }

            var att16 = ll.GroupBy(i => i.Admissions);
            if (att16 != null)
            {
                foreach (var cc in att16)
                {
                    model.Admissions += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p16 += cc.Count();
                }
            }

            var att17 = ll.GroupBy(i => i.Physician);
            if (att17 != null)
            {
                foreach (var cc in att17)
                {
                    model.Physician += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p17 += cc.Count();
                }
            }           

            var att18 = ll.GroupBy(i => i.Other);
            if (att18 != null)
            {
                foreach (var cc in att18)
                {
                    model.Other += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p18 += cc.Count();
                }
            }

            var att19 = ll.GroupBy(i => i.MOHLTCNotified);
            if (att19 != null)
            {
                foreach (var cc in att19)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.MOHLTCNotified += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p19 += cc.Count();
                }
            }

            var att20 = ll.GroupBy(i => i.CopyToVP);
            if (att20 != null)
            {
                foreach (var cc in att20)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.CopyToVP += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p20 += cc.Count();
                }
            }

            var att21 = ll.GroupBy(i => i.ResponseSent);
            if (att21 != null)
            {
                foreach (var cc in att21)
                {
                    model.ResponseSent += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p21 += cc.Count();
                }
            }

            var att22 = ll.GroupBy(i => i.ActionToken);
            if (att22 != null)
            {
                foreach (var cc in att22)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.ActionToken += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p22 += cc.Count();
                }
            }

            var att23 = ll.GroupBy(i => i.Resolved);
            if (att23 != null)
            {
                foreach (var cc in att23)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.Resolved += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p23 += cc.Count();
                }
            }

            var att24 = ll.GroupBy(i => i.MinistryVisit);
            if (att24 != null)
            {
                foreach (var cc in att24)
                {
                    string key = cc.Key == null ? "" : cc.Key.ToString();
                    if (key == "") continue;
                    else
                        model.MinistryVisit += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p24 += cc.Count();
                }
            }

            Counters.allp1 += Counters.p1; Counters.allp2 += Counters.p2; Counters.allp3 += Counters.p3;
            Counters.allp4 += Counters.p4; Counters.allp5 += Counters.p5; Counters.allp6 += Counters.p6;
            Counters.allp7 += Counters.p7; Counters.allp8 += Counters.p8; Counters.allp9 += Counters.p9;
            Counters.allp10 += Counters.p10; Counters.allp11 += Counters.p11; Counters.allp12 += Counters.p12;
            Counters.allp13 += Counters.p13; Counters.allp14 += Counters.p14; Counters.allp15 += Counters.p15;
            Counters.allp16 += Counters.p16; Counters.allp17 += Counters.p17; Counters.allp18 += Counters.p18;
            Counters.allp19 += Counters.p19; Counters.allp20 += Counters.p20; Counters.allp21 += Counters.p21;
            Counters.allp22 += Counters.p22; Counters.allp23 += Counters.p23; Counters.allp24 += Counters.p24;
            foundSummary.Add(model);
            model = new ComplaintsSummary();
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
            allSummary.Add(new ComplaintsSummaryAll
            {
                WritenOrVerbal = Counters.allp1,
                Receive_Directly = Counters.allp2,
                FromResident = Counters.allp3,
                ResidentName = Counters.allp4,
                Department = Counters.allp5,
                HomeArea = Counters.allp6,
                BriefDescription = Counters.allp7,
                IsAdministration = Counters.allp8,
                CareServices = Counters.allp9,
                PalliativeCare = Counters.allp10,
                Dietary = Counters.allp11,
                Housekeeping = Counters.allp12,
                Laundry = Counters.allp13,
                Maintenance = Counters.allp14,
                Programs = Counters.allp15,
                Admissions = Counters.allp16,
                Physician = Counters.allp17,
                Other = Counters.allp18,
                MOHLTCNotified = Counters.allp19,
                CopyToVP = Counters.allp20,
                ResponseSent = Counters.allp21,
                ActionToken = Counters.allp22,
                Resolved = Counters.allp23,
                MinistryVisit = Counters.allp24
            });
            #endregion
        }
        #endregion

        #region Clear all list for new search(for range):
        public static void ClearAllStatic()
        {
            checkRepead = false;
            foundSummary = new List<ComplaintsSummary>();
            allSummary = new List<ComplaintsSummaryAll>();
            locList = new List<string>();
            aa = new List<Complaint_DTO[]>();
            for (int i = 0; i < 11; i++)
                aa.Add(new Complaint_DTO[1]);
            Counters.p1 = Counters.p2 = Counters.p3 = Counters.p4 = Counters.p5 = Counters.p6 = Counters.p7 = Counters.p8 =
            Counters.p9 = Counters.p10 = Counters.p11 = Counters.p12 = Counters.p13 = Counters.p14 = Counters.p15 =
            Counters.p16 = Counters.p17 = Counters.p18 = Counters.p18 = Counters.p19 = Counters.p20 = Counters.p21 = Counters.p22 =
            Counters.p23 = Counters.p24 = Counters.p25 = Counters.p26 = 0;
            Counters.allp1 = Counters.allp2 = Counters.allp3 = Counters.allp4 = Counters.allp5 =
            Counters.allp6 = Counters.allp7 = Counters.allp8 = Counters.allp9 = Counters.allp10 = Counters.allp11 =
            Counters.allp12 = Counters.allp13 = Counters.allp14 = Counters.allp15 = Counters.allp16 = Counters.allp17 =
            Counters.allp18 = Counters.allp19 = Counters.allp20 = Counters.allp21 = Counters.allp22 = Counters.allp23 =
            Counters.allp24 = 0;
            Counters.Nullify();
        }
        #endregion
    }
}