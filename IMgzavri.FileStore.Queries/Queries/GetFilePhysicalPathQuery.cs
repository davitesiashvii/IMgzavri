using System;

namespace IMgzavri.FileStore.Queries.Queries
{
    public class GetFilePhysicalPathQuery : Query
    {
        public Guid FileId { get; set; }

        public GetFilePhysicalPathQuery(Guid fileId)
        {
            FileId = fileId;
        }
    }
}
