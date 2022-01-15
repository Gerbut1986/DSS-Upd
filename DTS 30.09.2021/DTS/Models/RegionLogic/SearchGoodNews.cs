﻿namespace DTS.Models.RegionLogic
{    
    using System.Linq;
    using DSS.BLL.DTO;
    using DSS.BLL.Services;
    using System.Collections.Generic;

    public class SearchGoodNews
    {
        #region Searching and Fill in 
        public static List<Good_News_DTO> RegionByLocId(int regnumb, ServiceDSS Db, string[] arrRegs)
        {
            var list = new List<Good_News_DTO>();
            switch (regnumb)
            {
                case 3:
                    for (int o = 0; o < arrRegs.Length; o++)
                        list.AddRange(Db.ReadNews().Where(l => l.Location == DSS.BLL.STREAM.GetIdLocByName(arrRegs[o])));
                    return list;
                case 4:
                    for (int o = 0; o < arrRegs.Length; o++)
                        list.AddRange(Db.ReadNews().Where(l => l.Location == DSS.BLL.STREAM.GetIdLocByName(arrRegs[o])));
                    return list;
                case 5:
                    for (int o = 0; o < arrRegs.Length; o++)
                        list.AddRange(Db.ReadNews().Where(l => l.Location == DSS.BLL.STREAM.GetIdLocByName(arrRegs[o])));
                    return list;
                case 6:
                    for (int o = 0; o < arrRegs.Length; o++)
                        list.AddRange(Db.ReadNews().Where(l => l.Location == DSS.BLL.STREAM.GetIdLocByName(arrRegs[o])));
                    return list;
                case 7:
                    for (int o = 0; o < arrRegs.Length; o++)
                        list.AddRange(Db.ReadNews().Where(l => l.Location == DSS.BLL.STREAM.GetIdLocByName(arrRegs[o])));
                    return list;
                case 10:
                    for (int o = 0; o < arrRegs.Length; o++)
                        list.AddRange(Db.ReadNews().Where(l => l.Location == DSS.BLL.STREAM.GetIdLocByName(arrRegs[o])));
                    return list;
                case 12:
                    for (int o = 0; o < arrRegs.Length; o++)
                        list.AddRange(Db.ReadNews().Where(l => l.Location == DSS.BLL.STREAM.GetIdLocByName(arrRegs[o])));
                    return list;
                default: return null;
            }
        }
        #endregion
    }
}