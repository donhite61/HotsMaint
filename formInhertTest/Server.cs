using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace FormInhertTest
{
    public abstract class Server
    {
        public abstract string ConnString { get; set; }
        protected bool isUp;

        internal abstract void DeleteAndCreateTablesOnServer(UInt32 startNumber);


        internal object ExecuteMySQLNonQuery(MySqlCommand cmd)
        {
            using (cmd.Connection = new MySqlConnection(ConnString))
            using (cmd)
            {
                try
                {
                    cmd.Connection.Open();
                    return cmd.ExecuteNonQuery();
                }
                catch
                {
                    return null;
                }
            }
        }

        internal object ExecuteMySqlScaler(MySqlCommand cmd)
        {
            using (cmd.Connection = new MySqlConnection(ConnString))
            using (cmd)
            {
                try
                {
                    cmd.Connection.Open();
                    return cmd.ExecuteScalar();
                }
                catch
                {
                    return null;
                }
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

        internal void WriteVariableToServer(string key, object value)
        {
            var cmd = new MySqlCommand
            {
                CommandText = "INSERT INTO variables(var_Value,var_Key) Values(?value,?key)" +
                            "ON DUPLICATE KEY UPDATE var_Value = ?value"
            };

            cmd.Parameters.AddWithValue("?key", key);
            cmd.Parameters.AddWithValue("?value", value.ToString());

            var result = GV.SerLoc.ExecuteMySQLNonQuery(cmd);
        }

        internal string ReadVariableFromServer(string key)
        {
            object result;
            var sql = "SELECT var_Value FROM variables WHERE var_Key = ?key";
            using (var conn = new MySqlConnection(ConnString))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("?key", key);
                conn.Open();
                result = cmd.ExecuteScalar();
            }

            return result?.ToString();
        }

        internal override void DeleteAndCreateTablesOnServer(UInt32 startNumber)
        {
            startNumber = startNumber * 100000000 + 1;

            var result = MessageBox.Show("Are you sure you want to erase the Local SavedVariables table?",
                                         "Destroy Local SavedVariables Table?", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
                DeleteAndCreateSvdVariablesTableOnServer();

            result = MessageBox.Show("Are you sure you want to erase the Local locations table?",
                                         "Destroy Local locations Table?", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
                DeleteAndCreateLocationsTableOnServer(startNumber);

            result = MessageBox.Show("Are you sure you want to erase the Local Vendors table?",
                                         "Destroy Local Vendors Table?", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
                DeleteAndCreateVendorsTableOnServer(startNumber);

            result = MessageBox.Show("Are you sure you want to erase the Local Vendor products table?",
                                         "Destroy Local VendorProducts Table?", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
                DeleteAndCreateVendProdTableOnServer(startNumber);
        }

        private void DeleteAndCreateSvdVariablesTableOnServer()
        {
            var cmd = new MySqlCommand(ConnString)
            {
                CommandText = " DROP TABLE IF EXISTS variables"
            };
            ExecuteMySQLNonQuery(cmd);

            cmd.CommandText = "CREATE TABLE variables(" +
                                "Var_Key VARCHAR(20) UNIQUE NOT NULL PRIMARY KEY," +
                                "loc_Value VARCHAR(100) NOT NULL)";
            ExecuteMySQLNonQuery(cmd);
        }

        private void DeleteAndCreateLocationsTableOnServer(UInt32 startNumber)
        {
            var cmd = new MySqlCommand(ConnString)
            {
                CommandText = " DROP TABLE IF EXISTS locations"
            };
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

        private void DeleteAndCreateVendorsTableOnServer(UInt32 startNumber)
        {
            var cmd = new MySqlCommand
            {
                CommandText = " DROP TABLE IF EXISTS vendors"
            };
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

        private void DeleteAndCreateVendProdTableOnServer(UInt32 startNumber)
        {
            var cmd = new MySqlCommand
            {
                CommandText = " DROP TABLE IF EXISTS vendProducts"
            };
            ExecuteMySQLNonQuery(cmd);

            cmd.CommandText = "CREATE TABLE vendProducts(" +
                                "vProd_Id int UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT," +
                                "vProd_Code VARCHAR(20) UNIQUE NOT NULL," +
                                "vProd_Description VARCHAR(100) DEFAULT ''," +
                                "vProd_Price DECIMAL(10,2)," +
                                "vProd_Quant DECIMAL(10,2)," +
                                "vProd_Units VARCHAR(20)," +
                                "vProd_VenId int UNSIGNED NOT NULL ," +
                                "vProd_CatNum VARCHAR(20)," +
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

        internal double GetServerOffsetInSeconds()
        {
            var delay = 0.0;
            var cmd = new MySqlCommand
            {
                CommandText = "SELECT NOW() FROM DUAL"
            };
            var result = ExecuteMySqlScaler(cmd);
            if (result != null)
            {
                if (DateTime.TryParse(result.ToString(), out DateTime webTime))
                    delay = (webTime - DateTime.Now).TotalSeconds;
            }

            return delay;
        }   

        internal override void DeleteAndCreateTablesOnServer(UInt32 startNumber)
        {
            var result = MessageBox.Show("Are you sure you want to erase the Web locations table?",
                                         "Destroy Web locations Table?", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
                DeleteAndCreateLocationsTableOnServer();

            result = MessageBox.Show("Are you sure you want to erase the Web Vendors table?",
                                         "Destroy Web Vendors Table?", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
                DeleteAndCreateVendorsTableOnServer();

            result = MessageBox.Show("Are you sure you want to erase the Web VendorProducts table?",
                                         "Destroy Web VendorProducts Table?", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
                DeleteAndCreateVendProdTableOnServer();
        }

        private void DeleteAndCreateLocationsTableOnServer()
        {
            var cmd = new MySqlCommand(ConnString)
            {
                CommandText = " DROP TABLE IF EXISTS locations"
            };
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

        private void DeleteAndCreateVendorsTableOnServer()
        {
            var cmd = new MySqlCommand
            {
                CommandText = " DROP TABLE IF EXISTS vendors"
            };
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

        private void DeleteAndCreateVendProdTableOnServer()
        {
            var cmd = new MySqlCommand
            {
                CommandText = " DROP TABLE IF EXISTS vendProducts"
            };
            ExecuteMySQLNonQuery(cmd);

            cmd.CommandText = "CREATE TABLE vendProducts(" +
                                "vProd_Id int UNSIGNED NOT NULL PRIMARY KEY," +
                                "vProd_Code VARCHAR(20) UNIQUE NOT NULL," +
                                "vProd_Name VARCHAR(100) NOT NULL DEFAULT ''," +
                                "vProd_VendCode VARCHAR(100) NOT NULL DEFAULT ''," +
                                "vProd_Price DECIMAL(10,2)," +
                                "vProd_Quant DECIMAL(10,2)," +
                                "vProd_Units VARCHAR(20)," +
                                "vProd_Inactive TINYINT(4) NOT NULL," +
                                "vProd_Timestamp TIMESTAMP )";
            ExecuteMySQLNonQuery(cmd);
        }
    }
}

