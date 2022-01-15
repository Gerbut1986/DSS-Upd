namespace DTS.Models.RegionLogic
{
    using System.IO;
    
    public class RegionFolder
    {
        public static string[] Extract(string path)
        {
            if (File.Exists(path))
                return File.ReadAllLines(path);
            else throw new System.Exception("This FileNamedoesn't exist...Try once again with correct path.");
        }
    }
}