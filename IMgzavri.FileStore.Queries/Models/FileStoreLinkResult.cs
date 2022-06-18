using System;

namespace IMgzavri.FileStore.Queries.Models
{
    public class FileStoreLinkResult
    {
        public Guid CorrelationId { get; set; }

        public string Link { get; set; }

        public FileStoreLinkResult()
        {
            
        }

        public FileStoreLinkResult(Guid correlationId, string link)
        {
            CorrelationId = correlationId;
            Link = link;
        }
    }
}
