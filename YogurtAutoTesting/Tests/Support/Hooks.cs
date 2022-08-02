using System.Data.SqlClient;

namespace YogurtAutoTesting.Tests.Support
{
    public class Hooks
    {
        public void AfterScenario()
        {
            string connectionString = @"Data Source = 80.78.240.16; Catalog = YogurtCleaning.DB; Persist Security Info = True; User ID = student; Password = qwe!23;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand();
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

                command.CommandText = "delete from dbo.[District]";
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
