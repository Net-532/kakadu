public class Order
{
    public int OrderNumber { get; set; }
    public string Status { get; set; }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        var order = obj as Order;
        return OrderNumber == order.OrderNumber;
    }

    public override int GetHashCode()
    {
        return OrderNumber.GetHashCode();
    }
}
