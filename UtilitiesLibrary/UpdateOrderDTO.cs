using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Utilities_Library
{
    public class UpdateOrderDTO
    {
        [JsonPropertyName("ID")]
        public int ID { get; set; }

        [JsonPropertyName("ProductID")]
        public int ProductID { get; set; }

        [JsonPropertyName("Date")]
        public DateTimeOffset Date { get; set; }

        [JsonPropertyName("Quantity")]
        public int Quantity { get; set; }
    }
}
