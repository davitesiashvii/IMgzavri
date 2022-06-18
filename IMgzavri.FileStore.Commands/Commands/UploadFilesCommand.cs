using IMgzavri.FileStore.Commands.Models;
using System.Collections.Generic;

namespace IMgzavri.FileStore.Commands.Commands
{
    public class UploadFilesCommand : Command
    {
        public List<FileSavingModel> Files { get; set; }

        public UploadFilesCommand(List<FileSavingModel> files)
        {
            Files = files;
        }
    }
}
