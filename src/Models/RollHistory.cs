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

        public static void AddResult(RollSummary summary)
        {
            var currentResults = GetJsonStore();
            currentResults = null != currentResults ? currentResults : new List<RollSummary>();
            currentResults.Add(summary);
            SetJsonStore(currentResults);
        }

        public static List<RollSummary> GetResults()
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

        private static List<RollSummary> GetJsonStore()
        {
            var result = new List<RollSummary>();

            var serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;

            if (File.Exists(storageFile))
            {
                using (var sr = new StreamReader(storageFile))
                using (var reader = new JsonTextReader(sr))
                {
                    result = serializer.Deserialize(reader, typeof(List<RollSummary>)) as List<RollSummary>;
                } 
            }

            return result;
        }

        private static void SetJsonStore(List<RollSummary> summary)
        {
            var serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;
            
            using (var sw = new StreamWriter(storageFile))
            using (var writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, summary);
            }
        }
    }
}