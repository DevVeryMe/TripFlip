using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;

namespace SqlPersonRepository
{
    /// <summary>
    /// Implements IRepository<Person>, and performs basic CRUD operations to a 'Person' table with the use of ADO.NET and SQL Server
    /// </summary>
    public class SqlPersonRepo : IRepository<Person>
    {
        public string ConnectionString { get; set; }
        public SqlConnection conn { get; set; }

        /// <summary>
        /// Creates a new instance of a repository
        /// </summary>
        /// <param name="conn_str">Database connection string</param>
        public SqlPersonRepo(string conn_str)
        {
            ConnectionString = conn_str;
        }

        public QueryResult<Person> Create(Person p)
        {
            var result = new QueryResult<Person>();

            conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open();

                // load data from the table into 'DataTable' object
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Person", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // add new row into 'DataTable' object
                DataRow dr = dt.NewRow();
                InitDataRow(dr, p);

                dt.Rows.Add(dr);

                // execute auto-generated INSERT query to add new row into 'Person' table
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                int added_rows = da.Update(dt);

                // init result object
                if (added_rows == 1)
                {
                    result.ResultType = ResultType.Ok;

                    // get new entry id
                    string sql_command = $"SELECT MAX(Id) FROM Person";
                    SqlCommand command = new SqlCommand(sql_command, conn);
                    int new_entry_id = (int)command.ExecuteScalar();

                    p.Id = new_entry_id;
                    result.Value = p;
                }
                else
                {
                    result.ResultType = ResultType.Error;
                    result.ErrorMsg = "Error: invalid INSERT query";
                }

                //dt.Clear();
            }
            catch(Exception ex)
            {
                result.ResultType = ResultType.Exception;
                result.ErrorMsg = ex.Message;
                // todo: Error logging
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public QueryResult<Person> Delete(int id)
        {
            var result = new QueryResult<Person>();

            conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open();

                string sql_command = $"DELETE FROM Person WHERE Id = {id}";
                SqlCommand command = new SqlCommand(sql_command, conn);
                int rows_deleted = command.ExecuteNonQuery();

                if (rows_deleted == 1)
                    result.ResultType = ResultType.Ok;
                else
                {
                    result.ResultType = ResultType.Error;
                    result.ErrorMsg = "Error: DELETE query didn't do anything";
                }
            }
            catch (Exception ex)
            {
                result.ResultType = ResultType.Exception;
                result.ErrorMsg = ex.Message;
                // todo: Error logging
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public QueryResult<Person> Read(int id)
        {
            var result = new QueryResult<Person>();

            conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open();

                string sql_command = $"SELECT * FROM Person WHERE Id = {id}";
                SqlCommand command = new SqlCommand(sql_command, conn);
                SqlDataReader dataReader = command.ExecuteReader();

                if (dataReader.HasRows)
                {
                    dataReader.Read();
                    result.Value = InitPersonFromReader(dataReader);
                    result.ResultType = ResultType.Ok;
                }
                else
                {
                    result.ResultType = ResultType.Error;
                    result.ErrorMsg = $"Error: there are no entries with id = {id}";
                }
            }
            catch (Exception ex)
            {
                result.ResultType = ResultType.Exception;
                result.ErrorMsg = ex.Message;
                // todo: Error logging
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public QueryResult<Person> Read()
        {
            var result = new QueryResult<Person>();

            conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open();

                string sql_command = $"SELECT * FROM Person";
                SqlCommand command = new SqlCommand(sql_command, conn);
                SqlDataReader dataReader = command.ExecuteReader();

                if (dataReader.HasRows)
                {
                    var list = new List<Person>();
                    while (dataReader.Read())
                    {
                        list.Add( InitPersonFromReader(dataReader) );
                    }
                    result.ValuesCollection = list;
                    result.ResultType = ResultType.Ok;
                }
                else
                {
                    result.ResultType = ResultType.Error;
                    result.ErrorMsg = "Error: no etries in the table";
                }
            }
            catch (Exception ex)
            {
                result.ResultType = ResultType.Exception;
                result.ErrorMsg = ex.Message;
                // todo: Error logging
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public QueryResult<Person> Update(int id, Person p)
        {
            var result = new QueryResult<Person>();
            p.Id = id;

            conn = new SqlConnection(ConnectionString);

            try
            {
                conn.Open();

                // load data from the table into 'DataTable' object
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Person", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    conn.Close();
                    return result;
                }
                dt.PrimaryKey = new DataColumn[] { dt.Columns[0] };

                // modify the existing row with the new data
                DataRow dr = dt.Rows.Find(id);
                InitDataRow(dr, p);

                // execute auto-generated UPDATE query to modify a row in a 'Person' table
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                int affected_rows = da.Update(dt);

                if (affected_rows == 1)
                {
                    result.ResultType = ResultType.Ok;
                    result.Value = p;
                }
                else
                {
                    result.ResultType = ResultType.Error;
                    result.ErrorMsg = "Error: invalid UPDATE query";
                }
                //dt.Clear();
            }
            catch (Exception ex)
            {
                result.ResultType = ResultType.Exception;
                result.ErrorMsg = ex.Message;
                // todo: Error logging
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        /// <summary>
        /// Initializes a given DataRow with a given Person instance
        /// </summary>
        /// <param name="dr">object that needs initializing</param>
        /// <param name="p">data to initialize a new row with</param>
        DataRow InitDataRow(DataRow dr, Person p)
        {
            dr[1] = p.Name;
            dr[2] = p.Surname;
            dr[3] = p.Sex;
            if (p.BirthDate == null) dr[4] = DBNull.Value;
            else dr[4] = p.BirthDate;
            if (p.IsAdult == null) dr[5] = DBNull.Value;
            else dr[5] = p.IsAdult;
            if (p.Balance == null) dr[6] = DBNull.Value;
            else dr[6] = p.Balance;

            return dr;
        }

        /// <summary>
        /// Initializes a new instance of a 'Person' class with a data stored in the given SqlDataReader object
        /// </summary>
        Person InitPersonFromReader(SqlDataReader reader)
        {
            /* 'Person' DB table:
             * [Column name] - [Column number] - [IsNullable]
             * -------------------------------
             * Id - 0 - NOT NULL
             * Name - 1 - NOT NULL
             * Surname - 2 - NOT NULL
             * Sex - 3 - NOT NULL
             * BirthDate - 4 - NULL
             * Age - 5 - NULL
             * IsAdult - 6 - NULL
             * Balance - 7 - NULL
            */

            Person p = new Person
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Surname = reader.GetString(2),
                Sex = reader.GetString(3).Substring(0, 1)
            };

            if (!reader.IsDBNull(4))
                p.BirthDate = reader.GetDateTime(4);
            if (!reader.IsDBNull(5))
                p.Age = reader.GetByte(5);
            if (!reader.IsDBNull(6))
                p.IsAdult = reader.GetBoolean(6);
            if (!reader.IsDBNull(7))
                p.Balance = reader.GetDecimal(7);

            return p;
        }
    }
}
