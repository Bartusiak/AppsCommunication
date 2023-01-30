using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Http.Json;
using AppA.Models;
using Newtonsoft.Json;
using static AppA.Config.ConstParameters;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace AppA.Helpers;

public class SendDataHelper
{
    public static async void SendKeyAndIVToAppB(byte[] key, byte[] IV)
    {
        try
        {
            var client = new HttpClient();
            var data = new {Key = key, SymmetricAlgorithm = IV};
            var response = client.PostAsJsonAsync(HTTP_ENDPOINT, data).Result;
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Callback>(responseContent);

            Console.WriteLine(response.IsSuccessStatusCode
                ? result?.Value
                : "Failed to send data to the Application B.");
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error while send data to decrypt a message: {ex.Message}");
        }
    }

    public static void SendEncryptedMessageToDb(IEnumerable message)
    {
        try
        {
            var connectionString = ConfigurationManager.ConnectionStrings["MessagesDb"].ConnectionString;
            var con = new SqlConnection(connectionString);
            con.Open();

            using (var cmd = new SqlCommand($"INSERT INTO Messages (EncodedMsg) VALUES (@EncodedMsg)", con))
            {
                cmd.Parameters.AddWithValue("@EncodedMsg", message);
                cmd.ExecuteNonQuery();
            }

            con.Close();
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"Received error while sending message to the database: {ex.Message}");
        }
    }
}