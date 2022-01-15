namespace DTS.Models
{
    using DSS.BLL;
    using DSS.BLL.DTO;
    using System.Linq;
    using DSS.BLL.PartialModels;
    using System.Collections.Generic;
    using DSS.BLL.Interfaces;
    using System;

    public class SummaryLogic
    {
        #region Fields:
        public static bool checkRepead = false;
        public static CriticalIncidentSummary model;
        public static GoodNewsSummary model3 = new GoodNewsSummary();
        public static List<Good_News_DTO> gg1, gg2, gg3, gg4, gg5, gg6, gg7, gg8, gg9, gg10, gg11;
        public static List<string> locList = new List<string>();
        public static List<CriticalIncidentSummary> foundSummary1 = new List<CriticalIncidentSummary>();
        public static List<ComplaintsSummary> foundSummary2 = new List<ComplaintsSummary>();
        public static List<GoodNewsSummary> foundSummary3 = new List<GoodNewsSummary>();

        public static List<IncidentSummaryAll> allSummary1 = new List<IncidentSummaryAll>();
        public static List<ComplaintsSummaryAll> allSummary2 = new List<ComplaintsSummaryAll>();
        public static List<GoodNewsSummaryAll> allSummary3 = new List<GoodNewsSummaryAll>();

        public static List<Critical_Incidents_DTO> ll1, ll2, ll3, ll4, ll5, ll6, ll7, ll8, ll9, ll10, ll11;
        public static List<Complaint_DTO> aa1, aa2, aa3, aa4, aa5, aa6, aa7, aa8, aa9, aa10, aa11, aa12, aa13, aa14, aa15, aa16,
            aa17, aa18, aa19, aa20, aa21, aa22, aa23, aa24, aa25, aa26, aa27;

        #endregion

        #region Constructor:
        static SummaryLogic()
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
            CriticalIncidentSummary model = new CriticalIncidentSummary();
            var locDistinct = new HashSet<string>();
            var locId = new List<int>();
            foreach (var it in TablesContainer.list1)
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
                    ll1 = TablesContainer.list1.Where
                 (loc => STREAM.GetLocNameById(loc.Location) == "Altamont Care Community\r\n").ToList();
                }
                else if (locList[i].Contains("Astoria Retirement Residence"))
                {
                    ll2 = TablesContainer.list1.Where
                       (loc => STREAM.GetLocNameById(loc.Location) == "Astoria Retirement Residence\r\n").ToList();
                }
                else if (locList[i].Contains("Barnswallow Place Care Community"))
                {
                    ll3 = TablesContainer.list1.Where
                  (loc => STREAM.GetLocNameById(loc.Location) == "Barnswallow Place Care Community\r\n").ToList();
                }
                else if (locList[i].Contains("Bearbrook Retirement Residence"))
                {
                    ll4 = TablesContainer.list1.Where
                  (loc => STREAM.GetLocNameById(loc.Location) == "Bearbrook Retirement Residence\r\n").ToList();
                }
                else if (locList[i].Contains("Bloomington Cove Care Community"))
                {
                    ll5 = TablesContainer.list1.Where
                   (loc => STREAM.GetLocNameById(loc.Location) == "Bloomington Cove Care Community\r\n").ToList();
                }
                else if (locList[i].Contains("Bradford Valley Care Community"))
                {
                    ll6 = TablesContainer.list1.Where
                    (loc => STREAM.GetLocNameById(loc.Location) == "Bradford Valley Care Community\r\n").ToList();
                }
                else if (locList[i].Contains("Brookside Lodge"))
                {
                    ll7 = TablesContainer.list1.Where
                   (loc => STREAM.GetLocNameById(loc.Location) == "Brookside Lodge\r\n").ToList();
                }
                else if (locList[i].Contains("Woodbridge Vista"))
                {
                    string retName = STREAM.GetLocNameById(8);
                    ll8 = TablesContainer.list1.Where
                    (loc => STREAM.GetLocNameById(loc.Location) == "Woodbridge Vista").ToList();
                }
                else if (locList[i].Contains("Norfinch"))
                {
                    ll9 = TablesContainer.list1.Where
                    (loc => STREAM.GetLocNameById(loc.Location) == "Norfinch\r\n").ToList();
                }
                else if (locList[i].Contains("Rideau"))
                {
                    ll10 = TablesContainer.list1.Where
                  (loc => STREAM.GetLocNameById(loc.Location) == "Rideau\r\n").ToList();
                }
                else if (locList[i].Contains("Villa da Vinci"))
                {
                    ll11 = TablesContainer.list1.Where
                    (loc => STREAM.GetLocNameById(loc.Location) == "Villa da Vinci\r\n").ToList();
                }
            }
        }
        #endregion

        #region Set Statistics for each Location:
        static void IncidentsStatistic(string locName, List<Critical_Incidents_DTO> ll, int counters)
        {
            model = new CriticalIncidentSummary();
            Counters.ResetPCount();
            model.LocationName = locList.Find(i => i == locName + " - " + counters);
            var attr1 = ll.GroupBy(i => i.MOHLTC_Follow_Up);
            if (attr1 != null)
            {
                foreach (var cc in attr1)
                {
                    string key = cc.Key == null ? "NULL" : cc.Key.ToString();
                    if (key == "NULL") continue;
                    model.MOHLTC_Follow_Up += $"{key}\t - \t{cc.Count()}" + " | ";
                    Counters.p1 += cc.Count();
                }
            }

            var attr2 = ll.GroupBy(i => i.CIS_Initiated);
            if (attr2 != null)
            {
                foreach (var cc in attr2)
                {
                    string key = cc.Key == null ? "NULL" : cc.Key;
                    if (key == "NULL") continue;
                    model.CIS_Initiated += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p2 += cc.Count();
                }
            }

            var attr3 = ll.GroupBy(i => i.MOH_Notified);
            if (attr3 != null)
            {
                foreach (var cc in attr3)
                {
                    string key = cc.Key == null ? "NULL" : cc.Key;
                    if (key == "NULL") continue;
                    model.MOH_Notified += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p3 += cc.Count();
                }
            }

            var attr4 = ll.GroupBy(i => i.POAS_Notified);
            if (attr4 != null)
            {
                foreach (var d in attr4)
                {
                    string key = d.Key == null ? "NULL" : d.Key;
                    if (key == "NULL") continue;
                    model.POAS_Notified += $"{key}\t - \t{d.Count()}" + " | "; Counters.p4 += d.Count();
                }
            }

            var attr5 = ll.GroupBy(i => i.Police_Notified);
            if (attr5 != null)
            {
                foreach (var cc in attr5)
                {
                    string key = cc.Key == null ? "NULL" : cc.Key;
                    if (key == "NULL") continue;
                    model.Police_Notified += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p5 += cc.Count();
                }
            }


            var attr6 = ll.GroupBy(i => i.Quality_Improvement_Actions);
            if (attr6 != null)
            {
                foreach (var cc in attr6)
                {
                    string key = cc.Key == null ? "NULL" : cc.Key;
                    if (key == "NULL") continue;
                    model.Quality_Improvement_Actions += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p6 += cc.Count();
                }
            }

            var attr7 = ll.GroupBy(i => i.Risk_Locked);
            if (attr7 != null)
            {
                foreach (var cc in attr7)
                {
                    string key = cc.Key == null ? "NULL" : cc.Key;
                    if (key == "NULL") continue;
                    model.Risk_Locked += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p7 += cc.Count();
                }
            }

            var attr8 = ll.GroupBy(i => i.Brief_Description);
            if (attr8 != null)
            {
                foreach (var cc in attr8)
                {
                    string key = cc.Key == null ? "NULL" : cc.Key;
                    if (key == "NULL") continue;
                    model.Brief_Description += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p8 += cc.Count();
                }
            }

            var attr9 = ll.GroupBy(i => i.Care_Plan_Updated);
            if (attr9 != null)
            {
                foreach (var e in attr9)
                {
                    string key = e.Key == null ? "NULL" : e.Key;
                    if (key == "NULL") continue;
                    model.Care_Plan_Updated += $"{key}\t - \t{e.Count()}" + " | "; Counters.p9 += e.Count();
                }
            }

            var attr10 = ll.GroupBy(i => i.CI_Form_Number);
            if (attr10 != null)
            {
                int count = 0;
                foreach (var cc in attr10)
                {
                    string key = cc.Key == null ? "NULL" : cc.Key;
                    if (key == "NULL") continue;
                    count += cc.Count();
                }
                model.CI_Form_Number = $"All\t - \t{count}"; Counters.p10 += count;
            }

            var attr11 = ll.GroupBy(i => i.File_Complete);
            if (attr11 != null)
            {
                foreach (var cc in attr11)
                {
                    string key = cc.Key == null ? "NULL" : cc.Key;
                    if (key == "NULL") continue;
                    model.File_Complete += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p11 += cc.Count();
                }
            }

            var attr112 = ll.GroupBy(i => i.Follow_Up_Amendments);
            if (attr112 != null)
            {
                foreach (var cc in attr112)
                {
                    string key = cc.Key == null ? "NULL" : cc.Key;
                    if (key == "NULL") continue;
                    model.Follow_Up_Amendments += $"{key}\t - \t{cc.Count()}" + " | "; Counters.p12 += cc.Count();
                }
            }
            Counters.allp1 += Counters.p1; Counters.allp2 += Counters.p2; Counters.allp3 += Counters.p3;
            Counters.allp4 += Counters.p4; Counters.allp5 += Counters.p5; Counters.allp6 += Counters.p6;
            Counters.allp7 += Counters.p7; Counters.allp8 += Counters.p8; Counters.allp19 += Counters.p9;
            Counters.allp10 += Counters.p10; Counters.allp11 += Counters.p11; Counters.allp12 += Counters.p12;

            foundSummary1.Add(model);
            model = new CriticalIncidentSummary();
        }
        #endregion

        #region Call All Statistics:
        public static void AllStatIncident()
        {
            if (ll1 != null)
                IncidentsStatistic(NamesLocations()[1], ll1, Counters.cnt1);
            // 2nd Location:
            if (ll2 != null)
                IncidentsStatistic(NamesLocations()[2], ll2, Counters.cnt2);
            // 3rd Location:
            if (ll3 != null)
                IncidentsStatistic(NamesLocations()[3], ll3, Counters.cnt3);
            // 4rd Location:
            if (ll4 != null)
                IncidentsStatistic(NamesLocations()[4], ll4, Counters.cnt4);
            // 5th Location:
            if (ll5 != null)
                IncidentsStatistic(NamesLocations()[5], ll5, Counters.cnt5);
            // 6th Location:
            if (ll6 != null)
                IncidentsStatistic(NamesLocations()[6], ll6, Counters.cnt6);
            // 7th Location:
            if (ll7 != null)
                IncidentsStatistic(NamesLocations()[7], ll7, Counters.cnt7);
            // 8th Location:
            if (ll8 != null)
                IncidentsStatistic(NamesLocations()[8], ll8, Counters.cnt8);
            // 9th Location:
            if (ll9 != null)
                IncidentsStatistic(NamesLocations()[9], ll9, Counters.cnt9);
            // 10th Location:
            if (ll10 != null)
                IncidentsStatistic(NamesLocations()[10], ll10, Counters.cnt10);
            // 11th Location:
            if (ll11 != null)
                IncidentsStatistic(NamesLocations()[11], ll11, Counters.cnt11);

            #region Add All Summary quantity on List:
            Type type = new Critical_Incidents_DTO().GetType();
            AddAllonList(type);
            #endregion
        }
        #endregion

        #region AddAll Summary qty on Lists:
        static void AddAllonList(Type type)
        {
            switch (type.Name)
            {
                case "Critical_Incidents_DTO":
                    allSummary1.Add(new IncidentSummaryAll
                    {
                        MOHLTC_Follow_Up = Counters.allp1,
                        CIS_Initiated = Counters.allp2,
                        MOH_Notified = Counters.allp3,
                        POAS_Notified = Counters.allp4,
                        Police_Notified = Counters.allp5,
                        Quality_Improvement_Actions = Counters.allp6,
                        Risk_Locked = Counters.allp7,
                        Brief_Description = Counters.allp8,
                        Care_Plan_Updated = Counters.allp9,
                        CI_Form_Number = Counters.allp10,
                        File_Complete = Counters.allp11,
                        Follow_Up_Amendments = Counters.allp12
                    });
                    break;
                case "Complaint_DTO":

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
                    break;
            }
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
            foundSummary1 = new List<CriticalIncidentSummary>();
            allSummary1 = new List<IncidentSummaryAll>();
            foundSummary2 = new List<ComplaintsSummary>();
            allSummary2 = new List<ComplaintsSummaryAll>();
            foundSummary3 = new List<GoodNewsSummary>();
            allSummary3 = new List<GoodNewsSummaryAll>();
            locList = new List<string>();
            ll1 = ll2 = ll3 = ll4 = ll5 = ll6 = ll7 = ll8 = ll9 = ll10 = ll11 = new List<Critical_Incidents_DTO>();
            Counters.p1 = Counters.p2 = Counters.p3 = Counters.p4 = Counters.p5 = Counters.p6 = Counters.p7 = Counters.p8 =
            Counters.p9 = Counters.p10 = Counters.p11 = Counters.p12 = Counters.p13 = Counters.p14 = Counters.p15 =
            Counters.p16 = Counters.p17 = Counters.p18 = Counters.p18 = Counters.p19 = Counters.p20 = Counters.p21 = Counters.p22 =
            Counters.p23 = Counters.p24 = Counters.p25 = Counters.p26 = Counters.p27 = 0;
            aa1 = aa2 = aa3 = aa4 = aa5 = aa6 = aa7 = aa8 = aa9 = aa10 = aa11 = aa12 = aa13 = aa14 = aa15 = aa16 =
            aa17 = aa18 = aa19 = aa20 = aa21 = aa22 = aa23 = aa24 = aa25 = aa26 = aa27 = new List<Complaint_DTO>();
            gg1 = gg2 = gg3 = gg4 = gg5 = gg6 = gg7 = gg8 = gg9 = gg10 = gg11 = new List<Good_News_DTO>();
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
