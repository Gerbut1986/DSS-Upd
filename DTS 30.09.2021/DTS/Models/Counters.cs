namespace DTS.Models
{
    using System;
    using System.IO;
    using System.Collections.Generic;

    public class Counters
    {
        public static int allp1 = 0, allp2 = 0, allp3 = 0, allp4 = 0, allp5 = 0, allp6 = 0, allp7 = 0, allp8 = 0, allp9 = 0, allp10 = 0, allp11 = 0,
            allp12 = 0, allp13 = 0, allp14 = 0, allp15 = 0, allp16 = 0, allp17 = 0, allp18 = 0, allp19 = 0, allp20 = 0, allp21 = 0, allp22 = 0, allp23 = 0,
            allp24 = 0, allp25 = 0, allp26 = 0, allp27 = 0;
        public static int p1 = 0, p2 = 0, p3 = 0, p4 = 0, p5 = 0, p6 = 0, p7 = 0, p8 = 0, p9 = 0, p10 = 0, p11 = 0, p12 = 0, p13 = 0,
            p14 = 0, p15 = 0, p16 = 0, p17 = 0, p18 = 0, p19 = 0, p20 = 0, p21 = 0, p22 = 0, p23 = 0, p24 = 0, p25 = 0, p26 = 0, p27 = 0;
        public static int cnt1 = 0, cnt2 = 0, cnt3 = 0, cnt4 = 0, cnt5 = 0, cnt6 = 0, cnt7 = 0, cnt8 = 0, cnt9 = 0, cnt10 = 0, cnt11 = 0;
        public static int[] cnt = new int[47];

        public static void Nullify()
        {
            cnt = new int[47];
            for (int i = 0; i < cnt.Length; i++)
                cnt[i] = 0;
        }

        public static void ResetAllP() => allp1 = allp2 = allp3 = allp4 = allp5 = allp6 = allp7 = allp8 = allp9 = allp10 = allp11 =
            allp12 = allp13 = allp14 = allp15 = allp16 = allp17 = allp18 = allp19 = allp20 = allp21 = allp22 = 0;

        public static void ResetPCount() => p1 = p2 = p3 = p4 = p5 = p6 = p7 = p8 = p9 = p10 = p11 = p12 = p13 =
                                      p14 = p15 = p16 = p17 = p18 = p19 = p20 = p21 = p22 = p23 = p24 = p25 = p26 = p27 = 0;
    }

    public class UnmanageCode
    {
        static string path = AppDomain.CurrentDomain.BaseDirectory + "All_Locations.txt";

        public static string WriteLocToFileFromDB()
        {
            using (var sw = new StreamWriter(path))
            {
                foreach (var loc in AppSettings.homes)
                {
                    sw.WriteLine(loc.Full_Home_Name);
                }
                return "All Location was writed successfully!";
            }
        }

        #region Get array of all location names from txt file:
        public static string[] ReadLocFromFile()
        {
            var list = new List<string>();
            list.Add("");
            if (File.Exists(path))
            {
                list.AddRange(File.ReadAllLines(path));
                return list.ToArray();
            }
            return null;
        }
        #endregion

        public static void AddEmptDateToDB()
        {

        }
    }
}