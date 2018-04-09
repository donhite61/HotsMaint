using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormInhertTest
{
    public static class GV
    {
        private static ServerLocal serLoc;
        private static ServerWeb serWeb;
        private static Double servTimeDiff = 0;

        public static ServerLocal SerLoc {
            get
            {
                if (serLoc == null)
                    serLoc = new ServerLocal();

                return serLoc;
            }
        }
        public static ServerWeb SerWeb {
            get
            {
                if (serWeb == null)
                    serWeb = new ServerWeb();

                return serWeb;
            }
        }
        public static Double ServTimeDiff {
            get
            {
                if (servTimeDiff == 0)
                    servTimeDiff = SetWebServerTimeDifference();

                return servTimeDiff;
            }
        }

        public enum TblName
        {
            locations,
            vendors,
            vendProducts
        };
        public enum SvdVariables
        {
            ServTimeDiff,
            DummySavedVariable
        };

        public static double SetWebServerTimeDifference()
        {
            var sec = GV.SerWeb.GetServerOffsetInSeconds();
            if (sec == -0.0) { // if failed to get time diff
                var secString = GV.SerLoc.ReadVariableFromServer("ServTimeDiff"); // read stored time diff
                if (secString != null) {
                    double.TryParse(secString, out sec);
                }
                else
                    sec = -7200;
            }
            else  // if success write local
                GV.SerLoc.WriteVariableToServer(GV.SvdVariables.ServTimeDiff.ToString(), sec);

            return sec;
        }
    }
}
