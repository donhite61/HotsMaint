using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace HotsMaint
{
    public abstract class Server
    {
        public abstract string ConnString { get; set; }

        public int ExecuteMySQLStmt(string sql)
        {
            using (var conn = new MySqlConnection(ConnString))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                var result = cmd.ExecuteNonQuery();
                return result;
            }
        }
    }

    public class ServerLocal : Server
    {
        public override string ConnString { get; set; }

        public ServerLocal()
        {
            ConnString = "server=localhost;user=root;database=hitephot_pos;port=3306;password=6716;";
        }
    }

    public class ServerWeb : Server
    {
        public override string ConnString { get; set; }

        public ServerWeb()
        {
            ConnString = "server=69.89.31.188;user=hitephot_don;database=hitephot_pos;port=3306;password=Hite1985;";
        }
    }
}
