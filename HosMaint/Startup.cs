using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotsMaint
{
    public class Startup
    {
        public static void DoStartup()
        {
            GV.SerLoc = new ServerLocal();
            GV.SerWeb = new ServerWeb();

            GV.ServTimeDiff = GV.SerWeb.GetServerOffsetInSeconds();

            GV.SerLoc.WriteVariableToServer(GV.SvdVariables.ServTimeDiff.ToString(), GV.ServTimeDiff);
        }
    }
}
