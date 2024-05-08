using Kakadu.Backend.Entities;
using System.Globalization;
using System.Text;

namespace Kakadu.WebServer
{
    internal class ProductToJsonConverter
    {
        public string Convert(Product product)
        {
            StringBuilder jsonsBuilder = new StringBuilder();
            jsonsBuilder.Append("{");
            jsonsBuilder.Append("\"id\":").Append(product.Id).Append(",");
            jsonsBuilder.Append("\"title\":").Append("\"").Append(product.Title).Append("\"").Append(",");
            jsonsBuilder.Append("\"price\":").Append("\"").Append(product.Price.ToString(CultureInfo.InvariantCulture)).Append("\"").Append(",");
            jsonsBuilder.Append("\"photoUrl\":").Append("\"").Append(product.PhotoUrl).Append("\"").Append(",");
            jsonsBuilder.Append("\"description\":").Append("\"").Append(product.Description);
            jsonsBuilder.Append("}");
            return jsonsBuilder.ToString();
        }

        public string Convert(List<Product> products)
        {
            StringBuilder jsonsBuilder = new StringBuilder();
            jsonsBuilder.Append("[");

            foreach (Product product in products)
            {
                jsonsBuilder.Append(Convert(product));
                jsonsBuilder.Append(",");
            }

            if (products.Count > 0)
            {
                jsonsBuilder.Length--;
            }

            jsonsBuilder.Append("]");
            return jsonsBuilder.ToString();
        }
    }
}
