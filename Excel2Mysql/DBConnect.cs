using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
//Add MySql Library
using MySql.Data.MySqlClient;

namespace Excel2Mysql
{
    class DBConnect
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        private string tablename;

        //Constructor
        public DBConnect()
        {            
        }

        //Initialize values
        public bool Initialize(string serverip, string serverport, string dbname, string table, string user, string pwd)
        {
            server = serverip;
            database = dbname;
            uid = user;
            password = pwd;
            tablename = table;

            string connectionString = "SERVER=" + server + "; Port=" + serverport + "; DATABASE=" + database
                            + "; UID=" + uid + "; PASSWORD=" + password + "; SslMode=none";
            string createtablestring = "CREATE TABLE `" + tablename + "` ("
                            + "`No` int(11) NOT NULL AUTO_INCREMENT,"
                            + "`Type` varchar(255) DEFAULT NULL,"
                            + "`Item` varchar(255) DEFAULT NULL,"
                            + "`Description` varchar(255) DEFAULT NULL,"
                            + "`QoH` int(11) NOT NULL,"
                            + "`QoO` int(11) NOT NULL,"
                            + "`Avl` int(11) NOT NULL,"
                            + "`Notes` varchar(255) DEFAULT NULL,"
                            + "`Date` date NOT NULL,"
                            + "PRIMARY KEY(`No`)"
                            + ") ENGINE = InnoDB DEFAULT CHARSET = latin1; ";
            connection = new MySqlConnection(connectionString);

            if (OpenConnection())
            {
                CloseConnection();
                if (!ExistTable())
                {
                    CreateTable(createtablestring);                    
                }
                return true;
            }
            return false;
        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool ExistTable()
        {
            bool result = false;
            string query = "SHOW TABLES LIKE '" + tablename + "'";

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    string value = cmd.ExecuteScalar().ToString();
                    if (value.Equals(tablename))
                    {
                        result = true;
                    }
                }
                catch
                {
                    result = false;
                }

                //close connection
                this.CloseConnection();
            }

            return result;
        }

        public void CreateTable(string query)
        {            
            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        public bool ExistDate(string date)
        {            
            string query = "SELECT Count(*) FROM " + tablename + " WHERE Date='" + date + "'";
            int Count = -1;

            //Open Connection
            if (this.OpenConnection() == true)
            {
                //Create Mysql Command
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar() + "");

                //close Connection
                this.CloseConnection();
            }
            
            if (Count > 0)
                return true;

            return false;
        }

        // show Avl for each day for each product
        public List<string>[] CustomizedQuery(int queryType, DateTime beginDate, DateTime endDate)
        {
            string query;
            MySqlCommand cmd;            

            string begin = beginDate.ToString("yyyy-MM-dd");
            string end = endDate.ToString("yyyy-MM-dd");
            int date_count = (int)(endDate - beginDate).TotalDays + 1;

            List<string>[] list = new List<string>[date_count + 2];
            for(int nid = 0; nid < date_count + 2; nid++)
                list[nid] = new List<string>();

            if (this.OpenConnection() == true)
            {
                query = "SELECT Item, Description FROM itemtable WHERE Date BETWEEN '" + begin
                + "' AND '" + end + "' GROUP BY Item";
                cmd = new MySqlCommand(query, connection);

                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    list[0].Add(dataReader["Item"] + "");
                    list[1].Add(dataReader["Description"] + "");
                    for (int ndate = 0; ndate < date_count; ndate++)
                        list[ndate + 2].Add("0");
                }
                dataReader.Close();

                if (list[0].Count > 0)
                {
                    for (int ndate = 0; ndate < date_count; ndate++)
                    {
                        DateTime current = beginDate.AddDays(ndate);
                        string curdate = current.ToString("yyyy-MM-dd");
                        query = "SELECT Item, SUM(Avl) as avl FROM itemtable WHERE Date = '" + curdate + "' GROUP BY Item";
                        cmd = new MySqlCommand(query, connection);

                        dataReader = cmd.ExecuteReader();
                        while (dataReader.Read())
                        {
                            string itemname = dataReader["Item"].ToString();
                            string avlcount = dataReader["avl"].ToString();
                            for (int nitem = 0; nitem < list[0].Count; nitem++)
                            {
                                if (itemname.Equals(list[0][nitem]))
                                {
                                    list[ndate + 2][nitem] = avlcount;
                                    break;
                                }
                            }
                        }
                        dataReader.Close();
                    }
                }


                this.CloseConnection();                            
            }                        
                    
            return list;
        }

        //Insert statement
        public void Insert(string type, string item, string desc, string qoh, string qoo, string avl, string notes, string date)
        {
            type = type.Replace("'", " ");
            desc = desc.Replace("'", " ");
            notes = notes.Replace("'", " ");

            string query = "INSERT INTO " + tablename + " (Type, Item, Description, QoH, QoO, Avl, Notes, Date)"
                + " VALUES('" + type + "', '" + item + "', '" + desc + "', " + qoh + ", " + qoo 
                + ", " + avl + ", '" + notes + "', '" + date + "')";

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);
                
                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        //Update statement
        public void Update()
        {
            string query = "UPDATE " + tablename + " SET name='Joe', age='22' WHERE name='John Smith'";

            //Open connection
            if (this.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        //Delete statement
        public void Delete()
        {
            string query = "DELETE FROM " + tablename + " WHERE name='John Smith'";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        //Select statement
        public List<string>[] Select()
        {
            string query = "SELECT * FROM " + tablename;

            //Create a list to store the result
            List<string>[] list = new List<string>[3];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                
                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list[0].Add(dataReader["id"] + "");
                    list[1].Add(dataReader["name"] + "");
                    list[2].Add(dataReader["age"] + "");
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }

        //Count statement
        public int Count()
        {
            string query = "SELECT Count(*) FROM " + tablename;
            int Count = -1;

            //Open Connection
            if (this.OpenConnection() == true)
            {
                //Create Mysql Command
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar()+"");
                
                //close Connection
                this.CloseConnection();

                return Count;
            }
            else
            {
                return Count;
            }
        }

        //Backup
        public void Backup()
        {
            try
            {
                DateTime Time = DateTime.Now;
                int year = Time.Year;
                int month = Time.Month;
                int day = Time.Day;
                int hour = Time.Hour;
                int minute = Time.Minute;
                int second = Time.Second;
                int millisecond = Time.Millisecond;

                //Save file to C:\ with the current date as a filename
                string path;
                path = "C:\\" + year + "-" + month + "-" + day + "-" + hour + "-" + minute + "-" + second + "-" + millisecond + ".sql";
                StreamWriter file = new StreamWriter(path);

                
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "mysqldump";
                psi.RedirectStandardInput = false;
                psi.RedirectStandardOutput = true;
                psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}", uid, password, server, database);
                psi.UseShellExecute = false;

                Process process = Process.Start(psi);

                string output;
                output = process.StandardOutput.ReadToEnd();
                file.WriteLine(output);
                process.WaitForExit();
                file.Close();
                process.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error , unable to backup!");
            }
        }

        //Restore
        public void Restore()
        {
            try
            {
                //Read file from C:\
                string path;
                path = "C:\\MySqlBackup.sql";
                StreamReader file = new StreamReader(path);
                string input = file.ReadToEnd();
                file.Close();


                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "mysql";
                psi.RedirectStandardInput = true;
                psi.RedirectStandardOutput = false;
                psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}", uid, password, server, database);
                psi.UseShellExecute = false;

                
                Process process = Process.Start(psi);
                process.StandardInput.WriteLine(input);
                process.StandardInput.Close();
                process.WaitForExit();
                process.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error , unable to Restore!");
            }
        }
    }
}
