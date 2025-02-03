using MySql.Data.MySqlClient;
using System.Data;

namespace Software_II__Advanced__CSharp__C969
{
    public static class CityDAO
    {
        public static DataTable GetCities()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = "SELECT cityId, city FROM city";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                conn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            return dt;
        }
    }
}
