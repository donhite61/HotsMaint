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
    public abstract class Server
    {
        public TimeSpan ServTimeOffset { get; set; }
        public abstract string ConnString { get; set; }

        public abstract void DeleteAndCreateTablesOnServer(UInt32 startNumber);

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

        public override void DeleteAndCreateTablesOnServer(UInt32 startNumber)
        {
            startNumber = startNumber * 100000000 +1;

            var result = MessageBox.Show("Are you sure you want to erase the Local locations table?",
                                         "Destroy Local locations Table?", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
                DeleteAndCreateLocationsTablesOnServer(startNumber);

            result = MessageBox.Show("Are you sure you want to erase the Local Vendors table?",
                                         "Destroy Local Vendors Table?", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
                DeleteAndCreateVendorsTableOnServer(startNumber);

            result = MessageBox.Show("Are you sure you want to erase the Local Vendor products table?",
                                         "Destroy Local VendorProducts Table?", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
                DeleteAndCreateVendProdTableOnServer(startNumber);
        }

        public void DeleteAndCreateLocationsTablesOnServer(UInt32 startNumber)
        {
            var cmd = new MySqlCommand(ConnString);
            cmd.CommandText = " DROP TABLE IF EXISTS locations";
            ExecuteMySQLNonQuery(cmd);

            cmd.CommandText = "CREATE TABLE locations(" +
                                "loc_Id int UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT," +
                                "loc_Code VARCHAR(20) UNIQUE  NOT NULL," +
                                "loc_Name VARCHAR(100) NOT NULL DEFAULT ''," +
                                "loc_Address VARCHAR(100) NOT NULL DEFAULT ''," +
                                "loc_Address2 VARCHAR(100) NOT NULL DEFAULT ''," +
                                "loc_City VARCHAR(100) NOT NULL DEFAULT ''," +
                                "loc_State VARCHAR(2) NOT NULL DEFAULT ''," +
                                "loc_Zip VARCHAR(15) NOT NULL DEFAULT ''," +
                                "loc_Phone VARCHAR(15) NOT NULL DEFAULT ''," +
                                "loc_Email VARCHAR(100) NOT NULL DEFAULT ''," +
                                "loc_Inactive TINYINT(4) NOT NULL DEFAULT '0'," +
                                "loc_Timestamp TIMESTAMP)";
            ExecuteMySQLNonQuery(cmd);

            cmd.CommandText = "ALTER TABLE locations AUTO_INCREMENT=" + startNumber;
            ExecuteMySQLNonQuery(cmd);
        }

        public void DeleteAndCreateVendorsTableOnServer(UInt32 startNumber)
        {
            var cmd = new MySqlCommand();
            cmd.CommandText = " DROP TABLE IF EXISTS vendors";
            ExecuteMySQLNonQuery(cmd);

            cmd.CommandText = "CREATE TABLE vendors(" +
                                "ven_Id int UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT," +
                                "ven_Code VARCHAR(20) UNIQUE NOT NULL," +
                                "ven_Name VARCHAR(100) NOT NULL DEFAULT ''," +
                                "ven_Address VARCHAR(100) NOT NULL DEFAULT ''," +
                                "ven_Address2 VARCHAR(100) NOT NULL DEFAULT ''," +
                                "ven_City VARCHAR(100) NOT NULL DEFAULT ''," +
                                "ven_State VARCHAR(2) NOT NULL DEFAULT ''," +
                                "ven_Zip VARCHAR(15) NOT NULL DEFAULT ''," +
                                "ven_Phone VARCHAR(15) NOT NULL DEFAULT ''," +
                                "ven_Email VARCHAR(100) NOT NULL DEFAULT ''," +
                                "ven_Inactive TINYINT(4) NOT NULL DEFAULT '0'," +
                                "ven_Timestamp TIMESTAMP)";
            ExecuteMySQLNonQuery(cmd);

            cmd.CommandText = "ALTER TABLE vendors AUTO_INCREMENT=" + startNumber;
            ExecuteMySQLNonQuery(cmd);
        }

        public void DeleteAndCreateVendProdTableOnServer(UInt32 startNumber)
        {
            var cmd = new MySqlCommand();
            cmd.CommandText = " DROP TABLE IF EXISTS vendProducts";
            ExecuteMySQLNonQuery(cmd);

            cmd.CommandText = "CREATE TABLE vendProducts(" +
                                "vProd_Id int UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT," +
                                "vProd_Code VARCHAR(20) UNIQUE NOT NULL," +
                                "vProd_Name VARCHAR(100) NOT NULL DEFAULT ''," +
                                "vProd_Description VARCHAR(100) NOT NULL DEFAULT ''," +
                                "vProd_Price DECIMAL(10,2)," +
                                "vProd_Quant DECIMAL(10,2)," +
                                "vProd_Units VARCHAR(20)," +
                                "vProd_Inactive TINYINT(4) NOT NULL DEFAULT '0'," +
                                "vProd_Timestamp TIMESTAMP )";
            ExecuteMySQLNonQuery(cmd);

            cmd.CommandText = "ALTER TABLE vendProducts AUTO_INCREMENT=" + startNumber;
            ExecuteMySQLNonQuery(cmd);
        }
    }

    public class ServerWeb : Server
    {
        public override string ConnString { get; set; }

        public ServerWeb()
        {
            ConnString = "server=69.89.31.188;user=hitephot_don;database=hitephot_pos;port=3306;password=Hite1985;";
        }

        public override void DeleteAndCreateTablesOnServer(UInt32 startNumber)
        {
            var result = MessageBox.Show("Are you sure you want to erase the Web locations table?",
                                         "Destroy Web locations Table?", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
                DeleteAndCreateLocationsTablesOnServer();

            result = MessageBox.Show("Are you sure you want to erase the Web Vendors table?",
                                         "Destroy Web Vendors Table?", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
                DeleteAndCreateVendorsTableOnServer();

            result = MessageBox.Show("Are you sure you want to erase the Web VendorProducts table?",
                                         "Destroy Web VendorProducts Table?", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
                DeleteAndCreateVendProdTableOnServer();
        }

        public void DeleteAndCreateLocationsTablesOnServer()
        {
            var cmd = new MySqlCommand(ConnString);
            cmd.CommandText = " DROP TABLE IF EXISTS locations";
            ExecuteMySQLNonQuery(cmd);

            cmd.CommandText = "CREATE TABLE locations(" +
                                "loc_Id int UNSIGNED NOT NULL PRIMARY KEY," +
                                "loc_Code VARCHAR(20) UNIQUE  NOT NULL," +
                                "loc_Name VARCHAR(100) NOT NULL DEFAULT ''," +
                                "loc_Address VARCHAR(100) NOT NULL DEFAULT ''," +
                                "loc_Address2 VARCHAR(100) NOT NULL DEFAULT ''," +
                                "loc_City VARCHAR(100) NOT NULL DEFAULT ''," +
                                "loc_State VARCHAR(2) NOT NULL DEFAULT ''," +
                                "loc_Zip VARCHAR(15) NOT NULL DEFAULT ''," +
                                "loc_Phone VARCHAR(15) NOT NULL DEFAULT ''," +
                                "loc_Email VARCHAR(100) NOT NULL DEFAULT ''," +
                                "loc_Inactive TINYINT(4) NOT NULL," +
                                "loc_Timestamp TIMESTAMP)";
            ExecuteMySQLNonQuery(cmd);
        }

        public void DeleteAndCreateVendorsTableOnServer()
        {
            var cmd = new MySqlCommand();
            cmd.CommandText = " DROP TABLE IF EXISTS vendors";
            ExecuteMySQLNonQuery(cmd);

            cmd.CommandText = "CREATE TABLE vendors(" +
                                "ven_Id int UNSIGNED NOT NULL PRIMARY KEY," +
                                "ven_Code VARCHAR(20) UNIQUE NOT NULL," +
                                "ven_Name VARCHAR(100) NOT NULL DEFAULT ''," +
                                "ven_Address VARCHAR(100) NOT NULL DEFAULT ''," +
                                "ven_Address2 VARCHAR(100) NOT NULL DEFAULT ''," +
                                "ven_City VARCHAR(100) NOT NULL DEFAULT ''," +
                                "ven_State VARCHAR(2) NOT NULL DEFAULT ''," +
                                "ven_Zip VARCHAR(15) NOT NULL DEFAULT ''," +
                                "ven_Phone VARCHAR(15) NOT NULL DEFAULT ''," +
                                "ven_Email VARCHAR(100) NOT NULL DEFAULT ''," +
                                "ven_Inactive TINYINT(4) NOT NULL," +
                                "ven_Timestamp TIMESTAMP)";
            ExecuteMySQLNonQuery(cmd);
        }

        public void DeleteAndCreateVendProdTableOnServer()
        {
            var cmd = new MySqlCommand();
            cmd.CommandText = " DROP TABLE IF EXISTS vendProducts";
            ExecuteMySQLNonQuery(cmd);

            cmd.CommandText = "CREATE TABLE vendProducts(" +
                                "vProd_Id int UNSIGNED NOT NULL PRIMARY KEY," +
                                "vProd_Code VARCHAR(20) UNIQUE NOT NULL," +
                                "vProd_Name VARCHAR(100) NOT NULL DEFAULT ''," +
                                "vProd_Description VARCHAR(100) NOT NULL DEFAULT ''," +
                                "vProd_Price DECIMAL(10,2)," +
                                "vProd_Quant DECIMAL(10,2)," +
                                "vProd_Units VARCHAR(20)," +
                                "vProd_Inactive TINYINT(4) NOT NULL," +
                                "vProd_Timestamp TIMESTAMP )";
            ExecuteMySQLNonQuery(cmd);
        }
    }
}
