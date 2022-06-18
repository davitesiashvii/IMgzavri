using System;

namespace IMgzavri.FileStore.Commands.Commands
{
    public class DeleteFileCommand : Command
    {
        public Guid FileId { get; set; }

        public DeleteFileCommand(Guid fileId)
        {
            FileId = fileId;
        }
    }
}
