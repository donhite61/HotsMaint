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
            //GV.SerLoc.SetServerOffset();
            //GV.SerWeb.SetServerOffset();
        }
    }
}
