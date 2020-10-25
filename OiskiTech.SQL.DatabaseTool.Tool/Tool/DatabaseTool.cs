using System;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;

namespace Oiski.SQL.DatabaseTool
{
    /// <summary>
    /// Represents a tool that can create and manipulate databases from scratch.
    /// <br/>
    /// </summary>
    public sealed class DatabaseTool
    {
        /*
            "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Oiski\source\repos\Oiskis_Web_Development\Oiski_Web_Development\Databank\Data\WepApp_Database.mdf;Integrated Security=True"
            "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=[Path]\[DBFileName].mdf;Integrated Security=True"
        */

        /// <summary>
        /// The name of the database
        /// </summary>
        public string DBName { get; }

        /// <summary>
        /// The path to the database MDF file without the file itself.
        /// </summary>
        public string PathToDatabase { get; set; }

        /// <summary>
        /// The connection string used to connect to the database
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return GenerateConnectionString();
            }
        }

        private string[] queries;

        private SqlConnection connection;

        private const string TEMPDBCONSTRING = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=tempdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        /// <summary>
        /// This will create a new log (.txt) file if it does not elready exist. if it does it will append the specified message to the files content
        /// </summary>
        /// <param name="_message"></param>
        private void AddToLog (string _message)
        {
            try
            {
                using ( StreamWriter writer = File.AppendText($"{PathToDatabase}\\{DBName}_Log.txt") )
                {
                    writer.WriteLine($"{DateTime.Now}: {_message}");
                }
            }
            catch ( Exception _e )
            {

                using ( StreamWriter writer = File.AppendText($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\{DBName}_Log.txt") )
                {
                    writer.WriteLine($"{DateTime.Now}: {_e}");
                }
            }
        }

        /// <summary>
        /// Checks if the MDF file exists and if it does it will set the internal <see cref="SqlConnection"/> using th e<see cref="ConnectionString"/>
        /// </summary>
        /// <returns></returns>
        public bool Exists ()
        {
            if ( File.Exists($"{PathToDatabase}\\{DBName}.mdf") )
            {
                connection = new SqlConnection(ConnectionString);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Creates an entirely new database if it does not already exist. (<i>This will generate and MDF file and a LOG file at the specified <see cref="PathToDatabase"/></i>)
        /// </summary>
        /// <returns></returns>
        public bool CreateDatabase ()
        {
            if ( !Exists() )
            {
                connection = CreateMDF();

                try
                {
                    connection.Open();
                    connection.Close();

                    AddToLog($"Database with name: {DBName} was created at: {PathToDatabase}");
                    return true;
                }
                catch ( Exception _e )
                {
                    AddToLog(_e.ToString());
                    return false;
                }
            }

            AddToLog($"Database with name: {DBName} Already Exists at: {PathToDatabase}");
            return false;
        }

        /// <summary>
        /// This will assemble tables for the database specified by the <see cref="ConnectionString"/>. This is done by executing the query found in the following file: Quries\AssembleQuery.txt
        /// </summary>
        /// <returns></returns>
        public bool AssembleDatabase ()
        {
            if ( connection != null || Exists() )
            {

                connection.Open();
                SqlCommand command = new SqlCommand(queries[( int ) Query.Assemble])
                {
                    Connection = connection
                };

                try
                {
                    command.ExecuteNonQuery();
                }
                catch ( Exception _e )
                {
                    AddToLog(_e.ToString());
                    return false;
                }
                finally
                {
                    connection.Close();
                }
                AddToLog($"Database with name: {DBName} assembled");
                return true;
            }

            AddToLog($"Connection on: {ConnectionString} returned null");
            return false;
        }

        /// <summary>
        /// This will create stored procedures for the database specified by the <see cref="ConnectionString"/>. This is done by executing the query found in the following file: Quries\ProcedureQuery.txt
        /// </summary>
        /// <returns></returns>
        public bool CreateProcedures ()
        {
            if ( connection != null || Exists() )
            {
                string[] procedures = queries[( int ) Query.Procedure].Split(':');

                SqlCommand command;

                foreach ( var procedure in procedures )
                {
                    connection.Open();
                    AddToLog($"Executing Query: {procedure}");
                    command = new SqlCommand(procedure)
                    {
                        Connection = connection
                    };

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch ( Exception _e )
                    {
                        AddToLog(_e.ToString());
                        return false;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }

                AddToLog($"Procedures for: {DBName} created");
                return true;
            }

            AddToLog($"Connection on: {ConnectionString} returned null");
            return false;
        }

        /// <summary>
        /// This will populate the database specified by the <see cref="ConnectionString"/> with test data. This is done by executing the query found in the following file: Quries\Populate.txt
        /// </summary>
        /// <returns></returns>
        public bool PopulateDatabase ()
        {
            if ( connection != null || Exists() )
            {
                connection.Open();
                SqlCommand command = new SqlCommand(queries[( int ) Query.Populate])
                {
                    Connection = connection
                };

                try
                {
                    command.ExecuteNonQuery();
                }
                catch ( Exception _e )
                {
                    AddToLog(_e.ToString());
                    return false;
                }
                finally
                {
                    connection.Close();
                }

                AddToLog($"Database with name: {DBName} populated");
                return true;
            }

            AddToLog($"Connection on: {ConnectionString} returned null");
            return false;
        }

        /// <summary>
        /// This will delete all data including tables and procedures for the database specified by the <see cref="ConnectionString"/>. This is done by executing the query found in the following file: Quries\DeletionQuery.txt
        /// </summary>
        /// <returns></returns>
        public bool DeleteData ()
        {
            if ( connection != null || Exists() )
            {
                bool error = false;
                connection.Open();
                SqlCommand command = new SqlCommand(queries[( int ) Query.Delete])
                {
                    Connection = connection
                };

                try
                {
                    command.ExecuteNonQuery();
                }
                catch ( Exception _e )
                {
                    AddToLog(_e.ToString());
                    error = true;
                }
                finally
                {
                    connection.Close();
                }

                if ( error )
                {
                    return false;
                }

                AddToLog($"Database with name: {DBName} erased");
                return true;
            }

            AddToLog($"Connection on: {ConnectionString} returned null");
            return false;
        }

        /// <summary>
        /// This will delete the entire database, which includes the Master Database File (MDF) and the reference in the LocalDB.
        /// </summary>
        /// <param name="deleteLog">Whether or not to delete the log file associated with the database</param>
        /// <returns></returns>
        public bool DeleteDatabase (bool deleteLog = false)
        {
            try
            {
                string drop = $"drop database [{DBName}]";
                //DataContext tempDB = new DataContext(TEMPDBCONSTRING);
                SqlConnection tempDB = new SqlConnection(TEMPDBCONSTRING);
                //if ( connection.State != ConnectionState.Closed )
                //{
                //    connection.Close();
                //    connection.Dispose();
                //    SqlConnection.ClearPool(connection);
                //}

                //connection = null;
                //tempDB.ExecuteCommand($"ALTER DATABASE {DBName} SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
                //tempDB.ExecuteCommand(drop);
                SqlCommand dropCommand = new SqlCommand(drop, tempDB);
                SqlCommand alterCommand = new SqlCommand($"ALTER DATABASE {DBName} SET SINGLE_USER WITH ROLLBACK IMMEDIATE", tempDB);

                tempDB.Open();
                alterCommand.ExecuteNonQuery();
                dropCommand.ExecuteNonQuery();
                tempDB.Close();

                AddToLog($"{DBName} was dropped!");

                if ( deleteLog && File.Exists($"{PathToDatabase}\\{DBName}_Log.txt") )
                {
                    File.Delete($"{PathToDatabase}\\{DBName}_Log.txt");
                }

                return true;

            }
            catch ( Exception _e )
            {
                AddToLog(_e.ToString());
                return false;
            }
        }

        /// <summary>
        /// This creates a new database file (.mdf) for which this <see cref="DatabaseTool"/> instance can execute the four <see cref="Query"/> files.
        /// <br/>
        /// <strong>NOTE:</strong> <i>This will generate a clean database with nothing in it and place it in the specified <see cref="PathToDatabase"/></i> 
        /// </summary>
        /// <returns></returns>
        private SqlConnection CreateMDF ()
        {
            /*Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Oiski\source\repos\DBDynamicTest\DBDynamicTest\TestDB.mdf;Integrated Security=True*/
            string creationQuery = $"CREATE DATABASE {DBName} ON PRIMARY (NAME = {DBName}_Data, FILENAME = '{PathToDatabase}\\{DBName}.mdf') LOG ON (NAME = {DBName}_Log, FILENAME = '{PathToDatabase}\\{DBName}.ldf')";

            SqlConnection myConn = new SqlConnection(TEMPDBCONSTRING);

            SqlCommand myCommand = new SqlCommand(creationQuery, myConn);

            myConn.Open();
            myCommand.ExecuteNonQuery();
            myConn.Close();

            return new SqlConnection(ConnectionString);
        }

        private string GenerateConnectionString ()
        {
            return $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={PathToDatabase}\\{DBName}.mdf;Integrated Security=True";
        }

        /// <summary>
        /// Loads the text files in the \Quries folder
        /// </summary>
        /// <returns></returns>
        private bool ReadData ()
        {
            string filePath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\Queris";

            if ( queries == null )
            {
                queries = new string[4];
            }
            StreamReader[] queryFiles = new StreamReader[4];

            if ( File.Exists($"{filePath}\\AssembleQuery.txt")
                &&
                File.Exists($"{filePath}\\ProcedureQuery.txt")
                &&
                File.Exists($"{filePath}\\PopulateQuery.txt")
                &&
                File.Exists($"{filePath}\\DeletionQuery.txt") )
            {
                queryFiles[( int ) Query.Assemble] = File.OpenText($"{filePath}\\AssembleQuery.txt");
                queryFiles[( int ) Query.Procedure] = File.OpenText($"{filePath}\\ProcedureQuery.txt");
                queryFiles[( int ) Query.Populate] = File.OpenText($"{filePath}\\PopulateQuery.txt");
                queryFiles[( int ) Query.Delete] = File.OpenText($"{filePath}\\DeletionQuery.txt");
            }
            else
            {
                AddToLog($"One of more query files do not exist at: {filePath}");
                return false;
            }

            for ( int i = 0; i < queryFiles.Length; i++ )
            {
                using ( StreamReader reader = queryFiles[i] )
                {
                    string fileContent = reader.ReadToEnd();
                    if ( !string.IsNullOrWhiteSpace(fileContent) )
                    {
                        queries[i] = fileContent;
                    }
                    else
                    {
                        AddToLog($"Query file: {( Query ) i} was empty");
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// This will initialize a new instance of the <see cref="DatabaseTool"/> where the name of the database is set.
        /// <br/>
        /// The path where the database will be stored will be set according to <see cref="Path.GetDirectoryName(string)"/> based on the location of <see cref="Assembly.GetExecutingAssembly"/>
        /// </summary>
        /// <param name="_dbName">The name of the database</param>
        public DatabaseTool (string _dbName)
        {
            DBName = _dbName;
            PathToDatabase = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if ( !ReadData() )
            {
                AddToLog("An error accured while reading the query data. Make sure all files are present in the specified location");
            }
        }

        /// <summary>
        /// This will initialize a new instance of the <see cref="DatabaseTool"/> where the name and path for the database is set
        /// </summary>
        /// <param name="_dbName">The name of the database</param>
        /// <param name="_path">The path to the database MDF file without the file itself</param>
        public DatabaseTool (string _dbName, string _path)
        {

            DBName = _dbName;
            PathToDatabase = _path;

            if ( !ReadData() )
            {
                AddToLog("An error accured while reading the query data. Make sure all files are present in the specified location");
            }
        }

        /// <summary>
        /// Represents the 4 different .txt file entries found in the \Quries folder
        /// </summary>
        private enum Query
        {
            Assemble,
            Procedure,
            Populate,
            Delete
        }
    }
}