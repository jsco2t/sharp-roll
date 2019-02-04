using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace SharpRoll.Model
{
    public static class RollHistory
    {
        private static readonly string storageFile = @"./rollhistory.local.json";

        static RollHistory()
        {
            ClearResults();
        }

        public static void AddResult(int diceSideCount, int rollCount, int count, int entropy, int modifier)
        {
            var result = new RollResult(diceSideCount, rollCount, count, entropy, modifier);
            AddResult(result);
        }

        public static void AddResult(RollResult result)
        {
            var currentResults = GetJsonStore();
            currentResults = null != currentResults ? currentResults : new List<RollResult>();
            currentResults.Add(result);
            SetJsonStore(currentResults);
        }

        public static List<RollResult> GetResults()
        {
            return GetJsonStore();
        }

        public static void ClearResults()
        {
            if (File.Exists(storageFile))
            {
                File.WriteAllText(storageFile, string.Empty);
            }
        }

        private static List<RollResult> GetJsonStore()
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

        private static void SetJsonStore(List<RollResult> results)
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