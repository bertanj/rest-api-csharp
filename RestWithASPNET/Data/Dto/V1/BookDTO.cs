using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace RestWithASPNET.Data.Dto.V1
{
    public class BookDTO
    {
        [JsonPropertyOrder(1)]
        public long Id { get; set; }

        [JsonPropertyOrder(2)]
        [JsonPropertyName("author")]
        [XmlElement("author")]
        public string Author { get; set; }

        [JsonPropertyOrder(3)]
        [JsonPropertyName("title")]
        [XmlElement("title")]
        public string Title { get; set; }

        [JsonPropertyOrder(4)]
        [JsonPropertyName("price")]
        [XmlElement("price")]
        public decimal Price { get; set; }

        [JsonPropertyOrder(5)]
        [JsonPropertyName("launch_date")]
        [XmlElement("launch_date")]
        public DateTime LaunchDate { get; set; }
        
    }
}
