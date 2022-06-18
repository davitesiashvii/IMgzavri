using System;

namespace IMgzavri.FileStore.Commands.Models
{
    public class FileSavingResult
    {
        public Guid FileId { get; set; }
        public Guid CorrelationId { get; set; }

        public FileSavingResult(Guid fileId, Guid correlationId)
        {
            FileId = fileId;
            CorrelationId = correlationId;
        }
    }
}
