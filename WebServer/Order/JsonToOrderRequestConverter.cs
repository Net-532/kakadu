using System.Text.Json;


namespace Kakadu.WebServer.Order
{
    public class JsonToOrderRequestConverter
    {
        public OrderRequest Convert(string json)
        {
            try
            {
                return JsonSerializer.Deserialize<OrderRequest>(json);
            }
            catch (JsonException ex)
            { 
                Console.WriteLine($"Помилка десеріалізації JSON: {ex.Message}");
                return null;
            }
        }
    }
}
