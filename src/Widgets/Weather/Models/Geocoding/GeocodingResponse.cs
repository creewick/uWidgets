<<<<<<< HEAD
using System.Text.Json.Serialization;

namespace Weather.Models.Geocoding;

public class GeocodingResponse
{
    [JsonPropertyName("results")]
    public List<City> Cities { get; set; }
    
    [JsonPropertyName("error")]
    public string Error { get; set; }
    
    [JsonPropertyName("reason")]
    public string Reason { get; set; }
=======
using System.Text.Json.Serialization;

namespace Weather.Models.Geocoding;

public class GeocodingResponse
{
    [JsonPropertyName("results")]
    public List<City> Cities { get; set; }
    
    [JsonPropertyName("error")]
    public string Error { get; set; }
    
    [JsonPropertyName("reason")]
    public string Reason { get; set; }
>>>>>>> parent of 15524c5 (Delete src directory)
}