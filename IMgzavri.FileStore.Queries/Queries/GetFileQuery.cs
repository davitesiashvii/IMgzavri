using System;

namespace IMgzavri.FileStore.Queries.Queries
{
    public class GetFileQuery : Query
    {
        public Guid FileId { get; set; }

        public GetFileQuery(Guid fileId)
        {
            FileId = fileId;
        }
    }
}
