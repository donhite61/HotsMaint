using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace HotsMaint
{
    public abstract class Server
    {
        public abstract string ConnString { get; set; }

        internal object ExecuteMySQLNonQuery(MySqlCommand cmd)
        {
            using (cmd.Connection = new MySqlConnection(ConnString))
            using (cmd)
            {
                cmd.Connection.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        internal object ExecuteMySqlScaler(MySqlCommand cmd)
        {
            using (cmd.Connection = new MySqlConnection(ConnString))
            using (cmd)
            {
                cmd.Connection.Open();
                return cmd.ExecuteScalar();
            }
        }

        internal DataTable FillTable(DataTable tbl)
        {
            tbl.Clear();
            var sql = "SELECT * FROM " + tbl.TableName;

            using (var conn = new MySqlConnection(ConnString))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    int count = rd.FieldCount;
                    while (rd.Read())
                    {
                        var row = tbl.NewRow();
                        for (var i = 0; i < count; i++)
                        {
                            row[0] = rd.GetUInt32(0);
                            row[1] = SafeGetString(rd, 1);
                            row[2] = SafeGetString(rd, 2);
                            row[3] = SafeGetString(rd, 3);
                            row[4] = SafeGetString(rd, 4);
                            row[5] = SafeGetString(rd, 5);
                            row[6] = SafeGetString(rd, 6);
                            row[7] = SafeGetString(rd, 7);
                            row[8] = SafeGetString(rd, 8);
                            row[9] = SafeGetString(rd, 9);
                            row[10] = SafeGetBool(rd, 10);
                            row[11] = rd.GetDateTime(11);
                        }
                        tbl.Rows.Add(row);
                    }
                    tbl.AcceptChanges();
                }
            }
            return tbl;
        }

        public string SafeGetString(MySqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            return string.Empty;
        }
        public bool SafeGetBool(MySqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetBoolean(colIndex);
            return false;
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
