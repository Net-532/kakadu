namespace Kakadu.WebServer
{
    internal class ProductToJsonConverter
    {
       public string Convert(Product product)
        {
            StringBuilder jsonsBuilder = new StringBuilder();
            jsonsBuilder.Append("{");
            jsonsBuilder.Append("\"id\":").Append(product.Id).Append(",");
            jsonsBuilder.Append("\"title\":").Append(product.Title).Append(",");
            jsonsBuilder.Append("\"price\":").Append(product.Price.ToString(CultureInfo.InvariantCulture)).Append(",");
            jsonsBuilder.Append("\"photoUrl\":").Append(product.PhotoUrl).Append(",");
            jsonsBuilder.Append("\"description\":").Append(product.Description);
            jsonsBuilder.Append("}");
            return jsonsBuilder.ToString();
        }
    }
}
