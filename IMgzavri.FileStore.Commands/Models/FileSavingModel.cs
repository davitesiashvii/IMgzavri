using System;

namespace IMgzavri.FileStore.Commands.Models
{
    public class FileSavingModel
    {
        public string Name { get; set; }

        public string Extension { get; set; }

        public string ContentType { get; set; }

        public long Size { get; set; }

        public byte[] File { get; set; }

        public Guid CreatorId { get; set; }

        public Guid CorrelationId { get; set; }
    }
}
