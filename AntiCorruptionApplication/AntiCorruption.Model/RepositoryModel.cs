
namespace AntiCorruption.Model
{
    public class RepositoryModel
    {
        public long Id { get; set;  }
        public bool AutoInit { get; set; }
        public bool Private { get; set; }
        public string? Description { get; set; }
        public string? LicenseTemplate { get; set; }
        public string? Name { get; set; }
    }
}
