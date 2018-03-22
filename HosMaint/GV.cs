using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotsMaint
{
    public static class GV
    {
        public static ServerLocal SerLoc;
        public static ServerWeb SerWeb;

        public enum TblName
        {
            locations,
            vendors,
            vendProducts
        };

        public enum SvdVariables
        {
            ServTimeOffset,
            DummySavedVariable
        };
    }
}
