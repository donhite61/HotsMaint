using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;


namespace HotsMaint
{
    public class Locations
    {
        string ConnString = "server=69.89.31.188;user=hitephot_don;database=hitephot_pos;port=3306;password=Hite1985;";
        public DataTable tbl { get; set; }

        public Locations()
        {
            tbl = GetDataTable("locations");
        }

        public DataTable GetDataTable(string _tableName)
        {
            string sql = "SELECT * FROM " + _tableName;
            var table = new DataTable();
            table.Columns.Add("Id", typeof(UInt32));
            table.Columns.Add("Code", typeof(string));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Address", typeof(string));
            table.Columns.Add("Address2", typeof(string));
            table.Columns.Add("City", typeof(string));
            table.Columns.Add("State", typeof(string));
            table.Columns.Add("Zip", typeof(string));
            table.Columns.Add("Phone", typeof(string));
            table.Columns.Add("Email", typeof(string));
            table.Columns.Add("Inactive", typeof(Boolean));

            using (var conn = new MySqlConnection(ConnString))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        var row = table.NewRow();
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
                        table.Rows.Add(row);
                    }
                    table.AcceptChanges();
                }
            }
            //todo get last table update time
            return table;
        }

        public UInt32 InsertRecord(DataRow _row)
        {
            string sql = "INSERT INTO locations " +
                    "(loc_Code,loc_Name,loc_Address,loc_Address2,loc_City,loc_State,loc_Zip,loc_Phone,loc_Email,loc_Inactive)" +
                    "Values(?Code,?Name,?Add,?Add2,?City,?State,?Zip,?Phone,?Email,?Inactive)";

            using (var conn = new MySqlConnection(ConnString))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("?Code", _row.ItemArray[1].ToString());
                cmd.Parameters.AddWithValue("?Name", _row[2].ToString());
                cmd.Parameters.AddWithValue("?Add", _row.ItemArray[3].ToString());
                cmd.Parameters.AddWithValue("?Add2", _row.ItemArray[4].ToString());
                cmd.Parameters.AddWithValue("?City", _row.ItemArray[5].ToString());
                cmd.Parameters.AddWithValue("?State", _row.ItemArray[6].ToString());
                cmd.Parameters.AddWithValue("?Zip", _row.ItemArray[7].ToString());
                cmd.Parameters.AddWithValue("?Phone", _row.ItemArray[8].ToString());
                cmd.Parameters.AddWithValue("?Email", _row.ItemArray[9].ToString());
                cmd.Parameters.AddWithValue("?Inactive", Convert.ToBoolean(_row.ItemArray[10]));

                conn.Open();
                cmd.ExecuteNonQuery();
                UInt32 mySqlId = Convert.ToUInt32(cmd.LastInsertedId);
                return mySqlId;
                //update sqlchangetable
            }
        }

        public bool UpdateRecord(DataRow _row)
        {
            string sql = "UPDATE locations " +
                    "SET loc_Code=?Code,loc_Name=?Name,loc_Address=?Add,loc_Address2=?Add2,loc_City=?City," +
                    "loc_State=?State,loc_Zip=?Zip,loc_Phone=?Phone,loc_Email=?Email,loc_Inactive=?Inactive " +
                    "WHERE loc_Id = ?Id";

            using (var conn = new MySqlConnection(ConnString))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("?Id", Convert.ToUInt32(_row.ItemArray[0]));
                cmd.Parameters.AddWithValue("?Code", _row.ItemArray[1].ToString());
                cmd.Parameters.AddWithValue("?Name", _row[2].ToString());
                cmd.Parameters.AddWithValue("?Add", _row.ItemArray[3].ToString());
                cmd.Parameters.AddWithValue("?Add2", _row.ItemArray[4].ToString());
                cmd.Parameters.AddWithValue("?City", _row.ItemArray[5].ToString());
                cmd.Parameters.AddWithValue("?State", _row.ItemArray[6].ToString());
                cmd.Parameters.AddWithValue("?Zip", _row.ItemArray[7].ToString());
                cmd.Parameters.AddWithValue("?Phone", _row.ItemArray[8].ToString());
                cmd.Parameters.AddWithValue("?Email", _row.ItemArray[9].ToString());
                cmd.Parameters.AddWithValue("?Inactive", Convert.ToBoolean(_row.ItemArray[10]));

                conn.Open();
                cmd.ExecuteNonQuery();
                //update sqlchangetable
            }
            return true;
        }

        public bool DeleteRecord(UInt32 _id)
        {
            //todo
            //if Child records exist
            // e.Cancel = true
            string sql = "DELETE FROM locations WHERE loc_Id = ?Id";

            using (var conn = new MySqlConnection(ConnString))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("?Id", _id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return true;
        }

        public string GetNameFromCode(string code)
        {
            string sql = "SELECT loc_Description FROM locations WHERE loc_Code = ?code LIMIT 1";
            using (var conn = new MySqlConnection(ConnString))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("?Code", code);
                conn.Open();
                var result = cmd.ExecuteScalar();
                return result == null ? null : Convert.ToString(cmd.ExecuteScalar());
            }
        }

        private static string SafeGetString(MySqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            return string.Empty;
        }
        private static bool SafeGetBool(MySqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetBoolean(colIndex);
            return false;
        }
    }
}
