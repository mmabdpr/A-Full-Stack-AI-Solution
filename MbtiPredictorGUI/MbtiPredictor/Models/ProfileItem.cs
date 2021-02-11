using System.Text.Json.Serialization;

namespace MbtiPredictor.Models
{
    public class ProfileItem
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("wiki_description")]
        public string WikiDescription { get; set; }
        [JsonPropertyName("mbti_profile")]
        public string MbtiProfile { get; set; }
        [JsonPropertyName("subcategory")]
        public string Subcategory { get; set; }
        [JsonPropertyName("mbti_type")]
        public string MbtiType { get; set; }
        [JsonPropertyName("profile_image_url")]
        public string ProfileImageUrl { get; set; }
    }
}