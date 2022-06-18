

using IMgzavri.FileStore.Commands.Models;

namespace IMgzavri.FileStore.Commands.Commands
{
    public class UploadFileCommand : Command
    {
        public FileSavingModel File { get; set; }

        public UploadFileCommand(FileSavingModel file)
        {
            File = file;
        }
    }
}
