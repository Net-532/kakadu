namespace Kakadu.OrderQueue;
public class Order
{
    public int OrderNumber { get; set; }
    public string Status { get; set; }

    public override string ToString()
    {
        return $"OrderNumber: {OrderNumber}, Status: {Status}";
    }
}
