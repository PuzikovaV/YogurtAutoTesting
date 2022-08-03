using System.Data.SqlClient;

namespace YogurtAutoTesting.Support
{
    public class BaseClearCommand
    {
        public void ClearBase()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "delete from dbo.[Bundle]";
                command.ExecuteNonQuery();

                command.CommandText = "delete from dbo.[BundleOrder]";
                command.ExecuteNonQuery();

                command.CommandText = "delete from dbo.[Cleaner]";
                command.ExecuteNonQuery();

                command.CommandText = "delete from dbo.[CleanerDistrict]";
                command.ExecuteNonQuery();

                command.CommandText = "delete from dbo.[CleanerOrder]";
                command.ExecuteNonQuery();

                command.CommandText = "delete from dbo.[CleaningObject]";
                command.ExecuteNonQuery();

                command.CommandText = "delete from dbo.[Client]";
                command.ExecuteNonQuery();

                command.CommandText = "delete from dbo.[Comment]";
                command.ExecuteNonQuery();

                command.CommandText = "delete from dbo.[Order]";
                command.ExecuteNonQuery();

                command.CommandText = "delete from dbo.[OrderService]";
                command.ExecuteNonQuery();

                command.CommandText = "delete from dbo.[Service]";
                command.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}
