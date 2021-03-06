namespace DTS.Models.RegionLogic
{
    using System.Linq;
    using DSS.BLL.DTO;
    using DSS.BLL.Services;
    using System.Collections.Generic;

    public class SearchBC_LTC
    {
        #region Searching and Fill in 
        public static List<BC_LTC_Reportable_Incidents_DTO> RegionByLocId(int regnumb, ServiceDSS Db, string[] arrRegs)
        {
            var list = new List<BC_LTC_Reportable_Incidents_DTO>();
            switch (regnumb)
            {
                case 3:
                    for (int o = 0; o < arrRegs.Length; o++)
                        list.AddRange(Db.ReadBC_LTC().Where(l => l.CareCommName == DSS.BLL.STREAM.GetIdLocByName(arrRegs[o])));
                    return list;
                case 4:
                    for (int o = 0; o < arrRegs.Length; o++)
                        list.AddRange(Db.ReadBC_LTC().Where(l => l.CareCommName == DSS.BLL.STREAM.GetIdLocByName(arrRegs[o])));
                    return list;
                case 5:
                    for (int o = 0; o < arrRegs.Length; o++)
                        list.AddRange(Db.ReadBC_LTC().Where(l => l.CareCommName == DSS.BLL.STREAM.GetIdLocByName(arrRegs[o])));
                    return list;
                case 6:
                    for (int o = 0; o < arrRegs.Length; o++)
                        list.AddRange(Db.ReadBC_LTC().Where(l => l.CareCommName == DSS.BLL.STREAM.GetIdLocByName(arrRegs[o])));
                    return list;
                case 7:
                    for (int o = 0; o < arrRegs.Length; o++)
                        list.AddRange(Db.ReadBC_LTC().Where(l => l.CareCommName == DSS.BLL.STREAM.GetIdLocByName(arrRegs[o])));
                    return list;
                case 10:
                    for (int o = 0; o < arrRegs.Length; o++)
                        list.AddRange(Db.ReadBC_LTC().Where(l => l.CareCommName == DSS.BLL.STREAM.GetIdLocByName(arrRegs[o])));
                    return list;
                case 12:
                    for (int o = 0; o < arrRegs.Length; o++)
                        list.AddRange(Db.ReadBC_LTC().Where(l => l.CareCommName == DSS.BLL.STREAM.GetIdLocByName(arrRegs[o])));
                    return list;
                default: return null;
            }
        }
        #endregion
    }
}