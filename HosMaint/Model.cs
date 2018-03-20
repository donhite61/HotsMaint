﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace HotsMaint
{
    public abstract class Model
    {
        public abstract Server Serv { get; set; }
        public abstract DataSet Dset { get; set; }
        public abstract string IdFieldName { get; set; }
        public abstract string CodeFieldName { get; set; }
        public abstract string TableName { get; set; }
        public abstract UInt32 CurRecId { get; set; }
        public abstract BindingSource BSource { get; set; }

        internal abstract UInt32 InsertRecord(DataRow row);
        internal abstract bool UpdateRecord(DataRow _row);

        internal bool CodeHasBeenUsed(Model mod, string code)
        {
            var cmd = new MySqlCommand();
            cmd.CommandText = "SELECT EXISTS(SELECT * FROM " + mod.TableName +
                            " WHERE " + mod.CodeFieldName + " = ?Code)";
            //var sql = "SELECT COUNT(*) FROM " + mod.TableName +
            //    " WHERE " + mod.CodeFieldName + " = ?Code";

                cmd.Parameters.AddWithValue("?Code", code);
                var result = Serv.ExecuteMySqlScaler(cmd);
                return (Convert.ToInt32(result) > 0) ? true : false;
        }
        internal bool DeleteRecord(Model mod)
        {
            //todo
            //if Child records exist
            // e.Cancel = true
            var cmd = new MySqlCommand();
            cmd.CommandText = "DELETE FROM " + mod.TableName +
                             " WHERE " + mod.IdFieldName + " = ?Id";
            cmd.Parameters.AddWithValue("?Id", mod.CurRecId);
            var result = mod.Serv.ExecuteMySQLNonQuery(cmd);
            return (Convert.ToInt32(result) > 0) ? true : false;
        }
    }

    public class LocationsModel : Model
    {
        public override Server Serv { get; set; }
        public override DataSet Dset { get; set; }
        public override string IdFieldName { get; set; }
        public override string CodeFieldName { get; set; }
        public override string TableName { get; set; }
        public override UInt32 CurRecId { get; set; }
        public override BindingSource BSource { get; set; }

        public LocationsModel(Server server)
        {
            Serv = server;
            Dset = new DataSet();
            IdFieldName = "loc_Id";
            CodeFieldName = "loc_Code";
            TableName = GV.TblName.locations.ToString();

            var table = MakeNewDataTable(TableName);
            Serv.FillTable(table);
            Dset.Tables.Add(table);
            BSource = new BindingSource()
            {
                DataSource = Dset,
                DataMember = Dset.Tables[0].TableName,
                Filter = "Inactive = 0"
            };
        }

        public static void DeleteAndCreateLocationsTableOnServer(Server server, UInt32 startNumber)
        {
            var cmd = new MySqlCommand();
            cmd.CommandText = " DROP TABLE IF EXISTS locations";
            server.ExecuteMySQLNonQuery(cmd);

            cmd.CommandText = "CREATE TABLE locations(" +
                                "loc_Id int not NULL PRIMARY KEY AUTO_INCREMENT," +
                                "loc_Name VARCHAR(45) UNIQUE NOT NULL," +
                                "loc_Address VARCHAR(100)," +
                                "loc_Address2 VARCHAR(100)," +
                                "loc_City VARCHAR(100)," +
                                "loc_State VARCHAR(2)," +
                                "loc_Zip VARCHAR(10)," +
                                "loc_Phone VARCHAR(10)," +
                                "loc_Email VARCHAR(100)," +
                                "loc_Inactive TINYINT(4)," +
                                "loc_Timestamp TIMESTAMP )";
            server.ExecuteMySQLNonQuery(cmd);

            cmd.CommandText = "ALTER TABLE locations AUTO_INCREMENT="+startNumber;
            server.ExecuteMySQLNonQuery(cmd);
        }

        private DataTable MakeNewDataTable(string _tableName)
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
            table.Columns.Add("Timestamp", typeof(DateTime));
            return table;
        }

        internal override UInt32 InsertRecord(DataRow _row)
        {
            var cmd = new MySqlCommand();
            cmd.CommandText = "INSERT INTO locations " +
                        "(loc_Code,loc_Name,loc_Address,loc_Address2,loc_City,loc_State,loc_Zip,loc_Phone,loc_Email,loc_Inactive)" +
                        "Values(?Code,?Name,?Add,?Add2,?City,?State,?Zip,?Phone,?Email,?Inactive)";

            AddCommandParams(cmd, _row);
            var result = Serv.ExecuteMySQLNonQuery(cmd);
            UInt32 mySqlId = Convert.ToUInt32(cmd.LastInsertedId);
            return mySqlId;
        }

        internal override bool UpdateRecord(DataRow _row)
        {
            var cmd = new MySqlCommand();
            cmd.CommandText = "UPDATE locations " +
                    "SET loc_Code=?Code,loc_Name=?Name,loc_Address=?Add,loc_Address2=?Add2,loc_City=?City," +
                    "loc_State=?State,loc_Zip=?Zip,loc_Phone=?Phone,loc_Email=?Email,loc_Inactive=?Inactive " +
                    "WHERE loc_Id = ?Id";

            AddCommandParams(cmd, _row);
            var result = Serv.ExecuteMySQLNonQuery(cmd);
            return (Convert.ToInt32(result) > 0) ? true : false;
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

    }
}