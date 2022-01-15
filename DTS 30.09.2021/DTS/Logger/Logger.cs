namespace DTS.Logger
{
    using System.IO;

    public class Logger
    {
        //static int count = 0;

        //static Logger() { ++count; }

        private static string path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath($"/LogFiles/Log_{System.DateTime.Now}.txt"));

        public static string Write(string msg)
        {
            using (StreamWriter sw = new StreamWriter(path, true))
                sw.WriteLine($"{System.DateTime.Now} => {msg}");
            return msg;
        }

        public static string Read()
        {
            var res = string.Empty;
            using (StreamReader sr = new StreamReader(path))
                res = sr.ReadToEnd();
            return res;
        }
    }
}
