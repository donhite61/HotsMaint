﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace FormInhertTest
{
    public abstract class Model
    {
        public abstract Server Serv { get; set; }
        public abstract DataSet Dset { get; set; }
        public abstract UInt32 CurRecId { get; set; }
        public abstract BindingSource BSource { get; set; }

        internal abstract DataTable FillTable(DataTable tbl);
        internal abstract bool DeleteRecord(Model mod);
        internal abstract bool CodeHasBeenUsed(Model mod, string code);
        internal abstract UInt32 InsertRecord(DataRow row);
        internal abstract bool UpdateRecord(DataRow _row);


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

    public class LocationsModel : Model
    {
        public override Server Serv { get; set; }
        public override DataSet Dset { get; set; }
        public override UInt32 CurRecId { get; set; }
        public override BindingSource BSource { get; set; }

        public LocationsModel(Server server)
        {
            Serv = server;
            Dset = new DataSet();

            var table = MakeNewDataTable(GV.TblName.locations.ToString());
            FillTable(table);
            Dset.Tables.Add(table);
            BSource = new BindingSource()
            {
                DataSource = Dset,
                DataMember = table.TableName,
                Filter = "Inactive = 0"
            };
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
            table.Columns["Inactive"].DefaultValue = false;
            table.Columns.Add("Timestamp", typeof(DateTime));
            return table;
        }

        internal override DataTable FillTable(DataTable tbl)
        {
            tbl.Clear();
            var sql = "SELECT * FROM " + tbl.TableName;

            using (var conn = new MySqlConnection(Serv.ConnString))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    int count = rd.FieldCount;
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
                        row[11] = rd.GetDateTime(11);
                        tbl.Rows.Add(row);
                    }
                    tbl.AcceptChanges();
                }
            }
            return tbl;
        }

        internal override bool CodeHasBeenUsed(Model mod, string code)
        {
            var cmd = new MySqlCommand
            {
                CommandText = "SELECT EXISTS(SELECT * FROM locations" +
                              " WHERE loc_Code = ?Code)"
            };

            cmd.Parameters.AddWithValue("?Code", code);
            var result = Serv.ExecuteMySqlScaler(cmd);
            return (Convert.ToInt32(result) > 0) ? true : false;
        }

        internal override bool DeleteRecord(Model mod)
        {
            //todo
            //if Child records exist
            // e.Cancel = true

            // if code has been changed it must cascade to all child records

            var cmd = new MySqlCommand
            {
                CommandText = "DELETE FROM locations" +
                             " WHERE loc_Id = ?Id"
            };
            cmd.Parameters.AddWithValue("?Id", mod.CurRecId);
            var result = mod.Serv.ExecuteMySQLNonQuery(cmd);
            return (Convert.ToInt32(result) > 0) ? true : false;

            //todo add delete record to change table
        }

        internal override UInt32 InsertRecord(DataRow _row)
        {
            var cmd = new MySqlCommand
            {
                CommandText = "INSERT INTO locations " +
                        "(loc_Code,loc_Name,loc_Address,loc_Address2,loc_City,loc_State,loc_Zip,loc_Phone,loc_Email,loc_Inactive,loc_Timestamp)" +
                        "Values(?Code,?Name,?Add,?Add2,?City,?State,?Zip,?Phone,?Email,?Inactive,?Timestamp)"
            };

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
            var dt = DateTime.Now.AddSeconds(GV.ServTimeDiff);
            cmd.Parameters.AddWithValue("?Timestamp", DateTime.Now.AddSeconds(GV.ServTimeDiff));

            var result = Serv.ExecuteMySQLNonQuery(cmd);
            UInt32 mySqlId = Convert.ToUInt32(cmd.LastInsertedId);
            return mySqlId;

            //todo add insert record to change table
        }

        internal override bool UpdateRecord(DataRow _row)
        {
            // if code has been changed it must cascade to all child records

            var cmd = new MySqlCommand
            {
                CommandText = "UPDATE locations " +
                    "SET loc_Code=?Code,loc_Name=?Name,loc_Address=?Add,loc_Address2=?Add2,loc_City=?City," +
                    "loc_State=?State,loc_Zip=?Zip,loc_Phone=?Phone,loc_Email=?Email,loc_Inactive=?Inactive,loc_Timestamp=?Timestamp " +
                    "WHERE loc_Id = ?Id"
            };

            if (!_row.ItemArray[0].Equals(System.DBNull.Value))
                cmd.Parameters.AddWithValue("?Id", Convert.ToUInt32(_row.ItemArray[0]));
            else
                throw new Exception();

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
            cmd.Parameters.AddWithValue("?Timestamp", DateTime.Now.AddSeconds(GV.ServTimeDiff));

            var result = Serv.ExecuteMySQLNonQuery(cmd);
            return (Convert.ToInt32(result) > 0) ? true : false;
            //todo add update record to change table
        }
    }

    public class VendorsModel : Model
    {
        public override Server Serv { get; set; }
        public override DataSet Dset { get; set; }
        public override UInt32 CurRecId { get; set; }
        public override BindingSource BSource { get; set; }

        public VendorsModel(Server server)
        {
            Serv = server;
            Dset = new DataSet();

            var table = MakeNewDataTable(GV.TblName.vendors.ToString());
            FillTable(table);
            Dset.Tables.Add(table);
            BSource = new BindingSource()
            {
                DataSource = Dset,
                DataMember = Dset.Tables[0].TableName,
                Filter = "Inactive = 0"
            };
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
            table.Columns["Inactive"].DefaultValue = false;
            table.Columns.Add("Timestamp", typeof(DateTime));
            return table;
        }

        internal override DataTable FillTable(DataTable tbl)
        {
            tbl.Clear();
            var sql = "SELECT * FROM " + tbl.TableName;

            using (var conn = new MySqlConnection(Serv.ConnString))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    int count = rd.FieldCount;
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
                        row[11] = rd.GetDateTime(11);
                        tbl.Rows.Add(row);
                    }
                    tbl.AcceptChanges();
                }
            }
            return tbl;
        }

        internal override bool CodeHasBeenUsed(Model mod, string code)
        {
            var cmd = new MySqlCommand
            {
                CommandText = "SELECT EXISTS(SELECT * FROM vendors" +
                            " WHERE ven_Code = ?Code)"
            };

            cmd.Parameters.AddWithValue("?Code", code);
            var result = Serv.ExecuteMySqlScaler(cmd);
            return (Convert.ToInt32(result) > 0) ? true : false;
        }

        internal override bool DeleteRecord(Model mod)
        {
            //todo
            //if Child records exist
            // e.Cancel = true
            var cmd = new MySqlCommand
            {
                CommandText = "DELETE FROM vendors" +
                             " WHERE ven_Id = ?Id"
            };
            cmd.Parameters.AddWithValue("?Id", mod.CurRecId);
            var result = mod.Serv.ExecuteMySQLNonQuery(cmd);
            return (Convert.ToInt32(result) > 0) ? true : false;
        }

        internal override UInt32 InsertRecord(DataRow _row)
        {
            var cmd = new MySqlCommand
            {
                CommandText = "INSERT INTO vendors " +
                        "(ven_Code,ven_Name,ven_Address,ven_Address2,ven_City,ven_State,ven_Zip,ven_Phone,ven_Email,ven_Inactive,ven_Timestamp)" +
                        "Values(?Code,?Name,?Add,?Add2,?City,?State,?Zip,?Phone,?Email,?Inactive,?Timestamp)"
            };

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
            cmd.Parameters.AddWithValue("?Timestamp", DateTime.Now.AddSeconds(GV.ServTimeDiff));

            var result = Serv.ExecuteMySQLNonQuery(cmd);
            UInt32 mySqlId = Convert.ToUInt32(cmd.LastInsertedId);
            return mySqlId;
        }

        internal override bool UpdateRecord(DataRow _row)
        {
            var cmd = new MySqlCommand
            {
                CommandText = "UPDATE vendors " +
                    "SET ven_Code=?Code,ven_Name=?Name,ven_Address=?Add,ven_Address2=?Add2,ven_City=?City," +
                    "ven_State=?State,ven_Zip=?Zip,ven_Phone=?Phone,ven_Email=?Email,ven_Inactive=?Inactive,ven_Timestamp=?Timestamp " +
                    "WHERE ven_Id = ?Id"
            };

            if (!_row.ItemArray[0].Equals(System.DBNull.Value))
                cmd.Parameters.AddWithValue("?Id", Convert.ToUInt32(_row.ItemArray[0]));
            else
                throw new Exception();

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
            cmd.Parameters.AddWithValue("?Timestamp", DateTime.Now.AddSeconds(GV.ServTimeDiff));

            var result = Serv.ExecuteMySQLNonQuery(cmd);
            return (Convert.ToInt32(result) > 0) ? true : false;
        }
    }

    public class VendProdModel : Model
    {
        public override Server Serv { get; set; }
        public override DataSet Dset { get; set; }
        public override UInt32 CurRecId { get; set; }
        public override BindingSource BSource { get; set; }

        public VendProdModel(Server server)
        {
            Serv = server;
            Dset = new DataSet();

            var table = MakeNewDataTable(GV.TblName.vendProducts.ToString());
            FillTable(table);
            Dset.Tables.Add(table);

            BSource = new BindingSource()
            {
                DataSource = Dset,
                DataMember = Dset.Tables[0].TableName,
                Filter = "Inactive = 0"
            };
        }

        private DataTable MakeNewDataTable(string _tableName)
        {
            var table = new DataTable { TableName = _tableName };
            table.Columns.Add("Id", typeof(UInt32));
            table.Columns.Add("Code", typeof(string));
            table.Columns.Add("Description", typeof(string));
            table.Columns.Add("Price", typeof(decimal));
            table.Columns.Add("Quantity", typeof(decimal));
            table.Columns.Add("Units", typeof(string));
            table.Columns.Add("VenId", typeof(UInt32));
            table.Columns.Add("VendCatNum", typeof(string));
            table.Columns.Add("Inactive", typeof(Boolean));
            table.Columns["Inactive"].DefaultValue = false;
            table.Columns.Add("Timestamp", typeof(DateTime));


            return table;
        }

        internal override DataTable FillTable(DataTable tbl)
        {
            tbl.Clear();
            var sql = "SELECT * FROM " + tbl.TableName;

            using (var conn = new MySqlConnection(Serv.ConnString))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    int count = rd.FieldCount;
                    while (rd.Read())
                    {
                        var row = tbl.NewRow();
                        row[0] = rd.GetUInt32(0);
                        row[1] = SafeGetString(rd, 1);
                        row[2] = SafeGetString(rd, 2);
                        row[3] = rd.GetDecimal(3);
                        row[4] = rd.GetDecimal(4);
                        row[5] = SafeGetString(rd, 5);
                        row[6] = rd.GetUInt32(6);
                        row[7] = SafeGetString(rd, 7);
                        row[8] = SafeGetBool(rd, 8);
                        row[9] = rd.GetDateTime(9);
                        tbl.Rows.Add(row);
                    }
                    tbl.AcceptChanges();
                }
            }
            return tbl;
        }

        internal override bool CodeHasBeenUsed(Model mod, string code)
        {
            var cmd = new MySqlCommand
            {
                CommandText = "SELECT EXISTS(SELECT * FROM vendProducts" +
                            " WHERE vProd_Code = ?Code)"
            };

            cmd.Parameters.AddWithValue("?Code", code);
            var result = Serv.ExecuteMySqlScaler(cmd);
            return (Convert.ToInt32(result) > 0) ? true : false;
        }

        internal override bool DeleteRecord(Model mod)
        {
            //todo
            //if Child records exist
            // e.Cancel = true
            var cmd = new MySqlCommand
            {
                CommandText = "DELETE FROM vendProducts" +
                             " WHERE ven_Id = ?Id"
            };
            cmd.Parameters.AddWithValue("?Id", mod.CurRecId);
            var result = mod.Serv.ExecuteMySQLNonQuery(cmd);
            return (Convert.ToInt32(result) > 0) ? true : false;
        }

        internal override UInt32 InsertRecord(DataRow _row)
        {
            var cmd = new MySqlCommand
            {
                CommandText = "INSERT INTO vendProducts " +
                        "(vProd_Code,vProd_Description,vProd_Price,vProd_Quant,vProd_Units,VProd_VenId,vProd_CatNum,vProd_Inactive,vPRod_Timestamp)" +
                        "Values(?Code,?Desc,?Price,?Quant,?Units,?VenId,?VendCat,?Inactive,?Timestamp)"
            };

            cmd.Parameters.AddWithValue("?Code", _row.ItemArray[1].ToString());
            cmd.Parameters.AddWithValue("?Desc", _row.ItemArray[2].ToString());
            cmd.Parameters.AddWithValue("?Price", Convert.ToDecimal(_row.ItemArray[3]));
            cmd.Parameters.AddWithValue("?Quant", Convert.ToDecimal(_row.ItemArray[4]));
            cmd.Parameters.AddWithValue("?Units", _row.ItemArray[5].ToString());
            cmd.Parameters.AddWithValue("?VenId", Convert.ToUInt32(_row.ItemArray[6]));
            cmd.Parameters.AddWithValue("?VenCat", _row.ItemArray[7].ToString());
            cmd.Parameters.AddWithValue("?Inactive", Convert.ToBoolean(_row.ItemArray[8]));
            cmd.Parameters.AddWithValue("?Timestamp", DateTime.Now.AddSeconds(GV.ServTimeDiff));


            var result = Serv.ExecuteMySQLNonQuery(cmd);
            UInt32 mySqlId = Convert.ToUInt32(cmd.LastInsertedId);
            return mySqlId;
        }

        internal override bool UpdateRecord(DataRow _row)
        {
            var cmd = new MySqlCommand
            {
                CommandText = "UPDATE vendProducts " +
                    "SET vProd_Code=?Code,vProd_Description=?Desc,vProd_Price=?Price,vProd_Quant=?Quant,vProd_Units=?Units," +
                    "vProd_Id=?VenId,vProd_VenCatNum=VendCat,vProd_Inactive=?Inactive,vProd_Timestamp=?Timestamp " +
                    "WHERE vProd_Id = ?Id"
            };

            cmd.Parameters.AddWithValue("?Id", Convert.ToUInt32(_row.ItemArray[0]));
            cmd.Parameters.AddWithValue("?Code", _row.ItemArray[1].ToString());
            cmd.Parameters.AddWithValue("?Desc", _row.ItemArray[2].ToString());
            cmd.Parameters.AddWithValue("?Price", Convert.ToDecimal(_row.ItemArray[3]));
            cmd.Parameters.AddWithValue("?Quant", Convert.ToDecimal(_row.ItemArray[4]));
            cmd.Parameters.AddWithValue("?Units", _row.ItemArray[5].ToString());
            cmd.Parameters.AddWithValue("?VenId", Convert.ToUInt32(_row.ItemArray[6]));
            cmd.Parameters.AddWithValue("?VenCat", _row.ItemArray[7].ToString());
            cmd.Parameters.AddWithValue("?Inactive", Convert.ToBoolean(_row.ItemArray[8]));
            cmd.Parameters.AddWithValue("?Timestamp", DateTime.Now.AddSeconds(GV.ServTimeDiff));

            var result = Serv.ExecuteMySQLNonQuery(cmd);
            return (Convert.ToInt32(result) > 0) ? true : false;
        }
    }

}

