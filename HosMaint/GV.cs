using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotsMaint
{
    public static class GV
    {
        public static ServerLocal SerLoc = new ServerLocal();
        public static ServerWeb Serweb = new ServerWeb();
        public static DateTimeOffset TimeOffset;
        public enum TblName
        {
            locations,
            vendors,
            vendProducts
        };
    }
}
