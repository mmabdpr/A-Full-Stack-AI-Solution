using System.Text.Json.Serialization;

namespace MbtiPredictor.Models
{
    public class MBTIProfilesDataModel
    {
        [JsonPropertyName("profile_count")]
        public string ProfileCount { get; set; }
        [JsonPropertyName("items")]
        public ProfileItem[] Items { get; set; }
    }
}