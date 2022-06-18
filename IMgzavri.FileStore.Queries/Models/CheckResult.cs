
namespace IMgzavri.FileStore.Queries.Models
{
    public class CheckResult
    {
        public bool Exists { get; set; }

        public string Path { get; set; }

        public IMgzavri.FileStore.Domain.File File { get; set; }
    }
}
