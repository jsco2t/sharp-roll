using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace SharpRoll.Model
{
    public class RollHistory
    {
        private readonly string storageFile = @"./rollhistory.local.json";

        public void AddResult(int diceSideCount, int rollCount, int count, int modifier)
        {
            var result = new RollResult(diceSideCount, rollCount, count, modifier);
            AddResult(result);
        }
        
        public void AddResult(RollResult result)
        {
            var currentResults = GetJsonStore();
            currentResults.Add(result);
            SetJsonStore(currentResults);
        }

        public List<RollResult> GetResults()
        {
            return GetJsonStore();
        }

        public void ClearResults()
        {
            var results = new List<RollResult>();
            SetJsonStore(results);
        }

        private List<RollResult> GetJsonStore()
        {
            var result = new List<RollResult>();

            var serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;

            if (File.Exists(storageFile))
            {
                using (var sr = new StreamReader(storageFile))
                using (var reader = new JsonTextReader(sr))
                {
                    result = serializer.Deserialize(reader, typeof(List<RollResult>)) as List<RollResult>;
                } 
            }

            return result;
        }

        private void SetJsonStore(List<RollResult> results)
        {
            var serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;
            
            using (var sw = new StreamWriter(storageFile))
            using (var writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, results);
            }
        }
    }
}