using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Tracing.HandleRawSQL
{
    public class Handle
    {
        public Boolean CustomeSQL(String Sql)
        {
            using (SqlConnection connection = new SqlConnection(
                       Sql))
            {
                SqlCommand command = new SqlCommand(Sql, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }
                command.Connection.Close();
            }
        }
    }
}
