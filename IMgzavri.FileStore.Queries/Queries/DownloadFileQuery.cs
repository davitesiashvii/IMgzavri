using System;

namespace IMgzavri.FileStore.Queries.Queries
{
    public class DownloadFileQuery : Query
    {
        public Guid FileId { get; set; }

        public DownloadFileQuery(Guid fileId)
        {
            FileId = fileId;
        }
    }
}
