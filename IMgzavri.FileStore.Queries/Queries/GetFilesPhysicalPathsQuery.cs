using System;
using System.Collections.Generic;

namespace IMgzavri.FileStore.Queries.Queries
{
    public class GetFilesPhysicalPathsQuery : Query
    {
        public List<Guid> FileIds { get; set; }

        public GetFilesPhysicalPathsQuery(List<Guid> fileIds)
        {
            FileIds = fileIds;
        }
    }
}
