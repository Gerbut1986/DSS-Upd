namespace DSS.BLL
{
    using DTO;
    using System;
    using System.IO;
    using System.Web;
    using System.Linq;
    using DSS.BLL.Services;
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity.Owin;

    public class STREAM
    {
        private static ServiceDSS db = default;
        public static List<Home_DTO> list = default;
        private static List<Position_DTO> listPos = default;
        public static List<CI_Category_Type_DTO> listCI = default;
        static List <Home_DTO> AllLocations { get; set; } = default;

        public STREAM(ServiceDSS Db)
        {
            db = Db;
            list = AllLocations = db.ReadHomes().ToList();
            listPos = db.ReadPositions().ToList();
            listCI = db.ReadCICategory().ToList();
            // System.Threading.Tasks.Task.FromResult(InitCICategoryFromAsync());  // before
           // InitCICategoryFrom(); // after
        }

        // Already exist on Counters class 'UnmanageCode'
        //public static string WriteLocToFile()
        //{
        //    using(var sw = new StreamWriter(path))
        //    {
        //        foreach(var loc in db.ReadHomes())
        //        {
        //            sw.WriteLine(loc.Full_Home_Name);
        //        }
        //        return "All Location was writed successfully!";
        //    }
        //}

        #region Get array of all location names from txt file:
        public static string[] ReadLocFromFile(string path)
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

        public string WriteTo_CSV(List<Critical_Incidents_DTO> doc)
        {
            var msg = string.Empty;
            var size = doc.Count;
            string path = @"C:\Users\ldinovich-Admin\Documents\2. DSS - WOR Project\DSS (WOR Compliants) - Copy\DSS (WOR Compliants)\DTS-v3\mycsv.csv";
            var locNames = new List<string>();
            //var list = db.ReadHomes().ToList();
            foreach (var it in list)
                locNames.Add(it.Full_Home_Name);
            using (TextWriter tw = new StreamWriter(path))
            {
                tw.WriteLine($"Id,Date,CI_Form_Number,CI_Category_Type,Location,Brief_Description,MOH_Notified,Police_Notified,POAS_Notified,Care_Plan_Updated," +
                        $"Quality_Improvement_Actions,MOHLTC_Follow_Up," +
                        $"CIS_Initiated,Follow_Up_Amendments,Risk_Locked,File_Complete");
                for (int i = 0; i < size; i++)
                {
                    tw.WriteLine($"{doc[i].Id},{doc[i].Date},{doc[i].CI_Form_Number},{doc[i].CI_Category_Type},{locNames[doc[i].Location - 1]},{doc[i].Brief_Description},{doc[i].MOH_Notified}," +
                        $"{doc[i].Police_Notified},{doc[i].POAS_Notified},{doc[i].Care_Plan_Updated}," +
                        $"{doc[i].Quality_Improvement_Actions},{doc[i].MOHLTC_Follow_Up}," +
                        $"{doc[i].CIS_Initiated},{doc[i].Follow_Up_Amendments},{doc[i].Risk_Locked},{doc[i].File_Complete}");
                }

                msg = "All found records within the specified data range were written into a file successfuly!";
            }
            return msg;
        }

        public string WriteTo2_CSV(List<Good_News_DTO> doc)
        {
            var msg = string.Empty;
            var size = doc.Count;
            string path = @"C:\Users\ldinovich-Admin\Documents\2. DSS - WOR Project\DSS (WOR Compliants) - Copy\DSS (WOR Compliants)\DTS-v3\good_news.csv";
            var locNames = new List<string>();
            //var list = db.ReadHomes().ToList();
            foreach (var it in list)
                locNames.Add(it.Full_Home_Name.Split(new char[] { ' ' }).Last());
            using (TextWriter tw = new StreamWriter(path))
            {
                tw.WriteLine($"Id,Location,DateNews,Category,Department,SourceCompliment,ReceivedFrom,Description_Complim,Respect,Passion," +
                        $"Teamwork,Responsibility,Growth,Compliment,Spot_Awards,Awards_Details,NameAwards,Awards_Received,Community_Inititives");
                for (int i = 0; i < size; i++)
                {
                    tw.WriteLine($"{doc[i].Id},{10099887766},{doc[i].DateNews},{doc[i].Category},{doc[i].Department},{doc[i].SourceCompliment}," +
                        $"{doc[i].ReceivedFrom},{doc[i].Description_Complim},{doc[i].Respect},{doc[i].Passion},{doc[i].Teamwork},{doc[i].Responsibility}," +
                        $"{doc[i].Growth},{doc[i].Compliment},{doc[i].Spot_Awards},{doc[i].Awards_Details},{doc[i].NameAwards},{doc[i].Awards_Received}," +
                        $"{doc[i].Community_Inititives}");
                }

                msg = "All found records within the specified data range were written into a file successfuly!";
            }
            return msg;
        }

        #region Incapsulations Confirmation method to allow enter:
        public static object CheckConfirm(ref SignInStatus result, bool isConfirm)
        {
            if (result == SignInStatus.Failure)
            {
                if (!isConfirm)
                    result = SignInStatus.Failure;
                else result = SignInStatus.Success;
            }
            return result.ToString();
        }
        #endregion

        #region Test method:
        public void WriteToCSV(List<Good_News_DTO> doc)
        {
            var locNames = new List<string>();
            var list = db.ReadHomes().ToList();
            foreach (var it in list)
                locNames.Add(it.Full_Home_Name);

            string path = @"C:\Users\ldinovich-Admin\Documents\2. DSS - WOR Project\DSS (WOR Compliants) - Copy\DSS (WOR Compliants)\DTS-v3\new_csv.csv";

            System.Data.DataSet _result = new System.Data.DataSet();
            _result.Tables.Add("GoodNews");
            _result.Tables["GoodNews"].Columns.Add("Id");
            _result.Tables["GoodNews"].Columns.Add("Location");
            _result.Tables["GoodNews"].Columns.Add("DateNews");
            _result.Tables["GoodNews"].Columns.Add("Category");
            _result.Tables["GoodNews"].Columns.Add("Department");
            _result.Tables["GoodNews"].Columns.Add("SourceCompliment");
            _result.Tables["GoodNews"].Columns.Add("ReceivedFrom");
            _result.Tables["GoodNews"].Columns.Add("Description_Complim");
            _result.Tables["GoodNews"].Columns.Add("Respect");
            _result.Tables["GoodNews"].Columns.Add("Passion");
            _result.Tables["GoodNews"].Columns.Add("Teamwork");
            _result.Tables["GoodNews"].Columns.Add("Responsibility");
            _result.Tables["GoodNews"].Columns.Add("Growth");
            _result.Tables["GoodNews"].Columns.Add("Compliment");
            _result.Tables["GoodNews"].Columns.Add("Spot_Awards");
            _result.Tables["GoodNews"].Columns.Add("Awards_Details");
            _result.Tables["GoodNews"].Columns.Add("NameAwards");
            _result.Tables["GoodNews"].Columns.Add("Awards_Received");
            _result.Tables["GoodNews"].Columns.Add("Community_Inititives");

            System.Data.DataRow newRow = _result.Tables["GoodNews"].NewRow();
            newRow["Id"] = "1";
            newRow["Location"] = locNames[doc[0].Location - 1];

            _result.Tables["GoodNews"].Rows.Add(newRow);

            string fileName = "exportData";
            System.Data.DataTable data = _result.Tables[0];

            HttpContext context = HttpContext.Current;

            context.Response.Clear();
            context.Response.ContentType = "text/csv";
            context.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".csv");


            // Write data
            TextWriter tw = new StreamWriter(path);
            foreach (System.Data.DataRow row in data.Rows)
            {
                foreach (System.Data.DataColumn col in data.Columns)
                {
                    tw.Write(row[col.ColumnName].ToString() + ",");
                    context.Response.Write(row[col.ColumnName].ToString() + ",");
                }
                tw.WriteLine(System.Environment.NewLine);
                context.Response.Write(System.Environment.NewLine);
            }
            tw.Close();
            context.Response.End();
        }
        #endregion

        #region Methods get Names of Location, CI_Category_Type, Positions:
        public static IEnumerable<string> GetLocNames()
        {
            var locNames = new List<string>();
            foreach (var it in list)
                locNames.Add(it.Full_Home_Name.Replace('\r', '\0').Replace('\n', '\0'));
            return locNames;
        }

        public static IEnumerable<string> GetCINames()
        {
            var ciNames = new List<string>();
            var list = listCI;
            foreach (var it in list)
                ciNames.Add(it.Name.Replace('\r', '\0').Replace('\n', '\0'));
            return ciNames;
        }

        public static IEnumerable<string> GetPosNames()
        {
            var posNames = new List<string>();
            var list = listPos;
            foreach (var it in list)
                posNames.Add(it.Name.Replace('\r', '\0').Replace('\n', '\0'));
            return posNames;
        }

        void InitCICategoryFrom()
        {
            listCI = db.ReadCICategory().ToList();
        }

        async System.Threading.Tasks.Task InitCICategoryFromAsync()
        {
            var l = await db.ReadCICategoryAsync();
            listCI = l.ToList();
        }

        public static string GetLocNameById(int id)
        {
            if (id == 0) throw new ArgumentNullException();
            else
            {
                var obj = AllLocations.Where(i => i.Id == id).FirstOrDefault();
                var name = obj.Full_Home_Name;
                return name;
            }
        }

        public static int GetIdLocByName(string nameLoc)
        {
            return LocationsDic[nameLoc];
        }

        static Dictionary<string, int> LocationsDic = new Dictionary<string, int>()
        {
            { "Altamont Care Community",1 },
  {"Barnswallow Place Care Community",2 },
  {"Bloomington Cove Care Community",3},
  {"Bradford Valley Care Community",4},
  {"Brookside Lodge Care Community",5},
  {"Camilla Care Community",6},
  {"Case Manor Care Community",7},
  {"Cedarvale Lodge Care Community",8},
  {"Cheltenham Care Community",9},
  {"Creedan Valley Care Community",10},
  {"Deerwood Creek Care Community",11},
  {"Fieldstone Commons Care Community",12},
  {"Fountain View Care Community",13},
  {"Fox Ridge Care Community",14},
  {"Glenmore Lodge Care Community",15},
  {"Granite Ridge Care Community",16},
  {"Harmony Hills Care Community",17},
  {"Hawthorn Woods Care Community",18},
  {"Lake Country Lodge Care Community",19},
  {"Lakeview Lodge Care Community",20},
  {"Langstaff Square Care Community",21},
  {"Madonna Care Community",22},
  {"Maple Grove Care Community",23},
  {"Mariposa Gardens Care Community",24},
  {"Midland Gardens Care Community",25},
  {"Muskoka Shores Care Community",26},
  {"Nicola Lodge Care Community",27},
  {"Norfinch Care Community",28},
  {"NYGH Seniors Health Centre",29},
  {"Owen Hill Care Community",30},
  {"Ridgeview Lodge Care Community",31},
  {"Rockcliffe Care Community",32},
  {"Secord Trails Care Community",33},
  {"Silverthorn Care Community",34},
  {"Spencer House Care Community",35},
  {"St. George Care Community",36},
  {"Streetsville Care Community",37},
  {"The Cascades Care Community",38},
  {"Trillium Care Community",39},
  {"Tullamore Care Community",40},
  {"Victoria Manor Care Community",41},
  {"Villa Leonardo Gambin Care Community",42},
  {"Waters Edge Care Community",43},
  {"Weston Terrace Care Community",44},
  {"Woodbridge Vista Care Community",45},
  {"Woodhall Park Care Community",46},
  {"Woods Park Care Centre",47}
        };
        #endregion

        #region Method to delete all zero from arr:
        public static int[] DelZeros(int[] arr)
        {
            var wout = new List<int>();
            foreach (var it in arr)
            {
                if (it == 0)
                    continue;
                else wout.Add(it);
            }
            return wout.ToArray();
        }
        #endregion

        #region REturn contains element from arr:
        public static string ContainsLoc(List<string> locs, string n)
        {
            foreach (var l in locs)
                if (l.Contains(n))
                    return l;
            return null;
        }
        #endregion
    }
}