using System;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace Project
{
    public static class SqlQueries
    {
        public static OleDbConnection con = new OleDbConnection(Program.conPath);

        /*public static OleDbDataReader Select(string query)
        {
            try
            {
                

                return dataReader;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally {con.Close();}*/
        }
    }
