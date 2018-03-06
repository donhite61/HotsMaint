using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotsMaint
{
    class Sql
    {
        static string ConnString = "server=69.89.31.188;user=hitephot_don;database=hitephot_pos;port=3306;password=Hite1985;";

        public static DateTime GetTableUpdateTime(string tblName)
        {
            using (var cmd = new MySqlCommand())
            {
                cmd.CommandText = "SELECT upd_UpdateTime FROM tableUpdateTime WHERE upd_TblName = ?tblName";
                cmd.Connection = new MySqlConnection(ConnString);
                cmd.Parameters.AddWithValue("?tblName", tblName);
                cmd.Connection.Open();
                var result = cmd.ExecuteScalar().ToString();
                DateTime GetTableUpdateTime;
                DateTime.TryParse(result, out GetTableUpdateTime);
                return GetTableUpdateTime;
            }
        }

        public static DateTime SetTableUpdateTime(string tblName)
        {
            using (var cmd = new MySqlCommand())
            {
                cmd.CommandText = "SELECT NOW() FROM DUAL";
                cmd.Connection = new MySqlConnection(ConnString);
                cmd.Parameters.AddWithValue("?tblName", tblName);
                cmd.Connection.Open();
                var result = cmd.ExecuteScalar().ToString();
                DateTime GetTableUpdateTime;
                DateTime.TryParse(result, out GetTableUpdateTime);
                return GetTableUpdateTime;
            }
        }

        public static bool TableNeedsRefresh(ITable _table)
        {
            var locTime = _table.TblUpdTime;
            var webTime = GetTableUpdateTime(_table.DataSet.Tables[0].TableName);
            if(locTime != webTime)
            {
                _table.DataSet.Tables[0].Clear();
                FillTable(_table.DataSet.Tables[0]);
                _table.TblUpdTime = GetTableUpdateTime(_table.DataSet.Tables[0].TableName);
                return true;
            }
            return false;
        }

        public static DataTable FillTable(DataTable tbl)
        {
            tbl.Clear();
            using (var cmd = new MySqlCommand())
            {
                cmd.CommandText = "SELECT * FROM "+tbl.TableName;
                cmd.Connection = new MySqlConnection(ConnString);
                cmd.Connection.Open();
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        var row = tbl.NewRow();
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
                        tbl.Rows.Add(row);
                    }
                    tbl.AcceptChanges();
                }
            }

            return tbl;
        }

        public static UInt32 InsertRecord(MySqlCommand cmd)
        {
            try
            {
                using (cmd)
                {
                    cmd.Connection = new MySqlConnection(ConnString);
                    cmd.Connection.Open();
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        UInt32 mySqlId = Convert.ToUInt32(cmd.LastInsertedId);
                        return mySqlId;
                    }
                    return 0;
                }
            }
            catch (MySqlException e)
            {
                System.Windows.Forms.MessageBox.Show("WebServer Error, The record was not addded"+ "\r\n"+ "\r\n"+e);
                return 0;
            }
        }

        public static bool UpdateRecord(MySqlCommand cmd)
        {
            try
            {
                using (cmd)
                {
                    cmd.Connection = new MySqlConnection(ConnString);
                    cmd.Connection.Open();
                    if (cmd.ExecuteNonQuery() == 1)
                        return true;
                }
                return false;
            }
            catch (MySqlException e)
            {
                System.Windows.Forms.MessageBox.Show("WebServer Error, The record was not saved" + "\r\n" + "\r\n" + e);
                return false;
            }
        }

        public static bool DeleteRecord(MySqlCommand cmd)
        {
            //todo
            //if Child records exist
            // e.Cancel = true
            try
            {
                using (cmd)
                {
                    cmd.Connection = new MySqlConnection(ConnString);
                    cmd.Connection.Open();
                    if (cmd.ExecuteNonQuery() == 1)
                        return true;
                }
                return false;
            }
            catch (MySqlException e)
            {
                System.Windows.Forms.MessageBox.Show("WebServer Error, The record was not deleted" + "\r\n" + "\r\n" + e);
                return false;
            }
        }

        public static string SafeGetString(MySqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            return string.Empty;
        }
        public static bool SafeGetBool(MySqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetBoolean(colIndex);
            return false;
        }
    }
}
