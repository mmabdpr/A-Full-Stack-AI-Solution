using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using MbtiPredictor.Enums;
using MbtiPredictor.Models;


namespace MbtiPredictor.Data
{
    public class PersonalityDatabase
    {
        private static readonly string DataPath = Environment.GetEnvironmentVariable("MBTI_DATA");
        private Dictionary<string, MBTIProfilesDataModel> MbtiProfiles { get; set; }
        public PersonalityDatabase()
        {
            LoadData();
        }

        private void LoadData()
        {
            MbtiProfiles = new Dictionary<string, MBTIProfilesDataModel>();
            foreach (var label in Enum.GetNames(typeof(MBTILabel)))
            {
                var data = File.ReadAllText(Path.Combine(DataPath, $"{label}.json"));
                var jsonData = JsonSerializer.Deserialize<MBTIProfilesDataModel>(data);
                MbtiProfiles.Add(label, jsonData);
            }
        }
        public IEnumerable<Character> GetRandomPeople(string mbtiLabel, int count = 5)
        {
            var profiles = MbtiProfiles[mbtiLabel];
            var rand = new Random();
            var res = Enumerable.Range(0, int.Parse(profiles.ProfileCount))
                .OrderBy(_ => rand.Next())
                .Select(i => profiles.Items[i])
                .Where(p => !string.IsNullOrWhiteSpace(p.ProfileImageUrl))
                .Take(count)
                .Select(it => new Character
                {
                    Name = it.MbtiProfile,
                    Description = it.WikiDescription,
                    Photo = it.ProfileImageUrl,
                    Category = it.Subcategory
                })
                .ToArray();
            return res;
        }
    }
}