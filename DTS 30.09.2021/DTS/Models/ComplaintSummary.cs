namespace DTS.Models
{
    using System;
    using DSS.BLL;
    using DSS.BLL.DTO;
    using System.Linq;
    using DSS.BLL.Interfaces;
    using DSS.BLL.PartialModels;
    using System.Collections.Generic;


    public class ComplaintSummary
    {
        #region Fields:
        public static bool checkRepead = false;
        static ComplaintsSummary model;
        public static List<string> locList = new List<string>();
        public static List<ComplaintsSummary> foundSummary2 = new List<ComplaintsSummary>();
        public static List<ComplaintsSummaryAll> allSummary2 = new List<ComplaintsSummaryAll>();
        public static List<Complaint_DTO> aa1, aa2, aa3, aa4, aa5, aa6, aa7, aa8, aa9, aa10, aa11;
        #endregion

        #region Constructor:
        static ComplaintSummary()
        {

        }
        #endregion

        #region Accounting of all existing Locations:
        public static void CountLocs(IEnumerable<IModel> list)
        {
            checkRepead = true;
            foreach (var cc in list)
            {
                Type type = cc.GetType();
                switch (GetModel(type))
                {
                    case 1:
                        var cr = (Critical_Incidents_DTO)cc;
                        CheckLocation(cr.Location);
                        break;
                    case 2:
                        var cl = (Complaint_DTO)cc;
                        CheckLocation(cl.Location);
                        break;
                    case 3:
                        var gn = (Good_News_DTO)cc;
                        CheckLocation(gn.Location);
                        break;
                }
            }
        }
        #endregion

        #region Checking Location:
        static void CheckLocation(int location)
        {
            if (STREAM.GetLocNameById(location).Contains("Altamont Care Community"))
                Counters.cnt1++;
            else if (STREAM.GetLocNameById(location).Contains("Astoria Retirement Residence"))
                Counters.cnt2++;
            else if (STREAM.GetLocNameById(location).Contains("Barnswallow Place Care Community"))
                Counters.cnt3++;
            else if (STREAM.GetLocNameById(location).Contains("Bearbrook Retirement Residence"))
                Counters.cnt4++;
            else if (STREAM.GetLocNameById(location).Contains("Bloomington Cove Care Community"))
                Counters.cnt5++;
            else if (STREAM.GetLocNameById(location).Contains("Bradford Valley Care Community"))
                Counters.cnt6++;
            else if (STREAM.GetLocNameById(location).Contains("Brookside Lodge"))
                Counters.cnt7++;
            else if (STREAM.GetLocNameById(location).Contains("Woodbridge Vista"))
                Counters.cnt8++;
            else if (STREAM.GetLocNameById(location).Contains("Norfinch"))
                Counters.cnt9++;
            else if (STREAM.GetLocNameById(location).Contains("Rideau"))
                Counters.cnt10++;
            else if (STREAM.GetLocNameById(location).Contains("Villa da Vinci"))
                Counters.cnt11++;
        }
        #endregion

        #region Get Model by number:
        static int GetModel(Type type)
        {
            switch (type.Name)
            {
                case "Critical_Incidents_DTO": return 1;
                case "Complaint_DTO": return 2;
                case "Good_News_DTO": return 3;
                case "": return 0; // to be continue
            }
            return 0;
        }
        #endregion

        #region Get locations without repeat:
        public static void GetDistinctList(List<Home_DTO> listCommunity)
        {
            ComplaintsSummary model = new ComplaintsSummary();
            var locDistinct = new HashSet<string>();
            var locId = new List<int>();
            foreach (var it in TablesContainer.list2)
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
            {
                if (locList[i].Contains("Altamont Care Community"))
                    locList[i] = locList[i] + " - " + Counters.cnt1;
                else if (locList[i].Contains("Astoria Retirement Residence"))
                    locList[i] = locList[i] + " - " + Counters.cnt2;
                else if (locList[i].Contains("Barnswallow Place Care Community"))
                    locList[i] = locList[i] + " - " + Counters.cnt3;
                else if (locList[i].Contains("Bearbrook Retirement Residence"))
                    locList[i] = locList[i] + " - " + Counters.cnt4;
                else if (locList[i].Contains("Bloomington Cove Care Community"))
                    locList[i] = locList[i] + " - " + Counters.cnt5;
                else if (locList[i].Contains("Bradford Valley Care Community"))
                    locList[i] = locList[i] + " - " + Counters.cnt6;
                else if (locList[i].Contains("Brookside Lodge"))
                    locList[i] = locList[i] + " - " + Counters.cnt7;
                else if (locList[i].Contains("Woodbridge Vista"))
                    locList[i] = locList[i] + " - " + Counters.cnt8;
                else if (locList[i].Contains("Norfinch"))
                    locList[i] = locList[i] + " - " + Counters.cnt9;
                else if (locList[i].Contains("Rideau"))
                    locList[i] = locList[i] + " - " + Counters.cnt10;
                else if (locList[i].Contains("Villa da Vinci"))
                    locList[i] = locList[i] + " - " + Counters.cnt11;
            }
        }
        #endregion

        #region Fill out lists ll1,ll2,ll3...ll11 existing locations:
        public static void FillOutLists()
        {
            for (var i = 0; i < locList.Count; i++)
            {
                if (locList[i].Contains("Altamont Care Community"))
                {
                    aa1 = TablesContainer.list2.Where
                 (loc => STREAM.GetLocNameById(loc.Location) == "Altamont Care Community\r\n").ToList();
                }
                else if (locList[i].Contains("Astoria Retirement Residence"))
                {
                    aa2 = TablesContainer.list2.Where
                       (loc => STREAM.GetLocNameById(loc.Location) == "Astoria Retirement Residence\r\n").ToList();
                }
                else if (locList[i].Contains("Barnswallow Place Care Community"))
                {
                    aa3 = TablesContainer.list2.Where
                  (loc => STREAM.GetLocNameById(loc.Location) == "Barnswallow Place Care Community\r\n").ToList();
                }
                else if (locList[i].Contains("Bearbrook Retirement Residence"))
                {
                    aa4 = TablesContainer.list2.Where
                  (loc => STREAM.GetLocNameById(loc.Location) == "Bearbrook Retirement Residence\r\n").ToList();
                }
                else if (locList[i].Contains("Bloomington Cove Care Community"))
                {
                    aa5 = TablesContainer.list2.Where
                   (loc => STREAM.GetLocNameById(loc.Location) == "Bloomington Cove Care Community\r\n").ToList();
                }
                else if (locList[i].Contains("Bradford Valley Care Community"))
                {
                    aa6 = TablesContainer.list2.Where
                    (loc => STREAM.GetLocNameById(loc.Location) == "Bradford Valley Care Community\r\n").ToList();
                }
                else if (locList[i].Contains("Brookside Lodge"))
                {
                   aa7 = TablesContainer.list2.Where
                   (loc => STREAM.GetLocNameById(loc.Location) == "Brookside Lodge\r\n").ToList();
                }
                else if (locList[i].Contains("Woodbridge Vista"))
                {
                    string retName = STREAM.GetLocNameById(8);
                    aa8 = TablesContainer.list2.Where
                    (loc => STREAM.GetLocNameById(loc.Location) == "Woodbridge Vista").ToList();
                }
                else if (locList[i].Contains("Norfinch"))
                {
                    aa9 = TablesContainer.list2.Where
                    (loc => STREAM.GetLocNameById(loc.Location) == "Norfinch\r\n").ToList();
                }
                else if (locList[i].Contains("Rideau"))
                {
                    aa10 = TablesContainer.list2.Where
                  (loc => STREAM.GetLocNameById(loc.Location) == "Rideau\r\n").ToList();
                }
                else if (locList[i].Contains("Villa da Vinci"))
                {
                    aa11 = TablesContainer.list2.Where
                    (loc => STREAM.GetLocNameById(loc.Location) == "Villa da Vinci\r\n").ToList();
                }
            }
        }
        #endregion

        #region Set Complaints attr Statistics for each Location:
        static void ComplaintStatistic(string locName, List<Complaint_DTO> ll, int counters)
        {
            model = new ComplaintsSummary();
            Counters.ResetPCount();
            Counters.ResetAllP();
            model.LocationName = locList.Find(i => i == locName + " - " + counters);

            var att1 = TablesContainer.list2.GroupBy(i => i.DateReceived);
            if (att1 != null)
            {
                foreach (var cc in att1)
                {
                    string key = cc.Key == null ? "NULL" : cc.Key.ToString();
                    if (key == "NULL") continue;
                    model.DateReceived += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p1 += cc.Count();
                }
            }

            var att2 = TablesContainer.list2.GroupBy(i => i.WritenOrVerbal);
            if (att2 != null)
            {
                foreach (var cc in att2)
                {
                    string key = cc.Key == null ? "NULL" : cc.Key.ToString();
                    if (key == "NULL") continue;
                    model.WritenOrVerbal += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p2 += cc.Count();
                }
            }

            var att3 = TablesContainer.list2.GroupBy(i => i.Receive_Directly);
            if (att3 != null)
            {
                foreach (var cc in att3)
                {
                    string key = cc.Key == null ? "NULL" : cc.Key.ToString();
                    if (key == "NULL") continue;
                    model.Receive_Directly += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p3 += cc.Count();
                }
            }

            var att4 = TablesContainer.list2.GroupBy(i => i.FromResident);
            if (att4 != null)
            {
                foreach (var cc in att4)
                {
                    string key = cc.Key == null ? "NULL" : cc.Key.ToString();
                    if (key == "NULL") continue;
                    model.FromResident += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p4 += cc.Count();
                }
            }

            var att5 = TablesContainer.list2.GroupBy(i => i.ResidentName);
            if (att5 != null)
            {
                foreach (var cc in att5)
                {
                    string key = cc.Key == null ? "NULL" : cc.Key.ToString();
                    if (key == "NULL") continue;
                    model.ResidentName += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p5 += cc.Count();
                }
            }

            var att6 = TablesContainer.list2.GroupBy(i => i.Department);
            if (att6 != null)
            {
                foreach (var cc in att6)
                {
                    model.Department += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p6 += cc.Count();
                }
            }

            var att7 = TablesContainer.list2.GroupBy(i => i.BriefDescription);
            if (att7 != null)
            {
                foreach (var cc in att7)
                {
                    string key = cc.Key == null ? "NULL" : cc.Key.ToString();
                    if (key == "NULL") continue;
                    model.BriefDescription += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p7 += cc.Count();
                }
            }

            var att8 = TablesContainer.list2.GroupBy(i => i.IsAdministration);
            if (att8 != null)
            {
                foreach (var cc in att8)
                {
                    //string key = cc.Key == null ? "NULL" : cc.Key.ToString();
                    //if (key == "NULL") continue;
                    model.IsAdministration += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p8 += cc.Count();
                }
            }

            var att9 = TablesContainer.list2.GroupBy(i => i.CareServices);
            if (att9 != null)
            {
                foreach (var cc in att9)
                {
                    model.CareServices += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p9 += cc.Count();
                }
            }

            var att10 = TablesContainer.list2.GroupBy(i => i.PalliativeCare);
            if (att10 != null)
            {
                foreach (var cc in att10)
                {
                    model.PalliativeCare += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p10 += cc.Count();
                }
            }

            var att11 = TablesContainer.list2.GroupBy(i => i.Dietary);
            if (att11 != null)
            {
                foreach (var cc in att11)
                {
                    model.Dietary += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p11 += cc.Count();
                }
            }

            var att12 = TablesContainer.list2.GroupBy(i => i.Housekeeping);
            if (att12 != null)
            {
                foreach (var cc in att2)
                {
                    string key = cc.Key == null ? "NULL" : cc.Key.ToString();
                    if (key == "NULL") continue;
                    model.Housekeeping += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p12 += cc.Count();
                }
            }

            var att13 = TablesContainer.list2.GroupBy(i => i.Laundry);
            if (att13 != null)
            {
                foreach (var cc in att13)
                {
                    model.Laundry += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p13 += cc.Count();
                }
            }

            var att14 = TablesContainer.list2.GroupBy(i => i.Maintenance);
            if (att14 != null)
            {
                foreach (var cc in att14)
                {
                    model.Maintenance += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p14 += cc.Count();
                }
            }

            var att15 = TablesContainer.list2.GroupBy(i => i.Programs);
            if (att15 != null)
            {
                foreach (var cc in att15)
                {
                    model.Programs += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p15 += cc.Count();
                }
            }

            var att16 = TablesContainer.list2.GroupBy(i => i.Physician);
            if (att16 != null)
            {
                foreach (var cc in att16)
                {
                    model.Physician += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p16 += cc.Count();
                }
            }         

            var att21 = TablesContainer.list2.GroupBy(i => i.Other);
            if (att21 != null)
            {
                foreach (var cc in att21)
                {
                    model.Other += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p21 += cc.Count();
                }
            }

            var att22 = TablesContainer.list2.GroupBy(i => i.MOHLTCNotified);
            if (att22 != null)
            {
                foreach (var cc in att22)
                {
                    model.MOHLTCNotified += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p22 += cc.Count();
                }
            }

            var att23 = TablesContainer.list2.GroupBy(i => i.CopyToVP);
            if (att23 != null)
            {
                foreach (var cc in att23)
                {
                    model.CopyToVP += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p23 += cc.Count();
                }
            }

            var att24 = TablesContainer.list2.GroupBy(i => i.ResponseSent);
            if (att15 != null)
            {
                foreach (var cc in att15)
                {
                    model.ResponseSent += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p24 += cc.Count();
                }
            }

            var att25 = TablesContainer.list2.GroupBy(i => i.ActionToken);
            if (att25 != null)
            {
                foreach (var cc in att25)
                {
                    model.ActionToken += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p25 += cc.Count();
                }
            }

            var att26 = TablesContainer.list2.GroupBy(i => i.Resolved);
            if (att26 != null)
            {
                foreach (var cc in att26)
                {
                    model.Resolved += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p26 += cc.Count();
                }
            }

            var att27 = TablesContainer.list2.GroupBy(i => i.MinistryVisit);
            if (att27 != null)
            {
                foreach (var cc in att27)
                {
                    model.MinistryVisit += $"{cc.Key}\t - \t{cc.Count()}" + " | "; Counters.p27 += cc.Count();
                }
            }

            Counters.allp1 += Counters.p1; Counters.allp2 += Counters.p2; Counters.allp3 += Counters.p3;
            Counters.allp4 += Counters.p4; Counters.allp5 += Counters.p5; Counters.allp6 += Counters.p6;
            Counters.allp7 += Counters.p7; Counters.allp8 += Counters.p8; Counters.allp19 += Counters.p9;
            Counters.allp10 += Counters.p10; Counters.allp11 += Counters.p11; Counters.allp12 += Counters.p12;

            foundSummary2.Add(model);
            model = new ComplaintsSummary();
        }
        #endregion

        #region Get All Stat Complaints
        public static void AllStatComplaint()
        {
            // Counters.ResetCtns();
            if (aa1 != null)
                ComplaintStatistic(NamesLocations()[1], aa1, Counters.cnt1);
            // 2nd Location:
            if (aa2 != null)
                ComplaintStatistic(NamesLocations()[2], aa2, Counters.cnt2);
            // 3rd Location:
            if (aa3 != null)
                ComplaintStatistic(NamesLocations()[3], aa3, Counters.cnt3);
            // 4rd Location:
            if (aa4 != null)
                ComplaintStatistic(NamesLocations()[4], aa4, Counters.cnt4);
            // 5th Location:
            if (aa5 != null)
                ComplaintStatistic(NamesLocations()[5], aa5, Counters.cnt5);
            // 6th Location:
            if (aa6 != null)
                ComplaintStatistic(NamesLocations()[6], aa6, Counters.cnt6);
            // 7th Location:
            if (aa7 != null)
                ComplaintStatistic(NamesLocations()[7], aa7, Counters.cnt7);
            // 8th Location:
            if (aa8 != null)
                ComplaintStatistic(NamesLocations()[8], aa8, Counters.cnt8);
            // 9th Location:
            if (aa9 != null)
                ComplaintStatistic(NamesLocations()[9], aa9, Counters.cnt9);
            // 10th Location:
            if (aa10 != null)
                ComplaintStatistic(NamesLocations()[10], aa10, Counters.cnt10);
            // 11th Location:
            if (aa11 != null)
                ComplaintStatistic(NamesLocations()[11], aa11, Counters.cnt11);

            #region Add All Summary quantity on List:
            allSummary2.Add(new ComplaintsSummaryAll
            {
                ActionToken = Counters.allp1,
                //Beautician = Counters.allp2,
                PalliativeCare = Counters.allp3,
                ResidentName = Counters.allp4,
                CareServices = Counters.allp5,
                FromResident = Counters.allp6,
                Receive_Directly = Counters.allp7,
                WritenOrVerbal = Counters.allp8,
                DateReceived = Counters.allp9,
                BriefDescription = Counters.allp10,
                CopyToVP = Counters.allp11,
                Department = Counters.allp12,
                //DentalCare = Counters.allp13,
                Dietary = Counters.allp14,
                //FootCare = Counters.allp15,
                Laundry = Counters.allp16,
                Maintenance = Counters.allp17,
                Housekeeping = Counters.allp18,
                Physician = Counters.allp19,
                Programs = Counters.allp20,
                MinistryVisit = Counters.allp21,
                Resolved = Counters.allp22,
                ResponseSent = Counters.allp23,
                IsAdministration = Counters.allp24,
                //Physio = Counters.allp25,
                MOHLTCNotified = Counters.allp26,
                Other = Counters.allp27
            });
            #endregion
        }
        #endregion
      
        #region Get array of all location names:
        public static string[] NamesLocations()
        {
            return new string[]
            {
                "",
                "Altamont Care Community\r\n",
                "Astoria Retirement Residence\r\n",
                "Barnswallow Place Care Community\r\n",
                "Bearbrook Retirement Residence\r\n",
                "Bloomington Cove Care Community\r\n",
                "Bradford Valley Care Community\r\n",
                "Brookside Lodge\r\n",
                "Woodbridge Vista\r\n",
                "Brookside Lodge\r\n",
                "Rideau\r\n",
                "Villa da Vinci\r\n"
            };
        }
        #endregion

        #region Clear all list for new search(for range):
        public static void ClearAllStatic()
        {
            checkRepead = false;
            foundSummary2 = new List<ComplaintsSummary>();
            allSummary2 = new List<ComplaintsSummaryAll>();
            locList = new List<string>();
            Counters.p1 = Counters.p2 = Counters.p3 = Counters.p4 = Counters.p5 = Counters.p6 = Counters.p7 = Counters.p8 =
            Counters.p9 = Counters.p10 = Counters.p11 = Counters.p12 = Counters.p13 = Counters.p14 = Counters.p15 =
            Counters.p16 = Counters.p17 = Counters.p18 = Counters.p18 = Counters.p19 = Counters.p20 = Counters.p21 = Counters.p22 =
            Counters.p23 = Counters.p24 = Counters.p25 = Counters.p26 = Counters.p27 = 0;
            aa1 = aa2 = aa3 = aa4 = aa5 = aa6 = aa7 = aa8 = aa9 = aa10 = aa11 = new List<Complaint_DTO>();
            Counters.allp1 = Counters.allp2 = Counters.allp3 = Counters.allp4 = Counters.allp5 = Counters.allp6 =
                Counters.allp7 = Counters.allp8 = Counters.allp9 = Counters.allp10 = Counters.allp11 =
            Counters.allp12 = Counters.allp13 = Counters.allp14 = Counters.allp15 = Counters.allp16 = Counters.allp17 =
            Counters.allp18 = Counters.allp19 = Counters.allp20 = Counters.allp21 = Counters.allp22 = Counters.allp23 =
            Counters.allp24 = Counters.allp25 = Counters.allp26 = Counters.allp27 =
            Counters.cnt1 = Counters.cnt2 = Counters.cnt3 = Counters.cnt4 = Counters.cnt5 = Counters.cnt6 =
            Counters.cnt7 = Counters.cnt8 = Counters.cnt9 = Counters.cnt10 = Counters.cnt11 = 0;
        }
        #endregion
    }
}