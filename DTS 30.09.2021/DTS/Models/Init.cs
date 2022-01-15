namespace DTS.Models
{
    using System.IO;
    using DTS.Controllers;
    using System.Configuration;
    using System.Collections.Generic;

    public class Init
    {
        readonly static StreamReader sr = default;
        public static System.String ConnectionStr => ConfigurationManager.ConnectionStrings["dssConnectionString"].ConnectionString;
        public static System.String ConnectionStrAdm => ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        #region Regions:Method read list regions from txt file:
        public static string[] GetLocByRegion(int region)
        {
            var list = new List<string>();
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "Regions\\";
            switch (region)
            {
                case 1:
                    path = path + RegionPath.Region1.ToString() + ".txt";
                    if (File.Exists(path)) 
                        list.AddRange(File.ReadAllLines(path));
                    return list.ToArray();
                case 2:
                    path = path + RegionPath.Region2.ToString() + ".txt";
                    if (File.Exists(path))
                        list.AddRange(File.ReadAllLines(path));
                    return list.ToArray();
                case 3:
                    path = path + RegionPath.Region3.ToString() + ".txt";
                    if (File.Exists(path))
                        list.AddRange(File.ReadAllLines(path));
                    return list.ToArray();
                case 4:
                    path = path + RegionPath.Region4.ToString() + ".txt";
                    if (File.Exists(path))
                        list.AddRange(File.ReadAllLines(path));
                    return list.ToArray();
                case 5:
                    path = path + RegionPath.Region5.ToString() + ".txt";
                    if (File.Exists(path))
                        list.AddRange(File.ReadAllLines(path));
                    return list.ToArray();
                case 6:
                    path = path + RegionPath.Region6.ToString() + ".txt";
                    if (File.Exists(path))
                        list.AddRange(File.ReadAllLines(path));
                    return list.ToArray();
                case 7:
                    path = path + RegionPath.Region7.ToString() + ".txt";
                    if (File.Exists(path))
                        list.AddRange(File.ReadAllLines(path));
                    return list.ToArray();
                default: return null;            
            }
            #endregion
        }
    }
}