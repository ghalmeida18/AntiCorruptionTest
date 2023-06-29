using Newtonsoft.Json;

namespace AntiCorruption.Model
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class RepositoryHookModel
    {
        public long Id { get; set;  }   
        public string? Name { get; set; }
        public bool Active { get; set; }
        public List<string>? Events { get; set; }
        public Dictionary<string, string>? Config { get; set; }
    }
}
