using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;


namespace HotsMaint
{
    public interface ITable
    {
        DateTime TblUpdTime { get; set; }
        DataSet DataSet { get; set; }

        DataTable MakeNewDataTable(string _tableName);
        UInt32 InsertRecord(DataRow _row);
        bool UpdateRecord(DataRow _row);
        bool DeleteRecord(UInt32 _id);

        }

    public class Loc : ITable
    {
        public DateTime TblUpdTime { get; set; }
        public DataSet DataSet { get; set; }
        private string tableName = "locations";

        public Loc()
        {
            DataSet = new DataSet();
            var table = MakeNewDataTable(tableName);
            Sql.FillTable(table);
            DataSet.Tables.Add(table);
            TblUpdTime = Sql.GetTableUpdateTime(tableName);
        }

        public DataTable MakeNewDataTable(string _tableName)
        {
            var table = new DataTable { TableName = _tableName };
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

            return table;
        }

        public UInt32 InsertRecord(DataRow _row)
        {
            var cmd = new MySqlCommand();
            cmd.CommandText = "INSERT INTO locations " +
                    "(loc_Code,loc_Name,loc_Address,loc_Address2,loc_City,loc_State,loc_Zip,loc_Phone,loc_Email,loc_Inactive)" +
                    "Values(?Code,?Name,?Add,?Add2,?City,?State,?Zip,?Phone,?Email,?Inactive)";

            AddCommandParams(cmd, _row);

            var newId = Sql.InsertRecord(cmd);
            //update sqlchangetable
            return newId;
        }

        public bool UpdateRecord(DataRow _row)
        {
            var cmd = new MySqlCommand();
            cmd.CommandText = "UPDATE locations " +
                    "SET loc_Code=?Code,loc_Name=?Name,loc_Address=?Add,loc_Address2=?Add2,loc_City=?City," +
                    "loc_State=?State,loc_Zip=?Zip,loc_Phone=?Phone,loc_Email=?Email,loc_Inactive=?Inactive " +
                    "WHERE loc_Id = ?Id";

            AddCommandParams(cmd, _row);

            if (Sql.UpdateRecord(cmd))
            {
                //update sqlchangetable
                return true;
            }
            return false;
        }

        public bool DeleteRecord(UInt32 _id)
        {
            var cmd = new MySqlCommand();
            cmd.CommandText = "DELETE FROM locations WHERE loc_Id = ?Id";
            cmd.Parameters.AddWithValue("?Id", _id);
            return  Sql.DeleteRecord(cmd) ? true : false;
        }

        private MySqlCommand AddCommandParams(MySqlCommand cmd, DataRow _row)
        {
            if (!_row.ItemArray[0].Equals(System.DBNull.Value))
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
            return cmd;
        }

        //public static string GetNameFromCode(string code)
        //{
        //    //string sql = "SELECT loc_Description FROM locations WHERE loc_Code = ?code LIMIT 1";
        //    //using (var conn = new MySqlConnection(ConnString))
        //    //using (var cmd = new MySqlCommand(sql, conn))
        //    //{
        //    //    cmd.Parameters.AddWithValue("?Code", code);
        //    //    conn.Open();
        //    //    var result = cmd.ExecuteScalar();
        //    //    return result == null ? null : Convert.ToString(cmd.ExecuteScalar());
        //    //}
        //}
    }
}
