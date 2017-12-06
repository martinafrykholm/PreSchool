using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SQLLibrary_new
{
    public class SqlClass
    {
        static string connString = @"Server=tcp:preschoolserver.database.windows.net,1433;Initial Catalog=PreSchoolDB;Persist Security Info=False;User ID=preschoolAdmin;Password=Grupp1C#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
        static SqlConnection sqlConnection = new SqlConnection(connString);


        public static int AddParent(string firstName, string lastName, string aspId, int childId)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "AddParent";
            sqlCommand.Connection = sqlConnection;

            sqlCommand.Parameters.Add(CreateNVarCharParameter("@firstName", firstName, 50));
            sqlCommand.Parameters.Add(CreateNVarCharParameter("@lastName", lastName, 50));
            sqlCommand.Parameters.Add(CreateNVarCharParameter("@AspId", aspId, 450));
            sqlCommand.Parameters.Add(CreateIntParameter("@ChildID", childId));
            int rowsAffected;

            try
            {
                sqlConnection.Open();
                rowsAffected=sqlCommand.ExecuteNonQuery();

            }
            catch
            {
                rowsAffected = 0;
            }
            finally
            {
                sqlConnection.Close();
            }

            return rowsAffected;
        }


        public static int AddTeacher(string firstName, string lastName, string aspId, int unitId)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "AddTeacher";
            sqlCommand.Connection = sqlConnection;

            sqlCommand.Parameters.Add(CreateNVarCharParameter("@firstName", firstName, 50));
            sqlCommand.Parameters.Add(CreateNVarCharParameter("@lastName", lastName, 50));
            sqlCommand.Parameters.Add(CreateNVarCharParameter("@AspId", aspId, 450));
            sqlCommand.Parameters.Add(CreateIntParameter("@UnitsID", unitId));
            int rowsAffected;

            try
            {
                sqlConnection.Open();
                rowsAffected = sqlCommand.ExecuteNonQuery();

            }
            catch
            {
                rowsAffected = 0;
            }
            finally
            {
                sqlConnection.Close();
            }

            return rowsAffected;
        }


        public static int AddChild(string firstName, string lastName, int unitId)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "AddChild";
            sqlCommand.Connection = sqlConnection;

            sqlCommand.Parameters.Add(CreateNVarCharParameter("@firstName", firstName, 50));
            sqlCommand.Parameters.Add(CreateNVarCharParameter("@lastName", lastName, 50));
            sqlCommand.Parameters.Add(CreateIntParameter("@unitsId", unitId));
            int rowsAffected;

            try
            {
                sqlConnection.Open();
                rowsAffected = sqlCommand.ExecuteNonQuery();//Exekverar sqlcommandet

            }
            catch
            {
                rowsAffected = 0;
            }
            finally
            {
                sqlConnection.Close();
            }

            return rowsAffected;
        }

        public static int AddPreSchool(string preschoolName)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "AddPreSchool";
            sqlCommand.Connection = sqlConnection;

            sqlCommand.Parameters.Add(CreateNVarCharParameter("@PreSchoolName", preschoolName, 100));
            
            int rowsAffected;

            try
            {
                sqlConnection.Open();
                rowsAffected = sqlCommand.ExecuteNonQuery();//Exekverar sqlcommandet

            }
            catch
            {
                rowsAffected = 0;
            }
            finally
            {
                sqlConnection.Close();
            }

            return rowsAffected;
        }

        public static int AddUnit(string unitName, int preschoolID)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "AddUnit";
            sqlCommand.Connection = sqlConnection;

            sqlCommand.Parameters.Add(CreateNVarCharParameter("@UnitName", unitName, 100));
            sqlCommand.Parameters.Add(CreateIntParameter("@PreSchoolID", preschoolID));


            int rowsAffected;

            try
            {
                sqlConnection.Open();
                rowsAffected = sqlCommand.ExecuteNonQuery();//Exekverar sqlcommandet

            }
            catch
            {
                rowsAffected = 0;
            }
            finally
            {
                sqlConnection.Close();
            }

            return rowsAffected;
        }

        public static void EditSchedule(TimeSpan dropOff, TimeSpan pickUp, int scheduleItemId)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "EditSchedule";
            sqlCommand.Connection = sqlConnection;

            sqlCommand.Parameters.Add(CreateTimeParameter("@dropOff", dropOff));
            sqlCommand.Parameters.Add(CreateTimeParameter("@pickUp", pickUp));
            sqlCommand.Parameters.Add(CreateIntParameter("@ID", scheduleItemId));


            try
            {
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();//Exekverar sqlcommandet

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                sqlConnection.Close();
            }

            
        }


        private static SqlParameter CreateNVarCharParameter(string parameterName, string value, int size)
        {
            SqlParameter param = new SqlParameter();
            param.Direction = ParameterDirection.Input;
            param.ParameterName = parameterName;
            param.SqlDbType = SqlDbType.NVarChar;
            param.Size = size;
            param.Value = value;

            return param;
        }

        private static SqlParameter CreateIntParameter(string parameterName, int value)
        {
            SqlParameter param = new SqlParameter();
            param.Direction = ParameterDirection.Input;
            param.ParameterName = parameterName;
            param.SqlDbType = SqlDbType.Int;
            param.Value = value;

            return param;
        }

        private static SqlParameter CreateTimeParameter(string parameterName, TimeSpan value)
        {
            SqlParameter param = new SqlParameter();
            param.Direction = ParameterDirection.Input;
            param.ParameterName = parameterName;
            param.SqlDbType = SqlDbType.Time;
            param.Value = value;

            return param;
        }
    }
}
