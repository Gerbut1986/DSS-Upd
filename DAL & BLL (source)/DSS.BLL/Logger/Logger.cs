namespace DSS.BLL.Logger
{
    using System.IO;

    public class Logger
    {
        static int count = 0;

        static Logger() { count++; }

        static string path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("/LogFiles/Log_" + count));

        public static string Write(string msg)
        {
            var res = $"{System.DateTime.Now} => {msg}";
            using (StreamWriter sw = new StreamWriter(path, true)) 
                 sw.WriteLine($"{res}\n\n");
            return res;
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
